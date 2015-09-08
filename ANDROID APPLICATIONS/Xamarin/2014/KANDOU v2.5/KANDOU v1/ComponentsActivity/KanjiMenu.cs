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

namespace KANDOU_v1.ComponentsActivity
{
    class KanjiMenu
    {
        private Activity MainActivity;

        public MainKanjiGame mainKanjiGame = null;

        public SubmissionOfKanjiGame submissionOfKanjiGame = null;

        public KanjiInfo kanjiInfo = null;

        public SubmissionOfKanjiInfo submissionOfKanjiInfo = null;

        public int menuLevel = 0;

        KanjiDataType[] kanji;

        private SubmissionOfKanji[] submissions;

        public KanjiMenu(Activity mainActivity, KanjiDataType[] kanji)
        {
            this.MainActivity = mainActivity;

            this.kanji = kanji;


            ArrayList submissionList = new ArrayList();

            for (int i = 0; i < kanji.Length; i++)
                for (int j = 0; j < kanji[i].submissions.Length; j++)
                    if (kanji[i].submissions[j].priority >= 90)
                        submissionList.Add(kanji[i].submissions[j]);

            submissions = new SubmissionOfKanji[submissionList.Count];
            for (int i = 0; i < submissionList.Count; i++)
                submissions[i] = (SubmissionOfKanji)submissionList[i];
        }

        public void openLayoutActivity()
        {
            setMainMenuLayoutContent();
        }

        private void setMainMenuLayoutContent()
        {
            Button kanjiButton = MainActivity.FindViewById<Button>(Resource.Id.kanji_graButton);
            kanjiButton.Click += delegate
            {
                this.menuLevel++;

                MainActivity.SetContentView(Resource.Layout.MainKanjiGameLayout);

                if (mainKanjiGame == null) mainKanjiGame = new MainKanjiGame(MainActivity, kanji);

                mainKanjiGame.openLayoutActivity(true);
            };

            Button submissionOfKanjiButton = MainActivity.FindViewById<Button>(Resource.Id.zlozenia_kanji_graButton);
            submissionOfKanjiButton.Click += delegate
            {
                this.menuLevel++;

                MainActivity.SetContentView(Resource.Layout.MainVocabularyGameLayout);

                if (submissionOfKanjiGame == null) submissionOfKanjiGame = new SubmissionOfKanjiGame(MainActivity, submissions);

                submissionOfKanjiGame.openLayoutActivity(true);
            };

            Button kanjiListButton = MainActivity.FindViewById<Button>(Resource.Id.lista_kanjiButton);
            kanjiListButton.Click += delegate
            {
                this.menuLevel++;

                MainActivity.SetContentView(Resource.Layout.KanjiInfoLayout);

                if (kanjiInfo == null) kanjiInfo = new KanjiInfo(MainActivity, kanji);

                kanjiInfo.openLayoutActivity(0);
            };

            Button submissionOfKanjiListButton = MainActivity.FindViewById<Button>(Resource.Id.lista_zlozen_kanjiButton);
            submissionOfKanjiListButton.Click += delegate
            {
                this.menuLevel++;

                MainActivity.SetContentView(Resource.Layout.VocabularyInfoLayout);

                if (submissionOfKanjiInfo == null) submissionOfKanjiInfo = new SubmissionOfKanjiInfo(MainActivity, submissions);

                submissionOfKanjiInfo.openLayoutActivity(0);
            };
        }
    }
}