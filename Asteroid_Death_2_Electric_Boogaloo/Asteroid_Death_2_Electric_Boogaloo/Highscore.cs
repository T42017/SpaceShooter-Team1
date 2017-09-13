using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using System.IO;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    class HighScore
    {
        public string[] PlayerName;
        public int[] Score;
        public int Count;

        public HighScore(int count)
        {
            PlayerName = new string[count];
            Score = new int[count];

            Count = count;
        }

        /* More score variables */
        HighScore data;

        public string HighScoresFilename = "highscores.dat";
        int PlayerScore = 0;

        string scoreboard;

        // String for get name
        string cmdString = "Enter your player name and press Enter";

        // String we are going to display – initially an empty string
        string messageString = "";


        protected void Initialize()
        {
            // Get the path of the save game
            string fullpath = "highscores.dat";
            // Check to see if the save exists

            if (!File.Exists(fullpath))
            {
                //If the file doesn't exist, make a fake one...
                // Create the data to save
                data = new HighScore(5);
                data.PlayerName[0] = "neil";
                data.Score[0] = 2000;

                data.PlayerName[1] = "shawn";
                data.Score[1] = 1800;

                data.PlayerName[2] = "mark";
                data.Score[2] = 1500;

                data.PlayerName[3] = "cindy";
                data.Score[3] = 1000;

                data.PlayerName[4] = "sam";
                data.Score[4] = 500;

                SaveHighScores(data, HighScoresFilename, device);
            }
        }
        
        base.Initialize();


#endif

//ska till asteroidsgame
            /* Save highscores */
            public static void SaveHighScores(HighScore data, string filename, StorageDevice device)
            {
                // Get the path of the save game
                string fullpath = "highscores.dat";

#if WINDOWS
                // Open the file, creating it if necessary
                FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate);
                try
                {
                    // Convert the object to XML data and put it in the stream
                    XmlSerializer serializer = new XmlSerializer(typeof(HighScore));
                    serializer.Serialize(stream, data);
                }
                finally
                {
                    // Close the file
                    stream.Close();
                }
            }


            /* Load highscores */
            public static void LoadHighScores(string filename)
            {
                HighScore data;
                // Get the path of the save game
                fullpath = "highscores.dat";

#if WINDOWS

                // Open the file
                FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate, FileAccess.Read);
                try
                {
                    // Read the data from the file
                    XmlSerializer serializer = new XmlSerializer(typeof(HighScore));
                    data = (HighScore) serializer.Deserialize(stream);
                }
                finally

                {
                    // Close the file
                    stream.Close();
                }

#endif
            }

            /* Save player highscore when game ends */
            private void SaveHighScore()
            {
                // Create the data to saved
                HighScore data = LoadHighScores(HighScoresFilename);
                int scoreIndex = -1;
                for (int i = data.Count - 1; i > -1; i--)
                {
                    if (score.getScore() >= data.Score[i])
                    {
                        scoreIndex = i;
                    }
                }

                if (scoreIndex > -1)
                {
                    //New high score found ... do swaps
                    for (int i = data.Count - 1; i > scoreIndex; i--)
                    {
                        data.PlayerName[i] = data.PlayerName[i - 1];
                        data.Score[i] = data.Score[i - 1];
                    }

                    data.PlayerName[scoreIndex] = PlayerName; //Retrieve User Name Here
                    data.Score[scoreIndex] = score.getScore(); // Retrieve score here

                    SaveHighScores(data, HighScoresFilename, device);
                }
            }

            /* Iterate through data if highscore is called and make the string to be saved*/
            public string makeHighScoreString()
            {
                // Create the data to save
                HighScore data2 = LoadHighScores(HighScoresFilename);

                // Create scoreBoardString
                string scoreBoardString = "Highscores:\n\n";

                for (int i = 0; i < 5; i++) // this part was missing (5 means how many in the list/array/Counter)
                {
                    scoreBoardString = scoreBoardString + data2.PlayerName[i] + "-" + data2.Score[i] + "\n";
                }
                return scoreBoardString;
            }
        }
    }