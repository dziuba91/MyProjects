using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace aplikacja2__XNA_.BasicComponent
{
    class TextArea : DrawableGameComponent
    {
        # region Field

        Texture2D blank;
        SpriteFont text;

        public int fps;

        public int timeMean;
        public int timeMax;
        public int timeMin;
        public int timeStart;
        public int timeStop;

        public int tryb;

        public bool block;

        public bool pause;

        #endregion


        #region Initialization

        public TextArea(Game game)
            : base(game)
        {

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

        public override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            text = Game.Content.Load<SpriteFont>("tekst");

            blank = new Texture2D(GraphicsDevice, 1, 1);
            blank.SetData(new[] { Color.White });
        }

        public void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            //int border = 15;
            string t1 = "TRYB: " + tryb;

            Rectangle textPosition = new Rectangle(
                    (int)0,
                    (int)(3*GraphicsDevice.Viewport.Height/4),
                    (int)(GraphicsDevice.Viewport.Width),
                    (int)(GraphicsDevice.Viewport.Height));

            spriteBatch.Draw(blank, textPosition, Color.White);


            Vector2 miPosition = new Vector2((int)(textPosition.X + 50), textPosition.Y + 5);

            spriteBatch.DrawString(text, t1, miPosition, Color.Black);

            string t2 ="";

            if (block)
                t2 = "fps= " + fps + " (limited)";
            else
                t2 = "fps= " + fps;

            miPosition.Y += 30; ;

            spriteBatch.DrawString(text, t2, miPosition, Color.Black);

            string t3 = "";
            
            if (!pause)
                t3 = "section1= " + timeMean + "[ms] \n         (" + "t1= " + timeStart + "; tn= " + timeStop + "; tmin= " + timeMin + "; tmax= " + timeMax + ")";
            else
                t3 = "section1= " + timeMean + "[ms]   (pause)\n         (" + "t1= " + timeStart + "; tn= " + timeStop + "; tmin= " + timeMin + "; tmax= " + timeMax + ")";
            miPosition.Y += 30; ;

            spriteBatch.DrawString(text, t3, miPosition, Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion
    }
}
