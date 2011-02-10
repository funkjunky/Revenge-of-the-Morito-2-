using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Morito.Classes;

namespace Morito.Screens
{
    class LevelEditorScreen : GameScreen
    {
        protected MouseHandler _cursor;

        public override void LoadContent()
        {
            base.LoadContent();
            Texture2D cTex = content.Load<Texture2D>(@"Textures/cursor");
            Color transparentColour = Color.White;
            _cursor = new MouseHandler(new Vector2(0f, 0f), cTex, transparentColour);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                              bool coveredByOtherScreen)
       {
           base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
           MoritoFighterGame.MoritoFighterGameInstance.DisplayedMessages["cursorPos"] = "Cursor Pos: " + Camera.Relative2Dto3D(_cursor.Position);
           MoritoFighterGame.MoritoFighterGameInstance.DisplayedMessages["asteroidPos"] = "First asteroid Pos: " + Level.Pieces[4].Position2D;
           MoritoFighterGame.MoritoFighterGameInstance.DisplayedMessages["viewport height"] =
                "viewport height: " + MoritoFighterGame.MoritoFighterGameInstance.Graphics.GraphicsDevice.Viewport.Height;
            _cursor.Update();
       }

       public override void Draw(GameTime gameTime)
       {
           base.Draw(gameTime);
           _cursor.Draw(SpriteBatchDrawable);
       }
    }
}
