using MonoBrickFirmware.Display;
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

                throw new NotImplementedException(nameof(NotImplementedException));
            }
            catch (Exception e)
            {
                if (Robot.IsInitialized)
                {
                    Robot.HaltTracks();
                }
                else
                {
                    Robot.Print(Constants.initializeErrorMessage);
                }

                Robot.Print(e.Message);
                Thread.Sleep(Constants.lcdErrorDuration);
            }
        }
    }
}
