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
    class GameObject : Microsoft.Xna.Framework.GameComponent
    {
        public Texture2D txTexture;   //sprite texture
        public Vector2 v2dPosition;    //sprite position on screen
        public double dblSize;         //sprite radius
        public Vector2 v2dVelocity;    //the speed of the object
        private Vector2 v2dScreenSize;
        public Vector2 v2dOrigin;      //the point the object rotates on
        public float fltDirection;     //the direction in radians the object is pointing
    //    public Vector2 point;
        public float fltHullCap;       //what is fltHullCap?
        public float fltMaxHull;       //what is fltMaxHull?
        public float massConst;         //is this a constant? 'Cause it's called massConst...
        public bool isDead;


        public GameObject(Game game, Texture2D newTexture, Vector2 newPosition, double newSize, float Angle,
                          Vector2 newOrigin, float newHullCap, float newMaxHull, float newMassConst)
            : base(game)
        {
            txTexture = newTexture;
            v2dPosition = newPosition;
            dblSize = newSize;
         //  screenSize = new Vector2(((Morito.Game1)game).ScreenWidth, ((Morito.Game1)game).ScreenHeight);
            v2dScreenSize = new Vector2(800, 600);
            fltDirection = Angle;
            v2dOrigin = newOrigin;
            fltHullCap = newHullCap;
            fltMaxHull = newMaxHull;
            massConst = newMassConst;
        }


        public GameObject(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }


        public override void Initialize()
        {
            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
