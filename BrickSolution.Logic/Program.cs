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

                Robot.WaitForStartButtonPress();

                Robot.DetectTeamColor();

                Robot.PrintEmptyLine();
                Robot.Print($"detected-team: {Robot.TeamMode}");
                
                Thread.Sleep(Constants.STEP_DELAY);

                Robot.CollectFood();

                Thread.Sleep(Constants.STEP_DELAY);
                
                while (true)
                {
                    Robot.Drive();
                    Robot.Print($"last-stop-reason: {Robot.LastStopReason}");

                    Thread.Sleep(Constants.STEP_DELAY);

                    switch (Robot.LastStopReason)
                    {
                        case StopReason.AbyssDetected:
                            Robot.Rotate(
                                RotationMode.OtherMode,
                                Constants.ROTATION_SPEED_FORWARD,
                                Constants.ROTATION_SPEED_BACKWARD);
                            break;
                        case StopReason.ObstacleDetected:
                            Robot.Rotate(
                                RotationMode.OtherMode,
                                Constants.ROTATION_SPEED_FORWARD,
                                Constants.ROTATION_SPEED_BACKWARD);
                            break;
                        case StopReason.FenceDetected:
                            if (Robot.TeamMode == TeamMode.IAhTeam)
                            {
                                Robot.PushObjectInFrontToLeft();
                            }
                            else
                            {
                                Robot.Rotate(
                                    RotationMode.TimerMode,
                                    Constants.ROTATION_SPEED_FORWARD,
                                    Constants.ROTATION_SPEED_BACKWARD);
                            }
                            break;
                        case StopReason.TreeDetected:
                            Robot.Rotate(
                                RotationMode.TimerMode,
                                Constants.ROTATION_SPEED_FORWARD,
                                Constants.ROTATION_SPEED_BACKWARD);
                            break;
                        case StopReason.OurFoodDetected:
                            if (Robot.FoodState == FoodState.Searching)
                            {
                                Robot.CollectFood();
                            }
                            else
                            {
                                Robot.PushObjectInFrontToLeft();
                            }
                            
                            break;
                        case StopReason.EnemyFoodDetected:
                            Robot.PushObjectInFrontToLeft();
                            break;
                        case StopReason.MeadowDetected:
                            if (Robot.MeadowIsOurs()
                                && Robot.FoodState == FoodState.Carrying)
                            {
                                Robot.RealeaseBrickOnMeadow();
                            }
                            else
                            {
                                Robot.Rotate(
                                    RotationMode.HalfRotationMode,
                                    Constants.ROTATION_SPEED_FORWARD,
                                    Constants.ROTATION_SPEED_BACKWARD);
                            }
                            break;
                        default:
                            break;
                    }

                    Thread.Sleep(Constants.STEP_DELAY);
                }
            }
            catch (Exception e)
            {
                Robot.Print(e.Message);
                Robot.Print(e.TargetSite.ToString());
            }
            finally
            {
                Thread.Sleep(Constants.STEP_DELAY);

                Robot.HaltMotors();
                Robot.DisposeComponents();
                Robot.PrintEmptyLine();
                Robot.Print(Constants.PROGRAM_FINISHED_MSG);
                Thread.Sleep(Constants.PROGRAM_ABORTION_DELAY);
            }
        }
    }
}
