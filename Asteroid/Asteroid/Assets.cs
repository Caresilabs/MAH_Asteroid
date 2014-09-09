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
        public static Dictionary<String, Texture2D> textures;
        private static ContentManager manager;

        public static void load(ContentManager manager) {
            Assets.manager = manager;

            // Load our assets
            loadTexture("Graphics/gravel");
        }

        private static void loadTexture(string path) {
            textures.Add(path, manager.Load<Texture2D>(path));
        }

        internal static void unload()
        {
            manager.Dispose();
        }
    }
}
