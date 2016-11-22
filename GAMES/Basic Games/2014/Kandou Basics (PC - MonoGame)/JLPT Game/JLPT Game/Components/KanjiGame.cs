using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XMLDataTypes;

namespace JLPT_Game.Components
{
	class KanjiGame : DrawableGameComponent
	{
		#region Field

		Texture2D blank;
		SpriteFont text1;
		SpriteFont text2;
		SpriteFont text3;
		SpriteFont text4;
		SpriteFont text5;
		SpriteFont text6;
		KanjiDataType[] kanji;

		bool pressed = false;

		Rectangle mainText;
		Rectangle[] choose;

		int round = 0;
		int wins = 0;

		int CorectKanji;
		int[] kanjiIndex =new int [6];
		int CorectKanjiIndex;

		Random random;

		bool correctAnswer = false;
		int goodAnswer = 0;
		int wrongAnswer = 0;

		KanjiList kanjiList;

		bool pauseGame = false;
		bool replay = false;

		List<int> rep;
		int l_rep = 0;
		int max_rep = 25;

		#endregion


		#region Initialization

		public KanjiGame(Game game)
			: base(game)
		{
			random = new Random();

			this.kanjiList = new KanjiList(game);
			game.Components.Add(this.kanjiList);

			rep = new List<int>();
		}
		~ KanjiGame() 
		{
			Game.Components.Remove(this.kanjiList);
			kanjiList.Dispose();
			kanjiList = null;
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
			text4 = Game.Content.Load<SpriteFont>("test");
			text5 = Game.Content.Load<SpriteFont>("kanjiText4");
			text6 = Game.Content.Load<SpriteFont>("kanjiText6");

			kanji = Game.Content.Load<KanjiDataType[]>("XMLFile1");

			blank = new Texture2D(GraphicsDevice, 1, 1);
			blank.SetData(new[] { Color.White });

			
			SetKanjiGameRound();

			constructWirtualMenu();

			base.LoadContent();
		}

		public void Update(GameTime gameTime)
		{
			kanjiList.Update(gameTime);

			base.Update(gameTime);
		}

		public void Draw(GameTime gameTime, MouseState d, Vector2 position)
		{
			if (!pauseGame)
			{

				string test1 = kanji[CorectKanji].reading;
				string test2 = kanji[CorectKanji].meaning;

			   
				spriteBatch.Begin();

				string score = wins + " / " + round;
				if (replay) score += " [powtórka]";
				spriteBatch.DrawString(text4, score, new Vector2(10, 5), Color.Black);

				// solution
				if (round > 0)
				{
					Rectangle solutionBox = new Rectangle(GraphicsDevice.Viewport.Width - 200, 5, 180, 37);
					spriteBatch.Draw(blank, solutionBox, Color.Black);

					string goodKanji = kanji[goodAnswer].sign;

					if (correctAnswer)
					{
						if ((position.X >= solutionBox.X && position.X <= solutionBox.X + solutionBox.Width) &&
							(position.Y >= solutionBox.Y && position.Y <= solutionBox.Y + solutionBox.Height))
						{
							if (d.LeftButton == ButtonState.Pressed)
							{
								spriteBatch.Draw(blank, solutionBox, Color.Green);
								spriteBatch.DrawString(text5, goodKanji, new Vector2((int)(solutionBox.X + solutionBox.Width / 2 - text5.MeasureString(goodKanji).X / 2), (int)(solutionBox.Y + solutionBox.Height / 2 - text5.MeasureString(goodKanji).Y / 2)), Color.Black);
								pressed = true;
							}
							else if (pressed)
							{
								pressed = false;
								pauseGame = true;
								kanjiList.Index = goodAnswer;
								kanjiList.SetNewKanjiArea();
							}
							else
							{
								spriteBatch.Draw(blank, solutionBox, Color.DarkGreen);
								spriteBatch.DrawString(text5, goodKanji, new Vector2((int)(solutionBox.X + solutionBox.Width / 2 - text5.MeasureString(goodKanji).X / 2), (int)(solutionBox.Y + solutionBox.Height / 2 - text5.MeasureString(goodKanji).Y / 2)), Color.White);
							}
						}
						else
						{
							spriteBatch.Draw(blank, solutionBox, Color.Green);
							spriteBatch.DrawString(text5, goodKanji, new Vector2((int)(solutionBox.X + solutionBox.Width / 2 - text5.MeasureString(goodKanji).X / 2), (int)(solutionBox.Y + solutionBox.Height / 2 - text5.MeasureString(goodKanji).Y / 2)), Color.Black);
						}
					}
					else
					{
						string wrongKanji = kanji[wrongAnswer].sign;

						Rectangle solutionBox1 = new Rectangle((int)solutionBox.X,
															   (int)solutionBox.Y,
															   (int)solutionBox.Width / 2,
															   (int)solutionBox.Height);

						Rectangle solutionBox2 = new Rectangle((int)(solutionBox.X + solutionBox.Width / 2),
															   (int)solutionBox.Y,
															   (int)solutionBox.Width / 2,
															   (int)solutionBox.Height);

						if ((position.X >= solutionBox1.X && position.X <= solutionBox1.X + solutionBox1.Width) &&
							(position.Y >= solutionBox1.Y && position.Y <= solutionBox1.Y + solutionBox1.Height))
						{
							if (d.LeftButton == ButtonState.Pressed)
							{
								spriteBatch.Draw(blank, solutionBox1, Color.Red);
								spriteBatch.DrawString(text5, wrongKanji, new Vector2((int)(solutionBox1.X + solutionBox1.Width / 2 - text5.MeasureString(wrongKanji).X / 2), (int)(solutionBox1.Y + solutionBox1.Height / 2 - text5.MeasureString(wrongKanji).Y / 2)), Color.Black);
								pressed = true;
							}
							else if (pressed)
							{
								pressed = false;
								pauseGame = true;
								kanjiList.Index = wrongAnswer;
							}
							else
							{
								spriteBatch.Draw(blank, solutionBox1, Color.DarkRed);
								spriteBatch.DrawString(text5, wrongKanji, new Vector2((int)(solutionBox1.X + solutionBox1.Width / 2 - text5.MeasureString(wrongKanji).X / 2), (int)(solutionBox1.Y + solutionBox1.Height / 2 - text5.MeasureString(wrongKanji).Y / 2)), Color.White);
							}
						}
						else
						{
							spriteBatch.Draw(blank, solutionBox1, Color.Red);
							spriteBatch.DrawString(text5, wrongKanji, new Vector2((int)(solutionBox1.X + solutionBox1.Width / 2 - text5.MeasureString(wrongKanji).X / 2), (int)(solutionBox1.Y + solutionBox1.Height / 2 - text5.MeasureString(wrongKanji).Y / 2)), Color.Black);
						}


						if ((position.X >= solutionBox2.X && position.X <= solutionBox2.X + solutionBox2.Width) &&
							(position.Y >= solutionBox2.Y && position.Y <= solutionBox2.Y + solutionBox2.Height))
						{
							if (d.LeftButton == ButtonState.Pressed)
							{
								spriteBatch.Draw(blank, solutionBox2, Color.Green);
								spriteBatch.DrawString(text5, goodKanji, new Vector2((int)(solutionBox2.X + solutionBox2.Width / 2 - text5.MeasureString(goodKanji).X / 2), (int)(solutionBox2.Y + solutionBox2.Height / 2 - text5.MeasureString(goodKanji).Y / 2)), Color.Black);
								pressed = true;
							}
							else if (pressed)
							{
								pressed = false;
								pauseGame = true;
								kanjiList.Index = goodAnswer;
							}
							else
							{
								spriteBatch.Draw(blank, solutionBox2, Color.DarkGreen);
								spriteBatch.DrawString(text5, goodKanji, new Vector2((int)(solutionBox2.X + solutionBox2.Width / 2 - text5.MeasureString(goodKanji).X / 2), (int)(solutionBox2.Y + solutionBox2.Height / 2 - text5.MeasureString(goodKanji).Y / 2)), Color.White);
							}
						}
						else
						{
							spriteBatch.Draw(blank, solutionBox2, Color.Green);
							spriteBatch.DrawString(text5, goodKanji, new Vector2((int)(solutionBox2.X + solutionBox2.Width / 2 - text5.MeasureString(goodKanji).X / 2), (int)(solutionBox2.Y + solutionBox2.Height / 2 - text5.MeasureString(goodKanji).Y / 2)), Color.Black);
						}
					}
				}

				//
				// must "constructVirtualMenu()" -> realized

				spriteBatch.Draw(blank, mainText, Color.White);

				int lineSpacing = 0;

				if (text1.MeasureString(test1).Y > 40) lineSpacing = 5;
				else lineSpacing = 15;

				int textSpacing = lineSpacing + mainText.Y;

				Vector2 meaningPosition = Vector2.Zero;
				if (text1.MeasureString(test1).Y > 40)
				{
					meaningPosition = new Vector2((int)((mainText.X + mainText.Width / 2) - text6.MeasureString(test1).X / 2), textSpacing);
					spriteBatch.DrawString(text6, test1, meaningPosition, Color.Black);
				}
				else
				{
					meaningPosition = new Vector2((int)((mainText.X + mainText.Width / 2) - text1.MeasureString(test1).X / 2), textSpacing);
					spriteBatch.DrawString(text1, test1, meaningPosition, Color.Black);
				}


				if (text1.MeasureString(test1).Y > 40) textSpacing += (int)(text6.MeasureString(test1).Y + 1);
				else 
				textSpacing += (int)(text1.MeasureString(test1).Y + lineSpacing);

				test2 = "(" + test2 + ")";

				Vector2 readingPosition = new Vector2((int)((mainText.X + mainText.Width / 2) - text2.MeasureString(test2).X / 2), textSpacing);

				spriteBatch.DrawString(text2, test2, readingPosition, Color.Black);

				//
				// must "constructVirtualMenu()" -> realized

				for (int i = 0; i < 6; i++)
					if ((position.X >= choose[i].X && position.X <= choose[i].X + choose[i].Width &&
								(position.Y >= choose[i].Y && position.Y <= choose[i].Y + choose[i].Height)))
					{
						if (d.LeftButton == ButtonState.Pressed)
						{
							spriteBatch.Draw(blank, choose[i], Color.White);
							spriteBatch.DrawString(text3, kanji[kanjiIndex[i]].sign, new Vector2(choose[i].X + (choose[i].Width / 2) - (text3.MeasureString(kanji[kanjiIndex[i]].sign).X / 2), choose[i].Y + (choose[i].Height / 2) - (text3.MeasureString(kanji[kanjiIndex[i]].sign).Y / 2)), Color.Black);
							pressed = true;
						}
						else if (pressed)
						{
							pressed = false;

							round++;

							if (i == CorectKanjiIndex)
							{
								wins++;
								correctAnswer = true;
								goodAnswer = kanjiIndex[i];

								if (replay)
								{
									rep.Remove(goodAnswer);
								}
							}
							else
							{
								correctAnswer = false;
								goodAnswer = kanjiIndex[CorectKanjiIndex];
								wrongAnswer = kanjiIndex[i];

								if (!replay)
								{
									rep.Add(goodAnswer);
								}
							}

							if (!replay)
							{
								l_rep++;

								if (l_rep == max_rep && rep.Count != 0) replay = true;
								else if (l_rep == max_rep && rep.Count == 0) l_rep = 0;
							}
							else
							{
								if (rep.Count == 0)
								{
									replay = false;
									l_rep = 0;
								}
							}

							SetKanjiGameRound();
						}
						else
						{
							spriteBatch.Draw(blank, choose[i], Color.Black);
							spriteBatch.DrawString(text3, kanji[kanjiIndex[i]].sign, new Vector2(choose[i].X + (choose[i].Width / 2) - (text3.MeasureString(kanji[kanjiIndex[i]].sign).X / 2), choose[i].Y + (choose[i].Height / 2) - (text3.MeasureString(kanji[kanjiIndex[i]].sign).Y / 2)), Color.White);
						}
					}
					else
					{
						spriteBatch.Draw(blank, choose[i], Color.White);
						spriteBatch.DrawString(text3, kanji[kanjiIndex[i]].sign, new Vector2(choose[i].X + (choose[i].Width / 2) - (text3.MeasureString(kanji[kanjiIndex[i]].sign).X / 2), choose[i].Y + (choose[i].Height / 2) - (text3.MeasureString(kanji[kanjiIndex[i]].sign).Y / 2)), Color.Black);
					}

				spriteBatch.End();
			}
			else
			{
				kanjiList.Draw(gameTime, d, position);
				returnButton(d, position);
			}

			base.Draw(gameTime);
		}

		#endregion


		#region publicMethods

		public void SetKanjiGameRound()
		{
			int max_tab = kanji.Length;
			bool repeat = false;
			bool r1 = true;

			for (int i = 0; i < 6; i++)
			{
				int number = 0;

				if (!replay || !r1)
				{
					if (!r1 && CorectKanjiIndex == i) continue;

					number = random.Next(0, max_tab);
				}
				else
				{
					number = random.Next(0, rep.Count);
					CorectKanjiIndex = random.Next(0, 6);
					kanjiIndex[CorectKanjiIndex] = rep[number];
					r1 = false;

					if (CorectKanjiIndex == 0) continue;
					else number = random.Next(0, max_tab);
				}

			check:
				for (int j = 0; j < i; j++)
				{
					if (kanjiIndex[j] == number || number == CorectKanji)
					{
						number++;
						if (number == max_tab) number = 0;
						repeat = true;
						break;
					}
				}

				if (repeat)
				{
					repeat = false;
					goto check;
				}

				kanjiIndex[i] = number;
			}

			if (!replay) CorectKanjiIndex = random.Next(0, 6);
			CorectKanji = kanjiIndex[CorectKanjiIndex];
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

			Rectangle chooseText = new Rectangle(
				(int)0,
				(int)(mainText.Y + mainText.Height + upSpace),
				(int)(GraphicsDevice.Viewport.Width),
				(int)(GraphicsDevice.Viewport.Height - (mainText.Y + mainText.Height + upSpace) - downSpace));

			//
			int upChooseSpace = 15;
			int chooseHeight = (int)(((chooseText.Height - (upChooseSpace * 4)) / 3));
			int chooseWidth = 225;
			int centerChooseSpace = 120;

			int choosePosition = upChooseSpace + chooseText.Y;

			choose = new Rectangle[6];

			choose[0] = new Rectangle(
				(int)(GraphicsDevice.Viewport.Width / 2 - chooseWidth - (centerChooseSpace / 2)),
				(int)(choosePosition),
				(int)(chooseWidth),
				(int)(chooseHeight));

			choose[1] = new Rectangle(
				(int)(GraphicsDevice.Viewport.Width / 2 + (centerChooseSpace / 2)),
				(int)(choosePosition),
				(int)(chooseWidth),
				(int)(chooseHeight));

			choosePosition += (chooseHeight + upChooseSpace);

			choose[2] = new Rectangle(
				(int)(GraphicsDevice.Viewport.Width / 2 - chooseWidth - (centerChooseSpace / 2)),
				(int)(choosePosition),
				(int)(chooseWidth),
				(int)(chooseHeight));

			choose[3] = new Rectangle(
				(int)(GraphicsDevice.Viewport.Width / 2 + (centerChooseSpace / 2)),
				(int)(choosePosition),
				(int)(chooseWidth),
				(int)(chooseHeight));

			choosePosition += (chooseHeight + upChooseSpace);

			choose[4] = new Rectangle(
				(int)(GraphicsDevice.Viewport.Width / 2 - chooseWidth - (centerChooseSpace / 2)),
				(int)(choosePosition),
				(int)(chooseWidth),
				(int)(chooseHeight));

			choose[5] = new Rectangle(
				(int)(GraphicsDevice.Viewport.Width / 2 + (centerChooseSpace / 2)),
				(int)(choosePosition),
				(int)(chooseWidth),
				(int)(chooseHeight));
		}

		private void returnButton(MouseState d, Vector2 position)
		{
			spriteBatch.Begin();

			int border = 35;
			string t1 = "powrót do gry";

			Rectangle textPosition = new Rectangle(
					(int)(GraphicsDevice.Viewport.Width - (2*border + text4.MeasureString(t1).X)),
					(int)(GraphicsDevice.Viewport.Height - (border + text4.MeasureString(t1).Y)),
					(int)(text4.MeasureString(t1).X + 2 * border),
					(int)(text4.MeasureString(t1).Y + border));


			Vector2 miPosition = new Vector2((int)((textPosition.X + textPosition.Width / 2) - text4.MeasureString(t1).X / 2), textPosition.Y + 5);

			if ((position.X >= textPosition.X && position.X <= textPosition.X + textPosition.Width) &&
				(position.Y >= textPosition.Y && position.Y <= textPosition.Y + textPosition.Height))
			{
				if (d.LeftButton == ButtonState.Pressed)
				{
					spriteBatch.Draw(blank, textPosition, Color.DarkGreen);
					spriteBatch.DrawString(text4, t1, miPosition, Color.White);
					pressed = true;
				}
				else if (pressed)
				{
					pressed = false;
					pauseGame = false;
				}
				else
				{
					spriteBatch.Draw(blank, textPosition, Color.Black);
					spriteBatch.DrawString(text4, t1, miPosition, Color.White);
				}
			}
			else
			{
				spriteBatch.Draw(blank, textPosition, Color.LightGray);
				spriteBatch.DrawString(text4, t1, miPosition, Color.Black);
			}

			spriteBatch.End();
		}

		#endregion
	}
}
