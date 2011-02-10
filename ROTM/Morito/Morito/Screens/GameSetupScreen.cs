

namespace Morito.Screens
{
    /// <summary>
    /// This Screen asks the user how many users are joining the game...and the Level!
    /// </summary>
    class GameSetupScreen : MenuScreen
    {
        #region Fields

        MenuEntry PlayerNumberMenuEntry;
        MenuEntry LevelMenuEntry;

        static string[] PlayersNumber = { "One", "Two", "Three", "Four" };
        static int currentPlayerNumber = 0;

        static string[] Level = { "Level 1!", "MIA Level 1", "MIA Level 2", "MIA Level 3" };
        static int currentLevel = 0;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameSetupScreen()
            : base("Choose the Number of Players!")
        {
            PlayerNumberMenuEntry = new MenuEntry(string.Empty);
            LevelMenuEntry = new MenuEntry(string.Empty);

            SetMenuEntryText();

            MenuEntry StartMenuEntry = new MenuEntry("Start Game");

            MenuEntry backMenuEntry = new MenuEntry("Back");
            

            // Hook up menu event handlers.
            PlayerNumberMenuEntry.Selected += PlayerNumberMenuEntrySelected;
            LevelMenuEntry.Selected += LevelMenuEntrySelected;
            backMenuEntry.Selected += OnCancel;
            StartMenuEntry.Selected += StartMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(LevelMenuEntry);
            MenuEntries.Add(PlayerNumberMenuEntry);
            MenuEntries.Add(StartMenuEntry);
            MenuEntries.Add(backMenuEntry);
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            PlayerNumberMenuEntry.Text = "Number of Players: " + PlayersNumber[currentPlayerNumber];
            LevelMenuEntry.Text = "Level: " + Level[currentLevel];
        }


        #endregion

        #region Handle Input



        /// <summary>
        /// Event handler for when the Language menu entry is selected.
        /// </summary>
        void PlayerNumberMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentPlayerNumber = (currentPlayerNumber + 1) % PlayersNumber.Length;

            SetMenuEntryText();
        }

        void LevelMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentLevel = (currentLevel + 1) % Level.Length;
            SetMenuEntryText();
        }

        void StartMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {

            //TODO: Improve in Phase 3 !
            ScreenManager.AddScreen(new GameplayScreen(Level, PlayersNumber,currentLevel,currentPlayerNumber),e.PlayerIndex);

        }

        #endregion
    }
}