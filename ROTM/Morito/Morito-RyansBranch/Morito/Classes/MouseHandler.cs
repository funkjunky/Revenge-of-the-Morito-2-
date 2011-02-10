using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Morito.ScreenManager;

namespace Morito.Classes
{
    public class MouseHandler
    {
        //public delegate void MouseClickEvent(MouseHandler mouse);
        //protected event MouseClickEvent _click;

        protected Vector2 _positionClickStarted;
        protected Vector2 _position;
        protected Texture2D _tex;
        protected Color _tColour;
        protected MouseState _mouseState;
        protected MouseState _oldMouseState;

        #region Initialization
        public  MouseHandler()
        {
            Visible = false;
            Position = Vector2.Zero;
        }


        public MouseHandler(Vector2 pos, Texture2D tex)
        {
            Visible = true;
            Position = pos;
            _tex = tex;
        }
        #endregion

        #region Properties
        public bool Visible
        {
            get; set;
        }

        protected bool drawable
        {
            get { return Visible && _tex != null; }
        }

        //public event MouseClickEvent Click
        //{
        //    add { _click += value; }
        //    remove { _click -= value; }
        //}

        public bool IsNewLeftMouseClick
        {
            get
            {
                _positionClickStarted = Position;
                return _oldMouseState.LeftButton == ButtonState.Released 
                    && _mouseState.LeftButton == ButtonState.Pressed;
            }
        }

        public bool IsNewLeftMouseRelease
        {
            get
            {
                return _oldMouseState.LeftButton == ButtonState.Pressed
                    && _mouseState.LeftButton == ButtonState.Released;
            }
        }

        public bool LeftMouseDown
        {
            get
            { return _mouseState.LeftButton == ButtonState.Pressed; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        #endregion

        #region Public Methods
        public Vector3 Position3DSpace
        {
            get
            {
                return MoritoFighterGame.MoritoFighterGameInstance.Camera1.Relative2Dto3D(Position);
            }
        }

        public void SetCursor(Texture2D tex)
        {
            _tex = tex;
        }

        public void Update()
        {
            _mouseState = Mouse.GetState();

            MoritoFighterGame.MoritoFighterGameInstance._screenManager.DisplayedMessages["mousestate"]
                = "The left mouse button: " + _mouseState.LeftButton;

            _position.X = _mouseState.X;    //update the positions.
            _position.Y = _mouseState.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(_tex, _position, Color.White);
                spriteBatch.End();
            }
        }
        #endregion

    }
}
