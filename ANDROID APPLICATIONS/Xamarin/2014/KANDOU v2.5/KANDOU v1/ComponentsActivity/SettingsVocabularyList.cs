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

using KANDOU_v1.Serialization;
using KANDOU_v1.FileManager;

namespace KANDOU_v1.ComponentsActivity
{
    class SettingsVocabularyList
    {
        private Activity MainActivity;

        private SubmissionOfKanji[] vocabulary;

        private SubmissionOfKanji[] actualVocabularyList;

        private bool[] vocabularyStatus;

        public bool changes = false;

        public bool changes1 = false;

        Button b2 = null;

        ObjectSerialization os = new ObjectSerialization();

        ObjectFilesManager ofm = new ObjectFilesManager();


        public SettingsVocabularyList(Activity mainActivity, SubmissionOfKanji[] vocabulary, bool[] vocabularyStatus)
        {
            this.MainActivity = mainActivity;

            this.vocabulary = vocabulary;

            this.vocabularyStatus = vocabularyStatus;
        }


        private void setVocabularyListSettingsLayoutContent()
        {
            ScrollView scroll = MainActivity.FindViewById<ScrollView>(Resource.Id.scrollView1);

            LinearLayout list = MainActivity.FindViewById<LinearLayout>(Resource.Id.linearLayout1);

            //Button b1 = new Button(MainActivity);
            //CheckBox c1 = new CheckBox(MainActivity);

            b2 = MainActivity.FindViewById<Button>(Resource.Id.save_button);
            b2.Click += delegate
            {
                save();

                changes = true;
            };

            checkSaveButtonStatus(changes1);

            CheckBox[] c1 = new CheckBox[vocabulary.Length];
            for (int i = 0; i < vocabulary.Length; i++)
            {
                c1[i] = new CheckBox(MainActivity);
                c1[i].Text = vocabulary[i].reading + " " + vocabulary[i].signs + " (" + vocabulary[i].meaning + ")";
                c1[i].Checked = vocabularyStatus[i];
                c1[i].Id = i;
                int id = c1[i].Id;

                c1[i].CheckedChange += delegate
                {
                    if (vocabularyStatus[id])
                        vocabularyStatus[id] = false;
                    else
                        vocabularyStatus[id] = true;

                    changes1 = true;

                    checkSaveButtonStatus(changes1);
                };

                list.AddView(c1[i]);
            }
                //scroll.AddView(b1);
            //list.AddView(c1);
            Button b1 = MainActivity.FindViewById<Button>(Resource.Id.deselect_button);
            b1.Click += delegate
            {
                for(int i = 0;i<vocabulary.Length; i++)
                    c1[i].Checked = false;
            };
        }

        private void checkSaveButtonStatus(bool status)
        {
            if (status)
            {
                b2.Visibility = ViewStates.Visible;
            }
            else
                b2.Visibility = ViewStates.Invisible;
        }

        private void setVocabularyListSettingsLayoutData()
        {
            /*
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
             * */
        }

        public void openLayoutActivity()
        {
            setVocabularyListSettingsLayoutContent();
            setVocabularyListSettingsLayoutData();

            /*
            actualIndex = index;

            if (actualIndex + 1 >= vocabulary.Length) nextB.Visibility = ViewStates.Invisible;
            if (actualIndex <= 0) previousB.Visibility = ViewStates.Invisible;
             * */
        }

        public void closeLayoutActivity(ref bool[] VS, ref SubmissionOfKanji[] V)
        {
            if (changes)
            {
                VS = vocabularyStatus;
                V = actualVocabularyList;
            }
        }

        private void actualizeVocabularyList(bool[] vocabularyStatus)
        {
            //os.serializeObjectArray(vocabularyStatus);

            ArrayList list = new ArrayList();

            for (int i = 0; i < vocabularyStatus.Length; i++)
                if (vocabularyStatus[i])
                    list.Add(vocabulary[i]);

            //actualVocabularyList = null;
            //actualVocabularyList.
            SubmissionOfKanji[] actualVocabularyList1 = new SubmissionOfKanji[list.Count];

            for (int i = 0; i < list.Count; i++)
                actualVocabularyList1[i] = (SubmissionOfKanji)list[i];

            actualVocabularyList = actualVocabularyList1;

            //SubmissionOfKanji[] sub = null;
            //os.deserializeObjectArray(ref sub);

            //os.serializeObjectArray(actualVocabularyList);


        }

        public void save()
        {
            //vocabularyStatus = Main.settingsMenu.closeLayoutActivity();

            actualizeVocabularyList(vocabularyStatus);

            //bool[] t1 = null;
            //SubmissionOfKanji[] t2 = null;

            //os.deserializeObjectArray(ref t1);
            //os.deserializeObjectArray(ref t2);

            ofm.saveObjectArray(vocabularyStatus, ofm.filePath3);
            ofm.saveObjectArray(actualVocabularyList);
            
            //os.serializeObjectArray(vocabularyStatus);
            //os.serializeObjectArray(actualVocabularyList);

            changes1 = false;

            checkSaveButtonStatus(changes1);

            //changes = false;
        }
    }
}