using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Morito.Screens;

namespace Morito.Classes
{
    public class Level
    {
        List<VisualObject3D> _pieces;
        Texture2D _backgroundTexture;
        string _backgroundResourceName;
        Screens.GameScreen _gameScreen;

        public Level() { }

        #region Properties
        public List<VisualObject3D> Pieces
        {
            get { return _pieces; }
            set { _pieces = value; }
        }
        public Texture2D BackgroundTexture
        {
            get { return _backgroundTexture; }
            set { _backgroundTexture = value; }
        }
        public string BackgroundResourceName
        {
            get { return _backgroundResourceName; }
            set { _backgroundResourceName = value; }
        }
        public Screens.GameScreen GameScreen
        {
            get { return _gameScreen; }
            set { _gameScreen = value; }
        }
        #endregion
        #region Public Methods

        public void LoadContent(GameScreen screen)
        {
            //Now that we are physically loading the level we need to know what screen we are attaching to
            //The screen gives us access to a screenmanager which gives us access to the ability to load content.
            //This may not be the best method =P.
            GameScreen = screen;
            
            //Hopefully your CurrentDirectory isn't out of whack somehow.
            loadLevelFromFile(
                Environment.CurrentDirectory + @"\Content\Levels\MoritoLevel.xml");
            return;
        }
        private void old_LoadContent(GameplayScreen screen)
        {
            Pieces = new List<VisualObject3D>();

            BackgroundResourceName = "Textures\\Space_BackGround";
            BackgroundTexture = GameScreen.ScreenManager.Game.Content.Load<Texture2D>(BackgroundResourceName);
            string modelResourceName = "Models\\asteroid1";
            Model AsteroidModel = GameScreen.ScreenManager.Game.Content.Load<Model>(modelResourceName);
            Random Rand = new Random();

            //create 10 asteroids with some random stats.
            int numOfAsteroids = 10;
            for (int i = 0; i < numOfAsteroids; i++)
            {
                Asteroid newAsteroid = new Asteroid(GameScreen)
                {
                    ObjectModel = AsteroidModel,
                    ModelResourceName = modelResourceName,
                    BSphere = AsteroidModel.Meshes[0].BoundingSphere,
                    ReferenceCamera = screen.Camera1,
                    Velocity = (new Vector2(Rand.nextSignedFloat(), Rand.nextSignedFloat())).getDirectedVector(0.3f),
                    RotationVelocity = Rand.nextVector3Interval(new Vector3(-0.1f, -0.1f, -0.1f), new Vector3(0.1f, 0.1f, 0.1f))
                };
                Pieces.Add(newAsteroid);
            }
        }

        public void Draw(GameTime gameTime)
        {
            //draw the background.
            Rectangle screenRectangle = new Rectangle(0, 0, GameScreen.ScreenManager.Game.GraphicsDevice.PresentationParameters.BackBufferWidth, GameScreen.ScreenManager.Game.GraphicsDevice.PresentationParameters.BackBufferHeight);
            GameScreen.SpriteBatchDrawable.Begin();
            GameScreen.SpriteBatchDrawable.Draw(BackgroundTexture, screenRectangle, Color.LightGray);
            GameScreen.SpriteBatchDrawable.End();

            //draw all objects for the level.
            foreach (VisualObject3D piece in Pieces)
                piece.Draw();
        }
        public void Update(GameTime gameTime)
        {
            //update all objects for the level. The Background doesn't need to be updated as it is merely a texture.
            for (int i = 0; i != Pieces.Count; ++i)
                if (Pieces[i] is isMortal && ((isMortal)Pieces[i]).Died)
                {
                    RemoveObjectFromGame(Pieces[i]);
                    --i;
                }
                else
                    Pieces[i].Update(gameTime);
        }

        public void RemoveObjectFromGame(VisualObject3D obj)
        {
            if (obj is isCollidable)
                GameScreen.Collisions.Collidables.Remove((isCollidable)obj);

            if (obj is hasPosition2D)
                GameScreen.ObjectsToBeWrapped.Remove((hasPosition2D)obj);

            GameScreen.Level.Pieces.Remove(obj);
        }

        public XElement serializeToXElement()
        {
            XElement root = 
                new XElement("Level",
                    new XElement("Pieces"),
                    new XElement("BackgroundTexture", BackgroundResourceName)
                );

            XElement level = root.Element("Pieces");
            foreach (VisualObject3D obj in Pieces)
                level.Add(obj.serializeToXElement());

            return root;
        }

        public void loadFromXElement(XElement root)
        {
            Pieces = new List<VisualObject3D>();
            foreach (XElement obj in root.Element("Pieces").Elements())
            {
                try
                {
                    Type type = Type.GetType(obj.Name.LocalName);
                    //my function requires all 3D objects have a constructor that takes in a gamescreen and camera respectively. They require it anyways >>.
                    VisualObject3D visualObject3D = (VisualObject3D)Activator.CreateInstance(type, GameScreen, GameScreen.Camera1);
                    visualObject3D.LoadFromXElement(obj);
                    visualObject3D.ObjectModel = GameScreen.ScreenManager.Game.Content.Load<Model>(visualObject3D.ModelResourceName);
                    if (typeof(FullyPhysicalObject).IsInstanceOfType(visualObject3D))
                        ((FullyPhysicalObject)visualObject3D).BSphere = visualObject3D.ObjectModel.Meshes[0].BoundingSphere;
                    Pieces.Add(visualObject3D);
                }
                catch (Exception e)
                {
                    string c = e.Message; //TODO: WTF is this for? Do this properly?
                    throw new Exception(c);
                }
            }
            BackgroundResourceName = root.Element("BackgroundTexture").Value;
            BackgroundTexture = GameScreen.ScreenManager.Game.Content.Load<Texture2D>(BackgroundResourceName);
        }

        /// <summary>
        /// saves the level to a file in XML ^^.
        /// </summary>
        /// <param name="absoluteFileName">It has to be absolute!</param>
        public void saveLevelToFile(string absoluteFileName)
        {
            try
            {
                StreamWriter file = new StreamWriter(absoluteFileName);
                file.WriteLine(serializeToXElement());

                file.Close();
            }
            catch (Exception e)
            {
                MoritoFighterGame.MoritoFighterGameInstance._screenManager.DisplayedMessages["Level1"]
                    = "Exception with the saving of a serialized object: " + e.Message;
            }
        }

        public void loadLevelFromFile(string absoluteFileName)
        {
            try
            {
                StreamReader file = new StreamReader(absoluteFileName);
                loadFromXElement(XElement.Parse(file.ReadToEnd()));

                file.Close();
            }
            catch (Exception e)
            {
                MoritoFighterGame.MoritoFighterGameInstance._screenManager.DisplayedMessages["Level1"]
                    = "Exception with the saving of a serialized object: " + e.Message;
            }
        }
        #endregion
    }
}
