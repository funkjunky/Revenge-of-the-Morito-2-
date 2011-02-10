#region Using Declaration
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion Using Declaration


namespace Morito
{
    public class HumanPlayer: Player, isControllable
    {
        #region Input
        protected KeyboardState _currentKeyboard;
        protected KeyboardState _lastKeyboard;
        protected GamePadState _currentGamePad;
        protected GamePadState _lastGamePad;
        protected MouseState _gameMouse;

        private Vector2 oldMomentum = Vector2.Zero;
        private Vector2 newMomentum = Vector2.Zero;
        private Vector2 newForce;
        private float force;

        //constants
        const float MAXFORCE = 0.025f;
        const float MAXVELOCITY = 14.0f;
        const float FRICTION = 0.0995f;
        #endregion

        public KeyboardState CurrentKeyboard
        {
            get { return _currentKeyboard; }
            set { _currentKeyboard = value; }
        }

        public GamePadState CurrentGamePad
        {
            get { return _currentGamePad; }
            set { _currentGamePad = value; }
        }

        public MouseState GameMouse
        {
            get { return _gameMouse; }
            set { _gameMouse = value; }
        }

        public HumanPlayer(Screens.GameplayScreen gameplayScreen, Vector3 respawnPoint)
            : base(gameplayScreen, respawnPoint)
        {
             
        }

        public void applyControlsGamePad()
        {
            //save the last gamepadstate and update the currentgamepad state
            _lastGamePad = _currentGamePad;
            _currentGamePad = GamePad.GetState(this.PlayersIndex);

            // Allows the game to exit
            if (_currentGamePad.Buttons.Back == ButtonState.Pressed)
                MoritoFighterGame.MoritoFighterGameInstance.Exit();            

            //store how far the ship moved on the previous update
            oldMomentum = newMomentum;

            //find a value for forward or backwards thrust
            float forwardinput = _currentGamePad.Triggers.Right;
            float backwardinput = _currentGamePad.Triggers.Left;
            
            //determine how far to move the ship
            force = (forwardinput - backwardinput);

            //check that force is a semi significant number, if it isn't apply
            //friction on the ship and set force to 0
            if (Math.Abs(force) < 0.010)
            {
                force = 0;
                oldMomentum *= FRICTION;
            }

            //apply the force based on the current input, this will be added to momentum
            newForce.X = (float)Math.Cos((double)this.PlayersShip.Rotation.Z);
            newForce.Y = (float)Math.Sin((double)this.PlayersShip.Rotation.Z);

            //normalize to make sure the values are based on a standard distance. no matter
            //where the stick lies.
            Vector2.Normalize(newForce);

            //check if the stick is in the dead zone of the controller
            if (!((Math.Abs(_currentGamePad.ThumbSticks.Left.X) < 0.010) &&
                ((Math.Abs(_currentGamePad.ThumbSticks.Left.Y) < 0.010))))
            {
                //rotate the ship
                PlayersShip.RotationAngle = TurnToFace(
                    PlayersShip.Position2D, 
                    new Vector2(_currentGamePad.ThumbSticks.Left.X, _currentGamePad.ThumbSticks.Left.Y) * 100,
                    PlayersShip.RotationAngle, 
                    PlayersShip.RotationSpeed);
            }

            //multiply by the current for which must be between +1 and -1
            newForce *= force;

            //multiply by the MAXFORCE to scale the input to how fast we want acceleration to be
            newForce *= MAXFORCE;

            PlayersShip.Force = newForce;
            
            MoritoFighterGame.MoritoFighterGameInstance.DisplayedMessages["in update"] = "ships velocity: " + PlayersShip.Velocity.ToString();
            
            if (CurrentKeyboard.IsKeyDown(Keys.Space))
                PlayersShip.ShootBullet();
        }

        public void applyControlsKeyboard()
        {
            _currentKeyboard = Keyboard.GetState();

            //set current force to zero every update
            force = 0;

            //will use wasd for key board controls
            //w and a will be thrust, s and d will turn the ship
            if (_currentKeyboard.IsKeyDown(Keys.W))
                force++;

            if (_currentKeyboard.IsKeyDown(Keys.S))
                force--;

            //create the newForce vector on the ship
            newForce.X = (float)(force * MAXFORCE * Math.Cos(PlayersShip.RotationAngle));
            newForce.Y = (float)(force * MAXFORCE * Math.Sin(PlayersShip.RotationAngle));

            PlayersShip.Force = newForce;

            if (_currentKeyboard.IsKeyDown(Keys.A))
                PlayersShip.RotationAngle += PlayersShip.RotationSpeed;

            if (_currentKeyboard.IsKeyDown(Keys.D))
                PlayersShip.RotationAngle -= PlayersShip.RotationSpeed;

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //is a controller is connected that corresponds to the player index
            //use that controller for the player
            if (GamePad.GetState(PlayersIndex).IsConnected)
            {
                applyControlsGamePad();
            }
            //if no controller is supported use the keyboard for input
            else
            {
                applyControlsKeyboard();
            }
        }
        private static float TurnToFace(Vector2 origin, Vector2 point,
            float currentAngle, float turnSpeed)
        {
            // consider this diagram:
            //        
            //        
            //      /  |
            //    /    | y
            //  / o    |
            // A--------
            //     x
            // 
            // where A is the position of the object, B is the position of the target,
            // and "o" is the angle that the object should be facing in order to 
            // point at the target. we need to know what o is. using trig, we know that
            //      tan(theta)       = opposite / adjacent
            //      tan(o)           = y / x
            // if we take the arctan of both sides of this equation...
            //      arctan( tan(o) ) = arctan( y / x )
            //      o                = arctan( y / x )
            // so, we can use x and y to find o, our "desiredAngle."
            // x and y are just the differences in position between the two objects.
            float x = point.X - origin.X;
            float y = point.Y - origin.Y;

            // we'll use the Atan2 function. Atan will calculates the arc tangent of 
            // y / x for us, and has the added benefit that it will use the signs of x
            // and y to determine what cartesian quadrant to put the result in.
            // http://msdn2.microsoft.com/en-us/library/system.math.atan2.aspx
            float desiredAngle = (float)Math.Atan2(y, x);

            // so now we know where we WANT to be facing, and where we ARE facing...
            // if we weren't constrained by turnSpeed, this would be easy: we'd just 
            // return desiredAngle.
            // instead, we have to calculate how much we WANT to turn, and then make
            // sure that's not more than turnSpeed.

            // first, figure out how much we want to turn, using WrapAngle to get our
            // result from -Pi to Pi ( -180 degrees to 180 degrees )
            float difference = WrapAngle(desiredAngle - currentAngle);

            // clamp that between -turnSpeed and turnSpeed.
            difference = MathHelper.Clamp(difference, -turnSpeed, turnSpeed);

            // so, the closest we can get to our target is currentAngle + difference.
            // return that, using WrapAngle again.
            return WrapAngle(currentAngle + difference);
        }

        private static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
