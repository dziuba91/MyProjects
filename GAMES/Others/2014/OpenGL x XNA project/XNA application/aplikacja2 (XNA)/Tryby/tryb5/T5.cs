using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using aplikacja2__XNA_.BasicComponent;

namespace aplikacja2__XNA_.Tryby.tryb5
{
	public class T5 : DrawableGameComponent
	{
		#region Field

		private int tryb = 0;

		KeyboardState currentKeyboard;
		KeyboardState previousKeyboard;

		Texture2D blank;
		SpriteFont text;
		SpriteFont text2;

		float max;

		bool ustawParametry = true;

		Rectangle wykresX;
		Rectangle wykresY;

		bool showAxisValue = true;

		#endregion


		#region Initialization

		public T5(Game game)
			: base(game)
		{	}

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
			text = Game.Content.Load<SpriteFont>("wykres_tryb");
			text2 = Game.Content.Load<SpriteFont>("wykres_tryb2");

			blank = new Texture2D(GraphicsDevice, 1, 1);
			blank.SetData(new[] { Color.White });
		}

		public void Update(GameTime gameTime)
		{
			currentKeyboard = Keyboard.GetState();

			if (this.currentKeyboard.IsKeyDown(Keys.Q))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.Q))
				{
					tryb = 0;
					ustawParametry = true;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.W))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.W))
				{
					tryb = 1;
					ustawParametry = true;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.E))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.E))
				{
					tryb = 2;
					ustawParametry = true;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.R))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.R))
				{
					tryb = 3;
					ustawParametry = true;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.T))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.T))
				{
					tryb = 4;
					ustawParametry = true;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.Y))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.Y))
				{
					tryb = 5;
					ustawParametry = true;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.Z))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.Z))
				{
					tryb = 10;
					ustawParametry = true;
				}
			}

			previousKeyboard = currentKeyboard;

			base.Update(gameTime);
		}

		public void Draw(GameTime gameTime, int[] fps0, int[] fps1, int[] fps2, int[] fps3, int[] fps4, int[] fps5)
		{
			spriteBatch.Begin();

			Vector2 textPosition = new Vector2(35.0f, 10.0f);

			spriteBatch.DrawString(text,"TRYB = " + tryb, textPosition, Color.Blue);

			wykresY = new Rectangle(
					(int)(50),
					(int)(50),
					(int)(1),
					(int)(400));

			spriteBatch.Draw(blank, wykresY, Color.Black);

			float arrow = 15.0f;
			drawLine(new Vector2(wykresY.X, wykresY.Y), new Vector2(wykresY.X + arrow + 1.0f, wykresY.Y + arrow + 1.0f), Color.Black);
			drawLine(new Vector2(wykresY.X, wykresY.Y), new Vector2(wykresY.X - arrow, wykresY.Y + arrow), Color.Black);

			//
			wykresX = new Rectangle(
					(int)(45),
					(int)(410),
					(int)(GraphicsDevice.Viewport.Width - 100),
					(int)(1));

			spriteBatch.Draw(blank, wykresX, Color.Black);

			drawLine(new Vector2(wykresX.X + wykresX.Width, wykresX.Y), new Vector2(wykresX.X + wykresX.Width - arrow, wykresX.Y + arrow), Color.Black);
			drawLine(new Vector2(wykresX.X + wykresX.Width, wykresX.Y), new Vector2(wykresX.X + wykresX.Width - arrow, wykresX.Y - arrow), Color.Black);

			//
			int pointHeight = 3;
			int pointWidth = 3;
			float space = 4.5f;

			Rectangle point = new Rectangle(
					(int)(0),
					(int)(0),
					(int)(pointWidth),
					(int)(pointHeight));

			Rectangle previousPoint = Rectangle.Empty;

			//
			int j;
			bool setLine = false;

			if (ustawParametry || tryb == 0 || tryb == 5 || tryb == 10)
			{
				set_max(fps0,fps1,fps2,fps3,fps4,fps5);

				ustawParametry = false;
			}

			//
			Rectangle wykres = new Rectangle(
					(int)(0),
					(int)(0),
					(int)(wykresX.Width - 40),
					(int)(1));

			if (showAxisValue && max > 3)
			{
				float caly_przedzial = wykresY.Height - 100;
				int ilosc_przedzialow = 6;
				float przedzial = caly_przedzial/ ilosc_przedzialow;

				for (j = 1; j< ilosc_przedzialow; j++)
				{
					spriteBatch.DrawString(text2, ((int)max / ilosc_przedzialow * j).ToString(), new Vector2(wykresY.X - 25, wykresX.Y - przedzial * j - 20), Color.Gray*0.5f);

					wykres.X = wykresY.X - 5;
					wykres.Y = (int)(wykresX.Y - przedzial * j);
					spriteBatch.Draw(blank, wykres, Color.Gray * 0.5f);
				}
			}

			//
			Rectangle maxLine = new Rectangle(
					(int)(wykresY.X - 4),
					(int)(wykresX.Y - skaluj(max+ 0.5f, max)),
					(int)(wykresX.Width - 8),
					(int)(1));

			spriteBatch.Draw(blank, maxLine, Color.Gray);

			Vector2 maxTextLine = new Vector2(maxLine.X + (maxLine.Width / 2) - 10.0f, wykresX.Y - skaluj(max + 0.5f, max) - 30.0f);
			spriteBatch.DrawString(text, max.ToString(), maxTextLine, Color.Gray);

			//
			int granica;
			int licznik = 0;
			Color c = new Color();

			for (j = 0; j < 100; j++)
			{
				if (tryb == 0)
				{
					point.Y = (wykresX.Y) - (int)skaluj(fps0[j] + pointWidth / 2 - 0.5f, max);
					point.X = (int)((wykresX.X + 50) + j * space);

					spriteBatch.Draw(blank, point, Color.Blue);
				}
				else if (tryb == 1)
				{
					point.Y = (wykresX.Y) - (int)skaluj(fps1[j] + pointWidth / 2 - 0.5f, max);
					point.X = (int)((wykresX.X + 50) + j * space);

					spriteBatch.Draw(blank, point, Color.Blue);
				}
				else if (tryb == 2)
				{
					point.Y = (wykresX.Y) - (int)skaluj(fps2[j] + pointWidth / 2 - 0.5f, max);
					point.X = (int)((wykresX.X + 50) + j * space);

					spriteBatch.Draw(blank, point, Color.Blue);
				}
				else if (tryb == 3)
				{
					point.Y = (wykresX.Y) - (int)skaluj(fps3[j] + pointWidth / 2 - 0.5f, max);
					point.X = (int)((wykresX.X + 50) + j * space);

					spriteBatch.Draw(blank, point, Color.Blue);
				}
				else if (tryb == 4)
				{
					point.Y = (wykresX.Y) - (int)skaluj(fps4[j] + pointWidth / 2 - 0.5f, max);
					point.X = (int)((wykresX.X + 50) + j * space);

					spriteBatch.Draw(blank, point, Color.Blue);
				}
				else if (tryb == 5)
				{
					point.Y = (wykresX.Y) - (int)skaluj(fps5[j] + pointWidth / 2 - 0.5f, max);
					point.X = (int)((wykresX.X + 50) + j * space);

					spriteBatch.Draw(blank, point, Color.Blue);
				}
				else if (tryb == 10)
				{
					granica = j/20;
					point.X = (int)((j)*space + wykresX.X + 50);
					c.A=255;

					if (granica == 0)
					{
						c.R = (byte)(255 / 2);
						c.G = (byte)(0);
						c.B = (byte)(255 / 2);
						point.Y = (wykresX.Y) - (int)skaluj(fps1[licznik] + pointWidth / 2 - 0.5f, max);
					}
					else if (granica == 1)
					{
						c.R = (byte)(255);
						c.G = (byte)(0);
						c.B = (byte)(255);
						point.Y = (wykresX.Y) - (int)skaluj(fps2[licznik] + pointWidth / 2 - 0.5f, max);
					}
					else if (granica == 2)
					{
						c.R = (byte)(255 / 2);
						c.G = (byte)(255 / 2);
						c.B = (byte)(0);
						point.Y = (wykresX.Y) - (int)skaluj(fps3[licznik] + pointWidth / 2 - 0.5f, max);
					}
					else if (granica == 3)
					{
						c.R = (byte)(0);
						c.G = (byte)(255 / 2);
						c.B = (byte)(0);
						point.Y = (wykresX.Y) - (int)skaluj(fps4[licznik] + pointWidth / 2 - 0.5f, max);
					}
					else if (granica == 4)
					{
						c.R = (byte)(0);
						c.G = (byte)(255 / 2);
						c.B = (byte)(255 / 2);
						point.Y = (wykresX.Y) - (int)skaluj(fps5[licznik] + pointWidth / 2 - 0.5f, max);
					}

					spriteBatch.Draw(blank, point, c);

					if (licznik != 0)
					{
						drawLine(new Vector2(point.X + 2, point.Y + 2), new Vector2(previousPoint.X + 2, previousPoint.Y + 2), c);
					}
					else if (granica !=0)
					{
						drawLine(new Vector2((point.X + previousPoint.X) / 2, maxLine.Y), new Vector2((point.X + previousPoint.X) / 2, wykresX.Y), Color.Gray * 0.5f);
					}

					if (licznik == 8)
					{
						spriteBatch.DrawString(text, (granica+1).ToString(), new Vector2(point.X,wykresX.Y+6), c);
					}

					licznik++;
					if (licznik == 20) licznik = 0;
				}

				if (setLine && tryb !=10)
				{
					drawLine(new Vector2(point.X+2, point.Y+2), new Vector2(previousPoint.X+2, previousPoint.Y+2), Color.Blue);
				}

				previousPoint = point;
				setLine = true;
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}

		#endregion


		#region Private Methods

		float skaluj(float liczba, float max)
		{
			return ((wykresY.Height - 100)	/ max) * liczba;
		}

		void set_max(int []fpsArray, int []fpsArray1, int []fpsArray2, int []fpsArray3, int []fpsArray4, int []fpsArray5)
		{
			max = 0;

			for (int i = 0; i < 100; i++)
			{
				if (tryb == 0)
				{
					if (max < fpsArray[i])
					{
						max = fpsArray[i];
					}
				}
				else if (tryb == 1)
				{
					if (max < fpsArray1[i])
					{
						max = fpsArray1[i];
					}
				}
				else if (tryb == 2)
				{
					if (max < fpsArray2[i])
					{
						max = fpsArray2[i];
					}
				}
				else if (tryb == 3)
				{
					if (max < fpsArray3[i])
					{
						max = fpsArray3[i];
					}
				}
				if (tryb == 4)
				{
					if (max < fpsArray4[i])
					{
						max = fpsArray4[i];
					}
				}
				if (tryb == 5)
				{
					if (max < fpsArray5[i])
					{
						max = fpsArray5[i];
					}
				}
				if (tryb == 10)
				{
					int granica = i / 20;

					if (granica == 0)
					{
						if (max < fpsArray1[i])
						{
							max = fpsArray1[i];
						}
					}
					else if (granica == 1)
					{
						if (max < fpsArray2[i - 20])
						{
							max = fpsArray2[i - 20];
						}
					}
					else if (granica == 2)
					{
						if (max < fpsArray3[i - 40])
						{
							max = fpsArray3[i - 40];
						}
					}
					else if (granica == 3)
					{
						if (max < fpsArray4[i - 60])
						{
							max = fpsArray4[i - 60];
						}
					}
					else if (granica == 4)
					{
						if (max < fpsArray5[i - 80])
						{
							max = fpsArray5[i - 80];
						}
					}
				}
			}
		}

		void drawLine(Vector2 p1, Vector2 p2, Color c)
		{
			float angle = (float)Math.Atan2(p1.Y - p2.Y, p1.X - p2.X);
			float dist = Vector2.Distance(p1, p2);

			spriteBatch.Draw(blank, new Rectangle((int)p2.X, (int)p2.Y, (int)dist, 1), null, c, angle, Vector2.Zero, SpriteEffects.None, 0);
		}

		#endregion
	}
}
