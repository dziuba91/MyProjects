using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XMLDataTypes;

namespace JLPT_Game.Components
{
    class KanjiList : DrawableGameComponent
    {
        #region Field

        Rectangle mainText;
        Rectangle chooseText;

        KanjiDataType [] kanji;

        SpriteFont text1;
        SpriteFont text2;
        SpriteFont text3;
        SpriteFont text4;
        SpriteFont text5;

        Texture2D blank;

        bool pressed = false;

        public int index = 0;

        KeyboardState previousKeyboard;
        KeyboardState currentKeyboard;

        bool extendedBarON = false;
        int maxIndex;
        int beginSubmissionText;
        int startIndex=0;

        bool show_texts = true;

        #endregion


        #region Initialization

        public KanjiList(Game game)
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
            text1 = Game.Content.Load<SpriteFont>("kanjiText1");
            text2 = Game.Content.Load<SpriteFont>("kanjiText2");
            text3 = Game.Content.Load<SpriteFont>("kanjiText3");
            text4 = Game.Content.Load<SpriteFont>("kanjiText4");
            text5 = Game.Content.Load<SpriteFont>("test");

            kanji = Game.Content.Load<KanjiDataType[]>("XMLFile1");

            blank = new Texture2D(GraphicsDevice, 1, 1);
            blank.SetData(new[] { Color.White });

            constructWirtualMenu();

            base.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            this.currentKeyboard = Keyboard.GetState();

            if (!show_texts)
            {
                if (this.currentKeyboard.IsKeyDown(Keys.Up))
                {
                    if (!this.previousKeyboard.IsKeyDown(Keys.Up))
                    {
                        if (index + 1 < kanji.Length)
                            index += 1;
                    }
                }

                if (this.currentKeyboard.IsKeyDown(Keys.Down))
                {
                    if (!this.previousKeyboard.IsKeyDown(Keys.Down))
                    {
                        if (index - 1 >= 0)
                            index -= 1;
                    }
                }
            }

            if (this.currentKeyboard.IsKeyDown(Keys.Right))
            {
                if (!this.previousKeyboard.IsKeyDown(Keys.Right))
                {
                    if (index + 25 < kanji.Length)
                    {
                        index += 25;

                        extendedBarON = false;
                        startIndex = 0;
                    }
                }
            }

            if (this.currentKeyboard.IsKeyDown(Keys.Left))
            {
                if (!this.previousKeyboard.IsKeyDown(Keys.Left))
                {
                    if (index - 25 >= 0)
                    {
                        index -= 25;

                        extendedBarON = false;
                        startIndex = 0;
                    }
                }
            }

            if (this.currentKeyboard.IsKeyDown(Keys.Down))
            {
                if (!this.previousKeyboard.IsKeyDown(Keys.Down))
                {
                    if (startIndex + maxIndex - 1 < kanji[index].submissions.Length && extendedBarON)
                        startIndex++;
                }
            }

            if (this.currentKeyboard.IsKeyDown(Keys.Up))
            {
                if (!this.previousKeyboard.IsKeyDown(Keys.Up))
                {
                    if (startIndex - 1 >= 0 && extendedBarON)
                        startIndex--;
                }
            }

            if (this.currentKeyboard.IsKeyDown(Keys.Space))
            {
                if (!this.previousKeyboard.IsKeyDown(Keys.Space))
                {
                    if (show_texts) show_texts = false;
                    else show_texts = true;
                }
            }

            previousKeyboard = currentKeyboard;

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, MouseState d, Vector2 position)
        {

            string test1 = kanji[index].sign;
            string test2 = kanji[index].reading;
            string test3 = kanji[index].meaning;

            spriteBatch.Begin();

            //
            // actual index
            spriteBatch.DrawString(text2, (index + 1) + " / " + kanji.Length,new Vector2(GraphicsDevice.Viewport.Width - 150, 10), Color.Black);


            //
            // must "constructWirtualMenu()" -> realized

            spriteBatch.Draw(blank, mainText, Color.White);
            

            //
            // must "constructWirtualMenu()" -> realized

            spriteBatch.Draw(blank, mainText, Color.White);

            int lineSpacing = 15;
            //int textPosition = mainText.Y + lineSpacing;
            int textSpacing = lineSpacing + mainText.Y;

            Vector2 meaningPosition = new Vector2((int)((mainText.X + mainText.Width / 2) - text3.MeasureString(test1).X / 2), textSpacing);

            spriteBatch.DrawString(text3, test1, meaningPosition, Color.Black);


            if (show_texts)  //fiszki mode
            {
                textSpacing += (int)(text3.MeasureString(test1).Y + lineSpacing);

                test2 = "(" + test2 + ")";

                Vector2 readingPosition = new Vector2((int)((mainText.X + mainText.Width / 2) - text4.MeasureString(test2).X / 2), textSpacing);

               spriteBatch.DrawString(text4, test2, readingPosition, Color.Black);


                // must "constructWirtualMenu()" -> realized

                spriteBatch.Draw(blank, chooseText, Color.White);

                //int lineSpacing = 15;
                //int textPosition = mainText.Y + lineSpacing;

                textSpacing = lineSpacing + chooseText.Y;

                Vector2 choosePosition1 = new Vector2((int)((chooseText.X + chooseText.Width / 2) - text2.MeasureString(test3).X / 2), textSpacing);

                spriteBatch.DrawString(text2, test3, choosePosition1, Color.Black);


                lineSpacing = 25;

                textSpacing += ((int)text2.MeasureString(test3).Y + lineSpacing - 10);

                beginSubmissionText = textSpacing;
            }



            // ramki

            int buttonSpacing = 20;
            int buttonWidth = 20;

            Rectangle previousButton = new Rectangle(0, chooseText.Y + buttonSpacing, buttonWidth, chooseText.Height - buttonSpacing * 2);

            if (index > 0)
            {
                if ((position.X >= previousButton.X && position.X <= previousButton.X + previousButton.Width) &&
                (position.Y >= previousButton.Y && position.Y <= previousButton.Y + previousButton.Height))
                {
                    if (d.LeftButton == ButtonState.Pressed)
                    {
                        spriteBatch.Draw(blank, previousButton, Color.PaleVioletRed);
                        pressed = true;
                    }
                    else if (pressed)
                    {
                        pressed = false;
                        extendedBarON = false;
                        startIndex = 0;
                        index--;
                    }
                    else
                        spriteBatch.Draw(blank, previousButton, Color.OrangeRed);
                }
                else
                    spriteBatch.Draw(blank, previousButton, Color.Red);
            }
            else
                spriteBatch.Draw(blank, previousButton, Color.DarkRed);


            Rectangle nextButton = new Rectangle(GraphicsDevice.Viewport.Width - buttonWidth, chooseText.Y + buttonSpacing, buttonWidth, chooseText.Height - buttonSpacing * 2);

            if (index < kanji.Length - 1)
            {
                if ((position.X >= nextButton.X && position.X <= nextButton.X + nextButton.Width) &&
                (position.Y >= nextButton.Y && position.Y <= nextButton.Y + nextButton.Height))
                {
                    if (d.LeftButton == ButtonState.Pressed)
                    {
                        spriteBatch.Draw(blank, nextButton, Color.PaleVioletRed);
                        pressed = true;
                    }
                    else if (pressed)
                    {
                        pressed = false;
                        extendedBarON = false;
                        startIndex = 0;
                        index++;
                    }
                    else
                        spriteBatch.Draw(blank, nextButton, Color.OrangeRed);
                }
                else
                    spriteBatch.Draw(blank, nextButton, Color.Red);
            }
            else
                spriteBatch.Draw(blank, nextButton, Color.DarkRed);



            //złożenia

            if (show_texts)
            {
                if (kanji[index].submissions.Length != 0)
                {
                    SubmissionOfKanji[] test5 = kanji[index].submissions;

                    for (int i = startIndex; i < test5.Length; i++)
                    {
                        string test6 = test5[i].signs + " (" + test5[i].reading + ") -> " + test5[i].meaning;
                        Vector2 choosePosition2 = new Vector2(30, textSpacing);

                        if (choosePosition2.Y + text4.MeasureString(test6).Y > chooseText.Y + chooseText.Height)
                        {
                            if (!extendedBarON)
                            {
                                maxIndex = i;
                                extendedBarON = true;
                            }

                            break;
                        }
                        spriteBatch.DrawString(text4, test6, choosePosition2, Color.Black);

                        textSpacing += lineSpacing;
                    }

                    if (extendedBarON)
                    {
                        if (test5.Length - maxIndex + 2 > 0) { }
                        else
                        {
                            extendedBarON = false;
                            goto endDraw;
                        }

                        Rectangle extendedBar = new Rectangle(
                                nextButton.X - 8,
                                beginSubmissionText,
                                5,
                                chooseText.Y + chooseText.Height - beginSubmissionText - 6
                            );

                        spriteBatch.Draw(blank, extendedBar, Color.Gray);


                        float heightBar = extendedBar.Height / (test5.Length - maxIndex + 2);
                        float startBar = extendedBar.Y + startIndex * heightBar;

                        Rectangle actuallBar = new Rectangle(
                                extendedBar.X,
                                (int)startBar,
                                extendedBar.Width,
                                (int)heightBar
                            );

                        spriteBatch.Draw(blank, actuallBar, Color.Purple);
                    }
                }
            }
            endDraw:
            
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion


        #region privateMethods

        private void constructWirtualMenu()
        {
            mainText = new Rectangle(
                (int)0,
                (int)(GraphicsDevice.Viewport.Height / 10),
                (int)(GraphicsDevice.Viewport.Width),
                (int)(GraphicsDevice.Viewport.Height / 4));

            //
            int upSpace = 15;
            int downSpace = 50;

            chooseText = new Rectangle(
                (int)0,
                (int)(mainText.Y + mainText.Height + upSpace),
                (int)(GraphicsDevice.Viewport.Width),
                (int)(GraphicsDevice.Viewport.Height - (mainText.Y + mainText.Height + upSpace) - downSpace));

        }

        public void setNewKanjiArea()
        {
            extendedBarON = false;
            startIndex = 0;
        }

        #endregion
    }
}
