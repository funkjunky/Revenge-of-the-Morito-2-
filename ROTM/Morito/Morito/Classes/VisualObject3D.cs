using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Morito.Utilities;

namespace Morito
{
    public abstract class VisualObject3D : Microsoft.Xna.Framework.GameComponent, is3DDrawable, hasPosition2D
    {
        #region Members
        protected Screens.GameScreen _gameScreen; //TODO: When Level class is made change this to the level

        protected Vector3 _origin;
        protected Vector3 _rotation;                                    //rotating on each axis.
        protected Vector3 _position = Vector3.Zero;                     //default to centre of screen.
        protected Camera _referenceCamera;
        protected Model _model;
        protected string _modelResourceName;
        #endregion

        #region Constructors
        public VisualObject3D(Screens.GameScreen gameplayScreen)
            : base(MoritoFighterGame.MoritoFighterGameInstance)
        {
            GameScreen = gameplayScreen;

            //for now we will just default to our only camera for all 3D objects.
            ReferenceCamera = MoritoFighterGame.MoritoFighterGameInstance.Camera1;
        }

        public VisualObject3D(Screens.GameplayScreen gameplayScreen, XElement objectXML)
            : this(gameplayScreen)
        {
            LoadFromXElement(objectXML);
        }
        #endregion

        #region Properties
        public abstract string ClassName
        { get; }

        public Screens.GameScreen GameScreen
        {
            get { return _gameScreen; }
            set { _gameScreen = value; }
        }


        public Vector3 Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        public Vector3 Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 Position2D
        {
            get { return new Vector2(_position.X, _position.Y); }
            set { _position = new Vector3(value.X, value.Y, _position.Z); }
        }

        public Camera ReferenceCamera
        {
            get { return _referenceCamera; }
            set { _referenceCamera = value; }
        }

        public Model ObjectModel
        {
            get { return _model; }
            set { _model = value; }
        }

        public string ModelResourceName
        {
            get { return _modelResourceName; }
            set { _modelResourceName = value; }
        }

        #endregion

        #region Public Methods
        public void DrawModel()
        {
            //The camera isn't necessary... the camera things are constant. We will merely nee a reference to the camera.
            //MoritoFighterGame.MoritoFighterGameInstance.DisplayedMessages["in drawmodel1"] = "ships visualPosition: " + this.VisualPosition.ToString();
      
            foreach (ModelMesh mesh in ObjectModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    effect.World = Matrix.CreateRotationX(Rotation.X) 
                        * Matrix.CreateRotationY(Rotation.Y)
                        * Matrix.CreateRotationZ(Rotation.Z)
                        * Matrix.CreateTranslation(Position);
                    
                    effect.Projection = ReferenceCamera.CameraProjectionMatrix;
                    effect.View = ReferenceCamera.CameraViewMatrix;
                }
                mesh.Draw();
            }
        }

        public virtual void Draw()
        {
            DrawModel();
        }

        public virtual void LoadFromXElement(XElement root)
        {
            //this could probably be improved by making a static function for vector3... somehow lol... 
            //remember method extension =P x.x
            Position = new Vector3(); 
            Position = Position.loadFromXElement(root.Element("Position").Element("Vector3"));
            Origin = new Vector3(); 
            Origin = Origin.loadFromXElement(root.Element("Origin").Element("Vector3"));
            Rotation = new Vector3(); 
            Rotation = Rotation.loadFromXElement(root.Element("Rotation").Element("Vector3"));
            ModelResourceName = root.Element("ModelResourceName").Value;
        }

        public virtual XElement serializeToXElement()
        {
            return
                new XElement(ClassName,
                    new XElement("Position", Position.serializeToXElement()),
                    new XElement("Origin", Origin.serializeToXElement()),
                    new XElement("Rotation", Rotation.serializeToXElement()),
                    new XElement("ModelResourceName", ModelResourceName)
                );
        }
        #endregion
    }
}
