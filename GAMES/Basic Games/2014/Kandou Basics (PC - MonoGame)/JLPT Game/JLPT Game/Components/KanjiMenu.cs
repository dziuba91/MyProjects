using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using JLPT_Game.MenuItemsData;

namespace JLPT_Game.Components
{
	class KanjiMenu : DrawableGameComponent
	{
		#region Field

		SpriteFont text;
		KanjiItems MI = new KanjiItems();

		Texture2D blank;

		bool pressed = false;

		public int SelectItemNumber { get; set; }

		#endregion


		#region Initialization

		public KanjiMenu(Game game)
			: base(game)
		{
			this.SelectItemNumber = 0;
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
			text = Game.Content.Load<SpriteFont>("textMain");

			blank = new Texture2D(GraphicsDevice, 1, 1);
			blank.SetData(new[] { Color.White });

			base.LoadContent();
		}

		public void Draw(GameTime gameTime, MouseState d, Vector2 position)
		{
			spriteBatch.Begin();

			if (MI.menuItem.Length >= 1)
			{
				int lineSpacing = text.LineSpacing - 10;

				Rectangle textPosition = new Rectangle(
					(int)(GraphicsDevice.Viewport.Width / 4),
					(int)(GraphicsDevice.Viewport.Height / 2 - (((text.MeasureString(MI.menuItem[0]).Y * MI.menuItem.Length) + (lineSpacing * (MI.menuItem.Length + 1))) / 2)),
					(int)(GraphicsDevice.Viewport.Width / 2),
					(int)(text.MeasureString(MI.menuItem[0]).Y * MI.menuItem.Length) + (lineSpacing * (MI.menuItem.Length + 1)));

				spriteBatch.Draw(blank, textPosition, Color.White);

				int itemPosition = textPosition.Y + lineSpacing;

				for (int i = 0; i < MI.menuItem.Length; i++)
				{
					Vector2 miPosition = new Vector2((int)((textPosition.X + textPosition.Width / 2) - text.MeasureString(MI.menuItem[i]).X / 2), itemPosition);

					if ((position.X >= miPosition.X && position.X <= miPosition.X + text.MeasureString(MI.menuItem[i]).X) &&
						(position.Y >= miPosition.Y && position.Y <= miPosition.Y + text.MeasureString(MI.menuItem[i]).Y))
					{
						if (d.LeftButton == ButtonState.Pressed)
						{
							spriteBatch.DrawString(text, MI.menuItem[i], miPosition, Color.White);
							pressed = true;
						}
						else if (pressed)
						{
							pressed = false;
							SelectItemNumber = i + 1;
						}
						else
							spriteBatch.DrawString(text, MI.menuItem[i], miPosition, Color.Red);
					}
					else
						spriteBatch.DrawString(text, MI.menuItem[i], miPosition, Color.Black);

					itemPosition += (int)(text.MeasureString(MI.menuItem[i]).Y + lineSpacing);
				}
			}
			else
			{ }

			spriteBatch.End();

			base.Draw(gameTime);
		}

		#endregion
	}
}
