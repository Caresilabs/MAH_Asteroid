using Asteroid.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroid.Physic
{
    public interface ICollidable
    {
        void collide(GameEntity entity);
    }
}
