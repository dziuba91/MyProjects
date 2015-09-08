using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace KANDOU_v1.ComponentsActivity
{
    class MainKanjiGame
    {
        private Activity MainActivity;

        private KanjiDataType [] kanji;

        KanjiInfo kanjiInfo = null;

        Random random = new Random();

        int score = 0;
        int round = 0;

        Button bG;
        Button bR;

        int bG_id = 0;
        int bR_id = 0;

        Button b1;
        Button b2;
        Button b3;
        Button b4;
        Button b5;
        Button b6;

        TextView text;
        TextView scoreText;

        int CorectKanji = 0;
        int[] kanjiIndex = new int[6];
        int CorectKanjiIndex;

        bool goodAnswer = false;

        public MainKanjiGame(Activity mainActivity, KanjiDataType [] kanji)
        {
            this.MainActivity = mainActivity;

            this.kanji = kanji;
        }

        public void setKanjiGameRound()
        {
            int max_tab = kanji.Length;
            bool repeat = false;

            for (int i = 0; i < 6; i++)
            {
                int number = random.Next(0, max_tab);

              check:
                for (int j = 0; j < i; j++)
                {
                    if (kanjiIndex[j] == number || number == CorectKanji)
                    {
                        number++;
                        if (number == max_tab) number = 0;
                        repeat = true;
                        break;
                    }
                }

                if (repeat)
                {
                    repeat = false;
                    goto check;
                }

                kanjiIndex[i] = number;
            }

            CorectKanjiIndex = random.Next(0, 6);
            CorectKanji = kanjiIndex[CorectKanjiIndex];

            setKanjiMainData();
        }

        public void clearKanjiGameRound()
        {
            round = 0;
            score = 0;
        }


        public void correctAnswer(int index)
        {
            bR.Visibility = ViewStates.Invisible;

            bG.Visibility = ViewStates.Visible;
            bG.SetBackgroundColor(Color.Green);
            bG.Text = kanji[index].sign;
            bG_id = index;
        }

        public void incorrectAnswer(int indexG, int indexR)
        {
            bR.Visibility = ViewStates.Visible;
            bR.SetBackgroundColor(Color.Red);
            bR.Text = kanji[indexR].sign;
            bR_id = indexR;

            bG.Visibility = ViewStates.Visible;
            bG.SetBackgroundColor(Color.Green);
            bG.Text = kanji[indexG].sign;
            bG_id = indexG;
        }


        private void setKanjiMainContent()
        {
            bG = MainActivity.FindViewById<Button>(Resource.Id.buttonGreen);
            bR = MainActivity.FindViewById<Button>(Resource.Id.buttonRed);

            //TextView textK = FindViewById<TextView>(Resource.Id.textKanji);
            //TextView textM = FindViewById<TextView>(Resource.Id.textKanjiMeaning);
            //TextView textS = FindViewById<TextView>(Resource.Id.textSubmission);

            bG.Visibility = ViewStates.Invisible;
            bR.Visibility = ViewStates.Invisible;

            bG.Click += delegate
            {
                MainActivity.SetContentView(Resource.Layout.KanjiInfoLayout);

                if (kanjiInfo == null) kanjiInfo = new KanjiInfo(MainActivity, kanji, this);
                
                kanjiInfo.openLayoutActivity(bG_id);
            };
            bR.Click += delegate
            {
                MainActivity.SetContentView(Resource.Layout.KanjiInfoLayout);

                if (kanjiInfo == null) kanjiInfo = new KanjiInfo(MainActivity, kanji, this);

                kanjiInfo.openLayoutActivity(bR_id);
            };

            b1 = MainActivity.FindViewById<Button>(Resource.Id.button1);

            b2 = MainActivity.FindViewById<Button>(Resource.Id.button2);

            b3 = MainActivity.FindViewById<Button>(Resource.Id.button3);

            b4 = MainActivity.FindViewById<Button>(Resource.Id.button4);

            b5 = MainActivity.FindViewById<Button>(Resource.Id.button5);

            b6 = MainActivity.FindViewById<Button>(Resource.Id.button6);

            text = MainActivity.FindViewById<TextView>(Resource.Id.TEXT1);

            scoreText = MainActivity.FindViewById<TextView>(Resource.Id.textView1);

            b1.Click += delegate
            {
                if (CorectKanjiIndex == 0)
                {
                    score++;
                    correctAnswer(CorectKanji);

                    goodAnswer = true;
                }
                else
                {
                    incorrectAnswer(CorectKanji, kanjiIndex[0]);

                    goodAnswer = false;
                }

                round++;
                setKanjiGameRound();
            };
            b2.Click += delegate
            {
                if (CorectKanjiIndex == 1)
                {
                    score++;
                    correctAnswer(CorectKanji);

                    goodAnswer = true;
                }
                else
                {
                    incorrectAnswer(CorectKanji, kanjiIndex[1]);

                    goodAnswer = false;
                }

                round++;
                setKanjiGameRound();
            };
            b3.Click += delegate
            {
                if (CorectKanjiIndex == 2)
                {
                    score++;
                    correctAnswer(CorectKanji);

                    goodAnswer = true;
                }
                else
                {
                    incorrectAnswer(CorectKanji, kanjiIndex[2]);

                    goodAnswer = false;
                }

                round++;
                setKanjiGameRound();
            };
            b4.Click += delegate
            {
                if (CorectKanjiIndex == 3)
                {
                    score++;
                    correctAnswer(CorectKanji);

                    goodAnswer = true;
                }
                else
                {
                    incorrectAnswer(CorectKanji, kanjiIndex[3]);

                    goodAnswer = false;
                }

                round++;
                setKanjiGameRound();
            };
            b5.Click += delegate
            {
                if (CorectKanjiIndex == 4)
                {
                    score++;
                    correctAnswer(CorectKanji);

                    goodAnswer = true;
                }
                else
                {
                    incorrectAnswer(CorectKanji, kanjiIndex[4]);

                    goodAnswer = false;
                }

                round++;
                setKanjiGameRound();
            };
            b6.Click += delegate
            {
                if (CorectKanjiIndex == 5)
                {
                    score++;
                    correctAnswer(CorectKanji);

                    goodAnswer = true;
                }
                else
                {
                    incorrectAnswer(CorectKanji, kanjiIndex[5]);

                    goodAnswer = false;
                }

                round++;
                setKanjiGameRound();
            };
        }

        private void setKanjiMainData()
        {
            b1.Text = kanji[kanjiIndex[0]].sign;
            b2.Text = kanji[kanjiIndex[1]].sign;
            b3.Text = kanji[kanjiIndex[2]].sign;
            b4.Text = kanji[kanjiIndex[3]].sign;
            b5.Text = kanji[kanjiIndex[4]].sign;
            b6.Text = kanji[kanjiIndex[5]].sign;

            text.Text = kanji[CorectKanji].meaning + "\n" + kanji[CorectKanji].reading;

            scoreText.Text = score + "/" + round;
        }

        public void openLayoutActivity(bool newGame)
        {
            if (newGame)
            {
                setKanjiMainContent();
                setKanjiGameRound();
            }
            else
            {
                setKanjiMainContent();
                setKanjiMainData();
                if (goodAnswer) correctAnswer(bG_id);
                else incorrectAnswer(bG_id, bR_id);
            }
        }

        public void closeLayoutActivity()
        {
            clearKanjiGameRound();
        }
    }
}