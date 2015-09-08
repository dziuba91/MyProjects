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

using KANDOU_v1.DataTypes;

namespace KANDOU_v1.ComponentsActivity
{
    class MainMenu
    {
        private Activity MainActivity;

        public KanjiMenu kanjiMenu = null;

        public VocabularyMenu vocabularyMenu = null;

        public SettingsVocabularyList settingsMenu = null;

        public int menuLevel = 0;

        KanjiDataType[] kanji;

        SubmissionOfKanji[] vocabulary;

        bool[] vocabularyStatus;

        SubmissionOfKanji[] actualVocabulary;

        ObjectPermission[] vocabularySelectedExtendedObjectList;

        bool[] vocabularySelectedExtendedList;

        public MainMenu(Activity mainActivity, KanjiDataType[] kanji, SubmissionOfKanji[] vocabulary, SubmissionOfKanji[] actualVocabulary, bool[] vocabularyStatus, ObjectPermission[] vocabularySelectedExtendedObjectList, bool[] vocabularySelectedExtendedList)
        {
            this.MainActivity = mainActivity;

            this.kanji = kanji;

            this.vocabulary = vocabulary;

            this.vocabularyStatus = vocabularyStatus;

            this.actualVocabulary = actualVocabulary;

            this.vocabularySelectedExtendedObjectList = vocabularySelectedExtendedObjectList;

            this.vocabularySelectedExtendedList = vocabularySelectedExtendedList;
        }

        public void openLayoutActivity()
        {
            setMainMenuLayoutContent();
        }

        private void setMainMenuLayoutContent()
        {
            ImageButton kanjiButton = MainActivity.FindViewById<ImageButton>(Resource.Id.kanjiGameButton);
            kanjiButton.Click += delegate
            {
                this.menuLevel++;

                MainActivity.SetContentView(Resource.Layout.KanjiMenuLayout);

                if (kanjiMenu == null) kanjiMenu = new KanjiMenu(MainActivity, kanji);

                kanjiMenu.openLayoutActivity();
            };

            ImageButton vocabularyButton = MainActivity.FindViewById<ImageButton>(Resource.Id.wordsGameButton);
            vocabularyButton.Click += delegate
            {
                this.menuLevel++;

                MainActivity.SetContentView(Resource.Layout.VocabularyMenuLayout);

                if (vocabularyMenu == null) vocabularyMenu = new VocabularyMenu(MainActivity, actualVocabulary, vocabularySelectedExtendedObjectList, vocabularySelectedExtendedList);

                vocabularyMenu.openLayoutActivity();
            };

            ImageButton settingsButton = MainActivity.FindViewById<ImageButton>(Resource.Id.settingsButton);
            settingsButton.Click += delegate
            {
                this.menuLevel++;

                MainActivity.SetContentView(Resource.Layout.SettingsVocabularyListLayout);

                if (settingsMenu == null) settingsMenu = new SettingsVocabularyList(MainActivity, vocabulary, vocabularyStatus);

                settingsMenu.openLayoutActivity();
            };
        }

        public void actualizeVocabularyList(SubmissionOfKanji[] vocabularyList)
        {
            this.actualVocabulary = vocabularyList;
        }

        public void actualizeVocabularyExtendedList(bool[] vocabularyList, ObjectPermission[] vocabularyObjectList)
        {
            this.vocabularySelectedExtendedList = vocabularyList;
            this.vocabularySelectedExtendedObjectList = vocabularyObjectList;
        }
    }
}