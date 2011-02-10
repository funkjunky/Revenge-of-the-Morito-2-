using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Morito
{
    public class CombatText
    {
        #region private variables
        private Vector2 _position;                  // Where the event occured
        private Vector2 _origin;                    // Origin vector
        private Color _combatColor;                 // Damage = red, Shields = blue, Weps = green
        private string _text;                       // can represent damage, health, or a weapon name
        private float _scale;                       // effect value
        private int _expireTime;                    // the game time. used for removing CombatText entries
        private SpriteFont _font;                   // font... duh
        private Camera camRef;

        #endregion

        #region mutators
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        public Color CombatColor
        {
            get { return _combatColor; }
            set { _combatColor = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public SpriteFont Font
        {
            get { return _font; }
            set { _font = value; }
        }

        public int ExpireTime
        {
            get { return _expireTime; }
            set { _expireTime = value + 2; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for damage 2d
        /// </summary>
        public CombatText(bool isGood, float amount, GameTime gameTime, Vector2 pos, Screens.GameplayScreen screen, Camera cam)
        {
            camRef = cam;
            CombatColor = isGood ? Color.Blue : Color.Red;
            Text = amount.ToString();
            ExpireTime = gameTime.TotalGameTime.Seconds;
            Position = camRef.Relative3Dto2D(pos);
            Font = screen.ScreenManager.Game.Content.Load<SpriteFont>("Fonts\\monofont");
            Origin = new Vector2(0, 0);

        }

        /// <summary>
        /// Constructor for name 2d
        /// </summary>
        public CombatText(string name, GameTime gameTime, Vector2 pos, Screens.GameplayScreen screen, Camera cam)
        {
            camRef = cam;
            CombatColor = Color.Green;
            Text = name;
            ExpireTime = gameTime.TotalGameTime.Seconds;
            Position = camRef.Relative3Dto2D(pos);
            Font = screen.ScreenManager.Game.Content.Load<SpriteFont>("Fonts\\monofont");
            Origin = new Vector2(0, 0);
        }

        /// <summary>
        /// Constructor for damage 3d
        /// </summary>
        public CombatText(bool isGood, float amount, GameTime gameTime, Vector3 pos, Screens.GameplayScreen screen, Camera cam)
        {
            System.Console.WriteLine("Creating Combat Text");
            camRef = cam;
            CombatColor = isGood ? Color.Blue : Color.Red;
            Text = amount.ToString();
            ExpireTime = gameTime.TotalGameTime.Seconds;
            Position = new Vector2(pos.X, pos.Y);
            Position = camRef.Relative3Dto2D(Position);
            System.Console.WriteLine(Position.ToString());
            Font = screen.ScreenManager.Game.Content.Load<SpriteFont>("Fonts\\monofont");
            Origin = new Vector2(0, 0);
        }

        /// <summary>
        /// Constructor for name 3d
        /// </summary>
        public CombatText(string name, GameTime gameTime, Vector3 pos, Screens.GameplayScreen screen, Camera cam)
        {
            camRef = cam;
            CombatColor = Color.Green;
            Text = name;
            ExpireTime = gameTime.TotalGameTime.Seconds;
            Position = new Vector2(pos.X, pos.Y);
            Position = camRef.Relative3Dto2D(Position);
            Font = screen.ScreenManager.Game.Content.Load<SpriteFont>("Fonts\\monofont");
            Origin = new Vector2(0, 0);
        }
        #endregion

        #region Private Methods
        #endregion

        #region Update and Draw
        public void Draw(Morito.Screens.GameplayScreen screen, GameTime gameTime)
        {
            SpriteBatch spriteBatch = screen.ScreenManager.SpriteBatch;
            double time = gameTime.TotalGameTime.TotalSeconds;
            _position.Y--;


            spriteBatch.Begin();
            spriteBatch.DrawString(Font, Text, Position, CombatColor, 0,
                Origin, (float)1.2, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        #endregion
    }
}
