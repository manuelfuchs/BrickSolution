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
                
                //start debug code
                for (int cunt = 0; cunt < 1000; cunt++)
                {
                    LcdConsole.WriteLine("{0}", Robot.GetColorName());
                }
                //end debug code

                //robot.Drive(50, robot.IRBreakCondition, robot.GenerateParameter(Constants.tableEndDistance));
            }
            catch (Exception e)
            {
                if (Robot.IsInitialized)
                {
                    Robot.HaltWheels();
                }
                else
                {
                    LcdConsole.WriteLine("{0}", Constants.INITIALIZE_ERROR);
                }

                LcdConsole.WriteLine("{0}", e.Message);
                Thread.Sleep(15000);
            }
        }
    }
}
