using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace aplikacja2__XNA_.BasicComponent
{
    class Cube : DrawableGameComponent
    {
        #region Field


        public float speed = 0.5f;
        public float speed1 = 0.5f;
        public float speed2 = 0.05f;

        const int NUM_TRIANGLES = 4;
        const int NUM_VERTICES = 36;


        public bool startGame = false;
        public bool startCubeRotation = true;

        private VertexPositionNormalTexture[] _vertices;

        //private VertexBuffer _shapeBuffer;

        private bool _isConstructed = false;

        public int selectCubeNumber = -1;

        public Vector3 Size;
        public Vector3 Position;
        //public Vector3 Texture { get; set; }

        private float CubeWidth = 1.0f;

        BasicEffect cubeEffect;

        float aspectRatio = 0.0f;

        public float positionStartY = 1.2f;
        public float rotationStartY = 0.0f;
        public float rotationStartX = 0.0f;

        Vector3 cameraPosition = new Vector3(0, 0, 10);
        Vector3 modelPositionZero = Vector3.Zero;
        Vector3 modelPosition = Vector3.Zero;

        Effect beffect;

        #endregion


        #region Initialization

        public Cube(Game game, Vector3 size, Vector3 position)
            : base(game)
        {
            Size = size;
            Position = position;
        }

        public SpriteBatch spriteBatch
        {
            get
            {
                return (SpriteBatch)this.Game.Services.GetService(typeof(SpriteBatch));
            }
        }

        #endregion


        #region BasicComponentMethod

        public override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            aspectRatio = GraphicsDevice.Viewport.AspectRatio;
            cubeEffect = new BasicEffect(GraphicsDevice);

                        //beffect = Game.Content.Load<Effect>("metal");
        }

        public void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, int tryb)
        {
            if (tryb == 1)
                rotationStartY += speed;
            else if (tryb == 2)
                rotationStartY -= speed;
            else if (tryb == 3)
                rotationStartX += speed;
            else if (tryb == 4)
                rotationStartX -= speed;

            CubeWidth = 1.0f;
            Size.X = CubeWidth;

            modelPosition = new Vector3(0, positionStartY, 0);

            cubeEffect.World = Matrix.CreateRotationY(MathHelper.ToRadians(rotationStartY)) *
                   Matrix.CreateRotationX(MathHelper.ToRadians(rotationStartX)) * Matrix.CreateTranslation(modelPosition);

            cubeEffect.View = Matrix.CreateLookAt(cameraPosition, modelPositionZero, Vector3.Up);

            cubeEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 1000.0f);

            cubeEffect.DiffuseColor = new Vector3 (0.0f,0.0f,1.0f);
            //cubeEffect.DiffuseColor = new Vector3(0.5f, 0.5f, 0.5f);
            //cubeEffect.TextureEnabled = true;

            //cubeEffect.Texture = start;

            cubeEffect.EnableDefaultLighting();

            cubeEffect.SpecularColor = new Vector3(0.0f);
            cubeEffect.AmbientLightColor = new Vector3(0.25f);

            //cubeEffect.SpecularPower = 0;
            //cubeEffect.EmissiveColor = new Vector3(0.0f);
            //cubeEffect.DirectionalLight0.Enabled = false;
            //cubeEffect.DirectionalLight1.Enabled = false;
            //cubeEffect.DirectionalLight2.Enabled = false;

            /*
            Matrix InverseWorldMatrix = Matrix.Invert(cubeEffect.World);
            Matrix ViewInverse = Matrix.Invert(cubeEffect.View);

            //effect.CurrentTechnique = beffect.Techniques["Technique1"];
            beffect.Parameters["gWorldXf"].SetValue(cubeEffect.World);
            beffect.Parameters["gWorldITXf"].SetValue(InverseWorldMatrix);
            beffect.Parameters["gWvpXf"].SetValue(cubeEffect.World * cubeEffect.View * cubeEffect.Projection);
            beffect.Parameters["gViewIXf"].SetValue(ViewInverse);
            */
            foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                RenderToDevice(GraphicsDevice);
            }

            base.Draw(gameTime);
        }

        public void Draw(GameTime gameTime, int tryb, float posX, float posY, float posZ)
        {
            /*
            if (tryb == 1)
                rotationStartY += 0.05f;
            else if (tryb == 2)
                rotationStartY -= 0.05f;
            else if (tryb == 3)
                rotationStartX += 0.05f;
            else if (tryb == 4)
                rotationStartX -= 0.05f;
            */

            CubeWidth = 1.0f;
            Size.X = CubeWidth;

            modelPosition = new Vector3(posX, posY+ 1.2f, posZ);

            cubeEffect.World = Matrix.CreateRotationY(MathHelper.ToRadians(rotationStartY)) *
                   Matrix.CreateRotationX(MathHelper.ToRadians(rotationStartX)) * Matrix.CreateTranslation(modelPosition);

            cubeEffect.View = Matrix.CreateLookAt(cameraPosition, modelPositionZero, Vector3.Up);

            cubeEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 1000.0f);

            cubeEffect.DiffuseColor = new Vector3(0.0f, 0.0f, 1.0f);
            //cubeEffect.TextureEnabled = true;

            //cubeEffect.Texture = start;

            cubeEffect.EnableDefaultLighting();

            cubeEffect.SpecularColor = new Vector3(0.0f);
            cubeEffect.AmbientLightColor = new Vector3(0.25f);


            foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                RenderToDevice(GraphicsDevice);
            }

            base.Draw(gameTime);
        }

        /*
        public void Draw(GameTime gameTime, MouseState d, Vector2 position)
        {

            base.Draw(gameTime);
        }
         * */

        #endregion


        #region Private Methods

        public void RenderToDevice(GraphicsDevice device)
        {
            if (_isConstructed == false)
                ConstructCube();

            using (VertexBuffer buffer = new VertexBuffer(
                device,
                VertexPositionNormalTexture.VertexDeclaration,
                NUM_VERTICES,
                BufferUsage.WriteOnly))
            {
                buffer.SetData(_vertices);

                device.SetVertexBuffer(buffer);
            }

            device.DrawPrimitives(PrimitiveType.TriangleList, 0, NUM_TRIANGLES);
        }

        private void ConstructCube()
        {
            _vertices = new VertexPositionNormalTexture[NUM_VERTICES];

            // Calculate the position of the vertices on the top face.
            Vector3 topLeftFront = Position + new Vector3(-1.0f, 1.0f, -1.0f) * Size;
            Vector3 topLeftBack = Position + new Vector3(-1.0f, 1.0f, 1.0f) * Size;
            Vector3 topRightFront = Position + new Vector3(1.0f, 1.0f, -1.0f) * Size;
            Vector3 topRightBack = Position + new Vector3(1.0f, 1.0f, 1.0f) * Size;

            // Calculate the position of the vertices on the bottom face.
            Vector3 btmLeftFront = Position + new Vector3(-1.0f, -1.0f, -1.0f) * Size;
            Vector3 btmLeftBack = Position + new Vector3(-1.0f, -1.0f, 1.0f) * Size;
            Vector3 btmRightFront = Position + new Vector3(1.0f, -1.0f, -1.0f) * Size;
            Vector3 btmRightBack = Position + new Vector3(1.0f, -1.0f, 1.0f) * Size;

            // Normal vectors for each face (needed for lighting / display)
            Vector3 normalFront = new Vector3(0.0f, 0.0f, 1.0f) * Size;
            Vector3 normalBack = new Vector3(0.0f, 0.0f, -1.0f) * Size;
            Vector3 normalTop = new Vector3(0.0f, 1.0f, 0.0f) * Size;
            Vector3 normalBottom = new Vector3(0.0f, -1.0f, 0.0f) * Size;
            Vector3 normalLeft = new Vector3(-1.0f, 0.0f, 0.0f) * Size;
            Vector3 normalRight = new Vector3(1.0f, 0.0f, 0.0f) * Size;

            // UV texture coordinates
            Vector2 textureTopLeft = new Vector2((1.0f / CubeWidth) * Size.X, 0.0f * Size.Y);
            Vector2 textureTopRight = new Vector2(0.0f * Size.X, 0.0f * Size.Y);
            Vector2 textureBottomLeft = new Vector2((1.0f / CubeWidth) * Size.X, 1.0f * Size.Y);
            Vector2 textureBottomRight = new Vector2(0.0f * Size.X, 1.0f * Size.Y);
            Vector2 textureZero = new Vector2(0.0f, 0.0f);


            #region vertex comented
            /*
            // Add the vertices for the FRONT face.
            _vertices[0] = new VertexPositionNormalTexture(topLeftFront, normalFront, textureTopLeft);
            _vertices[1] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureBottomLeft);
            _vertices[2] = new VertexPositionNormalTexture(topRightFront, normalFront, textureTopRight);
            _vertices[3] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureBottomLeft);
            _vertices[4] = new VertexPositionNormalTexture(btmRightFront, normalFront, textureBottomRight);
            _vertices[5] = new VertexPositionNormalTexture(topRightFront, normalFront, textureTopRight);
             
            // Add the vertices for the BACK face.
            _vertices[6] = new VertexPositionNormalTexture(topLeftBack, normalBack, textureTopRight);
            _vertices[7] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);
            _vertices[8] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);
            _vertices[9] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);
            _vertices[10] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);
            _vertices[11] = new VertexPositionNormalTexture(btmRightBack, normalBack, textureBottomLeft);

            // Add the vertices for the TOP face.
            _vertices[12] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureBottomLeft);
            _vertices[13] = new VertexPositionNormalTexture(topRightBack, normalTop, textureTopRight);
            _vertices[14] = new VertexPositionNormalTexture(topLeftBack, normalTop, textureTopLeft);
            _vertices[15] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureBottomLeft);
            _vertices[16] = new VertexPositionNormalTexture(topRightFront, normalTop, textureBottomRight);
            _vertices[17] = new VertexPositionNormalTexture(topRightBack, normalTop, textureTopRight);

            // Add the vertices for the BOTTOM face. 
            _vertices[18] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureTopLeft);
            _vertices[19] = new VertexPositionNormalTexture(btmLeftBack, normalBottom, textureBottomLeft);
            _vertices[20] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureBottomRight);
            _vertices[21] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureTopLeft);
            _vertices[22] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureBottomRight);
            _vertices[23] = new VertexPositionNormalTexture(btmRightFront, normalBottom, textureTopRight);

            // Add the vertices for the LEFT face.
            _vertices[24] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureTopRight);
            _vertices[25] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureBottomLeft);
            _vertices[26] = new VertexPositionNormalTexture(btmLeftFront, normalLeft, textureBottomRight);
            _vertices[27] = new VertexPositionNormalTexture(topLeftBack, normalLeft, textureTopLeft);
            _vertices[28] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureBottomLeft);
            _vertices[29] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureTopRight);

            // Add the vertices for the RIGHT face. 
            _vertices[30] = new VertexPositionNormalTexture(topRightFront, normalRight, textureTopLeft);
            _vertices[31] = new VertexPositionNormalTexture(btmRightFront, normalRight, textureBottomLeft);
            _vertices[32] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureBottomRight);
            _vertices[33] = new VertexPositionNormalTexture(topRightBack, normalRight, textureTopRight);
            _vertices[34] = new VertexPositionNormalTexture(topRightFront, normalRight, textureTopLeft);
            _vertices[35] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureBottomRight);
            * */
            #endregion


            // Add the vertices for the FRONT face.
            _vertices[0] = new VertexPositionNormalTexture(topLeftFront, normalFront, textureZero);
            _vertices[1] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureZero);
            _vertices[2] = new VertexPositionNormalTexture(topRightFront, normalFront, textureZero);
            _vertices[3] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureZero);
            _vertices[4] = new VertexPositionNormalTexture(btmRightFront, normalFront, textureZero);
            _vertices[5] = new VertexPositionNormalTexture(topRightFront, normalFront, textureZero);

            // Add the vertices for the BACK face.
            _vertices[6] = new VertexPositionNormalTexture(topLeftBack, normalBack, textureTopRight);
            _vertices[7] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);
            _vertices[8] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);
            _vertices[9] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);
            _vertices[10] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);
            _vertices[11] = new VertexPositionNormalTexture(btmRightBack, normalBack, textureBottomLeft);

            // Add the vertices for the TOP face.
            _vertices[12] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureZero);
            _vertices[13] = new VertexPositionNormalTexture(topRightBack, normalTop, textureZero);
            _vertices[14] = new VertexPositionNormalTexture(topLeftBack, normalTop, textureZero);
            _vertices[15] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureZero);
            _vertices[16] = new VertexPositionNormalTexture(topRightFront, normalTop, textureZero);
            _vertices[17] = new VertexPositionNormalTexture(topRightBack, normalTop, textureZero);

            // Add the vertices for the BOTTOM face. 
            _vertices[18] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureZero);
            _vertices[19] = new VertexPositionNormalTexture(btmLeftBack, normalBottom, textureZero);
            _vertices[20] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureZero);
            _vertices[21] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureZero);
            _vertices[22] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureZero);
            _vertices[23] = new VertexPositionNormalTexture(btmRightFront, normalBottom, textureZero);

            // Add the vertices for the LEFT face.
            _vertices[24] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureZero);
            _vertices[25] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureZero);
            _vertices[26] = new VertexPositionNormalTexture(btmLeftFront, normalLeft, textureZero);
            _vertices[27] = new VertexPositionNormalTexture(topLeftBack, normalLeft, textureZero);
            _vertices[28] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureZero);
            _vertices[29] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureZero);

            // Add the vertices for the RIGHT face. 
            _vertices[30] = new VertexPositionNormalTexture(topRightFront, normalRight, textureZero);
            _vertices[31] = new VertexPositionNormalTexture(btmRightFront, normalRight, textureZero);
            _vertices[32] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureZero);
            _vertices[33] = new VertexPositionNormalTexture(topRightBack, normalRight, textureZero);
            _vertices[34] = new VertexPositionNormalTexture(topRightFront, normalRight, textureZero);
            _vertices[35] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureZero);

            _isConstructed = true;
        }

        #endregion
    }
}
