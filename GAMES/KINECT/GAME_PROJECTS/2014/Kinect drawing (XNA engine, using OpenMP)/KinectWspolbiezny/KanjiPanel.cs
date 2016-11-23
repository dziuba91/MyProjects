using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XMLDataTypes;

namespace KinectWspolbiezny
{
	class KanjiPanel : DrawableGameComponent
	{
		SpriteFont font;
		SpriteFont krzaki_font;
		Texture2D blank;
		KanjiDataType[] kanji;

		public TextPanel text_panel { get; private set; }

		public Vector2 Size { get; set; }
		public Vector2 Position { get; set; }

		public int Index { get; set; }

		public KanjiPanel(Game game)
			: base(game)
		{
			Size = new Vector2(400,75);
			Position = new Vector2(20,270);

			Index = -1;

			//
			this.text_panel = new TextPanel(game);
			Game.Components.Add(this.text_panel);
		}

		public SpriteBatch spriteBatch
		{
			get
			{
				return (SpriteBatch)this.Game.Services.GetService(typeof(SpriteBatch));
			}
		}

		public Kinect Chooser
		{
			get
			{
				return (Kinect)this.Game.Services.GetService(typeof(Kinect));
			}
		}

		public override void Initialize()
		{
			font = Game.Content.Load<SpriteFont>("SpriteFont1");
			krzaki_font = Game.Content.Load<SpriteFont>("KanjiFont");
			kanji = Game.Content.Load<KanjiDataType[]>("XMLFile1");

			blank = new Texture2D(Game.GraphicsDevice, 1, 1);
			blank.SetData(new[] { Color.White });

			base.Initialize();
		}

		public void Draw(GameTime gameTime)
		{
			int border = 10;
			int main_border_left = 10;

			spriteBatch.Begin();

			spriteBatch.DrawString(font, "Dostępne Krzaki: ", new Vector2(Position.X, Position.Y - 20), Color.Black);

			spriteBatch.Draw(blank, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color.White);

			int space = 0;
			for (int i = 0; i < kanji.Length; i++)
			{
				Vector2 p1 = krzaki_font.MeasureString(kanji[i].sign);
				Rectangle r1 = new Rectangle((int)(Position.X + main_border_left + border/2 + space),(int)(Position.Y+ Size.Y/2-p1.Y/2+5),(int)(p1.X + border),(int)p1.Y - 10);
				if (Index == i) spriteBatch.Draw(blank, r1, Color.Green);
				else spriteBatch.Draw(blank, r1, Color.LightBlue);
				spriteBatch.DrawString(krzaki_font, kanji[i].sign, new Vector2(r1.X+ border/2, r1.Y-3), Color.Black);

				space = r1.X + 4 * border;
			}
			
			spriteBatch.End();

			text_panel.Draw(gameTime);

			base.Draw(gameTime);
		}
	}
}