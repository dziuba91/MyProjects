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
    class KanjiInfo
    {
        private Activity MainActivity;

        private KanjiDataType[] kanji;

        private MainKanjiGame mainKanjiGame = null;

        int actualIndex = 0;

        Button nextB;
        Button previousB;

        public KanjiInfo(Activity mainActivity, KanjiDataType[] kanji, MainKanjiGame mainKanjiGame)
        {
            this.MainActivity = mainActivity;

            this.kanji = kanji;

            this.mainKanjiGame = mainKanjiGame;
        }

        public KanjiInfo(Activity mainActivity, KanjiDataType[] kanji)
        {
            this.MainActivity = mainActivity;

            this.kanji = kanji;
        }
        

        private void setKanjiInfoLayoutContent()
        {
            Button backButton = MainActivity.FindViewById<Button>(Resource.Id.back);
            if (mainKanjiGame != null)
            {
                backButton.Click += delegate
                {
                    MainActivity.SetContentView(Resource.Layout.MainKanjiGameLayout);

                    if (mainKanjiGame != null)
                        mainKanjiGame.openLayoutActivity(false);
                    else
                        mainKanjiGame.openLayoutActivity(true);
                };
            }
            else
            {
                backButton.Visibility = ViewStates.Invisible;
            }

            nextB = MainActivity.FindViewById<Button>(Resource.Id.buttonNext);
            previousB = MainActivity.FindViewById<Button>(Resource.Id.buttonPrevious);

            nextB.Click += delegate
            {
                if (actualIndex + 1 < kanji.Length)
                {
                    actualIndex++;

                    if (actualIndex + 1 >= kanji.Length) nextB.Visibility = ViewStates.Invisible;

                    if (previousB.Visibility == ViewStates.Invisible) previousB.Visibility = ViewStates.Visible;
                }

                setKanjiInfoLayoutData(actualIndex);
            };
            previousB.Click += delegate
            {
                if (actualIndex > 0)
                {
                    actualIndex--;

                    if (actualIndex <= 0) previousB.Visibility = ViewStates.Invisible;

                    if (nextB.Visibility == ViewStates.Invisible) nextB.Visibility = ViewStates.Visible;
                }

                setKanjiInfoLayoutData(actualIndex);
            };
        }

        private void setKanjiInfoLayoutData(int index)
        {
            TextView kanjiT = MainActivity.FindViewById<TextView>(Resource.Id.textKanji);
            TextView kanjiM = MainActivity.FindViewById<TextView>(Resource.Id.textKanjiMeaning);
            TextView kanjiS = MainActivity.FindViewById<TextView>(Resource.Id.textSubmission);

            kanjiT.Text = kanji[index].sign;
            kanjiM.Text = kanji[index].meaning + "\n" + kanji[index].reading;

            kanjiS.Text = "";
            for (int i = 0; i < kanji[index].submissions.Length; i++)
                kanjiS.Text += kanji[index].submissions[i].signs + " " +
                    kanji[index].submissions[i].reading + " " +
                    kanji[index].submissions[i].meaning + "\n";


            TextView indexText = MainActivity.FindViewById<TextView>(Resource.Id.textIndex);
            indexText.Text = index + 1 + "/" + kanji.Length;
        }

        public void openLayoutActivity(int index)
        {
            setKanjiInfoLayoutData(index);
            setKanjiInfoLayoutContent();

            actualIndex = index;

            if (actualIndex + 1 >= kanji.Length) nextB.Visibility = ViewStates.Invisible;
            if (actualIndex <= 0) previousB.Visibility = ViewStates.Invisible;
        }
    }
}