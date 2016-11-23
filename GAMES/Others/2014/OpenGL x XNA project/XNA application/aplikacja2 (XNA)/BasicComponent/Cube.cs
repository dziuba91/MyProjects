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

        const int NUM_TRIANGLES = 12;
        const int NUM_VERTICES = 36;

        private VertexPositionNormalTexture[] _vertices;

        private bool _isConstructed = false;

        Vector3 size;
        Vector3 position;

        private float cubeWidth = 1.0f;

        BasicEffect cubeEffect;

        float aspectRatio = 0.0f;

        float positionStartY = 1.2f;
        float rotationStartY = 0.0f;
        float rotationStartX = 0.0f;

        Vector3 cameraPosition = new Vector3(0, 0, 10);
        Vector3 modelPositionZero = Vector3.Zero;
        Vector3 modelPosition = Vector3.Zero;

        public float speed { get; set; }
        public float speed1 { get; set; }
        public float speed2 { get; set; }

        #endregion


        #region Initialization

        public Cube(Game game, Vector3 size, Vector3 position)
            : base(game)
        {
            this.size = size;
            this.position = position;

            this.speed = 0.5f;
            this.speed1 = 0.5f;
            this.speed2 = 0.05f;
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

            cubeWidth = 1.0f;
            size.X = cubeWidth;

            modelPosition = new Vector3(0, positionStartY, 0);

            cubeEffect.World = Matrix.CreateRotationY(MathHelper.ToRadians(rotationStartY)) *
                   Matrix.CreateRotationX(MathHelper.ToRadians(rotationStartX)) * Matrix.CreateTranslation(modelPosition);

            cubeEffect.View = Matrix.CreateLookAt(cameraPosition, modelPositionZero, Vector3.Up);

            cubeEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 1000.0f);

            cubeEffect.DiffuseColor = new Vector3 (0.0f,0.0f,1.0f);

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

        public void Draw(GameTime gameTime, int tryb, float posX, float posY, float posZ)
        {
            cubeWidth = 1.0f;
            size.X = cubeWidth;

            modelPosition = new Vector3(posX, posY+ 1.2f, posZ);

            cubeEffect.World = Matrix.CreateRotationY(MathHelper.ToRadians(rotationStartY)) *
                   Matrix.CreateRotationX(MathHelper.ToRadians(rotationStartX)) * Matrix.CreateTranslation(modelPosition);

            cubeEffect.View = Matrix.CreateLookAt(cameraPosition, modelPositionZero, Vector3.Up);

            cubeEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 1000.0f);

            cubeEffect.DiffuseColor = new Vector3(0.0f, 0.0f, 1.0f);

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
            Vector3 topLeftFront = position + new Vector3(-1.0f, 1.0f, -1.0f) * size;
            Vector3 topLeftBack = position + new Vector3(-1.0f, 1.0f, 1.0f) * size;
            Vector3 topRightFront = position + new Vector3(1.0f, 1.0f, -1.0f) * size;
            Vector3 topRightBack = position + new Vector3(1.0f, 1.0f, 1.0f) * size;

            // Calculate the position of the vertices on the bottom face.
            Vector3 btmLeftFront = position + new Vector3(-1.0f, -1.0f, -1.0f) * size;
            Vector3 btmLeftBack = position + new Vector3(-1.0f, -1.0f, 1.0f) * size;
            Vector3 btmRightFront = position + new Vector3(1.0f, -1.0f, -1.0f) * size;
            Vector3 btmRightBack = position + new Vector3(1.0f, -1.0f, 1.0f) * size;

            // Normal vectors for each face (needed for lighting / display)
            Vector3 normalFront = new Vector3(0.0f, 0.0f, 1.0f) * size;
            Vector3 normalBack = new Vector3(0.0f, 0.0f, -1.0f) * size;
            Vector3 normalTop = new Vector3(0.0f, 1.0f, 0.0f) * size;
            Vector3 normalBottom = new Vector3(0.0f, -1.0f, 0.0f) * size;
            Vector3 normalLeft = new Vector3(-1.0f, 0.0f, 0.0f) * size;
            Vector3 normalRight = new Vector3(1.0f, 0.0f, 0.0f) * size;

            // UV texture coordinates
            Vector2 textureTopLeft = new Vector2((1.0f / cubeWidth) * size.X, 0.0f * size.Y);
            Vector2 textureTopRight = new Vector2(0.0f * size.X, 0.0f * size.Y);
            Vector2 textureBottomLeft = new Vector2((1.0f / cubeWidth) * size.X, 1.0f * size.Y);
            Vector2 textureBottomRight = new Vector2(0.0f * size.X, 1.0f * size.Y);
            Vector2 textureZero = new Vector2(0.0f, 0.0f);

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
