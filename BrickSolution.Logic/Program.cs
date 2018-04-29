using BrickSolution.Logic.Enumerations;
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
            catch (Exception e)
            {
                if (Robot.IsInitialized)
                {
                    Robot.HaltMotors();
                }
                else
                {
                    Robot.Print(Constants.InitializeErrorMessage);
                }

                Robot.Print(e.Message);
            }
            finally
            {
                Robot.PrintEmptyLine();
                Robot.Print(Constants.ClosingMessage);
                Thread.Sleep(Constants.LcdMessageDisplayDuration);
            }
        }
    }
}
