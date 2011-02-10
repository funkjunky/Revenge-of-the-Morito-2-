

namespace Morito.Screens
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    class EndGameScreen : MenuScreen
    {
        #region Fields

        MenuEntry GameStatus;
        int WinnerNumber = -1;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public EndGameScreen(int WinnerNumber)
            : base("Choose the Number of Players!")
        {
            GameStatus = new MenuEntry(string.Empty);
            this.WinnerNumber = WinnerNumber;
            SetMenuEntryText();

       
            MenuEntry backMenuEntry = new MenuEntry("Back");
            

            // Hook up menu event handlers.
            backMenuEntry.Selected += OnCancel1;

            // Add entries to the menu.
            MenuEntries.Add(GameStatus);
            MenuEntries.Add(backMenuEntry);
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {

            if (WinnerNumber == -1)
            GameStatus.Text= "Who is the winner? ???";
            else
            GameStatus.Text= "Player "+ WinnerNumber +" is the winner!";


        }


        #endregion

        #region Handle Input

        void OnCancel1(object sender, PlayerIndexEventArgs e)
        {

            //TODO: Improve in Phase 3 !
            ScreenManager.AddScreen(new MainMenuScreen(), e.PlayerIndex);

        }

        #endregion
    }
}