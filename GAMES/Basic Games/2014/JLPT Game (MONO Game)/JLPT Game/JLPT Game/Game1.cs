#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using JLPT_Game.Components;
#endregion

namespace JLPT_Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont text;
        Texture2D cursor;
        Vector2 textureCenter;

        KeyboardState currentKeyboard;
        MouseState d;

        MainMenu mainMenu;
        KanjiGame kanjiGame;
        KanjiMenu kanjiMenu;
        KanjiList kanjiList;
        SubmissionGame submissionGame;
        VocabularyMenu vocabularyMenu;
        VocabularyGame vocabularyGame;
        VocabularyList vocabularyList;
        VocabularyGame2 vocabularyGame2;

        Texture2D blank;

        bool pressed = false;
        bool changeMenu = false;

        //XDocument xml;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //IsMouseVisible = false;

            d = Mouse.GetState();


            this.mainMenu = new MainMenu(this);
            Components.Add(this.mainMenu);

            //this.kanjiGame = new KanjiGame(this);
            //Components.Add(this.kanjiGame);

            this.kanjiMenu = new KanjiMenu(this);
            Components.Add(this.kanjiMenu);

            //this.kanjiList = new KanjiList(this);
            //Components.Add(this.kanjiList);

            //this.submissionGame = new SubmissionGame(this);
            //Components.Add(this.submissionGame);

            this.vocabularyMenu = new VocabularyMenu(this);  //?
            Components.Add(this.vocabularyMenu);

            //this.vocabularyGame = new VocabularyGame(this);
            //Components.Add(this.vocabularyGame);

            //this.vocabularyList = new VocabularyList(this);
            //Components.Add(this.vocabularyList);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), this.spriteBatch);

            text = Content.Load<SpriteFont>("test");
            cursor = Content.Load<Texture2D>("cursor");

            //xml = Content.Load<XDocument>("XMLFile1");

            blank = new Texture2D(GraphicsDevice, 1, 1);
            blank.SetData(new[] { Color.White });

            textureCenter = new Vector2(cursor.Width / 2.0f, cursor.Height / 2.0f);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            d = Mouse.GetState();

            if (d.X > 0 && d.X < GraphicsDevice.Viewport.Width-1 && d.Y > 0 && d.Y < GraphicsDevice.Viewport.Height-1) IsMouseVisible = false;
            else
                IsMouseVisible = true;

            // Allows the game to exit
            this.currentKeyboard = Keyboard.GetState();

            if (this.currentKeyboard.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //string t1 = "x= " + d.X + "; y= " + d.Y;
            Vector2 position = new Vector2(d.X, d.Y);

            if (mainMenu.selectItemNumber == 0)
            {
                mainMenu.Draw(gameTime, d, position);
                //kanjiGame.lotery = true;
                //submissionGame.lotery = true;
                //vocabularyGame.lotery = true;
            }
            else if (mainMenu.selectItemNumber == 1)
            {
                if (kanjiMenu.selectItemNumber == 0)
                {
                    kanjiMenu.Draw(gameTime, d, position);
                }
                else if (kanjiMenu.selectItemNumber == 1)
                {
                    if (kanjiGame == null)
                    {
                        this.kanjiGame = new KanjiGame(this);
                        Components.Add(this.kanjiGame);
                    }

                    kanjiGame.Draw(gameTime, d, position);
                    kanjiGame.Update(gameTime);
                }
                else if (kanjiMenu.selectItemNumber == 2)
                {
                    if (submissionGame == null)
                    {
                        this.submissionGame = new SubmissionGame(this);
                        Components.Add(this.submissionGame);
                    }

                    submissionGame.Draw(gameTime, d, position);
                    submissionGame.Update(gameTime);
                }
                else if (kanjiMenu.selectItemNumber == 3)
                {
                    if (kanjiList == null)
                    {
                        this.kanjiList = new KanjiList(this);
                        Components.Add(this.kanjiList);
                    }

                    kanjiList.Draw(gameTime, d, position);
                    kanjiList.Update(gameTime);
                }
                else
                { }

                back(position);
            }
            else if (mainMenu.selectItemNumber == 2)
            {
                if (vocabularyMenu.selectItemNumber == 0)
                {
                    vocabularyMenu.Draw(gameTime, d, position);
                }
                else if (vocabularyMenu.selectItemNumber == 1)
                {
                    if (vocabularyGame == null)
                    {
                        this.vocabularyGame = new VocabularyGame(this);
                        Components.Add(this.vocabularyGame);
                    }

                    vocabularyGame.Draw(gameTime, d, position);
                }
                else if (vocabularyMenu.selectItemNumber == 2)
                {
                    if (vocabularyGame2 == null)
                    {
                        this.vocabularyGame2 = new VocabularyGame2(this);
                        Components.Add(this.vocabularyGame2);
                    }

                    vocabularyGame2.Draw(gameTime, d, position);
                }
                else if (vocabularyMenu.selectItemNumber == 3)
                {
                    if (vocabularyList == null)
                    {
                        this.vocabularyList = new VocabularyList(this);
                        Components.Add(this.vocabularyList);
                    }

                    vocabularyList.Draw(gameTime, d, position);
                    vocabularyList.Update(gameTime);
                }
                else
                { }

                back(position);
            }
            else
            {
                back(position);
            }

            spriteBatch.Begin();

            //spriteBatch.DrawString(text, t1, new Vector2(625.0f, 25.0f), Color.Black);
            //spriteBatch.DrawString(text, mainMenu.selectItemNumber.ToString(), new Vector2(625.0f, 50.0f), Color.Black);

            if (d.X > 0 && d.X < GraphicsDevice.Viewport.Width && d.Y > 0 && d.Y < GraphicsDevice.Viewport.Height)
            {
                spriteBatch.Draw(cursor, position, null, Color.White, 0.0f, textureCenter, 1.0f, SpriteEffects.None, 0.0f);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }


        private void back(Vector2 position)
        {
            spriteBatch.Begin();

            int border = 15;
            string t1 = "wróć";

            Rectangle textPosition = new Rectangle(
                    border,
                    (int)(GraphicsDevice.Viewport.Height - (border + text.MeasureString(t1).Y)),
                    (int)(text.MeasureString(t1).X + border),
                    (int)(text.MeasureString(t1).Y + border));

            spriteBatch.Draw(blank, textPosition, Color.White);


            Vector2 miPosition = new Vector2((int)((textPosition.X + textPosition.Width / 2) - text.MeasureString(t1).X / 2), textPosition.Y + 5);

            if ((position.X >= miPosition.X && position.X <= miPosition.X + text.MeasureString(t1).X) &&
                (position.Y >= miPosition.Y && position.Y <= miPosition.Y + text.MeasureString(t1).Y))
            {
                if (d.LeftButton == ButtonState.Pressed)
                {
                    spriteBatch.DrawString(text, t1, miPosition, Color.White);
                    pressed = true;
                }
                else if (pressed)
                {
                    deleteObject();

                    pressed = false;
                    mainMenu.selectItemNumber = 0;
                    kanjiMenu.selectItemNumber = 0;
                    vocabularyMenu.selectItemNumber = 0;
                }
                else
                    spriteBatch.DrawString(text, t1, miPosition, Color.Red);
            }
            else
                spriteBatch.DrawString(text, t1, miPosition, Color.Black);

            spriteBatch.End();
        }

        private void deleteObject()
        {
            if (mainMenu.selectItemNumber == 1)
            {
                if (kanjiMenu.selectItemNumber == 1)
                {
                    Components.Remove(this.kanjiGame);
                    kanjiGame.Dispose();
                    kanjiGame = null;
                }
                else if (kanjiMenu.selectItemNumber == 2)
                {
                    Components.Remove(this.submissionGame);
                    submissionGame.Dispose();
                    submissionGame = null;
                }
                else if (kanjiMenu.selectItemNumber == 3)
                {
                    Components.Remove(this.kanjiList);
                    kanjiList.Dispose();
                    kanjiList = null;
                }
                else
                { }
            }
            else if (mainMenu.selectItemNumber == 2)
            {
                if (vocabularyMenu.selectItemNumber == 1)
                {
                    Components.Remove(this.vocabularyGame);
                    vocabularyGame.Dispose();
                    vocabularyGame = null;
                }
                else if (vocabularyMenu.selectItemNumber == 2)
                {
                    Components.Remove(this.vocabularyGame2);
                    vocabularyGame2.Dispose();
                    vocabularyGame2 = null;
                }
                else if (vocabularyMenu.selectItemNumber == 3)
                {
                    Components.Remove(this.vocabularyList);
                    vocabularyList.Dispose();
                    vocabularyList = null;
                }
                else
                { }
            }
            else { }
        }
    }
}
