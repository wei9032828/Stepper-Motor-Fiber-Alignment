using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Motor_Encoder
{
    class EZ_Stepper
    {
        public int Velocity { get; } = 12800;
        public int BoardNumber { get; set; } = 1;

        public string Initialize()
        {
            string commandStr;
            int microSteps = 64;
            int encoderCount_cpr = 2500 * 4;
            int motor_spr = microSteps * 200;
            int aE = 1000 * motor_spr / encoderCount_cpr;
            commandStr = $"/{BoardNumber}aM1aE{aE}j{microSteps}V{Velocity}m50h35aM2aE{aE}j{microSteps}V{Velocity}m50h35aM3aE{aE}j{microSteps}V{Velocity}m50h35R\r\n";
            return commandStr;
        }

        public string MoveSteps(int motor, bool direction, int steps)
        {
            char rotateDirection = direction ? 'P' : 'D';
            string commandStr = $"/{BoardNumber}V{Velocity}m50h35aM{motor}{rotateDirection}{steps}R\r\n";
            return commandStr;
        }

        public string XYMoveSteps(bool xdirection, int xsteps, bool ydirection, int ysteps)
        {
            string commandStr;
            char rotateDirection = xdirection ? 'P' : 'D';
            if (xdirection = ydirection)
            {
                commandStr = $"/{BoardNumber}V{Velocity}h35{rotateDirection}{xsteps},{ysteps},,R\r\n";
                return commandStr;
            }
            else
            {
                commandStr = $"/{BoardNumber}V{Velocity}h35{rotateDirection}{xsteps},-{ysteps},,R\r\n";
                return commandStr;
            }
        }


        public string GetEncoderCounts()
        {
            string commandStr = $"/{BoardNumber}?a8\r\n";
            return commandStr;
        }

        public string SelectMotor(int motor)
        {
            string commandStr = $"/{BoardNumber}aM{motor}R\r\n";
            return commandStr;
        }

        public string TerminatetMotor(int motor)
        {
            string commandStr = $"/{BoardNumber}T{motor}\r\n";
            return commandStr;
        }


        public string Unlock_All_Motors()
        {
            string commandStr = $"/{BoardNumber}h0,0,0,0R\r\n";
            return commandStr;
        }


        public string Zero_Motor(int motor)
        {
            string commandStr = $"/{BoardNumber}aM{motor}z0R\r\n";
            return commandStr;
        }

        public string DecodeReponse(string response)
        {
            int Index;
            StringBuilder packet_bits = new StringBuilder();


            Index = response.IndexOf('/') + 2;
            byte packet = Convert.ToByte(response[Index]);

            for (int i = 0; i < 8; i++)
            {
                int value;
                value = packet;
                packet_bits.Append((value & 128) == 0 ? 0 : 1);
                value <<= 1;
            }
            return packet_bits.ToString();
        }

    }
}
