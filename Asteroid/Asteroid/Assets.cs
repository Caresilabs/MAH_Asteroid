using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid
{
    public class Assets
    {
        private static Dictionary<String, Texture2D> textures;
        private static ContentManager manager;

        public static SpriteFont font;

        public static void load(ContentManager manager) {
            Assets.manager = manager;
            textures = new Dictionary<string, Texture2D>();

            // Load our assets
            loadTexture("Graphics/player");
            loadTexture("Graphics/astroid");
            loadTexture("Graphics/stars");
            loadTexture("Graphics/pixel");
            loadTexture("Graphics/pointer");

            // Load font 
            font = manager.Load<SpriteFont>("Font/font");
        }

        private static void loadTexture(string path) {
            textures.Add(path, manager.Load<Texture2D>(path));
        }

        public static Texture2D getTexture(string path) {
            return textures[path];
        }

        internal static void unload()
        {
            manager.Dispose();
        }
    }
}
