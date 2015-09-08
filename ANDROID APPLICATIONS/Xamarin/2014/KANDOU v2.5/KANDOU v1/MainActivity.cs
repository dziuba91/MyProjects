using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System.Xml.Linq;
using System.Collections;

using KANDOU_v1.ComponentsActivity;
//using KANDOU_v1.Serialization;
using KANDOU_v1.FileManager;
using KANDOU_v1.DataTypes;
using System.IO;

namespace KANDOU_v1
{
    [Activity(Label = "KANDOU_v2.5", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {   
        KanjiDataType[] kanji = null;

        SubmissionOfKanji[] vocabulary = null;

        SubmissionOfKanji[] actualVocabularyList = null;

        bool[] vocabularySelectedList = null;

        bool[] vocabularySelectedExtendedList = null;

        ObjectPermission[] vocabularySelectedExtendedObjectList = null;
        
        //int menuLevel = 0;

        MainMenu Main = null;

        //ObjectSerialization os = new ObjectSerialization();

        ObjectFilesManager ofm = new ObjectFilesManager();


        public override void OnBackPressed()
        {
            if (Main.menuLevel == 0) Finish();
            else if (Main.menuLevel == 1)
            {
                if (Main.kanjiMenu != null && Main.kanjiMenu.mainKanjiGame != null) Main.kanjiMenu.mainKanjiGame.closeLayoutActivity();
                if (Main.kanjiMenu != null && Main.kanjiMenu.submissionOfKanjiGame != null) Main.kanjiMenu.submissionOfKanjiGame.closeLayoutActivity();
                if (Main.vocabularyMenu != null && Main.vocabularyMenu.mainVocabularyGame1 != null) Main.vocabularyMenu.mainVocabularyGame1.closeLayoutActivity();
                if (Main.vocabularyMenu != null && Main.vocabularyMenu.mainVocabularyGame2 != null) Main.vocabularyMenu.mainVocabularyGame2.closeLayoutActivity();

                SetContentView(Resource.Layout.MainMenuLayout);

                Main.openLayoutActivity();

                Main.menuLevel--;

                if (Main.settingsMenu != null)
                {
                    if (Main.settingsMenu.changes)
                    {
                        Main.settingsMenu.closeLayoutActivity(ref vocabularySelectedList, ref actualVocabularyList);

                        actualizeVocabularyList(vocabularySelectedList);

                        //
                        getSelectedVocabularyArray();

                        actualizeVocabularyExtendedList(vocabularySelectedExtendedList, vocabularySelectedExtendedObjectList);

                        //
                        Main.settingsMenu.changes = false;
                    }
                }

                if (Main.vocabularyMenu != null && Main.vocabularyMenu.vocabularyExtendedInfo != null)
                {
                    if (Main.vocabularyMenu.vocabularyExtendedInfo.changes)
                    {
                        Main.vocabularyMenu.vocabularyExtendedInfo.closeLayoutActivity(ref vocabularySelectedExtendedList, ref vocabularySelectedExtendedObjectList);

                        actualizeVocabularyExtendedList(vocabularySelectedExtendedList, vocabularySelectedExtendedObjectList);

                        Main.vocabularyMenu.vocabularyExtendedInfo.changes = false;
                    }
                }

                if (Main.vocabularyMenu != null && Main.vocabularyMenu.mainVocabularyGame1 != null && Main.vocabularyMenu.mainVocabularyGame1.vocabularyInfoExtended != null)
                {
                    if (Main.vocabularyMenu.mainVocabularyGame1.vocabularyInfoExtended.changes)
                    {
                        Main.vocabularyMenu.mainVocabularyGame1.vocabularyInfoExtended.closeLayoutActivity(ref vocabularySelectedExtendedList, ref vocabularySelectedExtendedObjectList);

                        actualizeVocabularyExtendedList(vocabularySelectedExtendedList, vocabularySelectedExtendedObjectList);

                        Main.vocabularyMenu.mainVocabularyGame1.vocabularyInfoExtended.changes = false;
                    }
                }
                
                if (Main.vocabularyMenu != null && Main.vocabularyMenu.mainVocabularyGame2 != null && Main.vocabularyMenu.mainVocabularyGame2.vocabularyInfoExtended != null)
                {
                    if (Main.vocabularyMenu.mainVocabularyGame2.vocabularyInfoExtended.changes)
                    {
                        Main.vocabularyMenu.mainVocabularyGame2.vocabularyInfoExtended.closeLayoutActivity(ref vocabularySelectedExtendedList, ref vocabularySelectedExtendedObjectList);

                        actualizeVocabularyExtendedList(vocabularySelectedExtendedList, vocabularySelectedExtendedObjectList);

                        Main.vocabularyMenu.mainVocabularyGame2.vocabularyInfoExtended.changes = false;
                    }
                }

                if (Main.vocabularyMenu != null && Main.vocabularyMenu.vocabularyInfo != null)
                {
                    if (Main.vocabularyMenu.vocabularyInfo.changes)
                    {
                        Main.vocabularyMenu.vocabularyInfo.closeLayoutActivity(ref vocabularySelectedExtendedList, ref vocabularySelectedExtendedObjectList);

                        actualizeVocabularyExtendedList(vocabularySelectedExtendedList, vocabularySelectedExtendedObjectList);

                        Main.vocabularyMenu.vocabularyInfo.changes = false;
                    }
                }

                if (Main.vocabularyMenu != null && Main.vocabularyMenu.vocabularyInfo != null && Main.vocabularyMenu.vocabularyInfo.vocabularyTestGame != null && Main.vocabularyMenu.vocabularyInfo.vocabularyTestGame.vocabularyInfoExtended != null)
                {
                    if (Main.vocabularyMenu.vocabularyInfo.vocabularyTestGame.vocabularyInfoExtended.changes)
                    {
                        Main.vocabularyMenu.vocabularyInfo.vocabularyTestGame.vocabularyInfoExtended.closeLayoutActivity(ref vocabularySelectedExtendedList, ref vocabularySelectedExtendedObjectList);

                        actualizeVocabularyExtendedList(vocabularySelectedExtendedList, vocabularySelectedExtendedObjectList);

                        Main.vocabularyMenu.vocabularyInfo.vocabularyTestGame.vocabularyInfoExtended.changes = false;
                    }
                }
            }
            else
            {
                Finish();
            }

            //base.OnBackPressed();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource

            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.MyButton);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            //ContentProvider.


            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
            var xmlFilePath = System.IO.Path.Combine(sdCardPath, "XMLFile1.xml");

            XDocument doc;
            //if (System.IO.File.Exists(xmlFilePath))
            //{
            try
            {
                doc = XDocument.Load(Assets.Open("XMLFile1.xml"));

                ArrayList kanjiList = new ArrayList();

                foreach (XElement element in doc.Root.Elements("Kanji"))
                {
                    KanjiDataType kanjiData = new KanjiDataType();

                    kanjiData.sign = (string)element.Element("sign");
                    kanjiData.meaning = (string)element.Element("meaning");
                    kanjiData.reading = (string)element.Element("reading");

                    ArrayList submissionList = new ArrayList();

                    XElement element_sub = element.Element("submissions");
                    foreach (XElement element_sub1 in element_sub.Elements("submission"))
                    {
                        SubmissionOfKanji submission = new SubmissionOfKanji();

                        submission.signs = (string)element_sub1.Element("signs");
                        submission.meaning = (string)element_sub1.Element("meaning");
                        submission.reading = (string)element_sub1.Element("reading");
                        submission.priority = (int)element_sub1.Element("priority");

                        submissionList.Add(submission);
                    }

                    SubmissionOfKanji[] submissionListArray = new SubmissionOfKanji[submissionList.Count];
                    for (int i = 0; i < submissionList.Count; i++)
                    {
                        submissionListArray[i] = (SubmissionOfKanji)submissionList[i];
                    }

                    kanjiData.submissions = submissionListArray;

                    kanjiList.Add(kanjiData);
                }

                KanjiDataType[] kanjiListArray = new KanjiDataType[kanjiList.Count];
                for (int i = 0; i < kanjiList.Count; i++)
                {
                    kanjiListArray[i] = (KanjiDataType)kanjiList[i];
                }

                //text.Text = kanjiListArray[0].sign;

                kanji = kanjiListArray;

                //text.Text = kanji[0].sign;
                //text.Text = "XML FILE found!!!";
                //}
                //else
                //{
                //text.Text = "READING XML FILE ERROR!!!";
                //}
            }
            catch (FileNotFoundException)
            { }

            //Assets.Close();

            xmlFilePath = System.IO.Path.Combine(sdCardPath, "XMLFile2.xml");

            //if (System.IO.File.Exists())
            //{
            try
            {
                doc = XDocument.Load(Assets.Open("XMLFile2.xml"));

                ArrayList vocabularyList = new ArrayList();

                foreach (XElement element in doc.Root.Elements("Item"))
                {
                    SubmissionOfKanji vocbularyData = new SubmissionOfKanji();

                    vocbularyData.signs = (string)element.Element("signs");
                    vocbularyData.meaning = (string)element.Element("meaning");
                    vocbularyData.reading = (string)element.Element("reading");
                    vocbularyData.priority = (int)element.Element("priority");

                    vocabularyList.Add(vocbularyData);
                }

                SubmissionOfKanji[] vocabularyListArray = new SubmissionOfKanji[vocabularyList.Count];
                for (int i = 0; i < vocabularyList.Count; i++)
                {
                    vocabularyListArray[i] = (SubmissionOfKanji)vocabularyList[i];
                }

                vocabulary = vocabularyListArray;
                //}
                //else
                //{
                //text.Text = "READING XML FILE ERROR!!!";
                //}
            }
            catch (FileNotFoundException)
            { }

            //Assets.Close();

            //var directories = Directory.EnumerateDirectories("./");
            //foreach (var directory in directories)
            //{
            //    Console.WriteLine(directory);
            //}

            //ObjectSerialization os = new ObjectSerialization();
            //os.serializeObjectArray(kanji);
            //\os.serializeObjectArray(vocabulary);
            //var a = NSBundle.MainBundle.BundlePath;

            instalAplicationPath();

            getSelectedVocabularyArray();

            SetContentView(Resource.Layout.MainMenuLayout);

            Main = new MainMenu(this, kanji, vocabulary, actualVocabularyList, vocabularySelectedList, vocabularySelectedExtendedObjectList, vocabularySelectedExtendedList);

            Main.openLayoutActivity();

            //Console.WriteLine("A=" + actualVocabularyList.Length);
            //Console.WriteLine("B=" + vocabularySelectedList.Length);
        }

        void instalAplicationPath()
        {
            //GetExternalFilesDir("Kandou");
            

            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
            var directoryPath = System.IO.Path.Combine(sdCardPath, "KandouData2");

            if (!System.IO.Directory.Exists(directoryPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath);

                vocabularySelectedList = new bool[vocabulary.Length];
                for (int i = 0; i < vocabulary.Length; i++)
                {
                    vocabularySelectedList[i] = true;
                }

                ofm.saveObjectArray(vocabularySelectedList, ofm.filePath3);
                ofm.saveObjectArray(vocabularySelectedList, ofm.filePath4);
                ofm.saveObjectArray(kanji);
                ofm.saveObjectArray(vocabulary);


                //os.serializeObjectArray(vocabularySelectedList);
                //os.serializeObjectArray(kanji);
                //os.serializeObjectArray(vocabulary);

                actualVocabularyList = vocabulary;

                //Console.WriteLine("Z1=" + vocabularySelectedList.Length);
                //Console.WriteLine("Z2=" + actualVocabularyList.Length);
                //Console.WriteLine("Z3=" + vocabulary.Length);
            }
            else
            {
                actualVocabularyList = null;
                vocabularySelectedList = null;

                ofm.readObjectArrayFromFile(ref vocabularySelectedList, ofm.filePath3);
                ofm.readObjectArrayFromFile(ref vocabularySelectedExtendedList, ofm.filePath4);
                ofm.readObjectArrayFromFile(ref actualVocabularyList);

                //Console.WriteLine("Z1=" + vocabularySelectedList.Length);
                //Console.WriteLine("Z2=" + actualVocabularyList.Length);
                //Console.WriteLine("Z3=" + vocabulary.Length);
                //os.deserializeObjectArray(ref actualVocabularyList);
                //os.deserializeObjectArray(ref vocabularySelectedList);
            }


            //Android.OS.Environment.ExternalStorageDirectory.;
            //var xmlFilePath = System.IO.Path.Combine(sdCardPath, "XMLFile1.xml");
        }

        private void actualizeVocabularyList(bool[] vocabularyStatus)
        {
            if (Main != null)
                Main.actualizeVocabularyList(actualVocabularyList);

            if (Main != null && Main.vocabularyMenu != null)
                Main.vocabularyMenu.actualizeVocabularyList(actualVocabularyList);

            if (Main != null && Main.vocabularyMenu != null && Main.vocabularyMenu.vocabularyExtendedInfo != null)
                Main.vocabularyMenu.vocabularyExtendedInfo.actualizeVocabularyList(actualVocabularyList);

            if (Main != null && Main.vocabularyMenu != null && Main.vocabularyMenu.vocabularyInfo != null)
                Main.vocabularyMenu.vocabularyInfo.actualizeVocabularyList(actualVocabularyList);

            if (Main != null && Main.vocabularyMenu != null && Main.vocabularyMenu.mainVocabularyGame1 != null)
                Main.vocabularyMenu.mainVocabularyGame1.actualizeVocabularyList(actualVocabularyList);

            if (Main != null && Main.vocabularyMenu != null && Main.vocabularyMenu.mainVocabularyGame1 != null && Main.vocabularyMenu.mainVocabularyGame1.vocabularyInfoExtended != null)
                Main.vocabularyMenu.mainVocabularyGame1.vocabularyInfoExtended.actualizeVocabularyList(actualVocabularyList);

            if (Main != null && Main.vocabularyMenu != null && Main.vocabularyMenu.mainVocabularyGame2 != null)
                Main.vocabularyMenu.mainVocabularyGame2.actualizeVocabularyList(actualVocabularyList);

            if (Main != null && Main.vocabularyMenu != null && Main.vocabularyMenu.mainVocabularyGame2 != null && Main.vocabularyMenu.mainVocabularyGame2.vocabularyInfoExtended != null)
                Main.vocabularyMenu.mainVocabularyGame2.vocabularyInfoExtended.actualizeVocabularyList(actualVocabularyList);

            //
            if (Main != null && Main.vocabularyMenu != null && Main.vocabularyMenu.vocabularyInfo != null && Main.vocabularyMenu.vocabularyInfo.vocabularyTestGame != null)
                Main.vocabularyMenu.vocabularyInfo.vocabularyTestGame.actualizeVocabularyList(actualVocabularyList);

            if (Main != null && Main.vocabularyMenu != null && Main.vocabularyMenu.vocabularyInfo != null && Main.vocabularyMenu.vocabularyInfo.vocabularyTestGame != null && Main.vocabularyMenu.vocabularyInfo.vocabularyTestGame.vocabularyInfoExtended != null)
                Main.vocabularyMenu.vocabularyInfo.vocabularyTestGame.vocabularyInfoExtended.actualizeVocabularyList(actualVocabularyList);

        }

        private void actualizeVocabularyExtendedList(bool[] vocabularyList, ObjectPermission[] vocabularyObjectList)
        {
            if (Main != null)
                Main.actualizeVocabularyExtendedList(vocabularyList, vocabularyObjectList);

            if (Main != null && Main.vocabularyMenu != null)
                Main.vocabularyMenu.actualizeVocabularyExtendedList(vocabularyList, vocabularyObjectList);

            if (Main != null && Main.vocabularyMenu != null && Main.vocabularyMenu.vocabularyExtendedInfo != null)
                Main.vocabularyMenu.vocabularyExtendedInfo.actualizeVocabularyExtendedList(vocabularyList, vocabularyObjectList);

            if (Main != null && Main.vocabularyMenu != null && Main.vocabularyMenu.vocabularyInfo != null)
                Main.vocabularyMenu.vocabularyInfo.actualizeVocabularyExtendedList(vocabularyList, vocabularyObjectList);

            if (Main != null && Main.vocabularyMenu != null && Main.vocabularyMenu.mainVocabularyGame1 != null)
                Main.vocabularyMenu.mainVocabularyGame1.actualizeVocabularyExtendedList(vocabularyList, vocabularyObjectList);

            if (Main != null && Main.vocabularyMenu != null && Main.vocabularyMenu.mainVocabularyGame1 != null && Main.vocabularyMenu.mainVocabularyGame1.vocabularyInfoExtended != null)
                Main.vocabularyMenu.mainVocabularyGame1.vocabularyInfoExtended.actualizeVocabularyExtendedList(vocabularyList, vocabularyObjectList);

            if (Main != null && Main.vocabularyMenu != null && Main.vocabularyMenu.mainVocabularyGame2 != null)
                Main.vocabularyMenu.mainVocabularyGame2.actualizeVocabularyExtendedList(vocabularyList, vocabularyObjectList);

            if (Main != null && Main.vocabularyMenu != null && Main.vocabularyMenu.mainVocabularyGame2 != null && Main.vocabularyMenu.mainVocabularyGame2.vocabularyInfoExtended != null)
                Main.vocabularyMenu.mainVocabularyGame2.vocabularyInfoExtended.actualizeVocabularyExtendedList(vocabularyList, vocabularyObjectList);

            //
            if (Main != null && Main.vocabularyMenu != null && Main.vocabularyMenu.vocabularyInfo != null && Main.vocabularyMenu.vocabularyInfo.vocabularyTestGame != null)
                Main.vocabularyMenu.vocabularyInfo.vocabularyTestGame.actualizeVocabularyExtendedList(vocabularyList, vocabularyObjectList);

            if (Main != null && Main.vocabularyMenu != null && Main.vocabularyMenu.vocabularyInfo != null && Main.vocabularyMenu.vocabularyInfo.vocabularyTestGame != null && Main.vocabularyMenu.vocabularyInfo.vocabularyTestGame.vocabularyInfoExtended != null)
                Main.vocabularyMenu.vocabularyInfo.vocabularyTestGame.vocabularyInfoExtended.actualizeVocabularyExtendedList(vocabularyList, vocabularyObjectList);
        }

        public void save()
        {
            //Main.settingsMenu.closeLayoutActivity();

            actualizeVocabularyList(vocabularySelectedList);

            //bool[] t1 = null;
            //SubmissionOfKanji[] t2 = null;

            //os.deserializeObjectArray(ref t1);
            //os.serializeObjectArray(vocabularySelectedList);

            ofm.saveObjectArray(vocabularySelectedList, ofm.filePath3);

            //os.deserializeObjectArray(ref t2);
            //os.serializeObjectArray(actualVocabularyList);

            ofm.saveObjectArray(actualVocabularyList);

            Main.settingsMenu.changes = false;
        }

        public void getSelectedVocabularyArray()
        {
            ArrayList list = new ArrayList();

            for (int i = 0; i < vocabularySelectedList.Length; i++)
            {
                if (vocabularySelectedList[i])
                {
                    list.Add(new ObjectPermission(i, vocabularySelectedExtendedList[i]));
                }
            }

            vocabularySelectedExtendedObjectList = new ObjectPermission[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                vocabularySelectedExtendedObjectList[i] = (ObjectPermission)list[i];
            }
        }
    }
}

