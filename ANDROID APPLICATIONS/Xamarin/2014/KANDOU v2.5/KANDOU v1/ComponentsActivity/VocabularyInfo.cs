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
using KANDOU_v1.FileManager;
using System.Collections;

namespace KANDOU_v1.ComponentsActivity
{
    class VocabularyInfo
    {
        private Activity MainActivity;

        private SubmissionOfKanji[] vocabulary;

        private SubmissionOfKanji[] vocabularyData;

        private MainVocabularyGame mainVocabularyGame = null;

        public VocabularyTestGame vocabularyTestGame = null;

        int actualIndex = 0;

        Button nextB;
        Button previousB;

        Button testB;

        Button backButton;

        TextView vocabularyR;
        TextView vocabularyK;
        TextView vocabularyM;

        TextView indexText;

        bool vocabularyR_switch = true;

        ObjectPermission[] vocabularySelectedExtendedObjectList;

        bool[] vocabularySelectedExtendedList;

        public bool changes = false;

        bool emptyList = false;

        ObjectFilesManager ofm = new ObjectFilesManager();

        int[] indexToTest = null;

        public VocabularyInfo(Activity mainActivity, SubmissionOfKanji[] vocabulary, MainVocabularyGame mainVocabularyGame, ObjectPermission[] vocabularySelectedExtendedObjectList, bool[] vocabularySelectedExtendedList)
        {
            this.MainActivity = mainActivity;

            this.vocabulary = vocabulary;

            this.mainVocabularyGame = mainVocabularyGame;

            this.vocabularySelectedExtendedObjectList = vocabularySelectedExtendedObjectList;

            this.vocabularySelectedExtendedList = vocabularySelectedExtendedList;
        }

        public VocabularyInfo(Activity mainActivity, SubmissionOfKanji[] vocabulary, ObjectPermission[] vocabularySelectedExtendedObjectList, bool[] vocabularySelectedExtendedList)
        {
            this.MainActivity = mainActivity;

            this.vocabulary = vocabulary;

            this.vocabularySelectedExtendedObjectList = vocabularySelectedExtendedObjectList;

            this.vocabularySelectedExtendedList = vocabularySelectedExtendedList;
        }


        private void setVocabularyInfoLayoutContent()
        {
            backButton = MainActivity.FindViewById<Button>(Resource.Id.back_2);
            //if (mainVocabularyGame != null)
            //{
            backButton.Click += delegate
            {
                clearList();
                displayEmptyLayout();
            };
            //}
            //else
            //{
            //    backButton.Visibility = ViewStates.Invisible;
            //}

            nextB = MainActivity.FindViewById<Button>(Resource.Id.buttonNext_2);
            previousB = MainActivity.FindViewById<Button>(Resource.Id.buttonPrevious_2);

            nextB.Click += delegate
            {
                if (actualIndex + 1 < vocabulary.Length)
                {
                    actualIndex++;

                    if (actualIndex + 1 >= vocabularyData.Length) nextB.Visibility = ViewStates.Invisible;

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

            testB = MainActivity.FindViewById<Button>(Resource.Id.test_button);
            testB.Click += delegate
            {
                MainActivity.SetContentView(Resource.Layout.MainVocabularyGameLayout);

                if (vocabularyTestGame == null) vocabularyTestGame = new VocabularyTestGame(MainActivity, vocabulary, vocabularySelectedExtendedObjectList, vocabularySelectedExtendedList, indexToTest);

                vocabularyTestGame.openLayoutActivity(true);
            };

            indexText = MainActivity.FindViewById<TextView>(Resource.Id.textIndex_2);

            vocabularyK = MainActivity.FindViewById<TextView>(Resource.Id.textKanji_2);
            vocabularyM = MainActivity.FindViewById<TextView>(Resource.Id.textMeaning_2);
        }

        private void setVocabularyInfoLayoutData(int index)
        {
            vocabularyR.Text = vocabularyData[index].reading;

            if (vocabularyR_switch)
            {
                vocabularyK.Text = vocabularyData[index].signs;
                vocabularyM.Text = vocabularyData[index].meaning;
            }
            else
            {
                vocabularyK.Text = "";
                vocabularyM.Text = "";
            }

            indexText.Text = index + 1 + "/" + vocabularyData.Length;
        }

        public void openLayoutActivity(int index)
        {
            emptyList = false;
            setVocabularyInfoLayoutContent();
            setData();

            if (!emptyList)
            {
                setVocabularyInfoLayoutData(index);

                actualIndex = index;

                if (actualIndex + 1 >= vocabularyData.Length) nextB.Visibility = ViewStates.Invisible;
                if (actualIndex <= 0) previousB.Visibility = ViewStates.Invisible;
            }
            else
            {
                displayEmptyLayout();
            }
        }

        public void closeLayoutActivity(ref bool[] VS, ref ObjectPermission[] V)
        {
            if (changes)
            {
                VS = vocabularySelectedExtendedList;
                V = vocabularySelectedExtendedObjectList;
            }
        }

        public void setData()
        {
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();

            for (int i = 0; i < vocabularySelectedExtendedObjectList.Length; i++)
            {
                if (vocabularySelectedExtendedObjectList[i].permission)
                {
                    list.Add(vocabulary[i]);
                    list2.Add(vocabularySelectedExtendedObjectList[i].id);
                }
            }

            if (list.Count == 0)
            {
                emptyList = true;

                return;
            }

            ArrayList list_quene = new ArrayList();

            Random r = new Random();
            while(list.Count > 1)
            {
                int i = r.Next(0, list.Count);
                list_quene.Add(list[i]);
                list.RemoveAt(i);
            }
            list_quene.Add(list[0]);

            vocabularyData = new SubmissionOfKanji[list_quene.Count];
            indexToTest = new int[list2.Count];

            for (int i = 0; i < list2.Count; i++)
            {
                vocabularyData[i] = (SubmissionOfKanji)list_quene[i];
                indexToTest[i] = (int)list2[i];
            }

            if (vocabularyTestGame != null)
                vocabularyTestGame.actualizeTestInedexes(indexToTest);
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

        public void displayEmptyLayout()
        {
            vocabularyR.Text = "Pusta Lista";

            vocabularyK.Text = "";
            vocabularyM.Text = "";

            indexText.Text = "";

            nextB.Visibility = ViewStates.Invisible;
            previousB.Visibility = ViewStates.Invisible;

            backButton.Visibility = ViewStates.Invisible;
        }

        public void clearList()
        {
            for (int i = 0; i < vocabularySelectedExtendedObjectList.Length; i++)
            {
                vocabularySelectedExtendedObjectList[i].permission = false;
                vocabularySelectedExtendedList[vocabularySelectedExtendedObjectList[i].id] = vocabularySelectedExtendedObjectList[i].permission;
            }

            actualizeVocabularyExtendedList(vocabularySelectedExtendedList, vocabularySelectedExtendedObjectList);

            ofm.saveObjectArray(vocabularySelectedExtendedList, ofm.filePath4);

            changes = true;
        }
    }
}