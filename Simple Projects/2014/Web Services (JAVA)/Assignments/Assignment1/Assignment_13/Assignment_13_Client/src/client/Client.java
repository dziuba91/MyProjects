//This is client.Client.java file

package client;

import java.io.BufferedReader;
import java.io.File;
import java.io.InputStreamReader;
import java.net.URL;
import java.util.Scanner;

import org.apache.xmlrpc.client.XmlRpcClient;
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;
import org.apache.xmlrpc.client.XmlRpcCommonsTransportFactory;
import org.apache.xmlrpc.client.util.ClientFactory;

//import handlers.IActionHandler;;

public class Client {

 public static void main(String[] args) {

  if (args.length < 2) {

   System.out.println("Usage: java Client [server] [port]");
   System.exit(-1);

  }
  double area = 0.0;

  XmlRpcClientConfigImpl config = new XmlRpcClientConfigImpl();
  try {

   config.setServerURL(new URL("http://" + args[0] + ":"
     + Integer.parseInt(args[1]) ));

   XmlRpcClient client = new XmlRpcClient();
   client.setTransportFactory(new XmlRpcCommonsTransportFactory(client));

   client.setConfig(config);

   //
   for(;;)
   {
	System.out.println(" ----------- ");
	System.out.println("Choose option: ");
   	System.out.println("1. Server file list.");
   	System.out.println("2. Read file.");
   	System.out.println("3. Write to file.");
   	System.out.println();
   	System.out.println("0. Exit.");
   
   	Scanner scanner = new Scanner(System.in);
   
   	Integer option = scanner.nextInt();
   	scanner.reset();
   	
   	if (option == 0) break;
   	else if (option == 1) 
   	{
   		System.out.println("Server File List: ");
   
   		int a;
   		Object [] params = new Object[] { };
   		String msg = (String) client.execute("action.getFileList2", params);
   		System.out.println(msg);
   		
   		Scanner scanner1 = new Scanner(System.in);
   		
   		System.out.println(" ----------- ");
   		
   		int tmp;
   		for (;;)
   		{
   			System.out.println(" Press: \"0\" (zero) to continue.");
   			
   			tmp = scanner1.nextInt();
   			scanner1.reset();
   			
   			if (tmp == 0) break;
   		}
   	}
   	else if (option == 2)
   	{
   		System.out.println("Select File: ");
   	   
   		Scanner scanner1 = new Scanner(System.in);
   		String str = scanner1.nextLine();
   		scanner1.reset();
   		
   		Object [] params = new Object[] { str };
   		String msg = (String) client.execute("action.ReadFromFile", params);
   		
   		System.out.println("File: " + str);
   		System.out.println(" ----------- ");
   		System.out.println(msg);
   		System.out.println(" ----------- ");
   		
   		int tmp;
   		for (;;)
   		{
   			System.out.println(" Press: \"0\" (zero) to continue.");
   			
   			tmp = scanner1.nextInt();
   			scanner1.reset();
   			
   			if (tmp == 0) break;
   		}
   	}
   	else if (option == 3)
   	{
   	//Scanner scanner = new Scanner(System.in);
   		Scanner scanner1 = new Scanner(System.in);
   		System.out.println("Select File: ");
   		String str1 = scanner1.nextLine();
   		
   		System.out.println("Type text to be written to selected file: ");
   		String str2 = scanner1.nextLine();
   		scanner1.reset();
   		
   		Object [] params = new Object[] { str1, str2 };
   		String msg = (String) client.execute("action.WriteToFile", params);
   		
   		System.out.println("File: " + str1);
   		System.out.println(" ----------- ");
   		System.out.println(msg);
   		
   		System.out.println(" ----------- ");
   		
   		int tmp;
   		for (;;)
   		{
   			System.out.println(" Press: \"0\" (zero) to continue.");
   			
   			tmp = scanner1.nextInt();
   			scanner1.reset();
   			
   			if (tmp == 0) break;
   		}
   	}
   	else
   	{
   		System.out.println("Wrong option number! Try again.");
   	}
   }
  } catch (Exception e) {
   System.out.println("Exception: " + e.getMessage());
  }
 }
}