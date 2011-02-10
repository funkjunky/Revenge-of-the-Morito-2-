using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Morito.Screens
{
    class LevelEditorScreen : GameScreen
    {
        public override void LoadContent()
        {
            base.LoadContent();
            Texture2D cTex = content.Load<Texture2D>(@"Textures/cursor");
            ScreenManager.Cursor.SetCursor(cTex);
            ScreenManager.Cursor.Visible = true;
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                              bool coveredByOtherScreen)
       {
           base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            /*for (int i = 0; i < Level.Pieces; i++)
            {
                for (int j = i; j < Level.Pieces; j++)
                {
                    
                }
            }*/
           MoritoFighterGame.MoritoFighterGameInstance.DisplayedMessages["asteroidPos"] = "First asteroid Pos: " + Level.Pieces[4].Position2D;
       }
    }
}
