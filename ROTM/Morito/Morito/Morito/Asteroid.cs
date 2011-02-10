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
    class Asteroid:GameObject
    {
        const float fltMAX_SPEED = 4.0f;//The max speed the asteroid
        public float _fltRotateSpeed;           //The rotation speed of the asteroid
        public float _fltBearing;        //The direction the asteroid moves
        SpriteBatch spriteBatch;            //The spritebatch needed for drawing the asteroid
        public float _fltScale = 0.5f;
        const float fltRESPAWN_TIME = 1;   //Respawn time in seconds
        private Vector2 GameScreen;         //does the asteroid really need a reference of the game screen dimensions?
                                            //it should be able to get it from another object, or a global something.
        Random rand = new Random();
        double dblCurrentTime;

        public Asteroid(Game game, Texture2D txrTexture, Vector2 v2dPosition, double dblSize, float fltAngle,
                      Vector2 v2dOrigin, SpriteBatch spriteBatch, float fltHullCap, float fltMaxHull, float fltMassConst)
            : base(game, txrTexture, v2dPosition, dblSize, fltAngle,
                   v2dOrigin, fltHullCap, fltMaxHull, fltMassConst)
        {
            this.spriteBatch = spriteBatch;
            GameScreen = new Vector2(800, 600); //no numbers... these need to be passed from somewhere (ie. a constant shared by all)
            rand = new Random(game.TargetElapsedTime.Milliseconds);
            this.v2dVelocity = new Vector2((float)rand.NextDouble() * 4);
            this.fltDirection = (float)rand.Next(0, (int)(MathHelper.Pi * 2)); ;
            this._fltRotateSpeed = (float)(rand.NextDouble()) / 10;
            this._fltBearing = (float)rand.NextDouble() * (MathHelper.Pi * 2);

            float cosAngle = (float)Math.Cos((float)rand.NextDouble() * (MathHelper.Pi * 2));
            float sinAngle = (float)Math.Sin((float)rand.NextDouble() * (MathHelper.Pi * 2));
            this.v2dVelocity.X *= cosAngle;
            this.v2dVelocity.Y *= sinAngle;
          

        }

       

        public void Move()
        {
            //Stops the asteroid from going too fast
            if (v2dVelocity.Length() > fltMAX_SPEED)
            {
                v2dVelocity.Normalize();
                v2dVelocity *= fltMAX_SPEED;
            }

            //Just makes the asteroid move
            v2dPosition = v2dPosition + v2dVelocity;


            //Wrapping code
            if (v2dPosition.X + v2dVelocity.X > (GameScreen.X))
                v2dPosition.X = 0.0f;

            if (v2dPosition.X + v2dVelocity.X < 0)
                v2dPosition.X = (GameScreen.X);

            if (v2dPosition.Y + v2dVelocity.Y > (GameScreen.Y))
                v2dPosition.Y = 0.0f;

            if (v2dPosition.Y + v2dVelocity.Y < 0)
                v2dPosition.Y = (GameScreen.Y);

        }

        public void Collide(GameObject other, Vector2 cloneVelo, SoundEffect collision, GameTime gameTime)
        {
            collision.Play();
            float mass = (other.fltHullCap + other.massConst);
            float asterMass = (fltHullCap + massConst);
            float bothMass = asterMass + mass;

            TakeDamage((asterMass - mass) * (v2dVelocity.Length() - cloneVelo.Length()), gameTime.TotalGameTime.TotalSeconds);

            if (mass == asterMass)
                v2dVelocity = cloneVelo;
            else
            {
                v2dVelocity.X = ((asterMass - mass) / bothMass * v2dVelocity.X)
                              + ((mass * 2) / bothMass * cloneVelo.X);

                v2dVelocity.Y = ((asterMass - mass) / bothMass * v2dVelocity.Y)
                              + ((mass * 2) / bothMass * cloneVelo.Y);
            }
        }


        public void CheckDeath(double gameTime)
        {
            //Checks if the asteroid has no health left
            if (fltHullCap < 0)
            {
                //Score Changes Here

                //Set the asteroid to destroyed
                isDead = true;
                dblCurrentTime = gameTime;

                //Death Effects
                //i.e: Sound, visuals
            }
        }


        public void CheckRespawn(double gameTime)
        {
            //If the ship is dead then check if it is time to respawn 
            if (isDead /*&& (currentTime >= 0)*/ )
            {
                if (gameTime > (dblCurrentTime + fltRESPAWN_TIME))
                {
                    double r;
                    Random rand = new Random();

                    v2dPosition.X = (float)(rand.NextDouble() * (GameScreen.X));
                    v2dPosition.Y = (float)(rand.NextDouble() * (GameScreen.Y));

                    v2dVelocity = new Vector2((float)rand.NextDouble() * fltMAX_SPEED);

                    _fltRotateSpeed = (float)(rand.NextDouble() / 10);

                    r = rand.NextDouble() * MathHelper.Pi * 2;
                    _fltBearing = (float)(r);

                    float cosAngle = (float)Math.Cos((float)r);
                    float sinAngle = (float)Math.Sin((float)r);
                    v2dVelocity.X *= cosAngle;
                    v2dVelocity.Y *= sinAngle;

                    //Set the ship back alive
                    isDead = false;
                    //Spawn at random spot

                    dblCurrentTime = 0;
                }
            }
        }


        public void TakeDamage(float damage, double gameTime)
        {
            //Make the asteroid lose health
            fltHullCap -= damage;

            //Check if the damage caused it to die
            CheckDeath(gameTime);
        }

        public void Rotate()
        {
            //Rotates the asteroid based on it's current rotate speed
            fltDirection += _fltRotateSpeed;
        }

        public override void Update(GameTime gameTime)
        {
            Move();
            Rotate();
            base.Update(gameTime);
        }
        public void Draw(SpriteBatch sb)
        {
            //Draws the asteroid if it's not destroyed
            if (!isDead)
                spriteBatch.Draw(txTexture, v2dPosition, null, Color.White, (float)fltDirection, v2dOrigin, _fltScale,
                                 SpriteEffects.None, 0f);
        }
        
    }
}
