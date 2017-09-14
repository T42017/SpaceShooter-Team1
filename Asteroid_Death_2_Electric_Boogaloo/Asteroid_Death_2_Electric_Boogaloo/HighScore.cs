using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class HighScore
    {

        public static string FileLocation { get; private set; } = ".\\Content\\Highscore.xml";

        public static string[] GetHighScores()
        {
            XmlDocument xDoc = GetXmlDoc();

            XmlNodeList players = xDoc.GetElementsByTagName("Player");
            XmlNodeList score = xDoc.GetElementsByTagName("Score");
            
            string[] strings = new string[players.Count];

            for (int i = 0; i < strings.Length; i++)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(players[i].InnerText + " ").Append(score[i].InnerText);
                strings[i] = builder.ToString();
            }
            return strings;
        }

        public static void SaveScore(String playerName, long score)
        {
            XmlDocument xDoc = GetXmlDoc();
            XmlNode root = xDoc.GetElementsByTagName("root")[0];
            XmlNodeList rootChildList = root.ChildNodes;
            XmlNode refNode = null;
            
            for (int i = 0; i < rootChildList.Count; i++)
            {
                if (rootChildList[i].Name.Equals("Score"))
                {
                    long childScore = long.Parse(rootChildList[i].InnerText);
                    if (score <= childScore)
                    {
                        refNode = rootChildList[i];
                        break;
                    }
                }
            }

            XmlNode playerNode = xDoc.CreateElement("Player");
            playerNode.InnerText = playerName;

            XmlNode scoreNode = xDoc.CreateElement("Score");
            scoreNode.InnerText = score + "";

            root.InsertAfter(playerNode, refNode);
            root.InsertAfter(scoreNode, playerNode);
            
            xDoc.Save(FileLocation);
        }

        private static XmlDocument GetXmlDoc()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(FileLocation);
            return xDoc;
        }
        
    }
}
