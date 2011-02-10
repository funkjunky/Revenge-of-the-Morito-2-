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
    class Background
    {
        Texture2D Texture;
        int _intScreenWidth;
        int _intScreenHeight;

        public Background(Texture2D txTexture)
        {
            Texture = txTexture;
        }
        public Background(Texture2D txTexture,float screenWidth,float screenHeight)
        {
            Texture = txTexture;
            this._intScreenWidth = (int)screenWidth;
            this._intScreenHeight = (int)screenHeight;


        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, new Rectangle(0, 0, _intScreenWidth, _intScreenHeight), Color.LightGray);
        }

    }
}
