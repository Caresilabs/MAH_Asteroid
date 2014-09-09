using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroid.Entity
{
    public class Player : GameEntity
    {
        public Player(float x, float y) : base(x, y)
        {

        }

        public override void update(float delta)
        {
            throw new NotImplementedException();
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            throw new NotImplementedException();
        }
    }
}
