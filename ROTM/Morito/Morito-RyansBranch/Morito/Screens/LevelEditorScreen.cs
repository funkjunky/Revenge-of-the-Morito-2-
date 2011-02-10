using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Morito;

namespace Morito.Screens
{
    class LevelEditorScreen : GameScreen
    {
        private VisualObject3D objectGrabbed;
        public  List<CollectionBox> Menus { get; set; }

        public override void LoadContent()
        {
            base.LoadContent();
            Texture2D cTex = content.Load<Texture2D>(@"Textures/cursor");
            Texture2D mTex = content.Load<Texture2D>(@"Textures/menuBG");
            
            ScreenManager.Cursor.SetCursor(cTex);
            ScreenManager.Cursor.Visible = true;

            Menus = new List<CollectionBox>();
            Menus.Add(new CollectionBox(mTex, new Rectangle(100,100,40,100)));
        }

        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            //is an object being grabbed?
            if(input.Mouse.IsNewLeftMouseClick)
            {
                int count = 0;
                float mx = Camera1.Relative2Dto3D(input.Mouse.Position).X;
                float my = Camera1.Relative2Dto3D(input.Mouse.Position).Y;
                foreach (var piece in Level.Pieces)
                {
                    float oRadius = piece.ObjectModel.Meshes[0].BoundingSphere.Radius;
                    
                    if(mx < piece.Position.X + oRadius 
                        && mx > piece.Position.X - oRadius
                        && my < piece.Position.Y + oRadius
                        && my > piece.Position.Y - oRadius)
                    {
                        ++count;
                        objectGrabbed = piece;
                    }
                }
                DisplayedMessages["levelEditor"] = "clicking on " + count + " number of objects. @ cur pos: " 
                                                    + mx + ", " + my;
            }

            //is an object being released?
            if(input.Mouse.IsNewLeftMouseRelease)
                objectGrabbed = null;

            //stick the object to the cursor if it's currently grabbed.
            if(objectGrabbed != null && input.Mouse.LeftMouseDown)
            {
                objectGrabbed.Position = input.Mouse.Position3DSpace;
            }
        }

        //drawing... hmmm how do i do menus.
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (CollectionBox collectionBox in Menus)
            {
                collectionBox.Draw(SpriteBatchDrawable);
            }
        }
    }
}
