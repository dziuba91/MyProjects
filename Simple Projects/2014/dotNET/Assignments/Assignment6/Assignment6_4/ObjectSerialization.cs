using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace Assignment6_4
{
    class ObjectSerialization
    {
        public string filePath = @"product.xml";


        //
        // ***********************
        // Serialize Object Arrays

        public bool serializeObjectArray(Product[] product, string filePath)
        {
            FileStream fileStream;

            try
            {
                fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
            }
            catch
            {
                return false;
            }

            SoapFormatter soapFormatter = new SoapFormatter();

            try
            {
                for (int i = 0; i < product.Length; i++)
                    soapFormatter.Serialize(fileStream, product[i]);
            }
            catch
            {
                fileStream.Flush();
                fileStream.Close();

                return false;
            }

            fileStream.Flush();
            fileStream.Close();

            return true;
        }

        public bool serializeHashtable(Hashtable productList)
        {
            FileStream fileStream;

            try
            {
                fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
            }
            catch
            {
                return false;
            }

            SoapFormatter soapFormatter = new SoapFormatter();

            try
            {
                soapFormatter.Serialize(fileStream, productList);
            }
            catch
            {
                fileStream.Flush();
                fileStream.Close();

                return false;
            }

            fileStream.Flush();
            fileStream.Close();

            return true;
        }

        public bool deserializeObjectArray(ref Product [] product, string filePath)
        {
            FileStream fileStream;

            try
            {
                fileStream = new FileStream(filePath, FileMode.Open);
            }
            catch (IOException e)
            {
                return false;
            }

            SoapFormatter soapFormatter = new SoapFormatter();

            object obj = null;

            ArrayList list = new ArrayList();

            for (; ; )
            {
                try
                {
                    obj = soapFormatter.Deserialize(fileStream);

                    if (obj is Product)
                    {
                        list.Add((Product)obj);
                    }
                    else return false;
                }
                catch (EndOfStreamException) { }
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

            product = new Product[list.Count];
            bool execute = false;

            for (int i = 0; i < list.Count; i++)
            {
                product[i] = (Product)list[i];

                execute = true;
            }

            fileStream.Flush();
            fileStream.Close();

            if (execute) 
                return true;

            return false;
        }

        public bool deserializeHashtable(ref Hashtable productList)
        {
            FileStream fileStream;

            try
            {
                fileStream = new FileStream(this.filePath, FileMode.Open);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }

            SoapFormatter soapFormatter = new SoapFormatter();

            object obj = null;

            try
            {
                obj = soapFormatter.Deserialize(fileStream);

                if (obj is Hashtable)
                {
                    productList = (Hashtable)obj;

                    fileStream.Flush();
                    fileStream.Close();

                    return true;
                }
                else
                {
                    fileStream.Flush();
                    fileStream.Close();

                    return false;
                }
            }
            catch (EndOfStreamException) { }
            catch (SerializationException e1)
            {
                Console.WriteLine(e1.Message);
                //break;
            }
            catch (System.Xml.XmlException e2)
            {
                 Console.WriteLine(e2.Message);
                 //break;
            }

            fileStream.Flush();
            fileStream.Close();

            return false;
        }

        public bool findObject(ref Product product, int ID)
        {
            FileStream fileStream;

            try
            {
                fileStream = new FileStream(filePath, FileMode.Open);
            }
            catch (IOException e)
            {
                return false;
            }

            SoapFormatter soapFormatter = new SoapFormatter();

            object obj = null;

            try
            {
                obj = soapFormatter.Deserialize(fileStream);

                if (obj is Hashtable)
                {
                    Hashtable list = (Hashtable)obj;

                    IDictionaryEnumerator en = list.GetEnumerator();
                    while (en.MoveNext())
                    {
                        if ((int)en.Key == ID)
                        {
                            fileStream.Flush();
                            fileStream.Close();

                            Product [] productArray = null;
                            if (deserializeObjectArray(ref productArray, (string)en.Value))
                            {
                                for (int i = 0; i < productArray.Length; i++)
                                {
                                    if (productArray[i].getID() == ID)
                                    {
                                        product = productArray[i];
                                        return true;
                                    }
                                }

                                return false;
                            }
                            else
                                return false;
                        }
                    }
                }
            }
            catch (EndOfStreamException) { }
            catch (SerializationException e1)
            {
                Console.WriteLine(e1.Message);
                //break;
            }
            catch (System.Xml.XmlException e2)
            {
                Console.WriteLine(e2.Message);
                //break;
            }

            fileStream.Flush();
            fileStream.Close();

            return false;
        }

        /*
        public void copyFile(string filePath1, string filePath2)
        {
            try
            {
                if (File.Exists(filePath1))
                {
                    File.Copy(filePath1, filePath2);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\nFile copy problem. File : " + filePath);
                return;
            }
        } */

        public void clearFile(string filePath1)
        {
            try
            {
                if (File.Exists(filePath1))
                {
                    File.Delete(filePath1);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\nFile deleting problem. File : " + filePath);
                return;
            }
        }
    }
}
