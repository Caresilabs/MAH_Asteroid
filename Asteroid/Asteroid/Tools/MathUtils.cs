using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroid.Tools
{

    public class MathUtils
    {
        public static Random rnd = new Random();

        public static float random(float max)
        {
            return (float)rnd.NextDouble() * max;
        }

        public static float random(float min, float max)
        {
            return min + (float)rnd.NextDouble() * (max - min);
        }

        // todo
        public static float atan(float degree)
        {
            return (float)Math.Atan((double)degree);
        }
    }
}
