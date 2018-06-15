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

                for (int i = 0; i < 10; i++)
                {
                    Robot.Print("press button:");

                    Wait();
                    FullColor color = Robot.GetFullColor();

                    if (color != null)
                    {
                        Robot.Print("  " + color.ToString());
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

        private static void Wait()
        {
            bool continueWithCompetition = false;

            ButtonEvents btnEvents = new ButtonEvents();

            Action btnAction = () => {
                continueWithCompetition = true;
            };

            btnEvents.EnterPressed += btnAction;

            Robot.PrintEmptyLine();
            Robot.Print("press middle button to read");
            Robot.Print("a colour");

            while (!continueWithCompetition)
            {
                Thread.Sleep(Constants.SAMPLING_RATE);
            }

            btnEvents.EnterPressed -= btnAction;
        }
    }
}
