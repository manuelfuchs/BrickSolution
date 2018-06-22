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

                Thread.Sleep(1000);
                
                Wait("Press button to calibrize:");
                FullColor calibColor = Robot.GetFullColor();

                for (int i = 0; i < 10; i++)
                {
                    Wait("press middle button to read");
                    FullColor color = Robot.GetFullColor();

                    if (color != null)
                    {
                        Robot.Print($"  calibCo = {calibColor.ToString()}");
                        Robot.Print($"  current = {color.ToString()}");
                        Robot.Print($"  equals = {color.Equals(calibColor)}");
                    }
                    else
                    {
                        Robot.Print("color == null");
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
                if (Robot.IsInitialized)
                {
                    Robot.HaltMotors();
                }
                Robot.DisposeComponents();
                Robot.PrintEmptyLine();
                Robot.Print(Constants.PROGRAM_FINISHED_MSG);
                Thread.Sleep(Constants.PROGRAM_ABORTION_DELAY);
            }
        }

        private static void Wait(string outputText)
        {
            bool continueWithCompetition = false;

            ButtonEvents btnEvents = new ButtonEvents();

            Action btnAction = () => {
                continueWithCompetition = true;
            };

            btnEvents.EnterPressed += btnAction;

            Robot.PrintEmptyLine();
            Robot.Print(outputText);

            while (!continueWithCompetition)
            {
                Thread.Sleep(Constants.SAMPLING_RATE);
            }

            btnEvents.EnterPressed -= btnAction;
        }
    }
}
