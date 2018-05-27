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

                Thread.Sleep(10000);

                Robot.CollectFood();

                Thread.Sleep(5000);

                Robot.PushObjectInFrontToLeft();

                //while (Robot.FoodState == FoodState.Carrying)
                //{
                //    Robot.Drive();

                //    switch (Robot.LastStopReason)
                //    {
                //        case StopReason.AbyssDetected:
                //            Robot.Rotate(
                //                RotationMode.OtherMode,
                //                Constants.ROTATION_SPEED_FORWARD,
                //                Constants.ROTATION_SPEED_BACKWARD);
                //            break;
                //        case StopReason.ObstacleDetected:
                //            Robot.Rotate(
                //                RotationMode.OtherMode,
                //                Constants.ROTATION_SPEED_FORWARD,
                //                Constants.ROTATION_SPEED_BACKWARD);
                //            break;
                //        case StopReason.FoodplaceDetected:
                //            break;
                //        case StopReason.SingleFoodDetected:
                //            Robot.PushObjectInFrontToLeft();
                //            break;
                //        case StopReason.EnclosureDetected:
                //            break;
                //        default:
                //            break;
                //    }
                //}
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
                Robot.DisposeComponents();
                Robot.PrintEmptyLine();
                Robot.Print(Constants.PROGRAM_FINISHED_MSG);
                Thread.Sleep(Constants.PROGRAM_ABORTION_DELAY);
            }
        }
    }
}
