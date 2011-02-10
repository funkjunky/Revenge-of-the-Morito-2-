using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Morito
{
    public class Morality
    {
        #region Instance Fields
        public static float COLLSION_DAMAGE = 1f;

        private uint _health = 100;

        private bool _isAnimateDeath;
        #endregion

        #region Properties
        public uint Health
        {
            get { return _health; }
        }

        public bool IsAnimateDeath
        {
            get { return _isAnimateDeath; }
            set
            {
                if (!IsAlive())
                    _isAnimateDeath = value;
            }
        }
        #endregion


        public Morality()
        {
            Reset();
        }


        /// <summary>
        /// Reset all the Values!
        /// </summary>
        public void Reset()
        {
            _health = 100;
        }

        /// <summary>
        /// Damage the player for x amount.
        /// </summary>
        public void TakeDamage(uint damage)
        {     
            _health -= damage;
        }

        /// <summary>
        /// Returns True if Alive and False if dead !
        /// </summary>
        public bool IsAlive()
        {
            return (_health > 0);
        }       
    }
}