using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace Morito
{
    public abstract class MortalPhysicalObject : FullyPhysicalObject, isMortal
    {
        public const float COLLISION_DAMAGE = 1f;

        #region Member Variables
            protected float _maxHealth;
            protected float _health;
            protected double _timeOfDeath;       //time of death
            protected bool _died = false;
            protected bool _isAnimateDeath = false;
            protected double _animateDeathTime;
        #endregion

        #region Properties
            public float MaxHealth
            {
                get { return _maxHealth; }
                set { _maxHealth = value; }
            }

            public float Health
            {
                get { return _health; }
                set { _health = value; }
            }

            public double TimeOfDeath
            {
                get { return _timeOfDeath; }
                set { _timeOfDeath = value; }
            }

            public bool Died
            {
                get { return _died; }
                set { _died = value; }
            }

            public bool IsAnimateDeath
            {
                get { return _isAnimateDeath; }
                set { _isAnimateDeath = value; }
            }

            public double AnimateDeathTime
            {
                get { return _animateDeathTime; }
                set { _animateDeathTime = value; }
            }
        #endregion

        #region Constructors
            public MortalPhysicalObject(Screens.GameScreen gameScreen, float maxHealth, double animateDeathTime)
                : base (gameScreen)
            {
                _health = maxHealth;
                _maxHealth = maxHealth;
                _animateDeathTime = animateDeathTime;
            }
        #endregion

        #region Complete Methods
            public bool IsAlive()
            {
                return Health > 0;
            }
            public bool IsDead()
            {
                return Health <= 0;
            }

            public void TakeDamage(float damage)
            {
                if (damage > 0)
                    Health -= damage;

                if (Health < 0)
                    Health = 0;
            }
            public void RestoreHealth(float healAmount)
            {
                if (IsAlive())
                {
                    Health += healAmount;

                    if (Health > MaxHealth)
                        Health = MaxHealth;
                }
            }

            private void StartDeathAnimation()
            {
                IsAnimateDeath = true;
            }
            private void StopDeathAnimation()
            {
                IsAnimateDeath = false;
            }

            /// <summary>
            /// The main Update for all Mortality related operations.
            /// Place this at the end of your object's Update function.
            /// </summary>
            /// <param name="gameTime"></param>
            public virtual void UpdateMortality(GameTime gameTime)
            {
                double seconds = gameTime.TotalRealTime.TotalSeconds;

                if (IsDead())
                {
                    if (!Died)
                    {
                        Died = true;
                        TimeOfDeath = seconds;
                        StartDeathAnimation();
                    }

                    if (seconds > TimeOfDeath + AnimateDeathTime)
                    {
                        StopDeathAnimation();
                    }
                }
            }


            public override void LoadFromXElement(XElement root)
            {
                base.LoadFromXElement(root);

                MaxHealth = Convert.ToSingle(root.Element("MaxHealth").Value);
            }

            public override XElement serializeToXElement()
            {
                XElement root = base.serializeToXElement();
                root.Add(new XElement("MaxHealth", MaxHealth));

                return root;
            }
        #endregion

        #region Abstract Methods
        #endregion
    }
}
