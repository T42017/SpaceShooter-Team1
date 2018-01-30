using System;
using System.Diagnostics;
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
        public static string FileLocation { get; } = "HighScoreList.xml";
        public static int MaxPlayers { get; } = 10;
        public static IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
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
            IsolatedStorageFileStream stream1 = new IsolatedStorageFileStream(FileLocation, FileMode.OpenOrCreate, file);
            xDoc.Save(stream1);
            stream1.Flush();
            stream1.Dispose();
        }
        public static void Create(IsolatedStorageFileStream stream)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode root = xDoc.CreateElement("root");
            XmlNode playerNode = xDoc.CreateElement("Player");
            playerNode.InnerText = "Player";
            XmlNode scoreNode = xDoc.CreateElement("Score");
            scoreNode.InnerText = "0";
            xDoc.AppendChild(root);
            root.AppendChild(playerNode);
            root.AppendChild(scoreNode);
            xDoc.Save(stream);
            stream.Flush();
            Debug.WriteLine("papas" +
                            "åäfkmalsdjkgaslö,");
        }
        #endregion

        #region Private static methods
        private static XmlDocument GetXmlDoc()
        {
            IsolatedStorageFileStream stream = null;
            var xDoc = new XmlDocument();
            if (!file.FileExists(FileLocation))
            {
                stream = new IsolatedStorageFileStream(FileLocation, FileMode.OpenOrCreate, file);
                Create(stream);
                stream.Flush();
                stream.Dispose();

            }
            
            stream = new IsolatedStorageFileStream(FileLocation, FileMode.OpenOrCreate, file);
            xDoc.Load(stream);
            stream.Flush();
            stream.Dispose();
            return xDoc;
        }   
        #endregion
    }
}