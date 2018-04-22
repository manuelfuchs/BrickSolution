using MonoBrickFirmware.Display;
using System;
using System.Threading;

namespace BrickSolution.Logic
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Robot robot = null;

            try
            {
                robot = Robot.GetInstance();
                
                //start debug code
                for (int cunt = 0; cunt < 1000; cunt++)
                {
                    LcdConsole.WriteLine("{0}", robot.GetColorName());
                }
                //end debug code

                //robot.Drive(50, robot.IRBreakCondition, robot.GenerateParameter(Constants.tableEndDistance));
            }
            catch (Exception e)
            {
                if (robot != null)
                {
                    robot.HaltWheels();
                }

                LcdConsole.WriteLine("{0}", e.Message);
                Thread.Sleep(15000);
            }
        }
    }
}
