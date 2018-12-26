using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGLab1
{
    public class PendulumState
    {
        public int ThreadLength;
        public double ThreadRotation; // колеблется от -А до А
        public double PendulumRotation; // меняется на 360 градусов

        public PendulumState(int threadLength, double threadRotation, double pendulumRotation)
        {
            ThreadLength = threadLength;
            ThreadRotation = threadRotation;
            PendulumRotation = pendulumRotation;
        }
    }
}
