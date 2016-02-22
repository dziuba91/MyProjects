using System;
using Microsoft.Kinect;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace KinectWspolbiezny
{
    class KinectStatusCheck : DrawableGameComponent
    {
        #region LayoutField

        public bool startGame = true;

        SpriteFont font;
        Texture2D blank;

        Texture2D logo;

        public bool kinectDetected;
        private bool delayTime = false;
        private bool gameAccepted = true;
        private bool startDelay = true;

        Stopwatch sw;

        #endregion


        #region Initialization

        public KinectStatusCheck(Game game)
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
            font = Game.Content.Load<SpriteFont>("SpriteFont1");

            blank = new Texture2D(GraphicsDevice, 1, 1);
            blank.SetData(new[] { Color.White });

            base.LoadContent();
        }

        public void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, bool userDetected, bool kinectDetected)
        {
            this.kinectDetected = kinectDetected;

            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Kinect Status= " + setText(userDetected, kinectDetected), new Vector2(20, 90), Color.Black);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion


        #region NewMethods

        public string setText(bool status1, bool status2)
        {
            if (status1 && status2) return "Jesteś poprawnie wykrywany przez urządzenie...";
            else if (!status1) return "Błąd! Urządzenie nie jest widoczne!";
            else if (!status2) return "Błąd! Użytkownik niewidoczny!";
            else return "Błąd Kinect'a !!!";
        }

        #endregion
    }
}
