using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroid.Entity;
using Asteroid.Model;
using Asteroid.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroid.Controller
{
    public class GameScreen : Screen
    {
        private World world;
        private WorldRenderer renderer;
        private Input input;
        private HUD hud;
        private GameState state;

        private float stateTime;

        public enum GameState
        {
            PAUSED, RUNNING, GAMEOVER
        }

        public override void init()
        {
            this.world = new World();
            this.renderer = new WorldRenderer(world);
            this.hud = new HUD(this);
            this.input = new Input(this, world.getPlayer());
            this.state = GameState.PAUSED;
            this.stateTime = 0;
        }

        public override void update(float delta)
        {
            stateTime += delta;

            switch(state) {
                case GameState.PAUSED:
                    // Press "anykey" to start
                    if (Keyboard.GetState().GetPressedKeys().Length > 0 || Mouse.GetState().LeftButton == ButtonState.Pressed)
                        setState(GameState.RUNNING);
                    break;
                case GameState.RUNNING:
                     input.update(delta);
                     world.update(delta);
                     updatePlayer(delta);

                    // Check if player is dead
                    if (world.getPlayer().isEntityAlive() == false) {
                        setState(GameState.GAMEOVER);
                    }
                    break;
                case GameState.GAMEOVER:
                    world.update(delta);

                    // Press "anykey" to restart
                    if (stateTime >= 3 && (Keyboard.GetState().GetPressedKeys().Length > 0 || Mouse.GetState().LeftButton == ButtonState.Pressed))
                        init();
                    break;
            }
        }

        private void updatePlayer(float delta)
        {
            // TODO follow player
            renderer.getCamera().setPosition(
                world.getPlayer().getPosition().X + world.getPlayer().getBounds().Width/2,
                world.getPlayer().getPosition().Y + world.getPlayer().getBounds().Height / 2
            );
        }

        public override void draw(SpriteBatch batch)
        {
            getGraphics().Clear(Color.Magenta);

            // Draw game
            renderer.render(batch, getGraphics());

            //Draw HUD
            batch.Begin();
            hud.draw(batch);
            batch.End();
           
        }

        public override void dispose()
        {

        }

        public World getWorld()
        {
            return world;
        }

        public GameState getState()
        {
            return state;
        }

        public float getStateTime()
        {
            return stateTime;
        }

        public void setState(GameState state) {
            this.state = state;
            this.stateTime = 0;
        }
    }
}
