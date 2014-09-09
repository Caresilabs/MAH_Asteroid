using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroid.Entity;
using Asteroid.Model;
using Asteroid.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid.Controller
{
    public class GameScreen : Screen
    {
        private World world;
        private WorldRenderer renderer;
        private Input input;

        public override void init()
        {
            this.world = new World();
            this.renderer = new WorldRenderer(world);
            this.input = new Input(world.getPlayer());
        }

        public override void update(float delta)
        {
            input.update(delta);

            world.update(delta);

            updatePlayer(delta);
        }

        private void updatePlayer(float delta)
        {
            // TODO follow player
            renderer.getCamera().setPosition(world.getPlayer().getPosition());
        }

        public override void draw(SpriteBatch batch)
        {
            getGraphics().Clear(Color.Magenta);

            // Draw game
            renderer.render(batch, getGraphics());

            //Draw HUD
            batch.Begin();

            batch.End();
           
        }

        public override void dispose()
        {

        }

        
    }
}
