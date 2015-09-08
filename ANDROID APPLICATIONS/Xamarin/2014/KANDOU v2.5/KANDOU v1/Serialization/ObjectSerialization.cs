using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using System.Collections;

using KANDOU_v1.DataTypes;

namespace KANDOU_v1.Serialization
{
    class ObjectSerialization
    {
        string filePath1 = @"XML1.dat";

        string filePath2 = @"XML2.dat";

        string filePath3 = @"XML2_per.dat";


        public void serializeObjectArray(KanjiDataType[] object1)
        {
            FileStream fileStream = new FileStream(pathBuilder(filePath1), FileMode.OpenOrCreate);

            //SoapFormatter soapFormatter = new SoapFormatter();
            BinaryFormatter binFormatter = new BinaryFormatter();

            for (int i = 0; i < object1.Length; i++)
                binFormatter.Serialize(fileStream, object1[i]);

            fileStream.Flush();
            fileStream.Close();
        }

        public void serializeObjectArray(SubmissionOfKanji[] object1)
        {
            //if (File.Exists(pathBuilder(filePath2)))
            //    File.Delete(pathBuilder(filePath2));

            FileStream fileStream = new FileStream(pathBuilder(filePath2), FileMode.OpenOrCreate);

            //SoapFormatter soapFormatter = new SoapFormatter();
            BinaryFormatter binFormatter = new BinaryFormatter();

            for (int i = 0; i < object1.Length; i++)
                binFormatter.Serialize(fileStream, object1[i]);

            fileStream.Flush();
            fileStream.Close();
        }

        public void serializeObjectArray(bool[] object1)
        {
            //if (File.Exists(pathBuilder(filePath3)))
            //    File.Delete(pathBuilder(filePath3));

            FileStream fileStream = new FileStream(pathBuilder(filePath3), FileMode.OpenOrCreate);

            //SoapFormatter soapFormatter = new SoapFormatter();
            BinaryFormatter binFormatter = new BinaryFormatter();

            ObjectPermission[] op = new ObjectPermission[object1.Length];
            for (int i = 0; i < object1.Length; i++)
            {
                op[i] = new ObjectPermission();
                op[i].id = i;
                if (object1[i]) op[i].permission = true;
                else op[i].permission = false;
            }

            for (int i = 0; i < op.Length; i++)
                binFormatter.Serialize(fileStream, op[i]);

            fileStream.Flush();
            fileStream.Close();
        }


        public bool deserializeObjectArray(ref KanjiDataType[] object1)
        {
            FileStream fileStream = new FileStream(pathBuilder(filePath1), FileMode.Open);

            //SoapFormatter soapFormatter = new SoapFormatter();
            BinaryFormatter binFormatter = new BinaryFormatter();

            object obj = null;

            ArrayList list1 = new ArrayList();

            for (; ; )
            {
                try
                {
                    obj = binFormatter.Deserialize(fileStream);

                    if (obj is KanjiDataType)
                    {
                        list1.Add((KanjiDataType)obj);
                    }
                    else return false;
                }
                catch (EndOfStreamException) { break; }
                catch (SerializationException)
                {
                    //Console.WriteLine(e.Message);
                    break;
                }
                catch (System.Xml.XmlException)
                {
                    //Console.WriteLine(e.Message);
                    break;
                }
            }

            object1 = new KanjiDataType[list1.Count];

            for (int i = 0; i < list1.Count; i++)
            {
                object1[i] = (KanjiDataType)list1[i];
            }

            fileStream.Flush();
            fileStream.Close();

            return true;
        }

        public bool deserializeObjectArray(ref SubmissionOfKanji[] object1)
        {
            FileStream fileStream = new FileStream(pathBuilder(filePath2), FileMode.Open);

            //SoapFormatter soapFormatter = new SoapFormatter();
            BinaryFormatter binFormatter = new BinaryFormatter();

            object obj = null;

            ArrayList list1 = new ArrayList();

            for (int i=0; ; i++)
            {
                try
                {
                    obj = binFormatter.Deserialize(fileStream);

                    if (obj is SubmissionOfKanji)
                    {
                        list1.Add((SubmissionOfKanji)obj);

                        //if (i > 1400) return true;
                    }
                    else return false;
                }
                catch (EndOfStreamException) { break; }
                catch (SerializationException)
                {
                    //Console.WriteLine(e.Message);
                    break;
                }
                catch (System.Xml.XmlException)
                {
                    //Console.WriteLine(e.Message);
                    break;
                }
            }

            object1 = new SubmissionOfKanji[list1.Count];

            for (int i = 0; i < list1.Count; i++)
            {
                object1[i] = (SubmissionOfKanji)list1[i];
            }

            fileStream.Flush();
            fileStream.Close();

            return true;
        }

        public bool deserializeObjectArray(ref bool[] object1)
        {
            FileStream fileStream = new FileStream(pathBuilder(filePath3), FileMode.Open);

            //SoapFormatter soapFormatter = new SoapFormatter();
            BinaryFormatter binFormatter = new BinaryFormatter();

            object obj = null;

            ArrayList list1 = new ArrayList();

            for (; ; )
            {
                try
                {
                    obj = binFormatter.Deserialize(fileStream);

                    if (obj is ObjectPermission)
                    {
                        list1.Add((ObjectPermission)obj);
                    }
                    else return false;
                }
                catch (EndOfStreamException) { break; }
                catch (SerializationException)
                {
                    //Console.WriteLine(e.Message);
                    break;
                }
                catch (System.Xml.XmlException)
                {
                    //Console.WriteLine(e.Message);
                    break;
                }
            }

            object1 = new bool[list1.Count];

            for (int i = 0; i < list1.Count; i++)
            {
                if (((ObjectPermission)list1[i]).permission)
                    object1[i] = true;
                else object1[i] = false;
            }

            fileStream.Flush();
            fileStream.Close();

            return true;
        }

        private string pathBuilder(string fileName)
        {
            //var a = Resource
            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
            //var a = NSBundle.MainBundle.BundlePath;
            var xmlFilePath = System.IO.Path.Combine(sdCardPath, "KandouData/" + fileName);
            //var assembly = typeof(LoadResourceText).GetTypeInfo().Assembly;
            return xmlFilePath;
        }
    }
}