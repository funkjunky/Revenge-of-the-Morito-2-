using Microsoft.Xna.Framework;

//AFTER GETTING 3D OBJECT TO WORK. 
//MOVE ALL CAMERA STUFF TO HERE, AND GIVE THE GAME A CAMERA, 
//AND THEN THE 3D OBJECT CAN USE THE CAMERA FROM THE GAME. =)

namespace Morito
{
    public class Camera
    {
        #region Members
        protected Vector3 _cameraPosition = new Vector3(0f, 0f, 160f);  //default 160 points back so you can see things.
        protected Matrix _cameraProjectionMatrix;
        protected Matrix _cameraViewMatrix;
        #endregion

        #region Constructors
        public Camera() {
            CameraProjectionMatrix = Matrix.CreatePerspectiveFieldOfView
                (MathHelper.ToRadians(45.0f),
                MoritoFighterGame.MoritoFighterGameInstance.Graphics.GraphicsDevice.Viewport.AspectRatio,
                1.0f, 500.0f);
            //MoritoFighterGame.MoritoFighterGameInstance.DisplayedMessages["Camera"] = "cam pos: " + CameraPosition;
            CameraViewMatrix = Matrix.CreateLookAt(
                CameraPosition,
                new Vector3(0.0f, 0.0f, 0.0f),
                Vector3.Up);
        }
        #endregion

        #region Public Methods

        //WARNING: this functions are super sketch. They use numbers, which I have no idea where they came from.
        public Vector3 Relative2Dto3D(Vector2 v2)
        {
            //return new Vector2();
            float height = (float)MoritoFighterGame.MoritoFighterGameInstance.Graphics.GraphicsDevice.Viewport.Height;
            float width = (float)MoritoFighterGame.MoritoFighterGameInstance.Graphics.GraphicsDevice.Viewport.Width;

            float aspectRatio = MoritoFighterGame.MoritoFighterGameInstance.Graphics.GraphicsDevice.Viewport.AspectRatio;
            MoritoFighterGame.MoritoFighterGameInstance.DisplayedMessages["asp"] = "aspect ratio: " + aspectRatio;
            //ok, so i'm not exactly sure where 10/12 comes from, perhaps aspect ratio.
            float normX = (v2.X - width/2)/ height * (10f/12f);
            float normY = -1f * (v2.Y - height / 2) / height *(10f / 12f);

            return new Vector3(normX*CameraPosition.Z, normY*CameraPosition.Z, 0f); //use cameraposition.z not 160
        }

        public Vector2 Relative3Dto2D(Vector2 v2)
        {
            //return new Vector2();
            float height = (float)MoritoFighterGame.MoritoFighterGameInstance.Graphics.GraphicsDevice.Viewport.Height;
            float width = (float)MoritoFighterGame.MoritoFighterGameInstance.Graphics.GraphicsDevice.Viewport.Width;

            float aspectRatio = MoritoFighterGame.MoritoFighterGameInstance.Graphics.GraphicsDevice.Viewport.AspectRatio;
            MoritoFighterGame.MoritoFighterGameInstance.DisplayedMessages["asp"] = "aspect ratio: " + aspectRatio;

            //Further I have no idea where this 0.3333f*v2.X
            float normX = (width / 2) +  ((v2.X * width * (10f / 12f)) / CameraPosition.Z) + 0.3333f*v2.X;
            float normY = (height / 2) + -1f * v2.Y * height * (12f / 10f) / CameraPosition.Z;

            return new Vector2(normX, normY);
        }

        #endregion

        #region Properties
        public Vector3 CameraPosition
        {
            get
            {
                return _cameraPosition;
            }
            set
            {
                _cameraPosition = value;
            }
        }
        public Matrix CameraProjectionMatrix
        {
            get { return _cameraProjectionMatrix; }
            set { _cameraProjectionMatrix = value; }
        }
        public Matrix CameraViewMatrix
        {
            get { return _cameraViewMatrix; }
            set { _cameraViewMatrix = value; }
        }
        #endregion
    }
}
