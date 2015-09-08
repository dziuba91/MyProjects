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
    class VocabularyMenu
    {
        private Activity MainActivity;

        public MainVocabularyGame mainVocabularyGame1 = null;

        public MainVocabularyGame mainVocabularyGame2 = null;

        public VocabularyInfoExtended vocabularyExtendedInfo = null;

        public VocabularyInfo vocabularyInfo = null;

        public int menuLevel = 0;

        SubmissionOfKanji[] vocabulary;

        ObjectPermission[] vocabularySelectedExtendedObjectList;

        bool[] vocabularySelectedExtendedList;

        public VocabularyMenu(Activity mainActivity, SubmissionOfKanji[] vocabulary)
        {
            this.MainActivity = mainActivity;

            this.vocabulary = vocabulary;
        }

        public VocabularyMenu(Activity mainActivity, SubmissionOfKanji[] vocabulary, ObjectPermission[] vocabularySelectedExtendedObjectList, bool[] vocabularySelectedExtendedList)
        {
            this.MainActivity = mainActivity;

            this.vocabulary = vocabulary;

            this.vocabularySelectedExtendedObjectList = vocabularySelectedExtendedObjectList;

            this.vocabularySelectedExtendedList = vocabularySelectedExtendedList;
        }

        public void openLayoutActivity()
        {
            setMainMenuLayoutContent();
        }

        private void setMainMenuLayoutContent()
        {
            Button jap_plButton = MainActivity.FindViewById<Button>(Resource.Id.jap_pl_graButton);
            jap_plButton.Click += delegate
            {
                this.menuLevel++;

                MainActivity.SetContentView(Resource.Layout.MainVocabularyGameLayout);

                if (mainVocabularyGame1 == null) mainVocabularyGame1 = new MainVocabularyGame(MainActivity, vocabulary, vocabularySelectedExtendedObjectList, vocabularySelectedExtendedList);

                mainVocabularyGame1.openLayoutActivity(true);
            };

            Button pl_japButton = MainActivity.FindViewById<Button>(Resource.Id.pl_jap_graButton);
            pl_japButton.Click += delegate
            {
                this.menuLevel++;

                MainActivity.SetContentView(Resource.Layout.MainVocabularyGameLayout);

                if (mainVocabularyGame2 == null) mainVocabularyGame2 = new MainVocabularyGame(MainActivity, vocabulary, true, vocabularySelectedExtendedObjectList, vocabularySelectedExtendedList);

                mainVocabularyGame2.openLayoutActivity(true);
            };

            Button vocabularyListButton = MainActivity.FindViewById<Button>(Resource.Id.lista_slowekButton);
            vocabularyListButton.Click += delegate
            {
                this.menuLevel++;

                MainActivity.SetContentView(Resource.Layout.VocabularyInfoLayout);

                if (vocabularyExtendedInfo == null) vocabularyExtendedInfo = new VocabularyInfoExtended(MainActivity, vocabulary, vocabularySelectedExtendedObjectList, vocabularySelectedExtendedList);

                vocabularyExtendedInfo.openLayoutActivity(0);
            };

            Button vocabularyListExtendedButton = MainActivity.FindViewById<Button>(Resource.Id.wybrane_slowkaButton);
            vocabularyListExtendedButton.Click += delegate
            {
                this.menuLevel++;

                MainActivity.SetContentView(Resource.Layout.VocabularySelectedInfoLayout);

                if (vocabularyInfo == null) vocabularyInfo = new VocabularyInfo(MainActivity, vocabulary, vocabularySelectedExtendedObjectList, vocabularySelectedExtendedList);

                vocabularyInfo.openLayoutActivity(0);
            };
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