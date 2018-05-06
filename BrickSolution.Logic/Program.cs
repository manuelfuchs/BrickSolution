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
                int fenceUsDistance = Robot.GetUltraSonicDistance();

                for (int i = 0; i < 5; i++)
                {
                    Wait("press to check for fence");
                    int currentUsDistance = Robot.GetUltraSonicDistance();

                    if (fenceUsDistance - Constants.FENCE_US_TOLERANCE < currentUsDistance
                        && currentUsDistance < fenceUsDistance + Constants.FENCE_US_TOLERANCE)
                    {
                        Robot.Print("This is a fence!");
                    }
                    else
                    {
                        Robot.Print("This is NOT a fence!");
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

        private static bool ColourMatchesWithTolerance(RGBColor colour1,
                                                       RGBColor colour2)
        {
            return (colour1.Red - Constants.COLOUR_TOLERANCE < colour2.Red
                    && colour2.Red < colour1.Red + Constants.COLOUR_TOLERANCE)
                && (colour1.Green - Constants.COLOUR_TOLERANCE < colour2.Green
                    && colour2.Green < colour1.Green + Constants.COLOUR_TOLERANCE)
                && (colour1.Blue - Constants.COLOUR_TOLERANCE < colour2.Blue
                    && colour2.Blue < colour1.Blue + Constants.COLOUR_TOLERANCE);
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
