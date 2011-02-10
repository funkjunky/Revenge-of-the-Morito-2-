using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Morito.ScreenManager;

namespace Morito.Classes
{
    class MouseHandler
    {
        protected Vector2 _position;
        protected Texture2D _tex;
        protected Color _tColour;
        protected MouseState _mouseState;

        public MouseHandler(Vector2 pos, Texture2D tex, Color TransparentColour)
        {
            _position = pos;
            _tex = tex;
            _tColour = TransparentColour;
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 Position3DSpace
        {
            get
            {
                return Camera.Relative2Dto3D(Position);
            }
        }

        public void Update()
        {
            _mouseState = Mouse.GetState();

            _position.X = _mouseState.X;    //update the positions.
            _position.Y = _mouseState.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_tex,_position,Color.White);
            spriteBatch.End();
        }
    }
}
