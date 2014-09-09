using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroid.Entity;
using Microsoft.Xna.Framework.Input;
using Asteroid.Tools;

namespace Asteroid
{
    public class Input
    {
        private GameEntity entity;

        public Input(GameEntity controlEntity)
        {
            this.entity = controlEntity;
        }

        public void update(float delta)
        {
            // Vertical
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                float y = (float)Math.Sin(-entity.getRotation() + Math.PI / 2);
                float x = (float)Math.Cos(-entity.getRotation() + Math.PI / 2);

                entity.addAcceleration(x * entity.getSpeed(), y * -entity.getSpeed());
            } else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                float y = (float)Math.Sin(-entity.getRotation() + Math.PI / 2);
                float x = (float)Math.Cos(-entity.getRotation() + Math.PI / 2);

                entity.addAcceleration(x * entity.getSpeed(), y * entity.getSpeed());
            }
            else
            {
                entity.setAcceleration(0, 0);
            }


            // Horizontal
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                entity.rotate(-delta);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                entity.rotate(delta);
            }
        }
    }
}
