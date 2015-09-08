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

using KANDOU_v1.DataTypes;

namespace KANDOU_v1.ComponentsActivity
{
    class MainVocabularyGame
    {
        private Activity MainActivity;

        public SubmissionOfKanji [] vocabulary;

        public VocabularyInfoExtended vocabularyInfoExtended = null;

        public Random random = new Random();

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

        TextView text1;
        TextView text2;
        TextView scoreText;

        public int CorectVocabulary = 0;
        public int[] vocabularyIndex = new int[6];
        public int CorectVocabularyIndex;

        bool goodAnswer = false;

        bool pl_japMode = false;

        bool text1_switch = true;

        ObjectPermission[] vocabularySelectedExtendedObjectList;

        bool[] vocabularySelectedExtendedList;

        public MainVocabularyGame(Activity mainActivity, SubmissionOfKanji[] vocabulary, ObjectPermission[] vocabularySelectedExtendedObjectList, bool[] vocabularySelectedExtendedList)
        {
            this.MainActivity = mainActivity;

            this.vocabulary = vocabulary;

            this.vocabularySelectedExtendedObjectList = vocabularySelectedExtendedObjectList;

            this.vocabularySelectedExtendedList = vocabularySelectedExtendedList;
        }

        public MainVocabularyGame(Activity mainActivity, SubmissionOfKanji[] vocabulary, bool pl_japMode, ObjectPermission[] vocabularySelectedExtendedObjectList, bool[] vocabularySelectedExtendedList)
        {
            this.MainActivity = mainActivity;

            this.vocabulary = vocabulary;

            this.pl_japMode = pl_japMode;

            this.vocabularySelectedExtendedObjectList = vocabularySelectedExtendedObjectList;

            this.vocabularySelectedExtendedList = vocabularySelectedExtendedList;
        }

        public MainVocabularyGame(Activity mainActivity, SubmissionOfKanji[] vocabulary)
        {
            this.MainActivity = mainActivity;

            this.vocabulary = vocabulary;
        }

        public virtual void setVocabularyGameRound()
        {
            int max_tab = vocabulary.Length;
            bool repeat = false;

            for (int i = 0; i < 6; i++)
            {
                int number = random.Next(0, max_tab);

              check:
                for (int j = 0; j < i; j++)
                {
                    if (vocabularyIndex[j] == number || number == CorectVocabulary)
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

                vocabularyIndex[i] = number;
            }

            CorectVocabularyIndex = random.Next(0, 6);
            CorectVocabulary = vocabularyIndex[CorectVocabularyIndex];

            setVocabularyMainData();
        }

        public void clearVocabularyGameRound()
        {
            round = 0;
            score = 0;
        }


        public void correctAnswer(int index)
        {
            bR.Visibility = ViewStates.Invisible;

            bG.Visibility = ViewStates.Visible;
            bG.SetBackgroundColor(Color.Green);
            bG.Text = vocabulary[index].reading;
            bG_id = index;
        }

        public void incorrectAnswer(int indexG, int indexR)
        {
            bR.Visibility = ViewStates.Visible;
            bR.SetBackgroundColor(Color.Red);
            bR.Text = vocabulary[indexR].reading;
            bR_id = indexR;

            bG.Visibility = ViewStates.Visible;
            bG.SetBackgroundColor(Color.Green);
            bG.Text = vocabulary[indexG].reading;
            bG_id = indexG;
        }


        private void setVocabularyMainContent()
        {
            bG = MainActivity.FindViewById<Button>(Resource.Id.buttonGreen_2);
            bR = MainActivity.FindViewById<Button>(Resource.Id.buttonRed_2);

            bG.Visibility = ViewStates.Invisible;
            bR.Visibility = ViewStates.Invisible;

            bG.Click += delegate
            {
                MainActivity.SetContentView(Resource.Layout.VocabularyInfoLayout);

                if (vocabularyInfoExtended == null) vocabularyInfoExtended = new VocabularyInfoExtended(MainActivity, vocabulary, this, vocabularySelectedExtendedObjectList, vocabularySelectedExtendedList);

                vocabularyInfoExtended.openLayoutActivity(bG_id);
            };
            bR.Click += delegate
            {
                MainActivity.SetContentView(Resource.Layout.VocabularyInfoLayout);

                if (vocabularyInfoExtended == null) vocabularyInfoExtended = new VocabularyInfoExtended(MainActivity, vocabulary, this, vocabularySelectedExtendedObjectList, vocabularySelectedExtendedList);

                vocabularyInfoExtended.openLayoutActivity(bR_id);
            };

            b1 = MainActivity.FindViewById<Button>(Resource.Id.button1_2);

            b2 = MainActivity.FindViewById<Button>(Resource.Id.button2_2);

            b3 = MainActivity.FindViewById<Button>(Resource.Id.button3_2);

            b4 = MainActivity.FindViewById<Button>(Resource.Id.button4_2);

            b5 = MainActivity.FindViewById<Button>(Resource.Id.button5_2);

            b6 = MainActivity.FindViewById<Button>(Resource.Id.button6_2);

            text1 = MainActivity.FindViewById<TextView>(Resource.Id.TEXT1_2);
            if (!pl_japMode)
            {
                text1.Click += delegate
                {
                    if (text1_switch)
                        text1_switch = false;
                    else
                        text1_switch = true;

                    setVocabularyMainData();
                };
            }

            text2 = MainActivity.FindViewById<TextView>(Resource.Id.TEXT2_2);

            scoreText = MainActivity.FindViewById<TextView>(Resource.Id.textView1_2);

            b1.Click += delegate
            {
                if (CorectVocabularyIndex == 0)
                {
                    score++;
                    correctAnswer(CorectVocabulary);

                    goodAnswer = true;
                }
                else
                {
                    incorrectAnswer(CorectVocabulary, vocabularyIndex[0]);

                    goodAnswer = false;
                }

                round++;
                setVocabularyGameRound();
            };
            b2.Click += delegate
            {
                if (CorectVocabularyIndex == 1)
                {
                    score++;
                    correctAnswer(CorectVocabulary);

                    goodAnswer = true;
                }
                else
                {
                    incorrectAnswer(CorectVocabulary, vocabularyIndex[1]);

                    goodAnswer = false;
                }

                round++;
                setVocabularyGameRound();
            };
            b3.Click += delegate
            {
                if (CorectVocabularyIndex == 2)
                {
                    score++;
                    correctAnswer(CorectVocabulary);

                    goodAnswer = true;
                }
                else
                {
                    incorrectAnswer(CorectVocabulary, vocabularyIndex[2]);

                    goodAnswer = false;
                }

                round++;
                setVocabularyGameRound();
            };
            b4.Click += delegate
            {
                if (CorectVocabularyIndex == 3)
                {
                    score++;
                    correctAnswer(CorectVocabulary);

                    goodAnswer = true;
                }
                else
                {
                    incorrectAnswer(CorectVocabulary, vocabularyIndex[3]);

                    goodAnswer = false;
                }

                round++;
                setVocabularyGameRound();
            };
            b5.Click += delegate
            {
                if (CorectVocabularyIndex == 4)
                {
                    score++;
                    correctAnswer(CorectVocabulary);

                    goodAnswer = true;
                }
                else
                {
                    incorrectAnswer(CorectVocabulary, vocabularyIndex[4]);

                    goodAnswer = false;
                }

                round++;
                setVocabularyGameRound();
            };
            b6.Click += delegate
            {
                if (CorectVocabularyIndex == 5)
                {
                    score++;
                    correctAnswer(CorectVocabulary);

                    goodAnswer = true;
                }
                else
                {
                    incorrectAnswer(CorectVocabulary, vocabularyIndex[5]);

                    goodAnswer = false;
                }

                round++;
                setVocabularyGameRound();
            };
        }

        public void setVocabularyMainData()
        {
            if (!pl_japMode)
            {
                b1.Text = vocabulary[vocabularyIndex[0]].meaning;
                b2.Text = vocabulary[vocabularyIndex[1]].meaning;
                b3.Text = vocabulary[vocabularyIndex[2]].meaning;
                b4.Text = vocabulary[vocabularyIndex[3]].meaning;
                b5.Text = vocabulary[vocabularyIndex[4]].meaning;
                b6.Text = vocabulary[vocabularyIndex[5]].meaning;

                text1.Text = vocabulary[CorectVocabulary].reading;
                
                if (text1_switch)
                    text2.Text = vocabulary[CorectVocabulary].signs.Split('[')[0];
                else text2.Text = "";
            }
            else
            {
                b1.Text = vocabulary[vocabularyIndex[0]].reading;
                b2.Text = vocabulary[vocabularyIndex[1]].reading;
                b3.Text = vocabulary[vocabularyIndex[2]].reading;
                b4.Text = vocabulary[vocabularyIndex[3]].reading;
                b5.Text = vocabulary[vocabularyIndex[4]].reading;
                b6.Text = vocabulary[vocabularyIndex[5]].reading;

                text1.Text = vocabulary[CorectVocabulary].meaning;
                text2.Text = "";
            }

            scoreText.Text = score + "/ " + round;
        }

        public void openLayoutActivity(bool newGame)
        {
            if (newGame)
            {
                setVocabularyMainContent();
                setVocabularyGameRound();
            }
            else
            {
                setVocabularyMainContent();
                setVocabularyMainData();
                if (goodAnswer) correctAnswer(bG_id);
                else incorrectAnswer(bG_id, bR_id);
            }
        }

        public void closeLayoutActivity()
        {
            clearVocabularyGameRound();

            text1_switch = true;
        }

        public void actualizeVocabularyList(SubmissionOfKanji[] vocabularyList)
        {
            this.vocabulary = vocabularyList;
        }

        public void actualizeVocabularyExtendedList(bool[] vocabularyList, ObjectPermission[] vocabularyObjectList)
        {
            this.vocabularySelectedExtendedList = vocabularyList;
            this.vocabularySelectedExtendedObjectList = vocabularyObjectList;
        }
    }
}