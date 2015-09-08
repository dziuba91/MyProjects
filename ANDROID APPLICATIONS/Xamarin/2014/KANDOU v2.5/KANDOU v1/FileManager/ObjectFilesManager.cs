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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace KANDOU_v1.FileManager
{
    class ObjectFilesManager
    {
        string filePath1 = @"XML1.dat";

        string filePath2 = @"XML2.dat";

        public string filePath3 = @"XML2_per.dat";

        public string filePath4 = @"XML2_per2.dat";


        public void saveObjectArray(KanjiDataType[] object1)
        {
            FileStream fileStream = new FileStream(pathBuilder(filePath1), FileMode.Create, FileAccess.Write);

            StreamWriter s = new StreamWriter(fileStream);

            for (int i = 0; i < object1.Length; i++)
            {
                s.WriteLine(object1[i].meaning);
                s.WriteLine(object1[i].reading);
                s.WriteLine(object1[i].sign);
                s.WriteLine("-");

                for (int j = 0; j < object1[i].submissions.Length; j++)
                {
                    s.WriteLine(object1[i].submissions[j].meaning);
                    s.WriteLine(object1[i].submissions[j].priority);
                    s.WriteLine(object1[i].submissions[j].reading);
                    s.WriteLine(object1[i].submissions[j].signs);
                }
                s.WriteLine("-");
            }

            s.Flush();
            s.Close();

            //fileStream.Flush();
            fileStream.Close();
            //
        }

        public void saveObjectArray(SubmissionOfKanji[] object1)
        {
            FileStream fileStream = new FileStream(pathBuilder(filePath2), FileMode.Create, FileAccess.Write);

            StreamWriter s = new StreamWriter(fileStream);

            for (int i = 0; i < object1.Length; i++)
            {
                s.WriteLine(object1[i].meaning);
                s.WriteLine(object1[i].reading);
                s.WriteLine(object1[i].signs);
                s.WriteLine(object1[i].priority);
            }

            //Console.WriteLine("X2=" + object1.Length);

            s.Flush();
            s.Close();

            //fileStream.Flush();
            fileStream.Close();
            //
        }

        public void saveObjectArray(bool[] object1, string path)
        {
            FileStream fileStream = new FileStream(pathBuilder(path), FileMode.Create, FileAccess.Write);

            StreamWriter s = new StreamWriter(fileStream);

            for (int i = 0; i < object1.Length; i++)
            {
                if (object1[i])
                    s.WriteLine("T");
                else
                    s.WriteLine("F");
            }

            //Console.WriteLine("X1=" + object1.Length);

            s.Flush();
            s.Close();

            //fileStream.Flush();
            fileStream.Close();
            //
        }

        public void readObjectArrayFromFile(ref KanjiDataType[] object1)
        {
            FileStream fileStream = new FileStream(pathBuilder(filePath1), FileMode.Open, FileAccess.Read);

            StreamReader s = new StreamReader(fileStream);

            string ret = s.ReadToEnd();

            ArrayList list = new ArrayList();

            ArrayList listSub = new ArrayList();

            string[] ret2 = ret.Split('\n');

            for (int i = 0; i < ret2.Length; i += 5)
            {
                string meaning = ret2[i];
                string reading = ret2[i+1];
                string sign = ret2[i+2];

                int sub = i + 3;
                if (ret2[sub] == "-")
                {
                    sub++;

                    while (ret2[sub] != "-")
                    {
                        listSub.Add(new SubmissionOfKanji(ret2[sub+3], ret2[sub], ret2[sub+2], int.Parse(ret2[sub+1])));

                        sub += 4;
                        i += 4;
                    }

                    SubmissionOfKanji [] subArray = new SubmissionOfKanji[listSub.Count];
                    for (int j = 0; j < listSub.Count; j++)
                        subArray[j] = (SubmissionOfKanji)listSub[j];

                    listSub.Clear();

                    list.Add(new KanjiDataType(sign, meaning, reading, subArray));
                }
            }

            object1 = new KanjiDataType[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                object1[i] = (KanjiDataType)list[i];
            }

            s.Close();

            fileStream.Close();
        }

        public void readObjectArrayFromFile(ref SubmissionOfKanji[] object1)
        {
            FileStream fileStream = new FileStream(pathBuilder(filePath2), FileMode.Open, FileAccess.Read);

            StreamReader s = new StreamReader(fileStream);

            string ret = s.ReadToEnd();

            ArrayList list = new ArrayList();

            string[] ret2 = ret.Split('\n');

            //Console.WriteLine("X1=" + ret2.Length);

            for (int i = 0; (i < ret2.Length) && (ret2[i] != ""); i+=4)
            {
                list.Add(new SubmissionOfKanji(ret2[i+2],ret2[i],ret2[i+1], Convert.ToInt32(ret2[i+3])));
            }

            object1 = new SubmissionOfKanji[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                object1[i] = (SubmissionOfKanji)list[i];
            }

            s.Close();

            fileStream.Close();
        }

        public void readObjectArrayFromFile(ref bool[] object1, string path)
        {
            FileStream fileStream = new FileStream(pathBuilder(path), FileMode.Open, FileAccess.Read);

            StreamReader s = new StreamReader(fileStream);

            string ret = s.ReadToEnd();

            string[] ret2 = ret.Split('\n');
            
            //Console.WriteLine("X2=" + ret2.Length);

            object1 = new bool[ret2.Length];

            for (int i = 0; i < ret2.Length; i++)
            {
                if (ret2[i].Equals("T"))
                    object1[i] = true;
                else
                    object1[i] = false;
            }

            s.Close();

            fileStream.Close();
        }

        private string pathBuilder(string fileName)
        {
            //var a = Resource
            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
            //var a = NSBundle.MainBundle.BundlePath;
            var xmlFilePath = System.IO.Path.Combine(sdCardPath, "KandouData2/" + fileName);
            //var assembly = typeof(LoadResourceText).GetTypeInfo().Assembly;
            return xmlFilePath;
        }
    }
}