using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Morito.ScreenManager;

namespace Morito.Screens
{
    class DebugScreen : Screen
    {
        public override void Draw(GameTime gametime)
        {
            ScreenManager.SpriteBatch.Begin();
            string[] temp = new string[100]; ScreenManager.DisplayedMessages.Values.CopyTo(temp, 0);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, string.Join("\n", temp), new Vector2(0.0f, 0.0f), Color.White, 0.0f, new Vector2(0.0f, 0.0f), 1.0f, SpriteEffects.None, 0);
            ScreenManager.SpriteBatch.End();
        }
    }
}