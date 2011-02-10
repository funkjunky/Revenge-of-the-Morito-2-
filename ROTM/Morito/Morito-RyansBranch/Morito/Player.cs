#region Using Declaration
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
#endregion Using Declaration


namespace Morito
{
    class Player
    {
        Ship playerShip;
        KeyboardState keyboard;
        public KeyboardState Keyboard
        {
            get { return keyboard; }
            set { keyboard = value; }
        }

        public Player(Texture2D txPlayerShip, Texture2D txBullet, WeaponTypes weaponTypes)
        {
            this.playerShip = new Ship(PlayerIndex.One, txPlayerShip, txBullet, weaponTypes);
            
        }

        public void Update(GameTime gameTime)
        {

            playerShip.Keyboard1 = keyboard;
            playerShip.Update(gameTime);
        }

        public void Draw(SpriteBatch sb)
        {
         //   sb.Draw(playerShip.ShipTexture, playerShip.Position, Color.White);
            playerShip.Draw(sb);
        }
    }
}
