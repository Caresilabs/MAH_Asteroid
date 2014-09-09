using Asteroid.Physic;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroid.Entity
{
    public class Asteroid : GameEntity
    {

        public enum AsteroidState
        {
            BIG, SMALL, DEAD
        }

        public Asteroid(Texture2D texture, float x, float y) : base(texture, x, y, 2, 2)
        {

        }

        public override void update(float delta)
        {

            Physics.processCollision(this);
        }

        public override void collide(GameEntity entity)
        {
        }
    }
}
