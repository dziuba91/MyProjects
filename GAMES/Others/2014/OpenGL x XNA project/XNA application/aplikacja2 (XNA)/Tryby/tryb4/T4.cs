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
    class T4 : DrawableGameComponent
    {
        #region Field

        private int tryb = 1;

        KeyboardState currentKeyboard;
        KeyboardState previousKeyboard;

        //Cube cube;
        public Rubik rubik;

        #endregion


        #region Initialization

        public T4(Game game)
            : base(game)
        {
            //cube = new Cube(game, new Vector3(1.0f, 1.0f, 1.0f), Vector3.Zero);
            //game.Components.Add(this.cube);

            rubik = new Rubik(game, new Vector3(1.0f, 1.0f, 1.0f), Vector3.Zero);
            game.Components.Add(this.rubik);
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

        }

        public void Update(GameTime gameTime)
        {
            currentKeyboard = Keyboard.GetState();

            if (this.currentKeyboard.IsKeyDown(Keys.Space))
            {
                if (!this.previousKeyboard.IsKeyDown(Keys.Space))
                {
                    if (tryb == 1) tryb = 2;
                    else tryb = 1;
                }
            }

            previousKeyboard = currentKeyboard;

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            if (tryb == 1)
                rubik.Draw1(gameTime);
            else if (tryb == 2)
                rubik.Draw2(gameTime);

            rubik.Update(gameTime);

            base.Draw(gameTime);
        }

        /*
        public void Draw(GameTime gameTime, MouseState d, Vector2 position)
        {

            base.Draw(gameTime);
        }
         * */

        #endregion


        #region Private Methods


        #endregion
    }
}
