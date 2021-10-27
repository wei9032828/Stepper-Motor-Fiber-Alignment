using DiCon.Instrument.HP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;

namespace Motor_Encoder
{
    public partial class UI_Form : Form
    {

        int steps; // This can probably be reduced, but not critical now
        EZ_Stepper Controller = new EZ_Stepper();
        Auto_Alignment Alignment = new Auto_Alignment();
        private HPPM PM1 = null;
        string path;
        double ref_loss;

        public UI_Form()
        {
            InitializeComponent();
            InitPowerMeter();
            LoadChart();
            Disable_Btns();
            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string reference = File.ReadAllText($"{path}\\Stepper Station Data\\Reference.txt");
            ref_loss = Convert.ToDouble(reference);
            Alignment.ref_loss = ref_loss;
            Product.SelectedIndex = 0;
            Alignment.productNumber = Product.SelectedIndex + 1;
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                ComPorts.Items.Add(port);
            }
            if (ComPorts.Items.Count != 0)
            {
                ComPorts.Text = ComPorts.Items[0].ToString();
            }
            Status.Text = "Please select Contoller Port & Product then connect";
            panel2.BringToFront();
            panel1.BringToFront();
            panel5.BringToFront();
            panel6.BringToFront();
            panel7.BringToFront();
        }

        private void UI_Timer_Tick(object sender, EventArgs e)
        {
            RefreshGUI();
        }

        public void RefreshGUI()
        {
            Invoke(new MethodInvoker(() =>
                {
                    if(!Alignment.UI_ReadLoss)
                    {
                        if (Alignment.UI_loss == 0) Thread.Sleep(500);
                        label_loss.Text = Alignment.UI_loss.ToString();
                    }
                    else if(Alignment.UI_ReadLoss)
                    {
                        label_loss.Text = Convert.ToString(Math.Round(Math.Abs(PM1.ReadPower() - ref_loss),3));
                    }
                    if (Alignment.stopWatch.Elapsed.TotalSeconds != 0)
                    {
                        labelTime.Text = $"{ Alignment.stopWatch.Elapsed:mm\\:ss}";
                    }
                    else
                    {
                        labelTime.Text = "00:00";
                    }
                    label_minloss.Text = Alignment.Alignment_Minimum.ToString();
                    ErrorCode.Text = Alignment.ErrorText;
                }
                ));
        }


        private void Disable_Btns()
        {
            auto_align.Enabled = false;
            Re_align.Enabled = false;
            PreCure.Enabled = false;
            curing.Enabled = false;
            Unlock.Enabled = false;
            Lock.Enabled = false;
            Btn_Init.Enabled = false;
        }

        private void Enable_Btns()
        {
            auto_align.Enabled = true;
            Re_align.Enabled = true;
            PreCure.Enabled = true;
            curing.Enabled = true;
            Unlock.Enabled = true;
            Lock.Enabled = true;
            Btn_Init.Enabled = true;
        }

        private void InitPowerMeter()
        {
            PM1 = new HPPM();
            PM1.Addr = 12;
            PM1.Slot = 2;
            PM1.BoardNumber = 0;
            PM1.Open();
            PM1.init();
            PM1.setUnit(1);
            PM1.AutoRange(true);
            PM1.aveTime(20);
        }
        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            if (Alignment.EZPort.IsOpen)
            {
                Alignment.EZPort.Close();
                Disable_Btns();
                Status.Text = "Disconnected";
                Btn_Connect.Text = "Connect";
            }
            else
            {
                try
                {
                    bool success = Alignment.Open_Port(ComPorts.Text);
                    if (success)
                    {
                        Status.Text = "Connected";
                        Enable_Btns();
                        Btn_Connect.Text = "Disconnect";
                    }
                    else return;
                    
                }
                catch
                {
                    MessageBox.Show("Serial port" + Alignment.EZPort.PortName + "Cannot be opened");
                }
            }
        }


        private void Btn_Init_Click(object sender, EventArgs e)
        {
            Alignment.Board_Initialization();
            ErrorCode.Text = Alignment.ErrorText;
            Status.Text = "Initialized";
        }


        private void getCountsButton_Click(object sender, EventArgs e)
        {
            List<int> templist;
            templist = Alignment.GetCounts();
            xMoveCount.Text = templist[0].ToString();
            yMoveCount.Text = templist[1].ToString();
            zMoveCount.Text = templist[2].ToString();
        }


        private void moveX_Click(object sender, EventArgs e)
        {
            steps = Convert.ToInt32(xMoveSteps.Text);
            xMoveCount.Text = Alignment.OpenLoop_Move(1, steps).ToString();

        }

        private void moveY_Click(object sender, EventArgs e)
        {
            steps = Convert.ToInt32(yMoveSteps.Text);
            yMoveCount.Text = Alignment.OpenLoop_Move(2, steps).ToString();
        }

        private void moveZ_Click(object sender, EventArgs e)
        {
            steps = Convert.ToInt32(zMoveSteps.Text);
            zMoveCount.Text = Alignment.OpenLoop_Move(3, steps).ToString();
        }

        private void ClosedLoopX_Click(object sender, EventArgs e)
        {

            steps = Convert.ToInt32(xMoveSteps.Text);
            xMoveCount.Text = Alignment.ClosedLoop_Move(1, steps).ToString();
        }

        private void ClosedLoopY_Click(object sender, EventArgs e)
        {
            steps = Convert.ToInt32(yMoveSteps.Text);
            yMoveCount.Text = Alignment.ClosedLoop_Move(2, steps).ToString();
        }

        private void ClosedLoopZ_Click(object sender, EventArgs e)
        {
            steps = Convert.ToInt32(zMoveSteps.Text);
            zMoveCount.Text = Alignment.ClosedLoop_Move(3, steps).ToString();
        }
        private void Unlock_Click(object sender, EventArgs e)
        {
            Alignment.EZPort.WriteLine(Controller.Unlock_All_Motors());
            Status.Text = "Motor Unlocked";
        }

        private void Lock_Click(object sender, EventArgs e)
        {
            Alignment.EZPort.WriteLine(Controller.Initialize());
            Status.Text = "Motor Engaged";
        }
        private async void curingAsync_Click(object sender, EventArgs e)
        {
            Status.Text = "Running Curing...";
            await Task.Run(() => {
                Alignment.Curing(PM1);
            });
            Status.Text = "Curing Finished";
            
        }


        private async void auto_alginAsync_Click(object sender, EventArgs e)
        {
            Alignment.Alignment_Minimum = 0;
            Alignment.Loss_Data.Clear();
            Alignment.xSteps_Data.Clear();
            Alignment.ySteps_Data.Clear();
            Alignment.zSteps_Data.Clear();
            Alignment.HardStop = Int32.MaxValue;
            //Moving out a little bit
            Alignment.ClosedLoop_Move(3, -1000);
            Status.Text = "Running Alignment...";
            await Task.Run(() => {
                Alignment.SpiralSearch(PM1);
                Alignment.Auto_Align(PM1);
                });
            Status.Text = "Alignment Finished";
        }

        private async void OutAlignAsync_Click(object sender, EventArgs e)
        {
            Alignment.Alignment_Minimum = 0;
            Alignment.Loss_Data.Clear();
            Alignment.xSteps_Data.Clear();
            Alignment.ySteps_Data.Clear();
            Alignment.zSteps_Data.Clear();
            //The hard stop is set here to prevent motor moving in closer than where we are right now
            //Take this out if you don't want to limit the Z travel
            Alignment.SetHardStop();
            switch (Product.SelectedIndex)
            {
                case 0:
                    Alignment.ClosedLoop_Move(3, -6000);
                    break;
                case 1:
                    Alignment.ClosedLoop_Move(3, -6000);
                    break;
                case 2:
                    Alignment.ClosedLoop_Move(3, -2000);
                    break;
            }
            
            
            Status.Text = "Running Out & Alignment...";
            await Task.Run(() => {
                Alignment.SpiralSearch(PM1);
                Alignment.Auto_Align(PM1, true);
            });
            Status.Text = "Out & Alignment Finished";
        }

        private async void PreCureAsync_Click(object sender, EventArgs e)
        {
            bool targetReached = false;
            Status.Text = "Running Pre Curing...";
            await Task.Run(() => {
                
                Alignment.SpiralSearch(PM1);
                targetReached = Alignment.Pre_Curing_Align(PM1);
                if (targetReached)
                {
                    Console.WriteLine($"Reach target = {Alignment.Alignment_Minimum} + 0.02");
                }
                else
                {
                    Console.WriteLine("failed to find minimum loss");
                }
            });
            if (targetReached)
            {
                Status.Text = $"Reach target = {Alignment.Alignment_Minimum} + 0.02";
            }
            else
            {
                Status.Text = "Pre Curing unable to find minimum loss";
            }
        }

        private void Reference_Click(object sender, EventArgs e)
        {
            StreamWriter referenceFile = new StreamWriter($"{path}\\Stepper Station Data\\Reference.txt");
            double temploss = Math.Round(PM1.ReadPower(), 3);
            referenceFile.WriteLine(temploss);
            referenceFile.Flush();
            Status.Text = $"Reference Captured {temploss}";
            Alignment.ref_loss = temploss;
            ref_loss = temploss;
            referenceFile.Close();
        }

        private void Product_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Alignment.productNumber = Product.SelectedIndex + 1;
        }
        private void XYAlign_Click(object sender, EventArgs e)
        {
            Alignment.Loss_Data.Clear();
            Alignment.xSteps_Data.Clear();
            Alignment.ySteps_Data.Clear();
            Alignment.zSteps_Data.Clear();

            Alignment.XY_Search(PM1,4);
        }

        private void xy_Scan_Area_Click(object sender, EventArgs e)
        {
            Alignment.XY_Loss_Profile_Scan(PM1);
        }

        private void UI_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PM1 != null && PM1.Open())
            {
                PM1.Close();
            }
            if (Alignment.EZPort.IsOpen) Alignment.EZPort.Close();

            Alignment.logFile.Close();
        }

        private void ReadLoss_Click(object sender, EventArgs e)
        {
            loss.Text = Convert.ToString(Math.Abs(PM1.ReadPower() - ref_loss));
        }

        private async void alignment_test_Click(object sender, EventArgs e)
        {
            await Task.Run(() => {
                List<double> test_losses = new List<double>();
                for (int i = 1; i < 100; i++)
                {
                    if (i * -400 < 8000)
                    {
                        Alignment.ClosedLoop_Move(3, i * -400);
                        Alignment.ClosedLoop_Move(2, i * 5);
                        Alignment.ClosedLoop_Move(1, i * 5);
                    }
                    else
                    {
                        Alignment.ClosedLoop_Move(3, -2000);
                        Alignment.ClosedLoop_Move(2, 150);
                        Alignment.ClosedLoop_Move(1, -170);
                    }

                    Alignment.Loss_Data.Clear();
                    Alignment.xSteps_Data.Clear();
                    Alignment.ySteps_Data.Clear();
                    Alignment.zSteps_Data.Clear();
                    Alignment.Auto_Align(PM1);
                    test_losses.Add(Alignment.Loss_Data.Min());
                    //logFile.WriteLine($"{Alignment.Loss_Data.Min()}, {Alignment.stopWatch.Elapsed.TotalSeconds}");
                    //logFile.Flush();
                }
            });
        }

        private void UI_Form_Load(object sender, EventArgs e)
        {

        }

        private void LoadChart()
        {
            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Position (Counts)",
                LabelFormatter = value => value.ToString("")
            });
            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;
            cartesianChart1.Background = new SolidColorBrush(Color.FromRgb(24, 30, 54));

            cartesianChart2.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Position (Counts)",
                LabelFormatter = value => value.ToString("")
            });
            cartesianChart2.LegendLocation = LiveCharts.LegendLocation.Right;
            cartesianChart2.Background = new SolidColorBrush(Color.FromRgb(24, 30, 54));

            cartesianChart3.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Position (Counts)",
                LabelFormatter = value => value.ToString("")
            });
            cartesianChart3.LegendLocation = LiveCharts.LegendLocation.Right;
            cartesianChart3.Background = new SolidColorBrush(Color.FromRgb(24, 30, 54));

        }

        private void btn_LoadChart_Click(object sender, EventArgs e)
        {
            if (Alignment.xSteps_Data.Count != 0)
            {
                //Init data
                cartesianChart1.Series.Clear();
                cartesianChart2.Series.Clear();
                cartesianChart3.Series.Clear();
                ChartValues<int> XSeries = new ChartValues<int>();
                foreach (int xdata in Alignment.xSteps_Data)
                {
                    XSeries.Add(xdata);
                }
                cartesianChart1.Series.Add(new LineSeries
                {
                    Values = XSeries,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Color.FromRgb(107, 185, 79)),
                    Fill = Brushes.Transparent,
                    LineSmoothness = 1,
                    PointGeometry = null
                });

                ChartValues<int> YSeries = new ChartValues<int>();
                foreach (int ydata in Alignment.ySteps_Data)
                {
                    YSeries.Add(ydata);
                }
                cartesianChart2.Series.Add(new LineSeries
                {
                    Values = YSeries,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Color.FromRgb(26, 122, 192)),
                    Fill = Brushes.Transparent,
                    LineSmoothness = 1,
                    PointGeometry = null
                    //PointGeometrySize = 20,
                    //PointForeground = new SolidColorBrush(Color.FromRgb(40, 26, 29))
                });

                ChartValues<int> ZSeries = new ChartValues<int>();
                foreach (int zdata in Alignment.zSteps_Data)
                {
                    ZSeries.Add(zdata);
                }
                cartesianChart3.Series.Add(new LineSeries
                {
                    Values = ZSeries,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Color.FromRgb(29, 137, 152)),
                    Fill = Brushes.Transparent,
                    LineSmoothness = 1,
                    PointGeometry = null
                    //PointGeometrySize = 20,
                    //PointForeground = new SolidColorBrush(Color.FromRgb(40, 26, 29))
                });

            }
        }

        private void btn_LoadCurigChart_Click(object sender, EventArgs e)
        {
            if (Alignment.CuringStartIndex != 0)
            {
                //Init data
                cartesianChart1.Series.Clear();
                cartesianChart2.Series.Clear();
                cartesianChart3.Series.Clear();
                ChartValues<int> XSeries = new ChartValues<int>();
                for (int i = Alignment.CuringStartIndex; i < Alignment.xSteps_Data.Count - 1; i++)
                {
                    XSeries.Add(Alignment.xSteps_Data[i]);
                }
                cartesianChart1.Series.Add(new LineSeries
                {
                    Values = XSeries,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Color.FromRgb(107, 185, 79)),
                    Fill = Brushes.Transparent,
                    LineSmoothness = 1,
                    PointGeometry = null
                });

                ChartValues<int> YSeries = new ChartValues<int>();
                for (int i = Alignment.CuringStartIndex; i < Alignment.xSteps_Data.Count - 1; i++)
                {
                    YSeries.Add(Alignment.ySteps_Data[i]);
                }
                cartesianChart2.Series.Add(new LineSeries
                {
                    Values = YSeries,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Color.FromRgb(26, 122, 192)),
                    Fill = Brushes.Transparent,
                    LineSmoothness = 1,
                    PointGeometry = null
                });

                ChartValues<int> ZSeries = new ChartValues<int>();
                for (int i = Alignment.CuringStartIndex; i < Alignment.xSteps_Data.Count - 1; i++)
                {
                    ZSeries.Add(Alignment.zSteps_Data[i]);
                }
                cartesianChart3.Series.Add(new LineSeries
                {
                    Values = ZSeries,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Color.FromRgb(29, 137, 152)),
                    Fill = Brushes.Transparent,
                    LineSmoothness = 1,
                    PointGeometry = null
                });

            }
        }

        private void LoadAlignmentChart_Click(object sender, EventArgs e)
        {
            if (Alignment.AlignmentEndIndex != 0)
            {
                //Init data
                cartesianChart1.Series.Clear();
                cartesianChart2.Series.Clear();
                cartesianChart3.Series.Clear();
                ChartValues<int> XSeries = new ChartValues<int>();
                for (int i = 0; i < Alignment.AlignmentEndIndex; i++)
                {
                    XSeries.Add(Alignment.xSteps_Data[i]);
                }
                cartesianChart1.Series.Add(new LineSeries
                {
                    Values = XSeries,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Color.FromRgb(107, 185, 79)),
                    Fill = Brushes.Transparent,
                    LineSmoothness = 1,
                    PointGeometry = null
                });

                ChartValues<int> YSeries = new ChartValues<int>();
                for (int i = 0; i < Alignment.AlignmentEndIndex; i++)
                {
                    YSeries.Add(Alignment.ySteps_Data[i]);
                }
                cartesianChart2.Series.Add(new LineSeries
                {
                    Values = YSeries,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Color.FromRgb(26, 122, 192)),
                    Fill = Brushes.Transparent,
                    LineSmoothness = 1,
                    PointGeometry = null
                });

                ChartValues<int> ZSeries = new ChartValues<int>();
                for (int i = 0; i < Alignment.AlignmentEndIndex; i++)
                {
                    ZSeries.Add(Alignment.zSteps_Data[i]);
                }
                cartesianChart3.Series.Add(new LineSeries
                {
                    Values = ZSeries,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Color.FromRgb(29, 137, 152)),
                    Fill = Brushes.Transparent,
                    LineSmoothness = 1,
                    PointGeometry = null
                });

            }
        }
    }
}
