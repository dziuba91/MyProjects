using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using aplikacja2__XNA_.BasicComponent;
using aplikacja2__XNA_.Tryby.tryb1;
using aplikacja2__XNA_.Tryby.tryb5;
using aplikacja2__XNA_.Helper;
using System.Diagnostics;

namespace aplikacja2__XNA_
{
	public class MainGame : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		int WindowWidth = 640;
		int WindowHeight = 640;

		KeyboardState previousKeyboard;
		KeyboardState currentKeyboard;

		int tryb = 1;
		bool tryb3Choose = true;

		TextArea textField;

		T1 tryb1;
		T2 tryb2;
		T3 tryb3;
		T4 tryb4;
		T5 tryb5;

		FpsCounter FPS;
		TimeCounter TIME;

		bool fpsCup = true;

		bool timePause = false;

		Stopwatch sw;

		public MainGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			this.Window.Title = "XNA";
			this.IsFixedTimeStep = false;
			this.IsMouseVisible = true;

			graphics.SynchronizeWithVerticalRetrace = true;

			this.SetScreenMode();

			this.previousKeyboard = Keyboard.GetState();

			this.tryb1 = new T1(this);
			Components.Add(this.tryb1);

			this.tryb2 = new T2(this);
			Components.Add(this.tryb2);

			this.tryb3 = new T3(this);
			Components.Add(this.tryb3);

			this.tryb4 = new T4(this);
			Components.Add(this.tryb4);

			this.tryb5 = new T5(this);
			Components.Add(this.tryb5);

			this.textField = new TextArea(this);
			Components.Add(this.textField);

			FPS = new FpsCounter();
			TIME = new TimeCounter();

			sw = new Stopwatch();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			this.Services.AddService(typeof(SpriteBatch), this.spriteBatch);
		}

		protected override void Update(GameTime gameTime)
		{
			this.currentKeyboard = Keyboard.GetState();

			if (this.currentKeyboard.IsKeyDown(Keys.Escape))
			{
				this.Exit();
			}

			if (this.currentKeyboard.IsKeyDown(Keys.D1))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.D1))
				{
					tryb = 1;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.D2))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.D2))
				{
					tryb = 2;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.D3))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.D3))
				{
					tryb = 3;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.D4))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.D4))
				{
					tryb = 4;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.D5))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.D5))
				{
					tryb = 5;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.D6))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.D6))
				{
					tryb = 6;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.D0))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.D0))
				{
					if (tryb3Choose && tryb == 3) tryb3Choose = false;
					else if (!tryb3Choose && tryb == 3) tryb3Choose = true;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.LeftControl))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.LeftControl))
				{
					if (timePause) timePause = false;
					else timePause = true;
				}
			}

			if (this.currentKeyboard.IsKeyDown(Keys.LeftShift))
			{
				if (!this.previousKeyboard.IsKeyDown(Keys.LeftShift))
				{
					if (fpsCup)
					{
						this.graphics.SynchronizeWithVerticalRetrace = false;
						this.graphics.ApplyChanges();
						this.IsFixedTimeStep = false;

						tryb1.cube.speed = tryb1.cube.speed2;
						tryb3.speed = tryb3.speed2;
						tryb4.rubik.speed = tryb4.rubik.speed2;
						tryb4.rubik.help.rotatuj = tryb4.rubik.help.rotatuj_2;

						fpsCup = false;
					}
					else if (!fpsCup)
					{
						this.graphics.SynchronizeWithVerticalRetrace = true;
						this.graphics.ApplyChanges();
						this.IsFixedTimeStep = false;

						tryb1.cube.speed = tryb1.cube.speed1;
						tryb3.speed = tryb3.speed1;
						tryb4.rubik.speed = tryb4.rubik.speed1;
						tryb4.rubik.help.rotatuj = tryb4.rubik.help.rotatuj_1;

						fpsCup = true;
					}
					
				}
			}

			FPS.countFPS(gameTime.TotalGameTime.Seconds, tryb);

			textField.fps = FPS.fps;
			textField.timeMean = TIME.meanTime;
			textField.timeStart = TIME.startTime;
			textField.timeStop = TIME.stopTime;
			textField.timeMin = TIME.minTime;
			textField.timeMax = TIME.maxTime;
			textField.tryb = tryb;
			textField.block = fpsCup;
			textField.pause = timePause;

			this.previousKeyboard = Keyboard.GetState();

			if (tryb == 1)
				tryb1.Update(gameTime);
			else if (tryb == 2)
				tryb2.Update(gameTime);
			else if (tryb == 3)
				tryb3.Update(gameTime);
			else if (tryb == 4)
				tryb4.Update(gameTime);
			else if (tryb == 5)
				tryb5.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			if (tryb != 5 && (tryb != 3 || !tryb3.fog))
				GraphicsDevice.Clear(Color.Black);
			else if (tryb == 3 && tryb3.fog)
				GraphicsDevice.Clear(Color.Gray);
			else
				GraphicsDevice.Clear(Color.LightGray * 1.08f);

			GraphicsDevice.BlendState = BlendState.Opaque;
			GraphicsDevice.DepthStencilState = DepthStencilState.Default;
			
			//
			sw.Start();
			
			if (tryb == 1)
				tryb1.Draw(gameTime);
			else if (tryb == 2)
				tryb2.Draw(gameTime);
			else if (tryb == 3)
			{
				if (!tryb3Choose)
					tryb3.Draw(gameTime);
				else tryb3.Draw2(gameTime);
			}
			else if (tryb == 4)
			{
				tryb4.Draw(gameTime);
			}
			else if (tryb == 5)
				tryb5.Draw(gameTime, FPS.fpsArray, FPS.fpsArray1, FPS.fpsArray2, FPS.fpsArray3, FPS.fpsArray4, FPS.fpsArray5);

			int periodTime = (int)sw.Elapsed.Milliseconds;
			sw.Stop();
			sw.Reset();

			if (!timePause) TIME.countTIME(gameTime.TotalGameTime.Seconds, periodTime, tryb);

			base.Draw(gameTime);
		}

		private void SetScreenMode()
		{
			this.graphics.PreferredBackBufferWidth = WindowWidth;
			this.graphics.PreferredBackBufferHeight = WindowHeight;
			this.graphics.IsFullScreen = false;
			this.graphics.ApplyChanges();
		}
	}
}