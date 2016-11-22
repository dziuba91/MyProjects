#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using JLPT_Game.Components;
#endregion

namespace JLPT_Game
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class MainGame : Microsoft.Xna.Framework.Game
	{
		Texture2D blank;
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteFont text;
		Texture2D cursor;
		Vector2 textureCenter;

		MainMenu mainMenu;
		KanjiGame kanjiGame;
		KanjiMenu kanjiMenu;
		KanjiList kanjiList;
		CompositionGame compositionGame;
		VocabularyMenu vocabularyMenu;
		VocabularyGame vocabularyGame;
		VocabularyList vocabularyList;
		VocabularyGame2 vocabularyGame2;

		KeyboardState currentKeyboard;
		MouseState d;

		bool pressed = false;
		bool changeMenu = false;

		public MainGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			d = Mouse.GetState();

			//
			this.mainMenu = new MainMenu(this);
			Components.Add(this.mainMenu);

			this.kanjiMenu = new KanjiMenu(this);
			Components.Add(this.kanjiMenu);

			this.vocabularyMenu = new VocabularyMenu(this);
			Components.Add(this.vocabularyMenu);
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			this.Services.AddService(typeof(SpriteBatch), this.spriteBatch);

			text = Content.Load<SpriteFont>("test");
			cursor = Content.Load<Texture2D>("cursor");

			blank = new Texture2D(GraphicsDevice, 1, 1);
			blank.SetData(new[] { Color.White });

			textureCenter = new Vector2(cursor.Width / 2.0f, cursor.Height / 2.0f);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			d = Mouse.GetState();

			if (d.X > 0 && d.X < GraphicsDevice.Viewport.Width-1 && d.Y > 0 && d.Y < GraphicsDevice.Viewport.Height-1) IsMouseVisible = false;
			else
				IsMouseVisible = true;

			// Allows the game to exit
			this.currentKeyboard = Keyboard.GetState();

			if (this.currentKeyboard.IsKeyDown(Keys.Escape))
			{
				this.Exit();
			}

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			Vector2 position = new Vector2(d.X, d.Y);

			if (mainMenu.SelectItemNumber == 0)
			{
				mainMenu.Draw(gameTime, d, position);
			}
			else if (mainMenu.SelectItemNumber == 1)
			{
				if (kanjiMenu.SelectItemNumber == 0)
				{
					kanjiMenu.Draw(gameTime, d, position);
				}
				else if (kanjiMenu.SelectItemNumber == 1)
				{
					if (kanjiGame == null)
					{
						this.kanjiGame = new KanjiGame(this);
						Components.Add(this.kanjiGame);
					}

					kanjiGame.Draw(gameTime, d, position);
					kanjiGame.Update(gameTime);
				}
				else if (kanjiMenu.SelectItemNumber == 2)
				{
					if (compositionGame == null)
					{
						this.compositionGame = new CompositionGame(this);
						Components.Add(this.compositionGame);
					}

					compositionGame.Draw(gameTime, d, position);
					compositionGame.Update(gameTime);
				}
				else if (kanjiMenu.SelectItemNumber == 3)
				{
					if (kanjiList == null)
					{
						this.kanjiList = new KanjiList(this);
						Components.Add(this.kanjiList);
					}

					kanjiList.Draw(gameTime, d, position);
					kanjiList.Update(gameTime);
				}
				else
				{ }

				back(position);
			}
			else if (mainMenu.SelectItemNumber == 2)
			{
				if (vocabularyMenu.SelectItemNumber == 0)
				{
					vocabularyMenu.Draw(gameTime, d, position);
				}
				else if (vocabularyMenu.SelectItemNumber == 1)
				{
					if (vocabularyGame == null)
					{
						this.vocabularyGame = new VocabularyGame(this);
						Components.Add(this.vocabularyGame);
					}

					vocabularyGame.Draw(gameTime, d, position);
				}
				else if (vocabularyMenu.SelectItemNumber == 2)
				{
					if (vocabularyGame2 == null)
					{
						this.vocabularyGame2 = new VocabularyGame2(this);
						Components.Add(this.vocabularyGame2);
					}

					vocabularyGame2.Draw(gameTime, d, position);
				}
				else if (vocabularyMenu.SelectItemNumber == 3)
				{
					if (vocabularyList == null)
					{
						this.vocabularyList = new VocabularyList(this);
						Components.Add(this.vocabularyList);
					}

					vocabularyList.Draw(gameTime, d, position);
					vocabularyList.Update(gameTime);
				}
				else
				{ }

				back(position);
			}
			else
			{
				back(position);
			}

			spriteBatch.Begin();

			if (d.X > 0 && d.X < GraphicsDevice.Viewport.Width && d.Y > 0 && d.Y < GraphicsDevice.Viewport.Height)
			{
				spriteBatch.Draw(cursor, position, null, Color.White, 0.0f, textureCenter, 1.0f, SpriteEffects.None, 0.0f);
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}


		private void back(Vector2 position)
		{
			spriteBatch.Begin();

			int border = 15;
			string t1 = "wróć";

			Rectangle textPosition = new Rectangle(
					border,
					(int)(GraphicsDevice.Viewport.Height - (border + text.MeasureString(t1).Y)),
					(int)(text.MeasureString(t1).X + border),
					(int)(text.MeasureString(t1).Y + border));

			spriteBatch.Draw(blank, textPosition, Color.White);


			Vector2 miPosition = new Vector2((int)((textPosition.X + textPosition.Width / 2) - text.MeasureString(t1).X / 2), textPosition.Y + 5);

			if ((position.X >= miPosition.X && position.X <= miPosition.X + text.MeasureString(t1).X) &&
				(position.Y >= miPosition.Y && position.Y <= miPosition.Y + text.MeasureString(t1).Y))
			{
				if (d.LeftButton == ButtonState.Pressed)
				{
					spriteBatch.DrawString(text, t1, miPosition, Color.White);
					pressed = true;
				}
				else if (pressed)
				{
					deleteObject();

					pressed = false;
					mainMenu.SelectItemNumber = 0;
					kanjiMenu.SelectItemNumber = 0;
					vocabularyMenu.SelectItemNumber = 0;
				}
				else
					spriteBatch.DrawString(text, t1, miPosition, Color.Red);
			}
			else
				spriteBatch.DrawString(text, t1, miPosition, Color.Black);

			spriteBatch.End();
		}

		private void deleteObject()
		{
			if (mainMenu.SelectItemNumber == 1)
			{
				if (kanjiMenu.SelectItemNumber == 1)
				{
					Components.Remove(this.kanjiGame);
					kanjiGame.Dispose();
					kanjiGame = null;
				}
				else if (kanjiMenu.SelectItemNumber == 2)
				{
					Components.Remove(this.compositionGame);
					compositionGame.Dispose();
					compositionGame = null;
				}
				else if (kanjiMenu.SelectItemNumber == 3)
				{
					Components.Remove(this.kanjiList);
					kanjiList.Dispose();
					kanjiList = null;
				}
				else
				{ }
			}
			else if (mainMenu.SelectItemNumber == 2)
			{
				if (vocabularyMenu.SelectItemNumber == 1)
				{
					Components.Remove(this.vocabularyGame);
					vocabularyGame.Dispose();
					vocabularyGame = null;
				}
				else if (vocabularyMenu.SelectItemNumber == 2)
				{
					Components.Remove(this.vocabularyGame2);
					vocabularyGame2.Dispose();
					vocabularyGame2 = null;
				}
				else if (vocabularyMenu.SelectItemNumber == 3)
				{
					Components.Remove(this.vocabularyList);
					vocabularyList.Dispose();
					vocabularyList = null;
				}
				else
				{ }
			}
			else { }
		}
	}
}
