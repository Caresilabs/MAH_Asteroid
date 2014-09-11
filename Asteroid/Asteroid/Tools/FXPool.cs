using Asteroid.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroid.Tools
{
    class FXPool : Pool<Particle>
    {
        public override Particle newObject()
        {
            return new Particle();
        }
    }
}
