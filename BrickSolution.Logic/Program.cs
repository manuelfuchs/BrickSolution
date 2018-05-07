using BrickSolution.Logic.Enumerations;
using MonoBrickFirmware.UserInput;
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
#if DEBUG
                ButtonEvents buttonEvents = new ButtonEvents();

                Action emergencyStopAction = () =>
                {
                    Robot.HaltMotors();
                    throw new Exception();
                };

                buttonEvents.EscapePressed += emergencyStopAction;


#endif
                
                Robot.CloseAndRiseGrappler();
            }
            catch (Exception e)
            {
                if (Robot.IsInitialized)
                {
                    Robot.HaltMotors();
                }
                else
                {
                    Robot.Print(Constants.INIT_ERROR_MSG);
                }

                Robot.Print(e.Message);
            }
            finally
            {
                Robot.PrintEmptyLine();
                Robot.Print(Constants.PROGRAM_FINISHED_MSG);
                Thread.Sleep(Constants.PROGRAM_ABORTION_DELAY);
            }
        }
    }
}
