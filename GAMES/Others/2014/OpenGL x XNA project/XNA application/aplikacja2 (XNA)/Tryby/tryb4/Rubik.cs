using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using aplikacja2__XNA_.Tryby;

namespace aplikacja2__XNA_.BasicComponent
{
	class Rubik : DrawableGameComponent
	{
		#region Field

		const int NUM_TRIANGLES = 12;
		const int NUM_VERTICES = 36;

		private VertexPositionNormalTexture[][] _vertices1;
		private VertexPositionNormalTexture[][] _vertices2;
		private VertexPositionNormalTexture[][] _vertices3;
		private VertexPositionNormalTexture[][] _vertices4;
		private VertexPositionNormalTexture[][] _vertices5;
		private VertexPositionNormalTexture[][] _vertices6;

		private bool _isConstructed = false;

		Vector3 size;

		private float cubeWidth = 1.0f;

		BasicEffect cubeEffect;

		float aspectRatio = 0.0f;

		float positionStartY = 4.0f;
		float rotationStartY = 0.0f;
		float rotationStartX = 0.0f;

		Vector3 cameraPosition = new Vector3(0, 0, 10);
		Vector3 modelPositionZero = Vector3.Zero;
		Vector3 modelPosition = Vector3.Zero;
		Vector3 modelPosition0 = Vector3.Zero;

		int ilosc = 3;
		float odl = 2.5f;

		int maxIndex;

		int tryb = 1;

		//
		private Model _model;

		Vector3 cameraPosition2 = new Vector3(10.0f, 10.0f, 10.0f);

		float odl2 = 2.0f;
		float positionStartY2 = 2.0f;

		KeyboardState currentKeyboard;
		KeyboardState previousKeyboard;

		bool rotationON = true;

		bool sim = false;

		public float speed { get; set; }
		public float speed1 { get; set; }
		public float speed2 { get; set; }

		public RubikCubeHelper help { get; private set; }

		#endregion


		#region Initialization

		public Rubik(Game game, Vector3 size, Vector3 position)
			: base(game)
		{
			this.size = size;

			this.speed = 0.7f;
			this.speed1 = 0.7f;
			this.speed2 = 0.07f;

			_vertices1 = new VertexPositionNormalTexture[ilosc * ilosc * ilosc][];
			_vertices2 = new VertexPositionNormalTexture[ilosc * ilosc * ilosc][];
			_vertices3 = new VertexPositionNormalTexture[ilosc * ilosc * ilosc][];
			_vertices4 = new VertexPositionNormalTexture[ilosc * ilosc * ilosc][];
			_vertices5 = new VertexPositionNormalTexture[ilosc * ilosc * ilosc][];
			_vertices6 = new VertexPositionNormalTexture[ilosc * ilosc * ilosc][];

			help = new RubikCubeHelper();
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

		protected override void LoadContent()
		{
			aspectRatio = GraphicsDevice.Viewport.AspectRatio;
			cubeEffect = new BasicEffect(GraphicsDevice);

			_model = Game.Content.Load<Model>("model/rubik");
		}

		Matrix Tryb4(int x)
		{
			if (x == 10)
				return Matrix.CreateRotationY(MathHelper.ToRadians(-90.0f));

			else if (x == 20)
				return Matrix.CreateRotationX(MathHelper.ToRadians(-90.0f));

			else if (x == 30)
				return Matrix.CreateRotationZ(MathHelper.ToRadians(-90.0f));

			else if (x == 40)
				return Matrix.CreateRotationY(MathHelper.ToRadians(90.0f));

			else if (x == 50)
				return Matrix.CreateRotationX(MathHelper.ToRadians(90.0f));

			else if (x == 60)
				return Matrix.CreateRotationZ(MathHelper.ToRadians(90.0f));

			return Matrix.CreateRotationY(MathHelper.ToRadians(-90.0f));
		}

		public void Update(GameTime gameTime)
		{
			currentKeyboard = Keyboard.GetState();

			if (this.currentKeyboard.IsKeyDown(Keys.D0))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.D0))
				{
					if (rotationON) rotationON = false;
					else rotationON = true;
				}
			}

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

			if (this.currentKeyboard.IsKeyDown(Keys.O))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.O))
				{
					if (sim) sim = false;
					else sim = true;
				}
			}

			if (help.gameKeyboard(currentKeyboard, previousKeyboard)) { tryb = 0; };
			if (sim) help.simulation();

			previousKeyboard = currentKeyboard;

			base.Update(gameTime);
		}

		public void Draw1(GameTime gameTime)
		{
			help.Rotate();

			int licznik = 0;
			float pol = ilosc / 2;

			if (rotationON)
			if (tryb == 1)
				rotationStartY += speed;
			else if (tryb == 2)
				rotationStartY -= speed;
			else if (tryb == 3)
				rotationStartX += speed;
			else if (tryb == 4)
				rotationStartX -= speed;

			// Draw the model.
			for (int k = 0; k < ilosc; k++)
					for (int i = 0; i < ilosc; i++)
						for (int j = 0; j < ilosc; j++)
						{
							float a = j;
							if (a > pol)
							{
								a -= pol;
								a *= -1;
							}

							float b = k;
							if (b > pol)
							{
								b -= pol;
								b *= -1;
							}

							float c = i;
							if (c > pol)
							{
								c -= pol;
								c *= -1;
							}

							float rot1 = 0;
							float rot2 = 0;
							float rot3 = 0;
							int s = 0;
							int x = 0;

							for (; s < 27; s++)
							{
								if (k == help.numb[s, 0] && j == help.numb[s, 1] && i == help.numb[s, 2])
								{
									rot1 = help.rot[s, 2];
									rot2 = help.rot[s, 1];
									rot3 = help.rot[s, 0];

									x = s;
								}
								licznik++;
							}

							foreach (ModelMesh mesh in _model.Meshes)
							{
								foreach (BasicEffect effect in mesh.Effects)
								{
									effect.EnableDefaultLighting();

									modelPosition0 = new Vector3(0.0f, positionStartY2, -10.0f);
									modelPosition = new Vector3(a*odl2, b * odl2, c*odl2);

									effect.World = Matrix.CreateTranslation(modelPosition);

									for (int z = 0; z < help.licznik[x]; z++)
									{
										effect.World *= Tryb4(help.rot_nr[x, z]);
									}

									effect.World *=
										Matrix.CreateRotationX(MathHelper.ToRadians(rot1)) *
										Matrix.CreateRotationY(MathHelper.ToRadians(rot2)) *
										Matrix.CreateRotationZ(MathHelper.ToRadians(rot3));

									effect.World *=
										Matrix.CreateRotationY(MathHelper.ToRadians(rotationStartY)) *
										Matrix.CreateRotationX(MathHelper.ToRadians(rotationStartX));

									effect.World *=
										Matrix.CreateTranslation(modelPosition0);

									effect.View = Matrix.CreateLookAt(cameraPosition, modelPositionZero, Vector3.Up);

									effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 1000.0f);
								}

								mesh.Draw();
							} 
						}

			base.Draw(gameTime);
		}

		public void Draw2(GameTime gameTime)
		{
			cubeWidth = 1.0f;
			size.X = cubeWidth;

			modelPosition = new Vector3(0.0f, positionStartY, -25.0f);

			cubeEffect.World = Matrix.CreateRotationY(MathHelper.ToRadians(rotationStartY)) *
				   Matrix.CreateRotationX(MathHelper.ToRadians(rotationStartX)) * Matrix.CreateTranslation(modelPosition);

			cubeEffect.View = Matrix.CreateLookAt(cameraPosition, modelPositionZero, Vector3.Up);

			cubeEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 1000.0f);

			for (int i = 1; i < 7; i++)
			{
			if (tryb == 1)
				rotationStartY += speed;
			else if (tryb == 2)
				rotationStartY -= speed;
			else if (tryb == 3)
				rotationStartX += speed;
			else if (tryb == 4)
				rotationStartX -= speed;

				if (i == 1) cubeEffect.DiffuseColor = new Vector3(0.0f, 0.0f, 1.0f);
				else if (i == 2) cubeEffect.DiffuseColor = new Vector3(0.0f, 1.0f, 0.0f);
				else if (i == 3) cubeEffect.DiffuseColor = new Vector3(1.0f, 0.0f, 0.0f);
				else if (i == 4) cubeEffect.DiffuseColor = new Vector3(0.0f, 0.0f, 0.0f);
				else if (i == 5) cubeEffect.DiffuseColor = new Vector3(0.0f, 1.0f, 1.0f);
				else if (i == 6) cubeEffect.DiffuseColor = new Vector3(1.0f, 0.0f, 1.0f);

				cubeEffect.EnableDefaultLighting();
				cubeEffect.SpecularColor = new Vector3(0.0f);
				cubeEffect.AmbientLightColor = new Vector3(0.25f);

				foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
				{
					pass.Apply();
					RenderToDevice(GraphicsDevice, i);
				}
			}

			base.Draw(gameTime);
		}

		#endregion


		#region Private Methods

		public void RenderToDevice(GraphicsDevice device, int color)
		{
			if (_isConstructed == false)
			{
				int index = 0;
				float pol = ilosc / 2;

				for (int k = 0; k < ilosc; k++)
					for (int i = 0; i < ilosc; i++)
						for (int j = 0; j < ilosc; j++)
						{
							float a = j;
							if (a > pol)
							{
								a -= pol;
								a *= -1;
							}

							float b = k;
							if (b > pol)
							{
								b -= pol;
								b *= -1;
							}

							float c = i;
							if (c > pol)
							{
								c -= pol;
								c *= -1;
							}

							ConstructCube(new Vector3(a * odl, b * odl, c * odl),index,color);
							index++;
						}

				maxIndex = index;
			}

			for (int i = 0; i < maxIndex; i++)
			{
				using (VertexBuffer buffer = new VertexBuffer(
					device,
					VertexPositionNormalTexture.VertexDeclaration,
					6,
					BufferUsage.WriteOnly))
				{
					if (color == 1) buffer.SetData(_vertices1[i]);
					else if (color == 2) buffer.SetData(_vertices2[i]);
					else if (color == 3) buffer.SetData(_vertices3[i]);
					else if (color == 4) buffer.SetData(_vertices4[i]);
					else if (color == 5) buffer.SetData(_vertices5[i]);
					else if (color == 6) buffer.SetData(_vertices6[i]);

					device.SetVertexBuffer(buffer);
				}

				device.DrawPrimitives(PrimitiveType.TriangleList, 0, NUM_TRIANGLES);
			}
		}

		private void ConstructCube(Vector3 Position, int index, int sciana)
		{
			if (sciana == 1) _vertices1[index] = new VertexPositionNormalTexture[6];
			else if (sciana == 2) _vertices2[index] = new VertexPositionNormalTexture[6];
			else if (sciana == 3) _vertices3[index] = new VertexPositionNormalTexture[6];
			else if (sciana == 4) _vertices4[index] = new VertexPositionNormalTexture[6];
			else if (sciana == 5) _vertices5[index] = new VertexPositionNormalTexture[6];
			else if (sciana == 6) _vertices6[index] = new VertexPositionNormalTexture[6];

			// Calculate the position of the vertices on the top face.
			Vector3 topLeftFront = Position + new Vector3(-1.0f, 1.0f, -1.0f) * size;
			Vector3 topLeftBack = Position + new Vector3(-1.0f, 1.0f, 1.0f) * size;
			Vector3 topRightFront = Position + new Vector3(1.0f, 1.0f, -1.0f) * size;
			Vector3 topRightBack = Position + new Vector3(1.0f, 1.0f, 1.0f) * size;

			// Calculate the position of the vertices on the bottom face.
			Vector3 btmLeftFront = Position + new Vector3(-1.0f, -1.0f, -1.0f) * size;
			Vector3 btmLeftBack = Position + new Vector3(-1.0f, -1.0f, 1.0f) * size;
			Vector3 btmRightFront = Position + new Vector3(1.0f, -1.0f, -1.0f) * size;
			Vector3 btmRightBack = Position + new Vector3(1.0f, -1.0f, 1.0f) * size;

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

			if (sciana == 1)
			{
				// Add the vertices for the FRONT face.
				_vertices1[index][0] = new VertexPositionNormalTexture(topLeftFront, normalFront, textureZero);
				_vertices1[index][1] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureZero);
				_vertices1[index][2] = new VertexPositionNormalTexture(topRightFront, normalFront, textureZero);
				_vertices1[index][3] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureZero);
				_vertices1[index][4] = new VertexPositionNormalTexture(btmRightFront, normalFront, textureZero);
				_vertices1[index][5] = new VertexPositionNormalTexture(topRightFront, normalFront, textureZero);
			}
			else if (sciana == 2)
			{
				// Add the vertices for the BACK face.
				_vertices2[index][0] = new VertexPositionNormalTexture(topLeftBack, normalBack, textureTopRight);
				_vertices2[index][1] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);
				_vertices2[index][2] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);
				_vertices2[index][3] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);
				_vertices2[index][4] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);
				_vertices2[index][5] = new VertexPositionNormalTexture(btmRightBack, normalBack, textureBottomLeft);
			}
			else if (sciana == 3)
			{
				// Add the vertices for the TOP face.
				_vertices3[index][0] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureZero);
				_vertices3[index][1] = new VertexPositionNormalTexture(topRightBack, normalTop, textureZero);
				_vertices3[index][2] = new VertexPositionNormalTexture(topLeftBack, normalTop, textureZero);
				_vertices3[index][3] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureZero);
				_vertices3[index][4] = new VertexPositionNormalTexture(topRightFront, normalTop, textureZero);
				_vertices3[index][5] = new VertexPositionNormalTexture(topRightBack, normalTop, textureZero);
			}
			else if (sciana == 4)
			{
				// Add the vertices for the BOTTOM face. 
				_vertices4[index][0] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureZero);
				_vertices4[index][1] = new VertexPositionNormalTexture(btmLeftBack, normalBottom, textureZero);
				_vertices4[index][2] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureZero);
				_vertices4[index][3] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureZero);
				_vertices4[index][4] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureZero);
				_vertices4[index][5] = new VertexPositionNormalTexture(btmRightFront, normalBottom, textureZero);
			}
			else if (sciana == 5)
			{
				// Add the vertices for the LEFT face.
				_vertices5[index][0] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureZero);
				_vertices5[index][1] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureZero);
				_vertices5[index][2] = new VertexPositionNormalTexture(btmLeftFront, normalLeft, textureZero);
				_vertices5[index][3] = new VertexPositionNormalTexture(topLeftBack, normalLeft, textureZero);
				_vertices5[index][4] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureZero);
				_vertices5[index][5] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureZero);
			}
			else 
			if (sciana == 6)
			{
				// Add the vertices for the RIGHT face. 
				_vertices6[index][0] = new VertexPositionNormalTexture(topRightFront, normalRight, textureZero);
				_vertices6[index][1] = new VertexPositionNormalTexture(btmRightFront, normalRight, textureZero);
				_vertices6[index][2] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureZero);
				_vertices6[index][3] = new VertexPositionNormalTexture(topRightBack, normalRight, textureZero);
				_vertices6[index][4] = new VertexPositionNormalTexture(topRightFront, normalRight, textureZero);
				_vertices6[index][5] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureZero);
			}
			
			if (sciana == 6) _isConstructed = true;
		}

		#endregion
	}
}