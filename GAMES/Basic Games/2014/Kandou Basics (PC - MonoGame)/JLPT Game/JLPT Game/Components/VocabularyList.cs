using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XMLDataTypes;

namespace JLPT_Game.Components
{
	class VocabularyList : DrawableGameComponent
	{
		#region Field

		Rectangle mainText;
		Rectangle chooseText;

		SubmissionOfKanji [] vocabulary;

		SpriteFont text1;
		SpriteFont text2;
		SpriteFont text3;
		SpriteFont text4;
		SpriteFont text5;

		Texture2D blank;

		bool pressed = false;

		KeyboardState previousKeyboard;
		KeyboardState currentKeyboard;

		bool text_show = true;

		public int Index { get; set; }

		#endregion


		#region Initialization

		public VocabularyList(Game game)
			: base(game)
		{
			this.Index = 0;
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
			text1 = Game.Content.Load<SpriteFont>("kanjiText1");
			text2 = Game.Content.Load<SpriteFont>("kanjiText2");
			text3 = Game.Content.Load<SpriteFont>("kanjiText3");
			text4 = Game.Content.Load<SpriteFont>("kanjiText4");
			text5 = Game.Content.Load<SpriteFont>("test");

			vocabulary = Game.Content.Load<SubmissionOfKanji[]>("XMLFile2");

			blank = new Texture2D(GraphicsDevice, 1, 1);
			blank.SetData(new[] { Color.White });

			constructWirtualMenu();

			base.LoadContent();
		}

		public void Update(GameTime gameTime)
		{
			this.currentKeyboard = Keyboard.GetState();

			if (this.currentKeyboard.IsKeyDown(Keys.Up))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.Up))
				{
					if (Index + 1 < vocabulary.Length)
						Index += 1;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.Down))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.Down))
				{
					if (Index - 1 >= 0)
						Index -= 1;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.Right))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.Right))
				{
					if (Index + 25 < vocabulary.Length)
						Index += 25;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.Left))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.Left))
				{
					if (Index - 25 >= 0)
						Index -= 25;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.Space))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.Space))
				{
					if (text_show) text_show = false;
					else text_show = true;
				}
			}

			previousKeyboard = currentKeyboard;

			base.Update(gameTime);
		}

		public void Draw(GameTime gameTime, MouseState d, Vector2 position)
		{

			string test1 = vocabulary[Index].reading;
			string test2 = vocabulary[Index].signs;
			string test3 = vocabulary[Index].meaning;

			spriteBatch.Begin();

			//
			// actual Index
			spriteBatch.DrawString(text2, (Index + 1) + " / " + vocabulary.Length, new Vector2(GraphicsDevice.Viewport.Width - 150, 10), Color.Black);

			//
			// must "constructVirtualMenu()" -> realized

			spriteBatch.Draw(blank, mainText, Color.White);
			
			//
			// must "constructVirtualMenu()" -> realized

			spriteBatch.Draw(blank, mainText, Color.White);

			int lineSpacing = 15;
			int textSpacing = lineSpacing + mainText.Y;

			Vector2 meaningPosition = new Vector2((int)((mainText.X + mainText.Width / 2) - text3.MeasureString(test1).X / 2), textSpacing);

			spriteBatch.DrawString(text3, test1, meaningPosition, Color.Black);


				textSpacing += (int)(text3.MeasureString(test1).Y + lineSpacing);

				test2 = "(" + test2 + ")";

				Vector2 readingPosition = new Vector2((int)((mainText.X + mainText.Width / 2) - text4.MeasureString(test2).X / 2), textSpacing);

				spriteBatch.DrawString(text4, test2, readingPosition, Color.Black);


			if (text_show)
			{
				//
				// must "constructVirtualMenu()" -> realized

				spriteBatch.Draw(blank, chooseText, Color.White);

				textSpacing = lineSpacing + chooseText.Y;

				Vector2 choosePosition1 = new Vector2((int)((chooseText.X + chooseText.Width / 2) - text2.MeasureString(test3).X / 2), textSpacing);

				spriteBatch.DrawString(text2, test3, choosePosition1, Color.Black);

				lineSpacing = 25;

				textSpacing += ((int)text2.MeasureString(test3).Y + lineSpacing - 10);
			}

			/*
			//
			//złożenia
			if (vocabulary[Index].Length != 0)
			{
				SubmissionOfKanji[] test5 = vocabulary[Index];

				for (int i = 0; i < test5.Length; i++)
				{
					string test6 = test5[i].signs + " (" + test5[i].reading + ") -> " + test5[i].meaning;
					Vector2 choosePosition2 = new Vector2(30, textSpacing);
					spriteBatch.DrawString(text4, test6, choosePosition2, Color.Black);

					textSpacing += lineSpacing;
				}
			}
			 * */

			//
			// ramki
			int buttonSpacing = 20;
			int buttonWidth = 20;

			Rectangle previousButton = new Rectangle(0, chooseText.Y + buttonSpacing, buttonWidth, chooseText.Height - buttonSpacing*2);

			if (Index > 0)
			{
				if ((position.X >= previousButton.X && position.X <= previousButton.X + previousButton.Width) &&
				(position.Y >= previousButton.Y && position.Y <= previousButton.Y + previousButton.Height))
				{
					if (d.LeftButton == ButtonState.Pressed)
					{
						spriteBatch.Draw(blank, previousButton, Color.PaleVioletRed);
						pressed = true;
					}
					else if (pressed)
					{
						pressed = false;
						Index--;
					}
					else
						spriteBatch.Draw(blank, previousButton, Color.OrangeRed);
				}
				else
					spriteBatch.Draw(blank, previousButton, Color.Red);
			}
			else
				spriteBatch.Draw(blank, previousButton, Color.DarkRed);


			Rectangle nextButton = new Rectangle(GraphicsDevice.Viewport.Width - buttonWidth, chooseText.Y + buttonSpacing, buttonWidth, chooseText.Height - buttonSpacing * 2);

			if (Index < vocabulary.Length - 1)
			{
				if ((position.X >= nextButton.X && position.X <= nextButton.X + nextButton.Width) &&
				(position.Y >= nextButton.Y && position.Y <= nextButton.Y + nextButton.Height))
				{
					if (d.LeftButton == ButtonState.Pressed)
					{
						spriteBatch.Draw(blank, nextButton, Color.PaleVioletRed);
						pressed = true;
					}
					else if (pressed)
					{
						pressed = false;
						Index++;
					}
					else
						spriteBatch.Draw(blank, nextButton, Color.OrangeRed);
				}
				else
					spriteBatch.Draw(blank, nextButton, Color.Red);
			}
			else
				spriteBatch.Draw(blank, nextButton, Color.DarkRed);


			spriteBatch.End();

			base.Draw(gameTime);
		}

		#endregion


		#region privateMethods

		private void constructWirtualMenu()
		{
			mainText = new Rectangle(
				(int)0,
				(int)(GraphicsDevice.Viewport.Height / 10),
				(int)(GraphicsDevice.Viewport.Width),
				(int)(GraphicsDevice.Viewport.Height / 4));

			//
			int upSpace = 15;
			int downSpace = 50;

			chooseText = new Rectangle(
				(int)0,
				(int)(mainText.Y + mainText.Height + upSpace),
				(int)(GraphicsDevice.Viewport.Width),
				(int)(GraphicsDevice.Viewport.Height - (mainText.Y + mainText.Height + upSpace) - downSpace));
		}

		#endregion
	}
}
