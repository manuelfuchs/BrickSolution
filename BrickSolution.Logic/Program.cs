using MonoBrickFirmware.Display;
using System;
using System.Threading;

namespace BrickSolution.Logic
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            try
            {
                Robot.InitRobot();
                
                
            }
            catch (Exception e)
            {
                if (Robot.IsInitialized)
                {
                    Robot.HaltWheels();
                }
                else
                {
                    Robot.Print(Constants.INITIALIZE_ERROR_MSG);
                }

                Robot.Print(e.Message);
                Thread.Sleep(Constants.LcdErrorDuration);
            }
        }
    }
}
