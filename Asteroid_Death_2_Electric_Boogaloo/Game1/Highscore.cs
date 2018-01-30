using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace Game1
{
    public class HighScore
    {
        #region Public static properties
        public static string FileLocation { get; } = "Highscore.xml";
        public static int MaxPlayers { get; } = 10;
        #endregion

        #region Public static methods
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
                    if (score >= childScore)
                    {
                        break;
                    }
                    refNode = rootChildList[i];
                }
            }

            XmlNode playerNode = xDoc.CreateElement("Player");
            playerNode.InnerText = playerName;
            XmlNode scoreNode = xDoc.CreateElement("Score");
            scoreNode.InnerText = score + "";

            root.InsertAfter(playerNode, refNode);
            root.InsertAfter(scoreNode, playerNode);

            if (root.ChildNodes.Count > MaxPlayers * 2)
            {
                rootChildList[root.ChildNodes.Count - 1].ParentNode.RemoveChild(rootChildList[root.ChildNodes.Count - 1]);
                rootChildList[root.ChildNodes.Count - 1].ParentNode.RemoveChild(rootChildList[root.ChildNodes.Count - 1]);
            }
            xDoc.Save(new IsolatedStorageFileStream(FileLocation,FileMode.OpenOrCreate,IsolatedStorageFile.GetUserStoreForApplication()));
        }
        #endregion

        #region Private static methods
        private static XmlDocument GetXmlDoc()
        {
            var xDoc = new XmlDocument();        
            //xDoc.Load(File.OpenRead(FileLocation));
            return xDoc;
        }   
        #endregion
    }
}