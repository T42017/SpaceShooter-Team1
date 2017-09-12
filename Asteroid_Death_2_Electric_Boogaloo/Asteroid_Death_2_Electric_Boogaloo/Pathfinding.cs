using System;
using System.Collections.Generic;
using System.Linq;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class Pathfinding
    {
        private int _distanceFromGameObjects = 100;
        public Pathfinding()
        {
            
        }

        public Vector2 GetNextPosition(List<Node> nodes, Vector2 GoToPoisition)
        {
            


            return Vector2.One;
        }

        public List<Node> ConvertGameObjectToNode(GameObject gameObject)
        {
            List<Node> nodes = new List<Node>();

            nodes.Add(new Node(new Vector2(gameObject.Position.X - _distanceFromGameObjects, gameObject.Position.Y)));
            nodes.Add(new Node(new Vector2(gameObject.Position.X + _distanceFromGameObjects, gameObject.Position.Y)));
            nodes.Add(new Node(new Vector2(gameObject.Position.X, gameObject.Position.Y + _distanceFromGameObjects)));
            nodes.Add(new Node(new Vector2(gameObject.Position.X, gameObject.Position.Y - _distanceFromGameObjects)));

            return nodes;
        }

        public class Node
        {
            public Vector2 Position;

            public Node(Vector2 position)
            {
                Position = position;
            }

        }

    }
}
