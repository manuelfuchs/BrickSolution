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
                for (int i = 0; i < 10; i++)
                {
                    Robot.SearchFood();

                    switch (Robot.LastStopReason)
                    {
                        case StopReason.AbyssDetected:
                            Robot.RotateClockWise(RotationMode.OtherMode);
                            break;
                        case StopReason.ObstacleDetected:
                            Robot.RotateClockWise(RotationMode.OtherMode);
                            break;
                        case StopReason.FoodplaceDetected:
                            break;
                        case StopReason.SingleFoodDetected:
                            break;
                        case StopReason.EnclosureDetected:
                            break;
                        default:
                            break;
                    }
                }
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
