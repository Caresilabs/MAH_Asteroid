using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroid.Entity;
using Microsoft.Xna.Framework.Input;

namespace Asteroid
{
    public class Input
    {
        private GameEntity entity;

        public Input(GameEntity controlEntity) {
            this.entity = controlEntity;
        }

        public void update(float delta)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Console.WriteLine("yooo");
            }
        }
    }
}
