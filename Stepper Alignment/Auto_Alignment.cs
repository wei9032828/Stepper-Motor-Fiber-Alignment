using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using DiCon.Instrument.HP;

namespace Motor_Encoder
{
    class Auto_Alignment
    {

        EZ_Stepper Controller = new EZ_Stepper();               //Stepper motor controller class  
        public Stopwatch stopWatch = new Stopwatch();           
        public StreamWriter logFile;                            
        public List<int> xSteps_Data = new List<int>();         //Store x encoder count
        public List<int> ySteps_Data = new List<int>();         // y..
        public List<int> zSteps_Data = new List<int>();         // z..
        public List<int> xSteps_OldData = new List<int>();      //Store previous x count for plotting
        public List<int> ySteps_OldData = new List<int>();      // y..
        public List<int> zSteps_OldData = new List<int>();      // z..
        public int AlignmentEndIndex { get; set; } = 0;         //Index for plotting
        public int CuringStartIndex { get; set; } = 0;          //Index for curing plotting
        public List<double> Loss_Data = new List<double>();     //Store Loss data
        private int velocity = 12800;                           //Default velocity
        public double Alignment_Minimum { get; set; } = 0;      //Record minimum loss after alignment 
        public string ErrorText { get; set; } = "None";         //Display error text for UI
        public double ref_loss { get; set; }                    //Laser reference loss
        public bool UI_ReadLoss { get; set; } = true;           //Flag to switch between UI PM trigger or this class trigger
        public double UI_loss {get; set;} = 0;                  //Used for curing loss display
        public int productNumber { get; set; } = 1;             // 1 = 1XN SM, 2 = 1XN MM, 3 = VOA
        private int Tolerance = 1;                              //Tolerance changes with loss but default is 1
        bool Fixed_Tolerance = false;                           //Fix tolerance to 1 in curing 
        public int HardStop { get; set; } = Int32.MaxValue;     //Prevent bumping into lens, only when out&align
        private double curing_tolerance = 0.03;                 //When to stop moving during curing
        private double align_tolerance = 0.02;                  //Use to find low loss at the end of alignment
        private int LateCycleTime;                              //Used to disable Z stepping in/out during curing
        private int ZOutCount;                                  //Limit the Z move out distance
        private int ZInCount;                                   //Limit when Z start to step in

        //delegate void SetTextCallback(string text);

        public SerialPort EZPort = new SerialPort();

        public Auto_Alignment()
        {
            CreateLogFile();
            velocity = Controller.Velocity;
            logFile.WriteLine("X,Y,Z,IL(dBm),time(ms)");
            
        }

        private void CreateLogFile()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Directory.CreateDirectory(path + "\\Stepper Station Data");
            logFile = new StreamWriter($"{path}\\Stepper Station Data\\logfile" + DateTime.Now.ToString("MMddHHmm") + ".txt");
        }
        public void Open_Port(string portName)
        {
            EZPort.PortName = portName;
            EZPort.BaudRate = 115200;
            try
            {
                EZPort.Open();
            }
            catch
            {
                ErrorText = "Serial port" + EZPort.PortName + "Cannot be opened";
            }
        }

        public void Board_Initialization()
        {
            if (EZPort.IsOpen)
            {
                string command = Controller.Initialize();
                EZPort.WriteLine(command);
                ErrorText = GetResponse();
            }
        }

        private void Reset_params()
        {
            xSteps_OldData = xSteps_Data;
            ySteps_OldData = ySteps_Data;
            zSteps_OldData = zSteps_Data;
            xSteps_Data.Clear();
            ySteps_Data.Clear();
            zSteps_Data.Clear();
            Loss_Data.Clear();
            stopWatch.Stop();
            Alignment_Minimum = 0;
            UI_ReadLoss = true;
            curing_tolerance = 0.03;
            HardStop = Int32.MaxValue;
            Fixed_Tolerance = false;
        }

        #region Motor Movement Related
        public List<int> Closed_Loop_RunSteps(int motor, bool direction, int steps)
        {

            if (steps <= 0)
            {
                ErrorText = "Step is <= 0, invalid step size in (Method:Closed_Loop_RunSteps)";
            }

            List<int> current_count, new_count;
            current_count = GetCounts();
            //Initial Run to position
            new_count = RunSteps(motor, direction, steps);
            //Correct the position with tolerance
            new_count = Position_Correction(motor, direction, current_count, new_count, steps);
            return new_count;
        }

        public List<int> Closed_Loop_RunSteps(int motor, bool direction, int steps, List<int> previous_count)
        {

            if (steps <= 0)
            {
                ErrorText = "Step is <= 0, invalid step size in (Method:Closed_Loop_RunSteps)";
            }

            List<int> new_count;
            //Initial Run to position
            new_count = RunSteps(motor, direction, steps);
            //Correct the position with tolerance but take into account of the previous position
            //the encoder count may change even when motor is not moving
            new_count = Position_Correction(motor, direction, previous_count, new_count, steps);

            return new_count;
        }


        public List<int> Position_Correction(int motor, bool direction, List<int> current_count, List<int> new_count, int steps)
        {
            int step_loss = Math.Abs(new_count[motor - 1] - current_count[motor - 1]) - steps;
            bool overStep = step_loss > 0;
            bool incorrect_Position = Math.Abs(step_loss) > 0;
            bool special_case_dir = false;
            int old_count = new_count[motor - 1];


            while (incorrect_Position)
            {
                bool compensate_Direction;
                if (special_case_dir)
                {
                    compensate_Direction = direction;
                    special_case_dir = false;
                }
                else
                {
                    // True table below for compensate direction
                    //                  Overstep(true)  |   Overstep(false)
                    //------------------------------------------------------
                    // Direction(true)|     False       |      True
                    //------------------------------------------------------
                    // Direction(false)|    True        |      False
                    compensate_Direction = overStep == direction ? false : true;
                }

                new_count = RunSteps(motor, compensate_Direction, Math.Abs(step_loss));

                //this if is a special case, where we command to go (+) but it goes (-)
                if ((new_count[motor - 1] - old_count) < 0 && compensate_Direction == true)
                {
                    step_loss = new_count[motor - 1] - old_count - Math.Abs(step_loss);
                    special_case_dir = true;
                }
                //this if is a special case, where we command to go (-) but it goes (+)
                else if ((new_count[motor - 1] - old_count) > 0 && compensate_Direction == false)
                {
                    step_loss = new_count[motor - 1] - old_count + Math.Abs(step_loss);
                    special_case_dir = true;
                }
                else
                {
                    //check the new position with the pre-moved position, we want the end position
                    //to move the same amount as "steps" (100 after corrected moved exactly 10 steps away
                    //from 90)
                    step_loss = Math.Abs(new_count[motor - 1] - old_count) - Math.Abs(step_loss);
                }

                overStep = step_loss > 0; //Add logic to check, overstep is true only if the step_loss is positive and direction is 

                // if step loss is larger than the tolerance, set TRUE to incorrect position
                incorrect_Position = Math.Abs(step_loss) > Tolerance;
                direction = compensate_Direction;
                old_count = new_count[motor - 1];
            }

            return new_count;
        }

        public List<int> RunSteps(int motor, bool direction, int steps)
        {
            //Convert to time then / 1000 to get ms then plus 8ms to send out response
            //this 8ms delay can be change with faster Baud rate
            int delayTime = Math.Abs(steps) / velocity * 1000 + 8;

            EZPort.DiscardOutBuffer();
            EZPort.DiscardInBuffer();
            //Get moving command
            string command = Controller.MoveSteps(motor, direction, steps);
            EZPort.WriteLine(command);
            Thread.Sleep(delayTime);
            List<int> counts = GetCounts();

            return counts;
        }

        public int ClosedLoop_Move(int motor, int steps)
        {
            List<int> return_count = new List<int>();
            bool dir;
            dir = steps > 0;
            return_count = Closed_Loop_RunSteps(motor, dir, Math.Abs(steps));

            return return_count[motor - 1];
        }

        public int OpenLoop_Move(int motor, int steps)
        {
            List<int> return_count;
            bool dir;
            dir = steps > 0;
            return_count = RunSteps(motor, dir, Math.Abs(steps));
            return return_count[motor - 1];
        }


        public List<int> GetCounts()
        {
            List<int> encoderCounts = new List<int>();
            try
            {
                if (EZPort.IsOpen)
                {

                    EZPort.DiscardOutBuffer();
                    EZPort.DiscardInBuffer();

                    EZPort.WriteLine(Controller.GetEncoderCounts());
                    Thread.Sleep(1);
                    string data = EZPort.ReadLine();
                    ErrorText = Controller.DecodeReponse(data);

                    //find the EOF index
                    int indexEnd = data.IndexOf("\u000d");
                    //if data is longer than 5 we assume it has count data in it
                    if (indexEnd > 5)
                    {
                        //assuming a format is /1@123456
                        int index = data.IndexOf('/') + 3;
                        string dataSubstring = data.Substring(index, indexEnd - index - 1);
                        string[] allCounts = dataSubstring.Split(',');

                        foreach (string count in allCounts)
                        {
                            if (int.TryParse(count, out int result))
                            {
                                encoderCounts.Add(result);

                            }
                            else
                            {
                                ErrorText = "cannot parse correctly in (Method: GetCounts)";
                            }
                        }
                        return encoderCounts;
                    }
                    else ErrorText = "Data too short"; return null;
                }
                else ErrorText = "Com Not Open"; return null;
            }
            catch
            {
                ErrorText = "Error in (Method: GetCounts)";
                return null;
            }
        }
        #endregion Motor Movement
        public string GetResponse()
        {
            try
            {

                ErrorText = String.Empty;
                Thread.Sleep(15);
                string data = EZPort.ReadLine();
                string status = Controller.DecodeReponse(data);
                return status;

            }
            catch
            {
                ErrorText = "Error in (Method: GetResponse)";
                return "Get Response Error";
            }
        }

        public void Curing(HPPM PM1)
        {
            CuringStartIndex = xSteps_Data.Count;               //Used for plotting curing position
            Fixed_Tolerance = true;                             //Fix toleramce to 1 count regardless of IL
            UI_ReadLoss = false;                                //Disable UI trigger PM reading
            Thread.Sleep(100);                                  //Used to avoid reading PM at the same time
            int stable_loss_count = 0;                          //track count when IL is stable
            List<int> tempEncoderList = GetCounts();            //Get encoder counts and save it in temp list 
            int zStart = tempEncoderList[2];                    //Track the Z count at start to avoid traveling in
            bool stopped = false;                               //Flag curing should stop for nested loop
            stopWatch.Restart();
            logFile.WriteLine($"Curing Starts: {Loss_Data.Min()}");
            Console.WriteLine($"Curing Starts: {Loss_Data.Min()}");
            //Switch case to avoid Z stepping in or out in late stage and limit the Z stepping range
            switch (productNumber)
            {
                case 1:
                    LateCycleTime = 6 * 60;
                    ZOutCount = 260;
                    ZInCount = 150;
                    break;
                case 2:
                    LateCycleTime = 6 * 60;
                    ZOutCount = 260;
                    ZInCount = 150;
                    break;
                case 3:
                    //large number to avoid it being used at all (testing)
                    LateCycleTime = 10 * 60;
                    ZOutCount = 160; 
                    ZInCount = 80;
                    break;
            }
            
            while (true)
            {
                Alignment_Minimum = Loss_Data.Min();
                stable_loss_count += 1;                         //Increment stable loss count to break out of the loop
                Thread.Sleep(1000);
                double tempLoss = GetLoss(PM1);
                UI_loss = tempLoss;
                logFile.WriteLine($"Loss: {tempLoss}");
                Console.WriteLine($"Loss: {tempLoss}");
                int expandtolerance = 5;
                if (productNumber == 3) expandtolerance = 7;

                int i = 1;                                      //Index to control the order of the XYZ moves
                //We keep in the loop at high loss
                while (tempLoss > curing_tolerance + Alignment_Minimum)
                {
                    
                    stable_loss_count = 0;                      //reset stable count
                    bool MeetTolerance = XYAlign(PM1, 1, curing_tolerance);
                    if (MeetTolerance) break;
                    tempLoss = GetLoss(PM1);
                    //For VOA, we move out Z every 3 XY attempts, try Z moving back in if it is out far enough
                    //Then we increase the tolerance every 5 XY attempts
                    if (productNumber == 3)
                    {
                        //check the last 6 elements, if the range is less than 0.03dB 
                        if (Check_Stop_Condition())
                        {
                            Console.WriteLine($"Epoxy hardened, curing stopped {stopWatch.Elapsed:mm\\:ss}");
                            logFile.WriteLine($"Epoxy hardened, curing stopped {stopWatch.Elapsed:mm\\:ss}");
                            stopped = true;
                            break;
                        }
                        if (i % 3 == 0 && zStart - zSteps_Data.Last() < ZOutCount && stopWatch.Elapsed.TotalSeconds < LateCycleTime)
                        {
                            if (zStart - zSteps_Data.Last() > ZInCount)
                            {
                                //move inward
                                ZCuringAlign(PM1, productNumber, tempLoss, false);
                            }
                            //move outward
                            ZCuringAlign(PM1, productNumber, tempLoss, true);
                            if (Loss_Data.Last() < curing_tolerance + Alignment_Minimum) break;
                        }

                        if (i % expandtolerance == 0)
                        {
                            Console.WriteLine("widening tolerance by 0.01");
                            logFile.WriteLine("widening tolerance by 0.01");
                            curing_tolerance += 0.01;
                        }
                        i++;
                    }
                    //1XN product will do X Y Z in each loop, the 4th time will see if it should step IN Z.
                    //if couldn't find lower IL, we increase tolerance by 0.01
                    else
                    {
                        if (stopWatch.Elapsed.TotalSeconds > LateCycleTime)
                        {
                            expandtolerance = 3;
                        }

                        //check the last 6 elements, if the range is less than 0.02dB + > 150 seconds
                        if (Check_Stop_Condition())
                        {
                            Console.WriteLine($"Epoxy hardened, curing stopped {stopWatch.Elapsed:mm\\:ss}");
                            logFile.WriteLine($"Epoxy hardened, curing stopped {stopWatch.Elapsed:mm\\:ss}");
                            break;
                        }
                        if (i != 4 && zStart - zSteps_Data.Last() < ZOutCount && stopWatch.Elapsed.TotalSeconds < LateCycleTime)
                        {
                            
                            //move outward
                            ZCuringAlign(PM1, productNumber, tempLoss, true);
                            if (Loss_Data.Last() < curing_tolerance + Alignment_Minimum) break;
                        }
                        if (i == 4 && zStart - zSteps_Data.Last() > ZInCount && stopWatch.Elapsed.TotalSeconds < LateCycleTime)
                        {
                            //move inward
                            ZCuringAlign(PM1, productNumber, tempLoss, false);
                        }
                        if (i % expandtolerance == 0)
                        {
                            Console.WriteLine("widening tolerance by 0.01");
                            logFile.WriteLine("widening tolerance by 0.01");
                            curing_tolerance += 0.01;
                        }
                        i++;
                    }
                    //for (int i = 0; i < loopcount; i++) // this used to be 5
                    //{
                    //    bool MeetTolerance = XYCuringAlign(PM1, productNumber, curing_tolerance);
                    //    tempLoss = GetLoss(PM1);
                    //    if (MeetTolerance) goto Found;
                    //    if (stopWatch.Elapsed.TotalSeconds > LateCycleTime)
                    //    {
                    //        loopcount = 3;
                    //        expandtolerance = 2;
                    //    }
                    //    //check the last 6 elements, if the range is less than 0.02dB + > 150 seconds
                    //    if (Check_Stop_Condition())
                    //    {
                    //        Console.WriteLine($"Epoxy hardened, curing stopped {stopWatch.Elapsed:mm\\:ss}");
                    //        logFile.WriteLine($"Epoxy hardened, curing stopped {stopWatch.Elapsed:mm\\:ss}");
                    //        goto NoChange;
                    //    }
                    //    //this used to be 2 (i % 2 == 1 && i <= 3 && ) 
                    //    if (i != 3 && zStart - zSteps_Data.Last() < ZOutCount && stopWatch.Elapsed.TotalSeconds < LateCycleTime) 
                    //    {
                    //        //move outward
                    //        ZCuringAlign(PM1, productNumber, tempLoss, true);
                    //    }

                    //    if (i == 3 && zStart - zSteps_Data.Last() > ZInCount && stopWatch.Elapsed.TotalSeconds < LateCycleTime)
                    //    {
                    //        //move inward
                    //        ZCuringAlign(PM1, productNumber, tempLoss, false);
                    //    }

                    //    //if XY alignment tried 3 loop and still unable to find better loss then we increase tolerance (VOA, add 60 seconds)
                    //    //stopWatch.Elapsed.TotalSeconds > 60 
                    //    if (i == expandtolerance) //this used to be 4
                    //    {
                    //        Console.WriteLine("widening tolerance by 0.01");
                    //        logFile.WriteLine("widening tolerance by 0.01");
                    //        curing_tolerance += 0.01;
                    //    }
                    //}
                }
                if (stopped) break;

                if(tempLoss < curing_tolerance + Alignment_Minimum)
                {
                    Console.WriteLine($"Tolerance Met: {curing_tolerance + Alignment_Minimum}");
                    logFile.WriteLine($"Tolerance Met: {curing_tolerance + Alignment_Minimum}");
                };

                // if we have 40 consecutive loss within tolerance and we have reached 120 seconds or more
                if (stable_loss_count > 40 && stopWatch.Elapsed.TotalSeconds > 120)
                {
                    Console.WriteLine($"Curing alignment has finished {stopWatch.Elapsed:mm\\:ss}");
                    logFile.WriteLine($"Curing alignment has finished {stopWatch.Elapsed:mm\\:ss}");
                    break;
                }
                // IT WAS NOT USED IN 1XN SM AT ALLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL
                //if (stable_loss_count > 2 && stopWatch.Elapsed.TotalSeconds > 2 * 60 && curing_tolerance > 0.04 && stopWatch.Elapsed.TotalSeconds < 3.5 * 60)
                //{
                //    List<double> temploss_list;
                //    temploss_list = Loss_sublist.GetRange(Loss_sublist.Count() - 5, 5);
                //    if (temploss_list.Min() + 0.03 <= curing_tolerance + Alignment_Minimum)
                //    {
                //        curing_tolerance = temploss_list.Min() + 0.03 - Alignment_Minimum;
                //        Console.WriteLine($"Lower tolerance : {curing_tolerance + Alignment_Minimum}");
                //        logFile.WriteLine($"Lower tolerance : {curing_tolerance + Alignment_Minimum}");
                //    }
                //}
            }

            //loop until 25 mins, keep reading loss data and reminder to turn off at 20 min mark
            while (true)
            {
                Thread.Sleep(1000);
                double tempLoss = GetLoss(PM1);
                UI_loss = tempLoss;
                Console.WriteLine($"Loss: {tempLoss}, Time: {stopWatch.Elapsed:mm\\:ss}");
                logFile.WriteLine($"Loss: {tempLoss}, Time: {stopWatch.Elapsed:mm\\:ss}");
                logFile.Flush();
                if (productNumber == 3 && stopWatch.Elapsed.TotalSeconds >= 14 * 60) break;

                if (productNumber == 1 && stopWatch.Elapsed.TotalSeconds >= 18 * 60) break;

            }

            logFile.Close();
            CreateLogFile();
            Reset_params();
        }

        public void Auto_Align(HPPM PM1, bool close_prox = false)
        {
            UI_ReadLoss = false;
            Thread.Sleep(100);
            stopWatch.Restart();
            List<int> tempEncoderList;
            GetCountSaveData(GetLoss(PM1));
            Console.WriteLine("Auto align starts: ");
            logFile.WriteLine("Auto align starts: ");

            int XY_Loop_Count;
            int Loss_index;
            int not_better = 0; //to track # of XYZ failure to find better loss
            List<double> loss_Sublist;    //Use to seperate list data from each XY alignment call
            double lossMin;


            XYAlign(PM1, 3, align_tolerance);

            //The while statement doesn't trigger often, usually it is the if statment
            //With break.
            do
            {
                //get the loop counts, bigger will take more time
                XY_Loop_Count = FindXY_IterationLoopCount(Loss_Data.Min());
                lossMin = Loss_Data.Min();

                //Get the current number of count to seperate new loss list from old list
                Loss_index = Loss_Data.Count();
                bool hardStopHit = ZAlign(PM1, productNumber, close_prox);
                XYAlign(PM1, XY_Loop_Count, align_tolerance);

                //Get loss sublist generated by the latest XY align and Z align
                loss_Sublist = Loss_Data.GetRange(Loss_index + 1, Loss_Data.Count() - Loss_index - 1);

                //If loss doesn't improve in the next 2 loops, stop looping X & Y & Z stepping
                if (loss_Sublist.Min() > lossMin && lossMin < 0.7) not_better += 1;
                if (not_better >= 2) break;

                //if time > 1mins and if it will bump into ferrule break!
                if (stopWatch.Elapsed.TotalMinutes > 1)
                {
                    ErrorText = "Time exceed 1min";
                    break;
                }
                    
                if (hardStopHit)
                {
                    ErrorText = "Lens Crash";
                    break;
                }

            } while (true);


            Alignment_Minimum = Loss_Data.Min();
            int index = Loss_Data.IndexOf(Alignment_Minimum);
            tempEncoderList = MoveToBest(xSteps_Data[index], ySteps_Data[index], zSteps_Data[index],
                xSteps_Data.Last(), ySteps_Data.Last(), zSteps_Data.Last());
            SaveReadData(tempEncoderList, 0, PM1, "Move to Best");
            XYAlign(PM1, 4, align_tolerance);
            Alignment_Minimum = Loss_Data.Min();
            Console.WriteLine($"Minimum Loss: {Alignment_Minimum}");
            logFile.WriteLine($"Minimum Loss: {Alignment_Minimum}");
            stopWatch.Stop();
            UI_ReadLoss = true;
            AlignmentEndIndex = xSteps_Data.Count - 1;
        }

        public void SpiralSearch(HPPM PM1)
        {
            UI_ReadLoss = false;
            Thread.Sleep(100);
            stopWatch.Restart();
            List<int> tempEncoderList;
            int stepSize;
            tempEncoderList = GetCountSaveData(GetLoss(PM1));
            List<double> iteration_Loss = new List<double>();
            if (Loss_Data.Last() <= 33) goto End;
            
            if(productNumber == 1) stepSize = 100;
            else stepSize = 200; 
            bool dir = false;
        Start:;
            iteration_Loss.Clear();
            for (int i = 1; i <= 20; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    tempEncoderList = Closed_Loop_RunSteps(1, dir, stepSize, tempEncoderList);  //Move 1 step size +x
                    SaveReadData(tempEncoderList, 1, PM1, $"X {dir}");
                    iteration_Loss.Add(Loss_Data.Last());
                    if (Loss_Data.Last() <= 33) goto End;
                    if (iteration_Loss.First() - iteration_Loss.Min() > 3)
                    {
                        if (productNumber == 1) stepSize = 50;
                        else stepSize = 200;
                        goto Start;
                    }
                }

                for (int j = 1; j <= i; j++)
                {
                    tempEncoderList = Closed_Loop_RunSteps(2, dir, stepSize, tempEncoderList);
                    SaveReadData(tempEncoderList, 2, PM1, $"Y {dir}");
                    iteration_Loss.Add(Loss_Data.Last());
                    if (Loss_Data.Last() <= 33) goto End;
                    if (iteration_Loss.First() - iteration_Loss.Min() > 3)
                    {
                        if (productNumber == 1) stepSize = 50;
                        else stepSize = 200;
                        goto Start;
                    }
                }
                dir = !dir;
            }

        End:;
        }

        private bool XYAlign(HPPM PM1, int loop_iteration, double curing_tol)
        {
            List<int> tempEncoderList;                           
            int stepSize;
            double tempLoss = GetLoss(PM1);
            tempEncoderList = GetCountSaveData(tempLoss);

            logFile.WriteLine($"X: {tempEncoderList[0]},Y: " +
                $"{tempEncoderList[1]},Z: {tempEncoderList[2]},Loss: {tempLoss},Time: {stopWatch.Elapsed:mm\\:ss}" +
                $"XY alignment starts!");
            Console.WriteLine($"Loss: {tempLoss},Time: {stopWatch.Elapsed:mm\\:ss\\:ff}," +
                $"X: {tempEncoderList[0]},Y: {tempEncoderList[1]},Z: {tempEncoderList[2]}. XY alignment starts");

            if (Loss_Data.Last() <= Alignment_Minimum + curing_tol) return true;

            //Find the proper stepsize based on current loss
            stepSize = FindStepSize(Loss_Data.Last(), productNumber);

            //Limit compelte X+ X- Y+ Y- to (loop_iteration) times, also break if the difference is < 0.005 dB
            for (int i = 0; i < loop_iteration; i++)
            {
                //x positive step first
                do
                {
                    tempEncoderList = Closed_Loop_RunSteps(1, true, stepSize, tempEncoderList);  //Move 1 step size +x
                    SaveReadData(tempEncoderList, 1, PM1, "X++");
                    if (Loss_Data.Last() <= Alignment_Minimum + curing_tol) return true;
                    if (Check_Stop_Condition()) return false;
                    stepSize = FindStepSize(Loss_Data.Last(), productNumber);
                } while (Loss_Data[Loss_Data.Count - 2] - Loss_Data.Last() > 0);//The loop continue if loss gets better

                //x negative now
                do
                {
                    tempEncoderList = Closed_Loop_RunSteps(1, false, stepSize, tempEncoderList);
                    SaveReadData(tempEncoderList, 1, PM1, "X--");
                    if (Loss_Data.Last() <= Alignment_Minimum + curing_tol) return true;
                    if (Check_Stop_Condition()) return false;
                    stepSize = FindStepSize(Loss_Data.Last(), productNumber);
                } while (Loss_Data[Loss_Data.Count - 2] - Loss_Data.Last() > 0);

                //move a step already so step back in +x direction
                tempEncoderList = Closed_Loop_RunSteps(1, true, stepSize, tempEncoderList);
                SaveReadData(tempEncoderList, 1, PM1, "StepBack X++");
                if (Loss_Data.Last() <= Alignment_Minimum + curing_tol) return true;
                if (Check_Stop_Condition()) return false;

                //y positive first
                do
                {
                    tempEncoderList = Closed_Loop_RunSteps(2, true, stepSize, tempEncoderList);
                    SaveReadData(tempEncoderList, 2, PM1, "Y++");
                    if (Loss_Data.Last() <= Alignment_Minimum + curing_tol) return true;
                    if (Check_Stop_Condition()) return false;
                    stepSize = FindStepSize(Loss_Data.Last(), productNumber);
                } while (Loss_Data[Loss_Data.Count - 2] - Loss_Data.Last() > 0);

                do
                {
                    tempEncoderList = Closed_Loop_RunSteps(2, false, stepSize, tempEncoderList);
                    SaveReadData(tempEncoderList, 2, PM1, "Y--");
                    if (Loss_Data.Last() <= Alignment_Minimum + curing_tol) return true;
                    if (Check_Stop_Condition()) return false;
                    stepSize = FindStepSize(Loss_Data.Last(), productNumber);
                } while (Loss_Data[Loss_Data.Count - 2] - Loss_Data.Last() > 0);

                //move a step already so step back in +y direction
                tempEncoderList = Closed_Loop_RunSteps(2, true, stepSize, tempEncoderList);
                SaveReadData(tempEncoderList, 2, PM1, "StepBack Y++");
                if (Loss_Data.Last() <= Alignment_Minimum + curing_tol) return true;
                if (Check_Stop_Condition()) return false;
            }
            if (Loss_Data.Last() <= Alignment_Minimum + curing_tol) return true;
            else return false;
        }

        public void XY_Search(HPPM PM1, int loopcount)
        {
            Alignment_Minimum = 0;
            XYAlign(PM1, loopcount, align_tolerance);
            int index = Loss_Data.IndexOf(Loss_Data.Min());

            List<int> tempEncoderList = MoveToBest(xSteps_Data[index], ySteps_Data[index], zSteps_Data[index],
                xSteps_Data.Last(), ySteps_Data.Last(), zSteps_Data.Last());
            double temploss = GetLoss(PM1);
            Loss_Data.Add(temploss);
            UI_loss = temploss;
            Alignment_Minimum = Loss_Data.Min();
            Console.WriteLine($"Best Loss = {Loss_Data.Min()}, at {xSteps_Data[index]}, {ySteps_Data[index]}, {zSteps_Data[index]}");
            Console.WriteLine($"After Adjusted = {Loss_Data.Last()}, at {tempEncoderList[0]}, {tempEncoderList[1]}, {tempEncoderList[2]}");
            logFile.WriteLine($"Best Loss = {Loss_Data.Min()}, at {xSteps_Data[index]}, {ySteps_Data[index]}, {zSteps_Data[index]}");
            logFile.WriteLine($"After Adjusted = {Loss_Data.Last()}, at {tempEncoderList[0]}, {tempEncoderList[1]}, {tempEncoderList[2]}");
        }

        private bool ZAlign(HPPM PM1, int productNumber, bool close_prox = false)
        {
            List<int> tempEncoderList;         
            int stepSize;
            double tempLoss = GetLoss(PM1);
            tempEncoderList = GetCountSaveData(tempLoss);

            logFile.WriteLine($"X: {tempEncoderList[0]},Y: " +
                $"{tempEncoderList[1]},Z: {tempEncoderList[2]},Loss: {tempLoss},Time: {stopWatch.Elapsed:mm\\:ss}" +
                $"Z alignment starts!");
            Console.WriteLine($"Loss: {tempLoss},Time: {stopWatch.Elapsed:mm\\:ss\\:ff}," +
                $"X: {tempEncoderList[0]},Y: {tempEncoderList[1]},Z: {tempEncoderList[2]}. Z alignment starts");


            //Find the proper stepsize based on current loss (Either use tempLoss(latest loss) or use minimum loss)
            stepSize = FindZStepSize(Loss_Data.Min(), productNumber);

            int z_Step_Iter = 0;
            //Z step loop, it steps until hit higher loss
            do
            {
                z_Step_Iter += 1;
                tempEncoderList = Closed_Loop_RunSteps(3, true, stepSize, tempEncoderList);
                SaveReadData(tempEncoderList, 3, PM1, "Z++");
                stepSize = FindZStepSize(Loss_Data.Min(), productNumber);
                if (zSteps_Data.Last() + stepSize > HardStop) return true;
            } while (Loss_Data[Loss_Data.Count - 2] - Loss_Data.Last() > 0);


            double overSteppingRatio = ZOverSteppingRatio(Loss_Data.Min(), productNumber, close_prox);
            //To speed things up, we add a multiplier to intentionally overtravel 
            //We continue to move in +Z direction. Only do this only if ratio is > 1 (product dependent)
            while (Loss_Data.Last() <= Loss_Data.Min() * overSteppingRatio)
            {
                tempEncoderList = Closed_Loop_RunSteps(3, true, stepSize, tempEncoderList);
                SaveReadData(tempEncoderList, 3, PM1, "Z++");
                if (zSteps_Data.Last() + stepSize > HardStop) return true;
            }

            //Step back if loss is low
            if (Loss_Data.Min() <= 0.8 && z_Step_Iter >= 2)
            {
                tempEncoderList = Closed_Loop_RunSteps(3, false, stepSize, tempEncoderList);
                SaveReadData(tempEncoderList, 3, PM1, "StepBack Z--");
            }

            return false;
        }

        public bool Pre_Curing_Align(HPPM PM1)
        {
            Fixed_Tolerance = true;
            stopWatch.Restart();
            
            if (productNumber == 1 || productNumber == 3)
            {
                double temploss = GetLoss(PM1);
                Loss_Data.Add(temploss);         //Pre-align loss
                UI_loss = temploss;
                UI_ReadLoss = false;
                Thread.Sleep(200);
                List<int> tempEncoderList = GetCounts();          //Get encoder counts and save it in temp list 
                xSteps_Data.Add(tempEncoderList[0]);    //Global x step list
                ySteps_Data.Add(tempEncoderList[1]);    //Global y step list
                zSteps_Data.Add(tempEncoderList[2]);    //Global z step list

                Console.WriteLine("Pre-curing starts ");
                logFile.WriteLine("Pre-curing starts ");
                bool found;
                XYAlign(PM1, 4, align_tolerance);

                found = Loss_Data.Last() < Alignment_Minimum + 0.02;
                while (!found && stopWatch.Elapsed.TotalSeconds < 20)
                {
                    XYAlign(PM1, 4, align_tolerance);
                    found = Loss_Data.Last() < Alignment_Minimum + 0.02;
                    if (found) { stopWatch.Stop(); UI_ReadLoss = true; Fixed_Tolerance = false; return found; };
                }
                stopWatch.Stop();
                UI_ReadLoss = true;
                Fixed_Tolerance = false;
                return (found);
            }

            stopWatch.Stop();
            UI_ReadLoss = true;
            Fixed_Tolerance = false;
            return false;
        }

        //This overload is for curing purpose
        private bool XYCuringAlign(HPPM PM1, int productNumber, double curing_tol)
        {
            
            int stepSize;
            double tempLoss = GetLoss(PM1);
            List<int> tempEncoderList = GetCountSaveData(tempLoss);
            if (Loss_Data.Last() < Alignment_Minimum + curing_tol) return true;

            logFile.WriteLine($"X: {tempEncoderList[0]},Y: " +
                $"{tempEncoderList[1]},Z: {tempEncoderList[2]},Loss: {tempLoss},Time: {stopWatch.Elapsed:mm\\:ss}" +
                $"XY alignment starts!");
            Console.WriteLine($"Loss: {tempLoss},Time: {stopWatch.Elapsed:mm\\:ss\\:ff}," +
                $"X: {tempEncoderList[0]},Y: {tempEncoderList[1]},Z: {tempEncoderList[2]}. XY alignment starts");

            //x positive step first
            //it takes 1 step first then add encodercounts, IL to list then check for loss change
            //if previous loss is higher than current loss then we keep moving

            //X direction
            //The loop continue if loss gets better
            do
            {
                stepSize = FindCuringStepSize(Loss_Data.Last(), productNumber);
                tempEncoderList = Closed_Loop_RunSteps(1, true, stepSize, tempEncoderList);
                SaveReadData(tempEncoderList, 1, PM1, "X++");
                if (Loss_Data.Last() <= Alignment_Minimum + curing_tol) return true;
                if (Check_Stop_Condition()) return false;
            } while (Loss_Data[Loss_Data.Count - 2] - Loss_Data.Last() > 0);

            //x negative now
            do
            {
                stepSize = FindCuringStepSize(Loss_Data.Last(), productNumber);
                tempEncoderList = Closed_Loop_RunSteps(1, false, stepSize, tempEncoderList);
                SaveReadData(tempEncoderList, 1, PM1, "X--");
                if (Loss_Data.Last() <= Alignment_Minimum + curing_tol) return true;
                if (Check_Stop_Condition()) return false;
            } while (Loss_Data[Loss_Data.Count - 2] - Loss_Data.Last() > 0);

            //move a step already so step back in +x direction
            tempEncoderList = Closed_Loop_RunSteps(1, true, stepSize, tempEncoderList);
            SaveReadData(tempEncoderList, 1, PM1, "X++");
            if (Loss_Data.Last() <= Alignment_Minimum + curing_tol) return true;
            if (Check_Stop_Condition()) return false;

            //y positive first
            do
            {
                stepSize = FindCuringStepSize(Loss_Data.Last(), productNumber);
                tempEncoderList = Closed_Loop_RunSteps(2, true, stepSize, tempEncoderList);
                SaveReadData(tempEncoderList, 2, PM1, "Y++");
                if (Loss_Data.Last() <= Alignment_Minimum + curing_tol) return true;
                if (Check_Stop_Condition()) return false;
            } while (Loss_Data[Loss_Data.Count - 2] - Loss_Data.Last() > 0);

            do
            {
                stepSize = FindCuringStepSize(Loss_Data.Last(), productNumber);
                tempEncoderList = Closed_Loop_RunSteps(2, false, stepSize, tempEncoderList);
                SaveReadData(tempEncoderList, 2, PM1, "Y--");
                if (Loss_Data.Last() <= Alignment_Minimum + curing_tol) return true;
                if (Check_Stop_Condition()) return false;
            } while (Loss_Data[Loss_Data.Count - 2] - Loss_Data.Last() > 0);
            //move a step already so step back in +y direction
            tempEncoderList = Closed_Loop_RunSteps(2, true, stepSize, tempEncoderList);
            SaveReadData(tempEncoderList, 2, PM1, "StepBack Y++");
            if (Loss_Data.Last() <= Alignment_Minimum + curing_tol) return true;
            else
            {
                return false;
            }
        }

        private bool Check_Stop_Condition()
        {
            if (Loss_Data.Count < 7) return false;
            List<double> Loss_sublist = Loss_Data.GetRange(Loss_Data.Count() - 6, 6);
            double lossRange = Loss_sublist.Max() - Loss_sublist.Min();
            if (lossRange < 0.003) return true;
            return false;
        }

        private void ZCuringAlign(HPPM PM1, int productNumber, double current_loss, bool outward)
        {
            int stepSize;
            List<int> tempEncoderList = GetCountSaveData(current_loss);

            logFile.WriteLine($"X: {tempEncoderList[0]},Y: " +
                $"{tempEncoderList[1]},Z: {tempEncoderList[2]},Loss: {current_loss},Time: {stopWatch.Elapsed:mm\\:ss}" +
                $"  Z Curing alignment starts!");
            Console.WriteLine($"Loss: {current_loss},Time: {stopWatch.Elapsed:mm\\:ss\\:ff}," +
                $"X: {tempEncoderList[0]},Y: {tempEncoderList[1]},Z: {tempEncoderList[2]}. Z Curing alignment starts");


            //Find the proper stepsize based on current loss (Either use tempLoss(latest loss) or use minimum loss)
            stepSize = FindCuringZStepSize(productNumber);
            int move_count = 0;
            if (outward)
            {
                //Z step loop out, it steps until hit higher loss
                do
                {
                    tempEncoderList = Closed_Loop_RunSteps(3, false, stepSize, tempEncoderList);
                    SaveReadData(tempEncoderList, 3, PM1, "Z--");
                    move_count += 1;
                } while (Loss_Data[Loss_Data.Count - 2] - Loss_Data.Last() > 0 && move_count < 10); //move_count was 4 for 1XN but i dont think it is useful

                //Step back
                tempEncoderList = Closed_Loop_RunSteps(3, true, stepSize, tempEncoderList);
                SaveReadData(tempEncoderList, 3, PM1, "StepBack Z++");
            }
            else
            {
                //if time is within 6 minutes (moving back in at late stage is dangerous)
                if (stopWatch.Elapsed.TotalSeconds <= 420)
                    //Z step loop inward, it steps until hit higher loss
                    do
                    {
                        tempEncoderList = Closed_Loop_RunSteps(3, true, stepSize, tempEncoderList);
                        SaveReadData(tempEncoderList, 3, PM1, "Z++ Inward");
                        move_count += 1;
                    } while (Loss_Data[Loss_Data.Count - 2] - Loss_Data.Last() > 0 && move_count < 4);

                //Step back
                tempEncoderList = Closed_Loop_RunSteps(3, false, stepSize, tempEncoderList);
                SaveReadData(tempEncoderList, 3, PM1, "StepBack Z--");
            }
        }

        private List<int> MoveToBest(int x_best, int y_best, int z_best, int x_last, int y_last, int z_last)
        {
            //Create a local list to store count
            List<int> counts = new List<int>();
            //pass the input directly out if no movement requried
            counts.Add(x_last);
            counts.Add(y_last);
            counts.Add(z_last);
            int x_steps, y_steps, z_steps;
            bool xdir, ydir, zdir;

            do
            {
                //Get the steps required to move
                x_steps = x_best - x_last;
                y_steps = y_best - y_last;
                z_steps = z_best - z_last;
                xdir = x_steps > 0;
                ydir = y_steps > 0;
                zdir = z_steps > 0;
                if (Math.Abs(x_steps) > 0)
                {
                    counts = Closed_Loop_RunSteps(1, xdir, Math.Abs(x_steps));
                }
                if (Math.Abs(y_steps) > 0)
                {
                    counts = Closed_Loop_RunSteps(2, ydir, Math.Abs(y_steps));
                }
                if (Math.Abs(z_steps) > 0)
                {
                    counts = Closed_Loop_RunSteps(3, zdir, Math.Abs(z_steps));
                }
                //Update new position and check if addtional adjustment is required
                x_last = counts[0];   //Update the latest x position
                y_last = counts[1];
                z_last = counts[2];
                //Calculate the steps to move if adjustment is required
                x_steps = x_best - x_last;
                y_steps = y_best - y_last;
                z_steps = z_best - z_last;

            } while (x_steps != 0 | y_steps != 0 | z_steps != 0);

            return counts;
        }

        
        private void SaveReadData(List<int> tempEncoderList, int motor, HPPM PM1, string display)
        {
            xSteps_Data.Add(tempEncoderList[0]);
            ySteps_Data.Add(tempEncoderList[1]);
            zSteps_Data.Add(tempEncoderList[2]);
            double tempLoss = GetLoss(PM1);
            WriteFile_DisplayConsole(tempEncoderList, tempLoss, motor, display);
            Loss_Data.Add(tempLoss);
            UI_loss = tempLoss;
            Tolerance = Find_Tolerance(Loss_Data.Min(), Fixed_Tolerance);
        }

        private List<int> GetCountSaveData(double tempLoss)
        {
            UI_loss = tempLoss;
            Loss_Data.Add(tempLoss);                    //Global loss data list
            List<int> tempEncoderList = GetCounts();    //Get encoder counts and save it in temp list 
            xSteps_Data.Add(tempEncoderList[0]);        //Global x step list
            ySteps_Data.Add(tempEncoderList[1]);        //Global y step list
            zSteps_Data.Add(tempEncoderList[2]);        //Global z step list

            return tempEncoderList;
        }

        public double GetLoss(HPPM PM1)
        {
            var IL = PM1.ReadPower();
            return Math.Round(Math.Abs(IL - ref_loss), 3);
        }
        public void SetHardStop()
        {
            //List<int> tempEncoderList = GetCounts();
            string command = Controller.Zero_Motor(3);
            EZPort.WriteLine(command);
            Thread.Sleep(50);
            HardStop = 0;
        }

        private void WriteFile_DisplayConsole(List<int> tempEncoderList, double tempLoss, int motor, string display = "")
        {
            int count;
            switch (motor)
            {

                case 1:
                    count = tempEncoderList[0];
                    break;
                case 2:
                    count = tempEncoderList[1];
                    break;
                case 3:
                    count = tempEncoderList[2];
                    break;
                default:
                    count = 0;
                    break;
            }

            logFile.WriteLine($"X: {tempEncoderList[0]},Y: " +
                $"{tempEncoderList[1]},Z: {tempEncoderList[2]},Loss: {tempLoss},Time: {stopWatch.Elapsed:mm\\:ss\\:ff}, {display} ");
            logFile.Flush();
            Console.WriteLine($"Loss: {tempLoss},Time: {stopWatch.Elapsed:mm\\:ss\\:ff},{count}, {display}");
        }

        //Function used to find the X+, X-, Y+, Y- loop count
        //Generally smaller loop count at high loss to avoid wasting time
        //Higher loop count at lower loss to make sure we do find the best point
        private int FindXY_IterationLoopCount(double IL)
        {
            if (IL > 2) return 1;
            else if (IL > 1) return 1;
            else if (IL > 0.5) return 2;
            else if (IL <= 0.5) return 3;
            else if (IL <= 0.4) return 3;
            return 0;
        }

        private int Find_Tolerance(double IL, bool curing)
        {
            if (!curing)
            {
                if (IL > 50) return 15;
                else if (IL > 20) return 5;
                else if (IL > 10) return 4;
                else if (IL > 1) return 3;
                else if (IL > 0.7) return 2;
                else return 1;
            }
            else return 1;
        }

        private int FindStepSize(double IL, int productNumber)
        {
            switch (productNumber)
            {
                //1XN
                case 1:

                    if (IL > 40) return 48; // 12800
                    else if (IL > 30 && IL <= 40) return 44; // 128 = 6400
                    else if (IL > 20 && IL <= 30) return 40; // 64 = 3200
                    else if (IL > 10 && IL <= 20) return 36; // 48 = 2400
                    else if (IL > 5 && IL <= 10) return 32; // 32 = 1600
                    else if (IL > 3 && IL <= 5) return 24; // 24 = 1200
                    else if (IL > 2 && IL <= 3) return 16; // 16 = 800nm
                    else if (IL > 1 && IL <= 2) return 12; // 12 = 600nm
                    else if (IL > 0.7 && IL <= 1) return 8; // 6 = 400nm
                    else if (IL > 0.4 && IL <= 0.7) return 4; // 6 = 400nm
                    else if (IL <= 0.4) return 4; // 4 = 200nm
                    break;


                //VOA 2500*4 encoder resolution
                case 3:
                    if (IL > 40) return 48; // 12800
                    else if (IL > 30 && IL <= 40) return 44; // 128 = 6400
                    else if (IL > 20 && IL <= 30) return 40; // 64 = 3200
                    else if (IL > 10 && IL <= 20) return 36; // 48 = 2400
                    else if (IL > 5 && IL <= 10) return 32; // 32 = 1600
                    else if (IL > 3 && IL <= 5) return 24; // 24 = 1200
                    else if (IL > 2 && IL <= 3) return 18; // 16 = 800nm
                    else if (IL > 1 && IL <= 2) return 14; // 12 = 600nm
                    else if (IL > 0.7 && IL <= 1) return 10; // 6 = 400nm
                    else if (IL > 0.4 && IL <= 0.7) return 8; // 6 = 400nm
                    else if (IL <= 0.4) return 8; // 4 = 200nm
                    break;
            }

            ErrorText = "Product# null";
            return -1;
        }

        private int FindCuringStepSize(double IL, int productNumber)
        {
            switch (productNumber)
            {
                //1XN SM
                case 1:

                    if (stopWatch.Elapsed.TotalSeconds < 5 * 60)
                    {
                        if (IL > 5) return 30; // 32 = 1600 was 32 (34)
                        else if (IL > 4) return 26; //(30)
                        else if (IL > 3) return 22; // 24 = 1200 was 24  (24)
                        else if (IL > 2) return 16; // 16 = 800nm was 16 (19)
                        else if (IL > 1) return 12; // 12 = 600nm was 12
                        else if (IL > 0.75) return 6; //(12)
                        else if (IL > 0.6) return 6; // 6 = 400nm
                        else if (IL <= 0.6) return 6; // 4 = 200nm was 3
                    }
                    else
                    {
                        if (IL > 2) return 22; // 16 = 800nm was 16 (19)
                        else if (IL > 1) return 18; // 12 = 600nm was 12
                        else if (IL > 0.75) return 10; //(12)
                        else if (IL > 0.6) return 10; // 6 = 400nm
                        else if (IL <= 0.6) return 10; // 4 = 200nm was 3
                    }

                    break;
                //VOA 2500*4 encoder resolution
                case 3:
                    if (stopWatch.Elapsed.TotalSeconds < 15 * 60)
                    {
                        if (IL > 5) return 24; // 32 = 1600 was 32 (34)
                        else if (IL > 4) return 24; //(30)
                        else if (IL > 3) return 20; // 24 = 1200 was 24  (24)
                        else if (IL > 2) return 16; // 16 = 800nm was 16 (19)
                        else if (IL > 1) return 12; // 12 = 600nm was 12
                        else if (IL > 0.75) return 8; //(12)
                        else if (IL > 0.6) return 8; // 6 = 400nm
                        else if (IL <= 0.6) return 8; // 4 = 200nm was 3
                    }
                    else
                    {
                        if (IL > 2) return 14; // 16 = 800nm was 16 (19)
                        else if (IL > 1) return 10; // 12 = 600nm was 12
                        else if (IL > 0.75) return 6; //(12)
                        else if (IL > 0.6) return 6; // 6 = 400nm
                        else if (IL <= 0.6) return 6; // 4 = 200nm was 3
                    }
                    break;
            }
            ErrorText = "Product# null";
            return -1;
        }

        private int FindZStepSize(double IL, int productNumber)
        {
            switch (productNumber)
            {
                //1XN
                case 1:
                    if (IL > 40) return 800;
                    else if (IL > 30) return 700;
                    else if (IL > 20) return 400;
                    else if (IL > 10) return 230;
                    else if (IL > 5) return 180;
                    else if (IL > 3) return 140;
                    else if (IL > 2) return 100;
                    else if (IL > 1) return 90;
                    else if (IL > 0.5) return 70;
                    else if (IL > 0.3) return 50;
                    else if (IL <= 0.3) return 40;
                    break;

                //VOA 2500*4 encoder resolution
                case 3:
                    if (IL > 40) return 800;
                    else if (IL > 30) return 700;
                    else if (IL > 20) return 400;
                    else if (IL > 10) return 230;
                    else if (IL > 5) return 180;
                    else if (IL > 3) return 140;
                    else if (IL > 2) return 100;
                    else if (IL > 1) return 90;
                    else if (IL > 0.5) return 70;
                    else if (IL > 0.3) return 50;
                    else if (IL <= 0.3) return 40;
                    break;
            }
            ErrorText = "Product# null";
            return -1;
        }



        private int FindCuringZStepSize(int productNumber)
        {
            switch (productNumber)
            {
                //1XN
                case 1:
                    return 20;
                //VOA
                case 3:
                    return 20;
            }
            ErrorText = "Product# null";
            return -1;
        }

        private double ZOverSteppingRatio(double IL, int productNumber, bool close_prox = false)
        {
            //This is used to save time by purposely overstepping in Z direction
            //The ratio should be adjusted accordingly
            switch (productNumber)
            {
                //1XN
                case 1:
                    if (close_prox)
                    {
                        return 1;
                    }
                    else
                    {
                        if (IL > 40) return 1.1;
                        else if (IL > 30) return 1.2;
                        else if (IL > 20) return 1.3;
                        else if (IL > 10) return 1.2;
                        else if (IL > 3) return 1.1;
                        else if (IL > 2) return 1;
                        else if (IL > 1) return 1;
                        else if (IL > 0.3) return 1;
                        else if (IL <= 0.3) return 1;
                    }
                    break;

                //VOA
                case 3:
                    if (close_prox)
                    {
                        return 1;
                    }
                    else
                    {
                        if (IL > 40) return 1.1;
                        else if (IL > 30) return 1.2;
                        else if (IL > 20) return 1.3;
                        else if (IL > 10) return 1.2;
                        else if (IL > 3) return 1.1;
                        else if (IL > 2) return 1;
                        else if (IL > 1) return 1;
                        else if (IL > 0.3) return 1;
                        else if (IL <= 0.3) return 1;
                    }
                    break;
            }
            return 0;
        }



        public void XY_Loss_Profile_Scan(HPPM PM1)
        {
            Stopwatch stopWatch = new Stopwatch();
            List<int> tempEncoderList = new List<int>();
            double tempLoss = Math.Round(Math.Abs(PM1.ReadPower() - ref_loss), 3);
            Loss_Data.Add(tempLoss);
            tempEncoderList = GetCounts();
            xSteps_Data.Add(tempEncoderList[0]);
            ySteps_Data.Add(tempEncoderList[1]);

            logFile.WriteLine($"{tempEncoderList[0]},{tempEncoderList[1]},{tempEncoderList[2]},{tempLoss},{stopWatch.ElapsedMilliseconds}");

            //start from center so we move to x-, y- edge first
            tempEncoderList = Closed_Loop_RunSteps(1, false, 25);
            tempEncoderList = Closed_Loop_RunSteps(2, false, 25);
            xSteps_Data.Add(tempEncoderList[0]);
            ySteps_Data.Add(tempEncoderList[1]);
            tempLoss = GetLoss(PM1);
            logFile.WriteLine($"{tempEncoderList[0]},{tempEncoderList[1]},{tempEncoderList[2]},{tempLoss},{stopWatch.ElapsedMilliseconds}");
            Loss_Data.Add(tempLoss);

            bool direction = true;
            for (int j = 0; j < 50; j++)
            {
                for (int i = 0; i < 51; i++)
                {
                    tempEncoderList = Closed_Loop_RunSteps(1, direction, 1, tempEncoderList);
                    xSteps_Data.Add(tempEncoderList[0]);
                    ySteps_Data.Add(tempEncoderList[1]);
                    tempLoss = GetLoss(PM1);
                    logFile.WriteLine($"{tempEncoderList[0]},{tempEncoderList[1]},{tempEncoderList[2]},{tempLoss},{stopWatch.ElapsedMilliseconds}");
                    Loss_Data.Add(tempLoss);
                }

                tempEncoderList = Closed_Loop_RunSteps(2, true, 1, tempEncoderList);
                xSteps_Data.Add(tempEncoderList[0]);
                ySteps_Data.Add(tempEncoderList[1]);
                tempLoss = GetLoss(PM1);
                logFile.WriteLine($"{tempEncoderList[0]},{tempEncoderList[1]},{tempEncoderList[2]},{tempLoss},{stopWatch.ElapsedMilliseconds}");
                Loss_Data.Add(tempLoss);
                direction = !direction;
            }
            logFile.Close();
        }
        private List<int> MoveToZBest(int z_best, int z_last)
        {
            //Create a local list to store count
            List<int> counts = new List<int>();
            //pass the input directly out if no movement requried
            counts.Add(z_last);
            int z_steps;
            bool zdir;

            do
            {
                //Get the steps required to move
                z_steps = z_best - z_last;
                zdir = z_steps > 0;
                if (Math.Abs(z_steps) > 0)
                {
                    counts = Closed_Loop_RunSteps(3, zdir, Math.Abs(z_steps));
                }
                //Update new position and check if addtional adjustment is required
                z_last = counts[2];
                //Calculate the steps to move if adjustment is required
                z_steps = z_best - z_last;

            } while (z_steps != 0);

            return counts;
        }

        //Leaving this overload here to serve as an option for every XY search
        private List<int> MoveToBest(int loss_index)
        {
            int new_Loss_index = Loss_Data.Count();

            List<double> loss_Sublist = Loss_Data.GetRange(loss_index, new_Loss_index - loss_index);
            List<int> x_sublist = xSteps_Data.GetRange(loss_index, new_Loss_index - loss_index);
            List<int> y_sublist = ySteps_Data.GetRange(loss_index, new_Loss_index - loss_index);
            int min_index = loss_Sublist.IndexOf(loss_Sublist.Min());

            int x_best = x_sublist[min_index];
            int y_best = y_sublist[min_index];
            int x_last = x_sublist.Last();
            int y_last = y_sublist.Last();
            List<int> counts = new List<int>();
            counts.Add(x_last);
            counts.Add(y_last);
            counts.Add(zSteps_Data.Last());
            int x_steps, y_steps;
            bool xdir, ydir;

            do
            {
                x_steps = x_best - x_last;
                y_steps = y_best - y_last;
                xdir = x_steps > 0;
                ydir = y_steps > 0;
                if (Math.Abs(x_steps) > 0)
                {
                    counts = Closed_Loop_RunSteps(1, xdir, Math.Abs(x_steps));
                }
                if (Math.Abs(y_steps) > 0)
                {
                    counts = Closed_Loop_RunSteps(2, ydir, Math.Abs(y_steps));
                }

                //Update new position and check if addtional adjustment is required
                x_last = counts[0];   //Update the latest x position
                y_last = counts[1];
                //Calculate the steps to move if adjustment is required
                x_steps = x_best - x_last;
                y_steps = y_best - y_last;

            } while (Math.Abs(x_steps) > 1 | Math.Abs(y_steps) > 1);

            return counts;
        }
    }
}
