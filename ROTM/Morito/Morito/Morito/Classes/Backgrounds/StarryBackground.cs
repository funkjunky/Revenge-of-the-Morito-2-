using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Morito.Classes.Backgrounds
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class StarryBackground : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region Variables

        int iNumStars;
        GraphicsDevice graphics;
        const int DefaultBufferSize = 500;
        VertexDeclaration vertexDeclaration;
        BasicEffect basicEffect;
        VertexPositionColor[] vertices = new VertexPositionColor[DefaultBufferSize];
        Random rand = new Random();

        #endregion

        public StarryBackground(Game game, GraphicsDeviceManager graphics)
            : base(game)
        {
           // this.graphics1 = graphics;
        }


        public StarryBackground(Game game)
            : base(game)
        {
            this.graphics = game.GraphicsDevice;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            int iWidth = graphics.Viewport.Width;
            int iHeight = graphics.Viewport.Height;

            for (int i = 0; i < DefaultBufferSize; i++)
            {
                vertices[i] = new VertexPositionColor();
                vertices[i].Position.X = rand.Next(iWidth);
                vertices[i].Position.Y = rand.Next(iHeight);
                vertices[i].Color = Color.White; //new Color(128, 128, 128);
            }
            graphics.RenderState.PointSize = 1;

            // create a vertex declaration, which tells the graphics card what kind of
            // data to expect during a draw call. We're drawing using
            // VertexPositionColors, so we'll use those vertex elements.
            vertexDeclaration = new VertexDeclaration(graphics,
                VertexPositionColor.VertexElements);

            // set up a new basic effect, and enable vertex colors.
            basicEffect = new BasicEffect(graphics, null);
            basicEffect.VertexColorEnabled = true;

            // projection uses CreateOrthographicOffCenter to create 2d projection
            // matrix with 0,0 in the upper left.
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter
                (0, iWidth,
                iHeight, 0,
                0, 1);


            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            byte iRGB;
            for (int i = 0; i < DefaultBufferSize; i++)
                if ((rand.Next() % 88) == 0)
                {
                    iRGB = (byte)(rand.Next(256));
                    vertices[i].Color = new Color(iRGB, iRGB, iRGB);
                }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {



            graphics.VertexDeclaration = vertexDeclaration;
            basicEffect.Begin();
            basicEffect.CurrentTechnique.Passes[0].Begin();
            // Draw the stars
            graphics.DrawUserPrimitives(PrimitiveType.PointList, vertices, 0, vertices.Length);
            basicEffect.CurrentTechnique.Passes[0].End();

            basicEffect.End();

            base.Game.GraphicsDevice.RenderState.DepthBufferEnable = true;
            base.Draw(gameTime);
        }

    }
}