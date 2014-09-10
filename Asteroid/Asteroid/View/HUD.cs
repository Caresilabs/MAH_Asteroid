using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroid.Controller;
using Asteroid.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid.View
{
    public class HUD
    {
        private World world;
        private GameScreen screen;

        public HUD(GameScreen game)
        {
            this.world = game.getWorld();
            this.screen = game;
        }

        public void draw(SpriteBatch batch)
        {
            batch.DrawString(Assets.font, world.getPlayer().getPosition() + "", new Vector2(10, 10), Color.White);
        }
    }
}
