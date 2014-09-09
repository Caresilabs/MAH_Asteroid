using Asteroid.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroid.Tools
{
    class BulletPool : Pool<Bullet>
    {
        public override Bullet newObject()
        {
            return new Bullet(); // TODO
        }
    }
}
