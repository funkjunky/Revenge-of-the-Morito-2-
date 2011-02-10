
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Morito.Screens
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    class OptionScreen : MenuScreen
    {
        #region Fields
        Settings MySettings;

        MenuEntry FullscreenMenuEntry;
        MenuEntry FullScreenRefreshRateInHzMenuEntry;
        MenuEntry EnableAutoDepthStencilMenuEntry;
        MenuEntry MultiSampleQualityMenuEntry, BackBufferWidthMenuEntry, BackBufferHeightMenuEntry;


        static string[] AntiAlaisingNumber = { "One", "Two", "Three", "Four" };
        static int CurrentAntiAlaisingNumber = 0;


        #endregion

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionScreen()
            : base("Options")
        {
            SetMenuEntryText();

            //MenuEntry StartMenuEntry = new MenuEntry("Start Game");

            MenuEntry backMenuEntry = new MenuEntry("Back");

            FullscreenMenuEntry = new MenuEntry("FullScreen: False (Hacked)");
            FullScreenRefreshRateInHzMenuEntry = new MenuEntry("Refresh Rate: 60 (Hacked)");
            EnableAutoDepthStencilMenuEntry = new MenuEntry("Enable Auto Depth Stencil: True (Hacked)");
            MultiSampleQualityMenuEntry = new MenuEntry("Multi Sample Quality: 0 (Hacked)");
            BackBufferWidthMenuEntry= new MenuEntry("Width: 640 (Hacked)");
            BackBufferHeightMenuEntry= new MenuEntry("Height: 480 (Hacked)");

            backMenuEntry.Selected += OnCancel;

            #region commented
            //  ScreenManager.Game.GraphicsDevice.PresentationParameters.MultiSampleQuality

            /* TODO: MileStone 4 ....
                GraphicsDevice gd=new GraphicsDevice();
                GraphicsDeviceManager gdm=new GraphicsDeviceManager();
            
                gd.PresentationParameters.FullScreenRefreshRateInHz;
                gd.PresentationParameters.EnableAutoDepthStencil;
                gd.PresentationParameters.MultiSampleQuality;
                gd.PresentationParameters.MultiSampleType;
                gd.PresentationParameters.BackBufferWidth;
                gd.PresentationParameters.BackBufferHeight;
                gd.PresentationParameters.BackBufferFormat;
                gd.PresentationParameters.AutoDepthStencilFormat;
                gd.PresentationParameters.IsFullScreen;

            
                gdm.SynchronizeWithVerticalRetrace = true;  //(or false to de-activate)
                gdm.MinimumPixelShaderProfile;
                gdm.MinimumVertexShaderProfile;
                gdm.PreferMultiSampling;
                gdm.PreferredBackBufferFormat;
                gdm.PreferredBackBufferHeight;
                gdm.PreferredBackBufferWidth;
                gdm.PreferredDepthStencilFormat;
                gdm.ToggleFullScreen;
        
        

                // Hook up menu event handlers.
                PlayerNumberMenuEntry.Selected += PlayerNumberMenuEntrySelected;
                LevelMenuEntry.Selected += LevelMenuEntrySelected;
                backMenuEntry.Selected += OnCancel;
                StartMenuEntry.Selected += StartMenuEntrySelected;

                // Add entries to the menu.
                MenuEntries.Add(LevelMenuEntry);
                MenuEntries.Add(PlayerNumberMenuEntry);
            MenuEntries.Add(StartMenuEntry);*/
            #endregion commented
            MenuEntries.Add(FullscreenMenuEntry);
            MenuEntries.Add(FullScreenRefreshRateInHzMenuEntry);
            MenuEntries.Add(EnableAutoDepthStencilMenuEntry);
            MenuEntries.Add(MultiSampleQualityMenuEntry);
            MenuEntries.Add(BackBufferWidthMenuEntry);
            MenuEntries.Add(BackBufferHeightMenuEntry);

            MenuEntries.Add(backMenuEntry);
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            //FullscreenMenuEmtry.Text = "FullScreen: " + ScreenManager.Game.GraphicsDevice.PresentationParameters.IsFullScreen;
         //   PlayerNumberMenuEntry.Text = "Number of Players: " + PlayersNumber[currentPlayerNumber];
          //  LevelMenuEntry.Text = "Level: " + Level[currentLevel];
        }


        #endregion

        #region Handle Input



        /// <summary>
        /// Event handler for when the Language menu entry is selected.
        /// </summary>
        void PlayerNumberMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
        //    currentPlayerNumber = (currentPlayerNumber + 1) % PlayersNumber.Length;

            SetMenuEntryText();
        }

        void LevelMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
          //  currentLevel = (currentLevel + 1) % Level.Length;
            SetMenuEntryText();
        }

        void StartMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {

         //  ScreenManager.AddScreen(new GameplayScreen(Level, PlayersNumber, currentLevel, currentPlayerNumber), e.PlayerIndex);

        }

        #endregion


        void graphics_PreparingDeviceSettings(object sender,
    PreparingDeviceSettingsEventArgs e)
        {
            // Xbox 360 and most PCs support FourSamples/0 
            // (4x) and TwoSamples/0 (2x) antialiasing.
            PresentationParameters pp =
                e.GraphicsDeviceInformation.PresentationParameters;
#if XBOX
            pp.MultiSampleQuality = 0;
            pp.MultiSampleType = MultiSampleType.FourSamples;
            return;
#else
            int quality = 0;
            GraphicsAdapter adapter = e.GraphicsDeviceInformation.Adapter;
            SurfaceFormat format = adapter.CurrentDisplayMode.Format;
            // Check for 4xAA
            if (adapter.CheckDeviceMultiSampleType(DeviceType.Hardware, format,
                false, MultiSampleType.FourSamples, out quality))
            {
                // even if a greater quality is returned, we only want quality 0
                pp.MultiSampleQuality = 0;
                pp.MultiSampleType =
                    MultiSampleType.FourSamples;
            }
            // Check for 2xAA
            else if (adapter.CheckDeviceMultiSampleType(DeviceType.Hardware,
                format, false, MultiSampleType.TwoSamples, out quality))
            {
                // even if a greater quality is returned, we only want quality 0
                pp.MultiSampleQuality = 0;
                pp.MultiSampleType =
                    MultiSampleType.TwoSamples;
            }
            return;
#endif
        }
    }
}