using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Asteroid.Tools
{
    public class SaveManager
    {
        public static void init() {

        }

        public static void saveHighscore(int highscore)
        {
            using (StreamWriter reader = new StreamWriter("Content/highscore.dat"))
            {
                reader.Write(highscore.ToString());
                reader.Close();
            }
        }

        public static int getHighscore()
        {
            string read = "";
            using (StreamReader reader = new StreamReader("Content/highscore.dat"))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    read += line;
                }

                reader.Close();
            }

            return Int32.Parse(read);
        }

    }
}
