using BrickSolution.Logic.Enumerations;
using MonoBrickFirmware.Sensors;
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
                Wait("press the middle button to calibrate");
                var foodstoneColour = Robot.GetRGBColor();

                for (int i = 0; i < 5; i++)
                {
                    Wait("press to check food");
                    var currentColour = Robot.GetRGBColor();

                    if (ColourMatchesWithTolerance(foodstoneColour, currentColour))
                    {
                        Robot.Print("This is our foodstone!");
                    }
                    else
                    {
                        Robot.Print("Foodstone is not for us!");
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

        private static bool ColourMatchesWithTolerance(RGBColor foodstoneColour,
                                                       RGBColor currentColour)
        {
            return (foodstoneColour.Red - Constants.COLOUR_TOLERANCE < currentColour.Red
                    && currentColour.Red < foodstoneColour.Red + Constants.COLOUR_TOLERANCE)
                && (foodstoneColour.Green - Constants.COLOUR_TOLERANCE < currentColour.Green
                    && currentColour.Green < foodstoneColour.Green + Constants.COLOUR_TOLERANCE)
                && (foodstoneColour.Blue - Constants.COLOUR_TOLERANCE < currentColour.Blue
                    && currentColour.Blue < foodstoneColour.Blue + Constants.COLOUR_TOLERANCE);
        }

        private static void Wait(string msg)
        {
            bool continueWithCompetition = false;

            ButtonEvents btnEvents = new ButtonEvents();

            Action btnAction = () => {
                continueWithCompetition = true;
            };

            btnEvents.EnterPressed += btnAction;

            Robot.PrintEmptyLine();
            Robot.Print(msg);

            while (!continueWithCompetition)
            {
                Thread.Sleep(Constants.SAMPLING_RATE);
            }

            btnEvents.EnterPressed -= btnAction;
        }
    }
}
