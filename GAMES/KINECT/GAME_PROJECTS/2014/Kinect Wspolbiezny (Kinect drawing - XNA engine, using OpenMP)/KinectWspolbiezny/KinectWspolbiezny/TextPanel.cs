using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XMLDataTypes;

namespace KinectWspolbiezny
{
    class TextPanel : DrawableGameComponent
    {
        SpriteFont font;
        SpriteFont krzaki_font;
        Texture2D blank;
        public Vector2 Size;
        public Vector2 Position;
        KanjiDataType[] kanji;

        public int index = -1;
 
        public TextPanel(Game game)
            : base(game)
        {
            Size = new Vector2(400,80);
            Position = new Vector2(20,380);
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

        /// <summary>
        /// Initializes the necessary children.
        /// </summary>
        public override void Initialize()
        {
            font = Game.Content.Load<SpriteFont>("SpriteFont1");
            krzaki_font = Game.Content.Load<SpriteFont>("KanjiFont2");
            kanji = Game.Content.Load<KanjiDataType[]>("XMLFile1");

            blank = new Texture2D(Game.GraphicsDevice, 1, 1);
            blank.SetData(new[] { Color.White });

            base.Initialize();
        }

        /// <summary>
        /// The update method where the new depth frame is retrieved.
        /// </summary>
        /// <param name="gameTime">The elapsed game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            int border = 7;
            int main_border_left = 10;
            int odst = 22;

            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Ostatnio rozpoznane Krzaczki: ", new Vector2(Position.X, Position.Y - 20), Color.Black);

            spriteBatch.Draw(blank, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color.White);

            Vector2 p1 = new Vector2(Position.X+main_border_left, Position.Y+border);
            if (index != -1)
            {
                spriteBatch.DrawString(krzaki_font, kanji[index].sign, p1, Color.Black);

                //p1.Y += odst;
                spriteBatch.DrawString(krzaki_font, "（ "+ kanji[index].reading + " ）", new Vector2 (p1.X + 40, p1.Y), Color.Black);

                p1.Y += odst;
                spriteBatch.DrawString(krzaki_font, kanji[index].meaning, p1, Color.Black);

                p1.Y += odst;
                spriteBatch.DrawString(krzaki_font, "CHIŃSKI: "+kanji[index].china_reading, p1, Color.Black);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }
    }
}
