using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Morito.Utilities;

namespace Morito
{
    public abstract class RespawnablePhysicalObject : MortalPhysicalObject, isRespawnable
    {
        #region Member Variables
            protected double _respawnTime;
            protected Vector3 _respawnPoint;
        #endregion

        #region Properties
            public double RespawnTime
            {
                get { return _respawnTime; }
                set { _respawnTime = value; }
            }

            public Vector3 RespawnPoint
            {
                get { return _respawnPoint; }
                set { _respawnPoint = value; }
            }
        #endregion

        #region Constructors
            public RespawnablePhysicalObject(Screens.GameScreen gameScreen, float maxHealth,
                double animateDeathTime, double respawnTime, Vector3 respawnPoint)
                : base(gameScreen, maxHealth, animateDeathTime)
            {
                _respawnTime = respawnTime;
                _respawnPoint = respawnPoint;

                Position = respawnPoint;
            }
        #endregion

        #region Complete Methods
            public override void UpdateMortality(GameTime gameTime)
            {
                double seconds = gameTime.TotalRealTime.TotalSeconds;

                //If the ship is dead then check if it is time to respawn 
                if (Died)
                {
                    if (seconds > (TimeOfDeath + RespawnTime))
                    {
                        if (this is isCollidable)
                        {
                            if (!GameScreen.Collisions.anyCollisions(this))
                            {
                                Respawn();
                            }
                        }
                        else
                        {
                            Respawn();
                        }
                    }
                }

                base.UpdateMortality(gameTime);
            }

            public void Respawn()
            {
                Position = RespawnPoint;

                Died = false;

                Health = MaxHealth;

                TimeOfDeath = 0;
            }

            public override void LoadFromXElement(XElement root)
            {
                base.LoadFromXElement(root);

                RespawnTime = Convert.ToDouble(root.Element("RespawnTime").Value);
                RespawnPoint = new Vector3();
                RespawnPoint = RespawnPoint.loadFromXElement(root.Element("RespawnPoint").Element("Vector3"));
            }

            public override XElement serializeToXElement()
            {
                XElement root = base.serializeToXElement();
                root.Add(new XElement("RespawnTime", RespawnTime));
                root.Add(new XElement("RespawnPoint", RespawnPoint.serializeToXElement()));

                return root;
            }
        #endregion

        #region Abstract Methods
            public abstract Vector2 CreateRespawnPoint();
        #endregion
    }
}
