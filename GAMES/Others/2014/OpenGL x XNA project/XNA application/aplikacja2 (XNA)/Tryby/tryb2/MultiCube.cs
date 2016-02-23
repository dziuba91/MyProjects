using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace aplikacja2__XNA_.BasicComponent
{
    class MultiCube : DrawableGameComponent
    {
        #region Field

        const int NUM_TRIANGLES = 12;
        const int NUM_VERTICES = 36;


        public bool startGame = false;
        public bool startCubeRotation = true;

        private VertexPositionNormalTexture[][] _vertices;

        //private VertexBuffer _shapeBuffer;

        private bool _isConstructed = false;

        public int selectCubeNumber = -1;

        public Vector3 Size;
        //public Vector3 Position;
        //public Vector3 Texture { get; set; }

        private float CubeWidth = 1.0f;

        BasicEffect cubeEffect;

        float aspectRatio = 0.0f;

        public float positionStartY = 4.0f;
        public float rotationStartY = 0.0f;
        public float rotationStartX = 0.0f;

        Vector3 cameraPosition = new Vector3(0, 0, 10);
        Vector3 modelPositionZero = Vector3.Zero;
        Vector3 modelPosition = Vector3.Zero;


        int ilosc = 7;

        float odl = 3.0f;

        float speed = 2.0f;

        int maxIndex;

        #endregion


        #region Initialization

        public MultiCube(Game game, Vector3 size, Vector3 position)
            : base(game)
        {
            Size = size;
            //Position = position;
            _vertices = new VertexPositionNormalTexture[ilosc*ilosc*ilosc][];
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

            modelPosition = new Vector3(0.0f, positionStartY, -35.0f);

            cubeEffect.World = Matrix.CreateRotationY(MathHelper.ToRadians(rotationStartY)) *
                   Matrix.CreateRotationX(MathHelper.ToRadians(rotationStartX)) * Matrix.CreateTranslation(modelPosition);

            cubeEffect.View = Matrix.CreateLookAt(cameraPosition, modelPositionZero, Vector3.Up);

            cubeEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 1000.0f);

            cubeEffect.DiffuseColor = new Vector3 (0.0f,0.0f,1.0f);
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

        #endregion


        #region Private Methods

        public void RenderToDevice(GraphicsDevice device)
        {
            if (_isConstructed == false)
            {
                int index = 0;
                int pol = ilosc / 2;

                for (int k = 0; k < ilosc; k++)
                    for (int i = 0; i < ilosc; i++)
                        for (int j = 0; j < ilosc; j++)
                        {
                            int a = j;
                            if (a > pol)
                            {
                                a -= pol;
                                a *= -1;
                            }

                            int b = k;
                            if (b > pol)
                            {
                                b -= pol;
                                b *= -1;
                            }

                            int c = i;
                            if (c > pol)
                            {
                                c -= pol;
                                c *= -1;
                            }

                            ConstructCube(new Vector3(a * odl, b * odl, c * odl),index);
                            index++;
                        }

                maxIndex = index;
            }

            /*
            VertexBuffer buf = new VertexBuffer(
                    device,
                    VertexPositionNormalTexture.VertexDeclaration,
                    NUM_VERTICES *maxIndex,
                    BufferUsage.WriteOnly); 
             * */

            for (int i = 0; i < maxIndex; i++)
            {
                using (VertexBuffer buffer = new VertexBuffer(
                    device,
                    VertexPositionNormalTexture.VertexDeclaration,
                    NUM_VERTICES,
                    BufferUsage.WriteOnly))
                {
                        buffer.SetData(_vertices[i]);

                        device.SetVertexBuffer(buffer);
                        //device.
                        //buf.sbuffer;
                }

                //device.RenderState.CullMode = CullMode.None;
                device.DrawPrimitives(PrimitiveType.TriangleList, 0, NUM_TRIANGLES);
            }
        }

        private void ConstructCube(Vector3 Position, int index)
        {
            _vertices[index] = new VertexPositionNormalTexture[NUM_VERTICES];

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
            _vertices[index][0] = new VertexPositionNormalTexture(topLeftFront, normalFront, textureZero);
            _vertices[index][1] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureZero);
            _vertices[index][2] = new VertexPositionNormalTexture(topRightFront, normalFront, textureZero);
            _vertices[index][3] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureZero);
            _vertices[index][4] = new VertexPositionNormalTexture(btmRightFront, normalFront, textureZero);
            _vertices[index][5] = new VertexPositionNormalTexture(topRightFront, normalFront, textureZero);

            // Add the vertices for the BACK face.
            _vertices[index][6] = new VertexPositionNormalTexture(topLeftBack, normalBack, textureTopRight);
            _vertices[index][7] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);
            _vertices[index][8] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);
            _vertices[index][9] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);
            _vertices[index][10] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);
            _vertices[index][11] = new VertexPositionNormalTexture(btmRightBack, normalBack, textureBottomLeft);

            // Add the vertices for the TOP face.
            _vertices[index][12] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureZero);
            _vertices[index][13] = new VertexPositionNormalTexture(topRightBack, normalTop, textureZero);
            _vertices[index][14] = new VertexPositionNormalTexture(topLeftBack, normalTop, textureZero);
            _vertices[index][15] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureZero);
            _vertices[index][16] = new VertexPositionNormalTexture(topRightFront, normalTop, textureZero);
            _vertices[index][17] = new VertexPositionNormalTexture(topRightBack, normalTop, textureZero);

            // Add the vertices for the BOTTOM face. 
            _vertices[index][18] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureZero);
            _vertices[index][19] = new VertexPositionNormalTexture(btmLeftBack, normalBottom, textureZero);
            _vertices[index][20] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureZero);
            _vertices[index][21] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureZero);
            _vertices[index][22] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureZero);
            _vertices[index][23] = new VertexPositionNormalTexture(btmRightFront, normalBottom, textureZero);

            // Add the vertices for the LEFT face.
            _vertices[index][24] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureZero);
            _vertices[index][25] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureZero);
            _vertices[index][26] = new VertexPositionNormalTexture(btmLeftFront, normalLeft, textureZero);
            _vertices[index][27] = new VertexPositionNormalTexture(topLeftBack, normalLeft, textureZero);
            _vertices[index][28] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureZero);
            _vertices[index][29] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureZero);

            // Add the vertices for the RIGHT face. 
            _vertices[index][30] = new VertexPositionNormalTexture(topRightFront, normalRight, textureZero);
            _vertices[index][31] = new VertexPositionNormalTexture(btmRightFront, normalRight, textureZero);
            _vertices[index][32] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureZero);
            _vertices[index][33] = new VertexPositionNormalTexture(topRightBack, normalRight, textureZero);
            _vertices[index][34] = new VertexPositionNormalTexture(topRightFront, normalRight, textureZero);
            _vertices[index][35] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureZero);

            _isConstructed = true;
        }

        #endregion
    }
}
