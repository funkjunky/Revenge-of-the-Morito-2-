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
    class Bullet
    {
        private Texture2D txTexture;
        private Vector2 v2dPosition = Vector2.Zero;
        private Vector2 v2dSpeed = new Vector2(-50, 0);

        public Vector2 Position
        {
            get { return v2dPosition; }
            set { v2dPosition = value; }
        }

        public Vector2 Speed
        {
            get { return v2dSpeed; }
            set { v2dSpeed = value; }
        }

        public void Update(GameTime gameTime)
        {
            v2dPosition -= v2dSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
             spriteBatch.Draw(txTexture, v2dPosition, Color.White);
          }

        public Bullet(Texture2D texture)
        {
            this.txTexture = texture;
        } 
    }
}
