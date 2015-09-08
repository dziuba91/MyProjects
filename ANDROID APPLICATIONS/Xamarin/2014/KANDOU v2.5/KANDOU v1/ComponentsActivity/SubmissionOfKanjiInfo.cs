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

namespace KANDOU_v1.ComponentsActivity
{
    class SubmissionOfKanjiInfo
    {
        private Activity MainActivity;

        private SubmissionOfKanji[] vocabulary;

        private SubmissionOfKanjiGame submissionOfKanjiGame = null;

        int actualIndex = 0;

        Button nextB;
        Button previousB;

        TextView vocabularyR;
        TextView vocabularyK;
        TextView vocabularyM;

        bool vocabularyR_switch = true;

        public SubmissionOfKanjiInfo(Activity mainActivity, SubmissionOfKanji[] vocabulary, SubmissionOfKanjiGame submissionOfKanjiGame)
        {
            this.MainActivity = mainActivity;

            this.vocabulary = vocabulary;

            this.submissionOfKanjiGame = submissionOfKanjiGame;
        }

        public SubmissionOfKanjiInfo(Activity mainActivity, SubmissionOfKanji[] vocabulary)
        {
            this.MainActivity = mainActivity;

            this.vocabulary = vocabulary;
        }


        private void setVocabularyInfoLayoutContent()
        {
            Button backButton = MainActivity.FindViewById<Button>(Resource.Id.back_2);
            if (submissionOfKanjiGame != null)
            {
                backButton.Click += delegate
                {
                    MainActivity.SetContentView(Resource.Layout.MainVocabularyGameLayout);

                    if (submissionOfKanjiGame != null)
                        submissionOfKanjiGame.openLayoutActivity(false);
                    else
                        submissionOfKanjiGame.openLayoutActivity(true);
                };
            }
            else
            {
                backButton.Visibility = ViewStates.Invisible;
            }

            nextB = MainActivity.FindViewById<Button>(Resource.Id.buttonNext_2);
            previousB = MainActivity.FindViewById<Button>(Resource.Id.buttonPrevious_2);

            nextB.Click += delegate
            {
                if (actualIndex + 1 < vocabulary.Length)
                {
                    actualIndex++;

                    if (actualIndex + 1 >= vocabulary.Length) nextB.Visibility = ViewStates.Invisible;

                    if (previousB.Visibility == ViewStates.Invisible) previousB.Visibility = ViewStates.Visible;
                }

                setVocabularyInfoLayoutData(actualIndex);
            };
            previousB.Click += delegate
            {
                if (actualIndex > 0)
                {
                    actualIndex--;

                    if (actualIndex <= 0) previousB.Visibility = ViewStates.Invisible;

                    if (nextB.Visibility == ViewStates.Invisible) nextB.Visibility = ViewStates.Visible;
                }

                setVocabularyInfoLayoutData(actualIndex);
            };

            vocabularyR = MainActivity.FindViewById<TextView>(Resource.Id.textReading_2);
            vocabularyR.Click += delegate
            {
                if (vocabularyR_switch)
                    vocabularyR_switch = false;
                else
                    vocabularyR_switch = true;

                setVocabularyInfoLayoutData(actualIndex);
            };

            vocabularyK = MainActivity.FindViewById<TextView>(Resource.Id.textKanji_2);
            vocabularyM = MainActivity.FindViewById<TextView>(Resource.Id.textMeaning_2);
        }

        private void setVocabularyInfoLayoutData(int index)
        {
            vocabularyR.Text = vocabulary[index].signs;

            if (vocabularyR_switch)
            {
                vocabularyK.Text = vocabulary[index].reading;
                vocabularyM.Text = vocabulary[index].meaning;
            }
            else
            {
                vocabularyK.Text = "";
                vocabularyM.Text = "";
            }

            TextView indexText = MainActivity.FindViewById<TextView>(Resource.Id.textIndex_2);
            indexText.Text = index + 1 + "/" + vocabulary.Length;
        }

        public void openLayoutActivity(int index)
        {
            setVocabularyInfoLayoutContent();
            setVocabularyInfoLayoutData(index);

            actualIndex = index;

            if (actualIndex + 1 >= vocabulary.Length) nextB.Visibility = ViewStates.Invisible;
            if (actualIndex <= 0) previousB.Visibility = ViewStates.Invisible;
        }
    }
}