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

namespace KANDOU_v1.ComponentsActivity
{
    class VocabularyInfoExtended
    {
        private Activity MainActivity;

        private SubmissionOfKanji[] vocabulary;

        private MainVocabularyGame mainVocabularyGame = null;

        private VocabularyTestGame mainVocabularyGame2 = null;

        int actualIndex = 0;

        Button nextB;
        Button previousB;
        Button saveB;

        TextView vocabularyR;
        TextView vocabularyK;
        TextView vocabularyM;

        CheckBox checkBox;

        bool vocabularyR_switch = true;

        ObjectPermission[] vocabularySelectedExtendedObjectList;

        bool[] vocabularySelectedExtendedList;

        ObjectPermission[] vocabularySelectedExtendedObjectList2;

        bool[] vocabularySelectedExtendedList2;

        bool changes1 = false;
        public bool changes = false;

        ObjectFilesManager ofm = new ObjectFilesManager();

        public VocabularyInfoExtended(Activity mainActivity, SubmissionOfKanji[] vocabulary, MainVocabularyGame mainVocabularyGame, ObjectPermission[] vocabularySelectedExtendedObjectList, bool[] vocabularySelectedExtendedList)
        {
            this.MainActivity = mainActivity;

            this.vocabulary = vocabulary;

            this.mainVocabularyGame = mainVocabularyGame;

            this.vocabularySelectedExtendedObjectList = vocabularySelectedExtendedObjectList;

            this.vocabularySelectedExtendedList = vocabularySelectedExtendedList;
        }

        public VocabularyInfoExtended(Activity mainActivity, SubmissionOfKanji[] vocabulary, VocabularyTestGame mainVocabularyGame, ObjectPermission[] vocabularySelectedExtendedObjectList, bool[] vocabularySelectedExtendedList)
        {
            this.MainActivity = mainActivity;

            this.vocabulary = vocabulary;

            this.mainVocabularyGame2 = mainVocabularyGame;

            this.vocabularySelectedExtendedObjectList = vocabularySelectedExtendedObjectList;

            this.vocabularySelectedExtendedList = vocabularySelectedExtendedList;
        }

        public VocabularyInfoExtended(Activity mainActivity, SubmissionOfKanji[] vocabulary, ObjectPermission[] vocabularySelectedExtendedObjectList, bool[] vocabularySelectedExtendedList)
        {
            this.MainActivity = mainActivity;

            this.vocabulary = vocabulary;

            this.vocabularySelectedExtendedObjectList = vocabularySelectedExtendedObjectList;

            this.vocabularySelectedExtendedList = vocabularySelectedExtendedList;
        }


        private void setVocabularyInfoLayoutContent()
        {
            saveB = MainActivity.FindViewById<Button>(Resource.Id.save_button2);
            saveB.Click += delegate
            {
                save();

                changes = true;
            };

            checkBox = MainActivity.FindViewById<CheckBox>(Resource.Id.checkBoxExtended);

            checkBox.CheckedChange += delegate
            {
                changeVocabularyExtendedData(actualIndex);
            };

            checkBox.Click += delegate
            {
                changes1 = true;
                checkSaveButtonStatus(changes1);
            };

            Button backButton = MainActivity.FindViewById<Button>(Resource.Id.back_2);
            if (mainVocabularyGame != null)
            {
                backButton.Click += delegate
                {
                    MainActivity.SetContentView(Resource.Layout.MainVocabularyGameLayout);

                    if (mainVocabularyGame != null)
                        mainVocabularyGame.openLayoutActivity(false);
                    else
                        mainVocabularyGame.openLayoutActivity(true);
                };
            }
            else if (mainVocabularyGame2 != null)
            {
                backButton.Click += delegate
                {
                    MainActivity.SetContentView(Resource.Layout.MainVocabularyGameLayout);

                    if (mainVocabularyGame2 != null)
                        mainVocabularyGame2.openLayoutActivity(false);
                    else
                        mainVocabularyGame2.openLayoutActivity(true);
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

                checkBox.Checked = vocabularySelectedExtendedObjectList[actualIndex].permission;
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

                checkBox.Checked = vocabularySelectedExtendedObjectList[actualIndex].permission;
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
            vocabularyR.Text = vocabulary[index].reading;

            if (vocabularyR_switch)
            {
                vocabularyK.Text = vocabulary[index].signs;
                vocabularyM.Text = vocabulary[index].meaning;
            }
            else
            {
                vocabularyK.Text = "";
                vocabularyM.Text = "";
            }

            TextView indexText = MainActivity.FindViewById<TextView>(Resource.Id.textIndex_2);
            indexText.Text = index + 1 + "/" + vocabulary.Length;

            //changes1 = false;
            //checkSaveButtonStatus(changes1);
        }

        public void openLayoutActivity(int index)
        {
            setVocabularyInfoLayoutContent();
            setVocabularyInfoLayoutData(index);

            actualIndex = index;

            if (actualIndex + 1 >= vocabulary.Length) nextB.Visibility = ViewStates.Invisible;
            if (actualIndex <= 0) previousB.Visibility = ViewStates.Invisible;

            changes1 = false;
            checkSaveButtonStatus(changes1);

            vocabularySelectedExtendedObjectList2 = vocabularySelectedExtendedObjectList;
            vocabularySelectedExtendedList2 = vocabularySelectedExtendedList;

            checkBox.Checked = vocabularySelectedExtendedObjectList[actualIndex].permission;
            //checkSaveButtonStatus(changes1);
        }
        
        public void closeLayoutActivity(ref bool[] VS, ref ObjectPermission[] V)
        {
            if (changes)
            {
                VS = vocabularySelectedExtendedList;
                V = vocabularySelectedExtendedObjectList;
            }
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

        private void checkSaveButtonStatus(bool status)
        {
            if (status)
            {
                saveB.Visibility = ViewStates.Visible;
            }
            else
                saveB.Visibility = ViewStates.Invisible;
        }

        public void changeVocabularyExtendedData(int index)
        {
            //if (vocabularySelectedExtendedObjectList2[index].permission) vocabularySelectedExtendedObjectList2[index].permission = false;
            //else vocabularySelectedExtendedObjectList2[index].permission = true;

            vocabularySelectedExtendedObjectList2[index].permission = checkBox.Checked;
            vocabularySelectedExtendedList2[vocabularySelectedExtendedObjectList2[index].id] = vocabularySelectedExtendedObjectList2[index].permission;
        }

        public void save()
        {
            actualizeVocabularyExtendedList(vocabularySelectedExtendedList2, vocabularySelectedExtendedObjectList2);

            ofm.saveObjectArray(vocabularySelectedExtendedList, ofm.filePath4);

            changes1 = false;

            checkSaveButtonStatus(changes1);
        }
    }
}