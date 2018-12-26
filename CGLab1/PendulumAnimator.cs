using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGLab1
{
    public static class PendulumAnimator
    {
        public static IEnumerable<PendulumState> EnumeratePendulumStates(double speed)
        {
            var curState = new PendulumState(200, 0, 0);

            yield return curState;

            var angleAmplitude = Math.PI * 2 / 3;

            var threadRotationDelta = Math.PI / 20.0;
            var pendulumRotationDelta = Math.PI / 30.0;

            while (curState.ThreadRotation < angleAmplitude)
            {
                curState.ThreadRotation += speed * threadRotationDelta;
                curState.PendulumRotation += speed * pendulumRotationDelta;
                yield return curState;
            }
        }
    }
}
