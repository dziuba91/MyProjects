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
using System.Collections;
using Android.Graphics;

namespace KANDOU_v1.ComponentsActivity
{
    class SubmissionOfKanjiGame
    {
        private Activity MainActivity;

        private SubmissionOfKanji [] submissions;

        SubmissionOfKanjiInfo submissionOfKanjiInfo = null;

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

        TextView text1;
        TextView text2;
        TextView scoreText;

        int CorectVocabulary = 0;
        int[] vocabularyIndex = new int[6];
        int CorectVocabularyIndex;

        bool goodAnswer = false;

        bool text2_switch = false;

        public SubmissionOfKanjiGame(Activity mainActivity, SubmissionOfKanji[] submissions)
        {
            this.MainActivity = mainActivity;

            this.submissions = submissions;
        }

        public void setVocabularyGameRound()
        {
            int max_tab = submissions.Length;
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
            bG.Text = submissions[index].signs;
            bG_id = index;
        }

        public void incorrectAnswer(int indexG, int indexR)
        {
            bR.Visibility = ViewStates.Visible;
            bR.SetBackgroundColor(Color.Red);
            bR.Text = submissions[indexR].signs;
            bR_id = indexR;

            bG.Visibility = ViewStates.Visible;
            bG.SetBackgroundColor(Color.Green);
            bG.Text = submissions[indexG].signs;
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

                if (submissionOfKanjiInfo == null) submissionOfKanjiInfo = new SubmissionOfKanjiInfo(MainActivity, submissions, this);

                submissionOfKanjiInfo.openLayoutActivity(bG_id);
            };
            bR.Click += delegate
            {
                MainActivity.SetContentView(Resource.Layout.VocabularyInfoLayout);

                if (submissionOfKanjiInfo == null) submissionOfKanjiInfo = new SubmissionOfKanjiInfo(MainActivity, submissions, this);

                submissionOfKanjiInfo.openLayoutActivity(bR_id);
            };

            b1 = MainActivity.FindViewById<Button>(Resource.Id.button1_2);

            b2 = MainActivity.FindViewById<Button>(Resource.Id.button2_2);

            b3 = MainActivity.FindViewById<Button>(Resource.Id.button3_2);

            b4 = MainActivity.FindViewById<Button>(Resource.Id.button4_2);

            b5 = MainActivity.FindViewById<Button>(Resource.Id.button5_2);

            b6 = MainActivity.FindViewById<Button>(Resource.Id.button6_2);

            text1 = MainActivity.FindViewById<TextView>(Resource.Id.TEXT1_2);
            text1.Click += delegate
            {
                if (text2_switch)
                    text2_switch = false;
                else text2_switch = true;

                setVocabularyMainData();
            };

            text2 = MainActivity.FindViewById<TextView>(Resource.Id.TEXT2_2);
            //text2.Visibility = ViewStates.Invisible;

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

        private void setVocabularyMainData()
        {
            b1.Text = submissions[vocabularyIndex[0]].meaning;
            b2.Text = submissions[vocabularyIndex[1]].meaning;
            b3.Text = submissions[vocabularyIndex[2]].meaning;
            b4.Text = submissions[vocabularyIndex[3]].meaning;
            b5.Text = submissions[vocabularyIndex[4]].meaning;
            b6.Text = submissions[vocabularyIndex[5]].meaning;

            text1.Text = submissions[CorectVocabulary].signs;
            if (text2_switch) text2.Text = submissions[CorectVocabulary].reading;
            else text2.Text = "";

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

            text2_switch = false;
        }
    }
}