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
	class T2 : DrawableGameComponent
	{
		#region Field

		private int tryb = 1;

		KeyboardState currentKeyboard;
		KeyboardState previousKeyboard;

		MultiCube Mcube;

		#endregion


		#region Initialization

		public T2(Game game)
			: base(game)
		{
			Mcube = new MultiCube(game, new Vector3(1.0f, 1.0f, 1.0f), Vector3.Zero);
			game.Components.Add(this.Mcube);
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

			previousKeyboard = currentKeyboard;

			base.Update(gameTime);
		}

		public void Draw(GameTime gameTime)
		{
			Mcube.Draw(gameTime, tryb);

			base.Draw(gameTime);
		}

		#endregion
	}
}
