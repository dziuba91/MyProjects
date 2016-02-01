using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.InteropServices;
using System.IO;

namespace KinectWspolbiezny
{
    public class Plane : DrawableGameComponent
    {
        public Vector2 Size;
        public Vector2 Position;

        Texture2D blank;
        SpriteFont font;
        int[] arr;

        List<float> mylistX;
        List<float> mylistY;

        List<float> mylistX2;
        List<float> mylistY2;

        List<int> mylist;

        int index = 0;
        int index2 = 0;

        public int index3 = 0;

        bool kreska = false;

        int a = 0;

        int t1_i = 0;
        int t3_i = 0;

        private KanjiPanel kanji_panel;


        [DllImport("obliczenia.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int kinus(float[] tX, float[] tY, int[] tI, int p1, int p2);

        /// <summary>
        /// Initializes a new instance of the DepthStreamRenderer class.
        /// </summary>
        /// <param name="game">The related game object.</param>
        public Plane(Game game)
            : base(game)
        {
            this.Size = new Vector2(320, 240);
            this.Position = new Vector2(460, 220);

            mylistX = new List<float>();
            mylistY = new List<float>();

            mylistX2 = new List<float>();
            mylistY2 = new List<float>();

            mylist = new List<int>();


            this.kanji_panel = new KanjiPanel(game);
            Game.Components.Add(this.kanji_panel);
        }

        /// <summary>
        /// Gets the SpriteBatch from the services.
        /// </summary>
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

        public void Draw(GameTime gameTime, Vector2 p1, Vector2 p11, bool p2, bool p3)
        {
            int painterWidth = 24;
            //int licz = 0;

            if (p3) kreska = true;
            if (!p3 && kreska)
            {
                kreska = false;
                index3++;
                mylist.Add(index);
                index=0;
            }

            spriteBatch.Begin();

            spriteBatch.DrawString(font, "il. kresek = " + index3, new Vector2(Position.X, Position.Y-20), Color.Black);

            spriteBatch.Draw(blank, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color.White);

            for (int i = 0; i < index2; i++)
            {
                spriteBatch.Draw(blank, new Rectangle((int)(mylistX2[i] + Position.X - painterWidth / 2), (int)(mylistY2[i] + Position.Y - painterWidth / 2), painterWidth, painterWidth), Color.Black);
            }

            if (p2)
            {
                spriteBatch.Draw(blank, new Rectangle((int)(p1.X + Position.X - painterWidth / 2), (int)(p1.Y + Position.Y - painterWidth / 2), painterWidth, painterWidth), Color.Brown);

                if (p3)
                {
                    mylistX.Add(p11.X);
                    mylistY.Add(p11.Y);
                    mylistX2.Add(p1.X);
                    mylistY2.Add(p1.Y);
                    index++;
                    index2++;
                }
            }

            spriteBatch.DrawString(font, "a = " + a.ToString(), new Vector2(Position.X +120, Position.Y - 20), Color.Black);
            spriteBatch.DrawString(font, "b = " + t1_i.ToString(), new Vector2(Position.X + 210, Position.Y - 20), Color.Black);

            //spriteBatch.DrawString(font, "t1 = " + t1_i, new Vector2(Position.X + 200, Position.Y - 20), Color.Black);

            //spriteBatch.DrawString(font, "t2 = " + t3_i, new Vector2(Position.X + 300, Position.Y - 20), Color.Black);

            spriteBatch.End();

            kanji_panel.Draw(gameTime);

            base.Draw(gameTime);
        }

        /// <summary>
        /// This method loads the Xna effect.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void clear()
        {
            index = 0;
            index2 = 0;
            index3 = 0;

            mylist.Clear();
            mylistX.Clear();
            mylistY.Clear();

            mylistX2.Clear();
            mylistY2.Clear();

            kanji_panel.index = -1;
        }

        public void realize()
        {
            float[] t1 = mylistX2.ToArray();
            float[] t2 = mylistY2.ToArray();
            int[] t3 = mylist.ToArray();

            t1_i = t1.Length;
            t3_i = t3.Length;

            if (t1_i!=0)
            a = kinus(t1,t2,t3,t1_i,t3_i);

            if (a > 10000 && a < 20000)
            {
                kanji_panel.index = 0;
                kanji_panel.text_panel.index = 0;
            }
            else if (a > 20000 && a < 30000)
            {
                kanji_panel.index = 1;
                kanji_panel.text_panel.index = 1;
            }
            else if (a > 30000 && a < 40000)
            {
                kanji_panel.index = 2;
                kanji_panel.text_panel.index = 2;
            }

            string path = @"dane\x.txt";
            StreamWriter sw = new StreamWriter(path);
            //sw.Write("Ala");
            for (int i = 0; i < t1_i;i++)
                sw.WriteLine(t1[i] + " " + t2[i]);
            //sw.WriteLine("i tak dalej!");
            sw.Close();
        }
    }
}
