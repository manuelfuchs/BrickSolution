﻿using BrickSolution.Logic.Enumerations;
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

                Thread.Sleep(1000);
                
                while (true)
                {
                    Robot.Drive();

                    Robot.Print($"last-stop-reason: {Robot.LastStopReason}");
                    Thread.Sleep(3000);

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
                Robot.DisposeComponents();
                Robot.PrintEmptyLine();
                Robot.Print(Constants.PROGRAM_FINISHED_MSG);
                Thread.Sleep(Constants.PROGRAM_ABORTION_DELAY);
            }
        }
    }
}
