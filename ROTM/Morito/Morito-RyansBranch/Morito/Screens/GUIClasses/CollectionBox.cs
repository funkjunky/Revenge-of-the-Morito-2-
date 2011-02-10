using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Morito.ScreenManager;
using Morito.Classes;

namespace Morito
{
    class CollectionBox
    {
        #region Properties
        public Texture2D BackgroundTexture { get; set; }
        public Rectangle PlacementBox { get; set; }
        #endregion

        #region Initialization
        public CollectionBox(Texture2D texture2D, Rectangle placementBox)
        {
            BackgroundTexture = texture2D;
            PlacementBox = placementBox;
        }
        #endregion

        #region Public Methods
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(BackgroundTexture, PlacementBox, Color.White);
            spriteBatch.End();
        }
        public void Update(GameTime gameTime)
        {
        }
        #endregion
    }
}
