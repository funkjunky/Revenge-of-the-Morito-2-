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
    /*
     * All the ship properties will go here
     * 
     * 
     */
    class Ship //: might want to add a item control system
    {

        PlayerIndex player;
        private Vector2 _v2dPosition = Vector2.Zero;
        private Vector2 _v2dSpeed = new Vector2(0, 450);
        private WeaponTypes _weaponType;
        private KeyboardState _keyboard;
        private Texture2D _txBulletTexture,_txShipTexture;
        private Bullet _bullet;
        private List<Bullet> _bulletList = new List<Bullet>();
        #region Properties
        public WeaponTypes WeaponTypes
        {
            get { return _weaponType; }
            set { _weaponType = value; }
        }

        public Texture2D ShipTexture
        {
            get { return _txShipTexture; }
            set { _txShipTexture = value; }
        }

        public Texture2D BulletTexture
        {
            get { return _txBulletTexture; }
            set { _txBulletTexture = value; }
        }

        public Vector2 Position
        {
            get { return _v2dPosition; }
            set { _v2dPosition = value; }
        }

        public KeyboardState Keyboard1
        {
            get { return _keyboard; }
            set { _keyboard = value; }
        }

        public Vector2 Speed
        {
            get { return _v2dSpeed; }
            set { _v2dSpeed = value; }
        }

        #endregion properties

        public Ship(PlayerIndex playerIndex, Texture2D txShipTexture,Texture2D txBulletTexture,WeaponTypes WeaponType)
        {
            this.player = playerIndex;
            this._weaponType = WeaponType;
            this.ShipTexture = txShipTexture;
            this._txBulletTexture = txBulletTexture;
        }

        public void Update(GameTime gameTime)
        {
            if (_keyboard.IsKeyDown(Keys.Left))
                _v2dPosition.X -= 5;

            if (_keyboard.IsKeyDown(Keys.Right))
                _v2dPosition.X += 5;

            if (_keyboard.IsKeyDown(Keys.Up))
                _v2dPosition.Y -= 5;

            if (_keyboard.IsKeyDown(Keys.Down))
                _v2dPosition.Y += 5;

            #region Bullet Update
            if (_keyboard.IsKeyDown(Keys.Space))
            {
                _bullet = new Bullet(_txBulletTexture);

                _bullet.Position = new Vector2(
                    _v2dPosition.X + _txShipTexture.Width / 2 - _txBulletTexture.Width / 2,
                    _v2dPosition.Y - _txShipTexture.Height / 2);
               
                _bulletList.Add(_bullet);
            }

            foreach (Bullet ShipBullet in _bulletList)
            {
                ShipBullet.Update(gameTime);
            }
            #endregion Bullet Update
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_txShipTexture, _v2dPosition, Color.White);
            foreach (Bullet shipBullet in _bulletList)
                shipBullet.Draw(spriteBatch);

        }
    }
}
