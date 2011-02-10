#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Morito.Classes;
#endregion

namespace Morito
{
    /// <summary>
    /// Helper for reading input from keyboard and gamepad. This class tracks both
    /// the current and previous state of both input devices, and implements query
    /// methods for high level input actions such as "move up through the menu"
    /// or "pause the game".
    /// </summary>
    public class InputState
    {
        #region Fields

        public const int MaxInputs = 4;

        public readonly KeyboardState[] CurrentKeyboardStates;
        public readonly GamePadState[] CurrentGamePadStates;

        public readonly KeyboardState[] LastKeyboardStates;
        public readonly GamePadState[] LastGamePadStates;

        public MouseHandler Mouse;

        public readonly bool[] GamePadWasConnected;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructs a new input state.
        /// </summary>
        public InputState()
        {
            CurrentKeyboardStates = new KeyboardState[MaxInputs];
            CurrentGamePadStates = new GamePadState[MaxInputs];

            LastKeyboardStates = new KeyboardState[MaxInputs];
            LastGamePadStates = new GamePadState[MaxInputs];

            Mouse = new MouseHandler();

            GamePadWasConnected = new bool[MaxInputs];
        }


        #endregion

        #region Public Methods

        #region other public methods
        /// <summary>
        /// Reads the latest state of the keyboard and gamepad.
        /// </summary>
        public void Update()
        {
            Mouse.Update();

            for (int i = 0; i < MaxInputs; i++)
            {
                LastKeyboardStates[i] = CurrentKeyboardStates[i];
                LastGamePadStates[i] = CurrentGamePadStates[i];

                CurrentKeyboardStates[i] = Keyboard.GetState((PlayerIndex)i);
                CurrentGamePadStates[i] = GamePad.GetState((PlayerIndex)i);

                // Keep track of whether a gamepad has ever been
                // connected, so we can detect if it is unplugged.
                if (CurrentGamePadStates[i].IsConnected)
                {
                    GamePadWasConnected[i] = true;
                }
            }
        }
        #endregion
        #region Generic Key Events
        /// <summary>
        /// Helper for checking if a key was newly pressed during this update. The
        /// controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a keypress
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsNewKeyPress(Keys key, PlayerIndex? controllingPlayer,
                                            out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;
                MoritoFighterGame.MoritoFighterGameInstance.DisplayedMessages["keyup"] = "keyup is: " + (CurrentKeyboardStates[i].IsKeyDown(key) &&
                        LastKeyboardStates[i].IsKeyUp(key)).ToString();
                return (CurrentKeyboardStates[i].IsKeyDown(key) &&
                        LastKeyboardStates[i].IsKeyUp(key));
            }
            else
            {
                // Accept input from any player.
                return (IsNewKeyPress(key, PlayerIndex.One, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Two, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Three, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Four, out playerIndex));
            }
        }

        /// <summary>
        /// Helper for checking if a key is being pressed during this update. The
        /// controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a KeyDown
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsKeyDown(Keys key, PlayerIndex? controllingPlayer,
                                            out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentKeyboardStates[i].IsKeyDown(key));
            }
            else
            {
                // Accept input from any player.
                return (IsKeyDown(key, PlayerIndex.One, out playerIndex) ||
                        IsKeyDown(key, PlayerIndex.Two, out playerIndex) ||
                        IsKeyDown(key, PlayerIndex.Three, out playerIndex) ||
                        IsKeyDown(key, PlayerIndex.Four, out playerIndex));
            }
        }

        /// <summary>
        /// Helper for checking if a button was newly pressed during this update.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a button press
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsNewButtonPress(Buttons button, PlayerIndex? controllingPlayer,
                                                     out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentGamePadStates[i].IsButtonDown(button) &&
                        LastGamePadStates[i].IsButtonUp(button));
            }
            else
            {
                // Accept input from any player.
                return (IsNewButtonPress(button, PlayerIndex.One, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Two, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Three, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Four, out playerIndex));
            }
        }

        /// <summary>
        /// Helper for checking if a key is being pressed during this update. The
        /// controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a KeyDown
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsButtonDown(Buttons button, PlayerIndex? controllingPlayer,
                                            out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return CurrentGamePadStates[i].IsButtonDown(button);
            }
            else
            {
                // Accept input from any player.
                return (IsNewButtonPress(button, PlayerIndex.One, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Two, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Three, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Four, out playerIndex));
            }
        }
        #endregion

        #region menuSelectionStates
        /// <summary>
        /// Checks for a "menu select" input action.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When the action
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsMenuSelect(PlayerIndex? controllingPlayer,
                                 out PlayerIndex playerIndex)
        {
            return IsNewKeyPress(Keys.Space, controllingPlayer, out playerIndex) ||
                   IsNewKeyPress(Keys.Enter, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.A, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.Start, controllingPlayer, out playerIndex) ||
                   Mouse.IsNewLeftMouseClick;
        }


        /// <summary>
        /// Checks for a "menu cancel" input action.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When the action
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsMenuCancel(PlayerIndex? controllingPlayer,
                                 out PlayerIndex playerIndex)
        {
            return IsNewKeyPress(Keys.Escape, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.B, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.Back, controllingPlayer, out playerIndex);
        }


        /// <summary>
        /// Checks for a "menu up" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public bool IsMenuUp(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(Keys.Up, controllingPlayer, out playerIndex) ||
                    IsNewKeyPress(Keys.W, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.DPadUp, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.LeftThumbstickUp, controllingPlayer, out playerIndex);
        }


        /// <summary>
        /// Checks for a "menu down" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public bool IsMenuDown(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(Keys.Down, controllingPlayer, out playerIndex) ||
                    IsNewKeyPress(Keys.S, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.DPadDown, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.LeftThumbstickDown, controllingPlayer, out playerIndex);
        }
        #endregion

        #region gameplayStates
        /// <summary>
        /// Checks for a "pause the game" input action.
        /// The controllingPlayer parameter specifies which player to read
        /// input for. If this is null, it will accept input from any player.
        /// </summary>
        public bool IsPauseGame(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return IsNewKeyPress(Keys.Escape, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.Back, controllingPlayer, out playerIndex) ||
                   IsNewButtonPress(Buttons.Start, controllingPlayer, out playerIndex);
        }

        public float AmountForwardThruster(PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            float thrustAmount = 0.0f;
            if (controllingPlayer.HasValue && GamePad.GetState((PlayerIndex) controllingPlayer).IsConnected)
            {
                playerIndex = (PlayerIndex) controllingPlayer; //we have the player set if it's a controller.
                float forwardinput = GamePad.GetState((PlayerIndex) controllingPlayer).Triggers.Right;
                float backwardinput = GamePad.GetState((PlayerIndex) controllingPlayer).Triggers.Left;
                if (forwardinput - backwardinput < -0.001 || forwardinput - backwardinput > 0.001)
                    thrustAmount = forwardinput - backwardinput;
            }
            else
            {
                if (IsKeyDown(Keys.LeftAlt, controllingPlayer, out playerIndex))
                {
                    thrustAmount++;
                    MoritoFighterGame.MoritoFighterGameInstance.DisplayedMessages["keyup"] = "keyup is being pressed ";
                }
                if (IsKeyDown(Keys.RightAlt, controllingPlayer, out playerIndex))
                    thrustAmount--;

            }
    
            return thrustAmount;
        }

        public Vector2 AmountFaceRightUp(PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            Vector2 amountFaceRightUp;
            bool w, a, s, d;
            w = a = s = d = false;
            if (controllingPlayer.HasValue && GamePad.GetState((PlayerIndex)controllingPlayer).IsConnected)
            {
                playerIndex = (PlayerIndex)controllingPlayer; //we have the player set if it's a controller.
                amountFaceRightUp = GamePad.GetState((PlayerIndex)controllingPlayer).ThumbSticks.Left;
            }
            else
            {
                w = IsKeyDown(Keys.W, controllingPlayer, out playerIndex);
                a = IsKeyDown(Keys.A, controllingPlayer, out playerIndex);
                s = IsKeyDown(Keys.S, controllingPlayer, out playerIndex);
                d = IsKeyDown(Keys.D, controllingPlayer, out playerIndex);

                float amountFaceRight = ((d) ? 1.0f : 0.0f) + (((a)) ? -1.0f : 0.0f);
                float amountFaceUp = ((w) ? 1.0f : 0.0f) + (((s)) ? -1.0f : 0.0f);

                amountFaceRightUp = new Vector2(amountFaceRight, amountFaceUp);
            }

            return amountFaceRightUp;
        }

        public bool IsShootingTopWeapon(PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue
                && GamePad.GetState((PlayerIndex)controllingPlayer).IsConnected)
            {
                playerIndex = (PlayerIndex)controllingPlayer;
                if (IsButtonDown(Buttons.Y, controllingPlayer, out playerIndex))
                    return true;
                else
                    return false;
            }
            else if (IsKeyDown(Keys.I, controllingPlayer, out playerIndex))
                return true;
            else
                return false;
        }

        public bool IsShootingLeftWeapon(PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue
                && GamePad.GetState((PlayerIndex)controllingPlayer).IsConnected
                && IsButtonDown(Buttons.X, controllingPlayer, out playerIndex))
            {
                playerIndex = (PlayerIndex)controllingPlayer;
                return true;
            }
            else if (IsKeyDown(Keys.J, controllingPlayer, out playerIndex))
                return true;
            else
                return false;
        }

        public bool IsShootingRightWeapon(PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue
                && GamePad.GetState((PlayerIndex)controllingPlayer).IsConnected
                && IsButtonDown(Buttons.B, controllingPlayer, out playerIndex))
            {
                playerIndex = (PlayerIndex)controllingPlayer;
                return true;
            }
            else if (IsKeyDown(Keys.L, controllingPlayer, out playerIndex))
                return true;
            else
                return false;
        }

        public bool IsShootingBottomWeapon(PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue
                && GamePad.GetState((PlayerIndex)controllingPlayer).IsConnected
                && IsButtonDown(Buttons.A, controllingPlayer, out playerIndex))
            {
                playerIndex = (PlayerIndex)controllingPlayer;
                return true;
            }
            else if (IsKeyDown(Keys.K, controllingPlayer, out playerIndex))
                return true;
            else
                return false;
        }

        #endregion

        #endregion
    }
}
