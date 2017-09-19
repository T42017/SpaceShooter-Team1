using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects;
using Asteroid_Death_2_Electric_Boogaloo.GameObjects.Powerups;
using Microsoft.Xna.Framework;

//namespace Asteroid_Death_2_Electric_Boogaloo.Factorys
//{
//    class Powerupfactory
//    {
//        private AsteroidsGame _game;

//        public Powerupfactory(AsteroidsGame game)
//        {
//            _game = game;
//        }

//        public Powerup GetRandomPowerup()
//        {
//            Powerup.Type powerupType = (Powerup.Type)Globals.RNG.Next(Enum.GetNames(typeof(Powerup.Type)).Length - 1);

//            Vector2 position = Vector2.Zero;
//            while (Vector2.Distance(position, _game.GameObjectManager.Player.Position) < 1000 || position.Equals(Vector2.Zero))
//                position = new Vector2(Globals.RNG.Next(_game.Level.SizeX - 1), Globals.RNG.Next(_game.Level.SizeY - 1));

//            Powerup powerup = new Powerup(_game, powerupType);
//            powerup.Position = position;

//            return powerup;
//        }
//    }
//}
