using Asteroid.Physic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroid.Entity
{
    public class Player : GameEntity
    {
        public const float width = 62;
        public const float height = 62;

        public Player(float x, float y) : base(Assets.getTexture("Graphics/gravel"), x, y, width, height)
        {

        }

        public override void update(float delta)
        {
            base.update(delta);

            //update player
            Physics.processCollision(this);
        }

        public override void collide(GameEntity entity)
        {
            Console.WriteLine("collide" + entity);
        }
    }
}
