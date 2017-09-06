using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Death_2_Electric_Boogaloo
{
    public class Camera
    {
        protected float _zoom; // Camera Zoom
        public Matrix Transform; // Matrix Transform
        private Vector2 _pos; // Camera Position
        private float _rotation; // Camera Rotation

        public Camera()
        {
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
        }

        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }
        
        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public void FollowPlayer(Player player)
        {
            Pos = player.Position;
            Rotation = -player.Rotation + Physic.DegreesToRadians(-90);
        }

        public Matrix get_transformation(GraphicsDevice graphicsDevice, int windowWidth, int windowHeight)
        {
            Transform =       // Thanks to o KB o for this solution
                Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(windowWidth * 0.5f, windowHeight * 0.5f, 0));
            return Transform;
        }

        public float CalculatZoomFromWindowSize(int windowWidth, int windowHeight)
        {
            float zoom = 0f;
            if (windowHeight < windowWidth / 16 * 9)
                zoom = 1280f / windowWidth;
            else
                zoom = 720f / windowHeight;

            return zoom * -1 + 2;
        }

    }
}
