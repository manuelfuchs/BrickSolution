using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoBrickTest
{
    public interface IRobot
    {
        void Drive(sbyte speed, Func<object[], bool> breakCondition = null, object[] parameter = null);
        object[] GenerateParameter(params object[] parameter);
        void RotateClockWise(sbyte speed, Func<object[], bool> breakCondition);
        void RotateCounterClockWise(sbyte speed, Func<object[], bool> breakCondition);
        void HaltWheels();
        bool IRBreakCondition(object[] parameter);
    }
}
