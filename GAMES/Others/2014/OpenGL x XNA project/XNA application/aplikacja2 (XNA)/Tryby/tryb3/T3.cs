using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using aplikacja2__XNA_.BasicComponent;

namespace aplikacja2__XNA_.Tryby.tryb1
{
    class T3 : DrawableGameComponent
    {
        #region Field

        const int NUM_TRIANGLES = 12;
        const int NUM_VERTICES = 36;

        private VertexPositionNormalTexture[] _vertices;

        public Vector3 Size;
        public Vector3 Position;

        private float CubeWidth = 1.0f;

        private bool _isConstructed = false;


        private int tryb = 1;

        KeyboardState currentKeyboard;
        KeyboardState previousKeyboard;


        BasicEffect cubeEffect;

        float aspectRatio = 0.0f;

        private Model _model;


        //Vector3 cameraPosition = new Vector3(10.0f, 10.0f, 10.0f);
        //Vector3 cameraPosition3 = new Vector3(0, 0, 10);

        float odl = 2.0f;

        public float positionStartY = 1.0f;

        float modelRotation;


        //public float positionStartY = 4.0f;
        public float rotationStartY = 0.0f;
        public float rotationStartX = 0.0f;

        Vector3 cameraPosition = new Vector3(0, 0, 10);
        Vector3 modelPositionZero = Vector3.Zero;
        Vector3 modelPosition = Vector3.Zero;
        Vector3 modelPosition0 = Vector3.Zero;

        //float odl = 2.5f;

        public float speed = 0.7f;
        public float speed1 = 0.7f;
        public float speed2 = 0.05f;

        //int tryb = 1;

        public bool fog = false;

        Texture2D tex;

        #endregion


        #region Initialization

        public T3(Game game)
            : base(game)
        {
            Size = new Vector3(1.0f, 1.0f, 1.0f);
            Position = Vector3.Zero;
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

            //add
            _model = Game.Content.Load<Model>("textured_mode/texturedCube");
            tex = Game.Content.Load<Texture2D>("textured_mode/texture");
            //aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
        }

        public void Update(GameTime gameTime)
        {
            currentKeyboard = Keyboard.GetState();

            if (this.currentKeyboard.IsKeyDown(Keys.Right))
            {
                if (!this.previousKeyboard.IsKeyDown(Keys.Right))
                {
                    tryb = 1;
                }
            }

            if (this.currentKeyboard.IsKeyDown(Keys.Left))
            {
                if (!this.previousKeyboard.IsKeyDown(Keys.Left))
                {
                    tryb = 2;
                }
            }

            if (this.currentKeyboard.IsKeyDown(Keys.Down))
            {
                if (!this.previousKeyboard.IsKeyDown(Keys.Down))
                {
                    tryb = 3;
                }
            }

            if (this.currentKeyboard.IsKeyDown(Keys.Up))
            {
                if (!this.previousKeyboard.IsKeyDown(Keys.Up))
                {
                    tryb = 4;
                }
            }

            if (this.currentKeyboard.IsKeyDown(Keys.F))
            {
                if (!this.previousKeyboard.IsKeyDown(Keys.F))
                {
                    if (fog) fog = false;
                    else fog = true;
                }
            }

            previousKeyboard = currentKeyboard;

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            if (tryb == 1)
                rotationStartY += speed;
            else if (tryb == 2)
                rotationStartY -= speed;
            else if (tryb == 3)
                rotationStartX += speed;
            else if (tryb == 4)
                rotationStartX -= speed;

            foreach (ModelMesh mesh in _model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //effect.DiffuseColor = new Vector3(0.0f, 0.0f, 1.0f);

                    effect.EnableDefaultLighting();

                    effect.SpecularColor = new Vector3(0.0f);
                    effect.AmbientLightColor = new Vector3(0.5f);

                    if (fog)
                    {
                        effect.FogEnabled = true;
                        effect.FogColor = Color.Gray.ToVector3(); // For best results, ake this color whatever your background is.
                        effect.FogStart = 7.0f;
                        effect.FogEnd = 10.0f;
                    }
                    else effect.FogEnabled = false;

                    modelPosition0 = new Vector3(0.0f, positionStartY, 0.0f);

                    effect.World =
                        Matrix.CreateRotationY(MathHelper.ToRadians(rotationStartY)) *
                        Matrix.CreateRotationX(MathHelper.ToRadians(rotationStartX));

                    effect.World *= Matrix.CreateTranslation(modelPosition0);


                    effect.View = Matrix.CreateLookAt(cameraPosition, modelPositionZero, Vector3.Up);

                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 1000.0f);
                }

                mesh.Draw();
            }

            base.Draw(gameTime);
        }

        public void Draw2(GameTime gameTime)
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

            //cubeEffect.DiffuseColor = new Vector3(0.0f, 0.0f, 1.0f);
            //cubeEffect.DiffuseColor = new Vector3(0.5f, 0.5f, 0.5f);
            cubeEffect.TextureEnabled = true;

            cubeEffect.Texture = tex;

            cubeEffect.EnableDefaultLighting();

            cubeEffect.SpecularColor = new Vector3(0.0f);
            cubeEffect.AmbientLightColor = new Vector3(0.5f);

            if (fog)
            {
                cubeEffect.FogEnabled = true;
                cubeEffect.FogColor = Color.Gray.ToVector3(); // For best results, ake this color whatever your background is.
                cubeEffect.FogStart = 7.0f;
                cubeEffect.FogEnd = 10.0f;
            }
            else cubeEffect.FogEnabled = false;

            foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                RenderToDevice(GraphicsDevice);
            }

            base.Draw(gameTime);
        }

        #endregion


        #region Private Methods

        /*
        private void DrawWithMetalEffect(Model model, Matrix world, Matrix view, Matrix proj, BasicEffect effect)
        {
            Matrix InverseWorldMatrix = Matrix.Invert(world);
            Matrix ViewInverse = Matrix.Invert(view);

            effect.CurrentTechnique = effect.Techniques["Simple"];
            effect.Parameters["gWorldXf"].SetValue(world);
            effect.Parameters["gWorldITXf"].SetValue(InverseWorldMatrix);
            effect.Parameters["gWvpXf"].SetValue(world * view * proj);
            effect.Parameters["gViewIXf"].SetValue(ViewInverse);

            foreach (ModelMesh meshes in model.Meshes)
            {
                foreach (ModelMeshPart parts in meshes.MeshParts)
                    parts.Effect = effect;
                meshes.Draw();
            }
        }
         * */

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

            #endregion

            /*
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
            */

            _isConstructed = true;
        }

        #endregion
    }
}
