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
        }

        public override void update(float delta)
        {
            input.update(delta);

            switch(state) {
                case GameState.PAUSED:
                    // Press "anykey" to start
                    if (Keyboard.GetState().GetPressedKeys().Length > 0 || Mouse.GetState().LeftButton == ButtonState.Pressed)
                        setState(GameState.RUNNING);
                    break;
                case GameState.RUNNING:
                     world.update(delta);
                     updatePlayer(delta);

                    // Check if player is dead
                    if (world.getPlayer().isEntityAlive() == false) {
                        setState(GameState.GAMEOVER);
                    }
                    break;
                case GameState.GAMEOVER:
                    // Press "anykey" to restart
                    if (Keyboard.GetState().GetPressedKeys().Length > 0 || Mouse.GetState().LeftButton == ButtonState.Pressed)
                        setState(GameState.RUNNING);
                    break;
            }
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

        public void setState(GameState state) {
            this.state = state;
        }
    }
}
