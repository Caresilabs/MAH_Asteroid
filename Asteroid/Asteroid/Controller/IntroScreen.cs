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
    public class IntroScene : Screen
    {
        private const float textTime = 2.65f;

        private int state;
        private float time;

        private String[] introTexts = {
           "RETRO ALPHA MEGA DEMOLITION by Simon Bothen",
           "In a galaxy far far away",
           "A ship called Ben, was faced",
           "With terrible fate, indeed",
           "Under your command, he is",
           "'WASD keys', will steer",
           "Shoot with mouse, he will",
           "Good luck my master!"
        };

        public override void init()
        {
            this.state = 0;
        }

        public override void update(float delta)
        {
            time += delta;

            if (time > textTime)
            {
                time = 0;
                state++;
                if (state >= introTexts.Length)
                    setScreen(new GameScreen());
            }
        }

        public override void draw(SpriteBatch batch)
        {
            getGraphics().Clear(Color.Black);

            batch.Begin();

            // Draw story text
            batch.DrawString(Assets.font, introTexts[state],
                new Vector2(
                    getGraphics().Viewport.Width / 2 - Assets.font.MeasureString(introTexts[state]).Length() / 2,
                    getGraphics().Viewport.Height / 2 - 32), Color.White);

            batch.End();
        }

        public override void dispose()
        {
        }
    }
}
