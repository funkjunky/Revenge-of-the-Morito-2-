using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Morito
{
    public class VisualObject2D : Microsoft.Xna.Framework.GameComponent, is2DDrawable, hasPosition2D
    {
        #region Constructors
            public VisualObject2D()
                : base(MoritoFighterGame.MoritoFighterGameInstance)
            { }
        #endregion
        #region Member Variables
            protected Texture2D     _texture;                               //sprite texture
            protected Vector2       _position;                              //sprite position on screen
            protected float         _rotation;                              //rotation of the object in radians
            protected Vector2       _origin;                                //spinning origin
            protected float         _scale = 1.0f;                          //normal scale is 1.0f   
            protected SpriteEffects _spriteEffect = SpriteEffects.None;    //holds whether or not the sprite should be flipped. This is here simply for completeness.
        #endregion
        #region is2DDrawable Members
            public float Scale
            {
                get { return _scale; }
                set { _scale = value; }
            }

            public Texture2D Texture
            {
                get { return _texture; }
                set {
                    _texture = value;
                    //set the origin to be the centre of the new texture.
                    //The textures origin is meaningless after changing the texture anyways.
                    //If I'm wrong then go ahead and take this out ;), 
                    //but make sure allobjects have their origin set then.
                    Origin = new Vector2(value.Width / 2, value.Height / 2);
                }
            }

            public Vector2 Position
            {
                get { return _position; }
                set { _position = value; }
            }

            public Vector2 Position2D
            {
                get { return Position; }
                set { Position = value; }
            }

            public float RotationAngle
            {
                get { return _rotation; }
                set { _rotation = value; }
            }

            public Vector2 Origin
            {
                get { return _origin; }
                set { _origin = value; }
            }

            public SpriteEffects SpriteEffect
            {
                get { return _spriteEffect; }
                set { _spriteEffect = value; }
            }
        #endregion
        #region Public Methods
            public virtual void Draw()
            {
                MoritoFighterGame.MoritoFighterGameInstance.SpriteBatchDrawable.Begin();
                MoritoFighterGame.MoritoFighterGameInstance.SpriteBatchDrawable.Draw(Texture, Position, null, Color.White, RotationAngle, Origin, Scale, SpriteEffect, 0f);
                MoritoFighterGame.MoritoFighterGameInstance.SpriteBatchDrawable.End();
            }
        #endregion
    }
}
