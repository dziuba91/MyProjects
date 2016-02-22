using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AvatarKinectGame.TEST
{
    class Label : DrawableGameComponent
    {
        SpriteFont font;
        Vector2 pos;

        public float number = 0;

        public bool testLabels = true;

        public string message = "";

        public Label(Game game, Vector2 position)
            : base(game)
        {
            pos = position;
        }

        public SpriteBatch spriteBatch
        {
            get
            {
                return (SpriteBatch)this.Game.Services.GetService(typeof(SpriteBatch));
            }
        }


        #region BasicComponentMethod

        public override void Initialize()
        {
            font = Game.Content.Load<SpriteFont>("FONTS/font1");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (testLabels)
            {
                spriteBatch.Begin();

                //string message = number.ToString();

                //Vector2 pos = new Vector2(GraphicsDevice.Viewport.Height - 80.0f, GraphicsDevice.Viewport.Width - 140.0f);
                spriteBatch.DrawString(font, message, pos, Color.DarkRed);

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        #endregion
    }
}
