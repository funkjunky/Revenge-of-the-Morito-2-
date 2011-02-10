#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Morito.Classes;

#endregion


namespace Morito.Screens
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class MainMenuScreen : MenuScreen
    {
        #region Initialization
        Morito.Classes.Backgrounds.StarryBackground StarryBackground;

        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen()
            : base("Main Menu")
        {
               // Create our menu entries.
           // MenuEntry playGameMenuEntry = new MenuEntry("Play Game");
            MenuEntry SMGameMenuEntry = new MenuEntry("Play Game!");
            MenuEntry LevelEditorMenuEntry = new MenuEntry("Level Editor");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");

            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            // Hook up menu event handlers.

            SMGameMenuEntry.Selected += SMGameMenuEntrySelected;
            //playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;
            LevelEditorMenuEntry.Selected += LevelEditorMenuSelected;

            // Add entries to the menu.
            MenuEntries.Add(SMGameMenuEntry);
            //  MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(LevelEditorMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }


        #endregion

        #region Public Methods
        public override void LoadContent()
        {
            base.LoadContent();

            ScreenManager.Cursor.SetCursor(ScreenManager.Game.Content.Load<Texture2D>(@"Textures/cursor"));
            ScreenManager.Cursor.Visible = true;
        }
        #endregion
        #region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen());
        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionScreen(), e.PlayerIndex);
        }

        void SMGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new GameSetupScreen(), e.PlayerIndex);
        }

        void LevelEditorMenuSelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new LevelEditorScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to exit this sample?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }


        #endregion
    }
}