
//This is client.Client.java file

package client;

import handlers.IWebService;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.net.URL;

import java.util.Locale;

import org.apache.xmlrpc.client.XmlRpcClient;

import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;

import org.apache.xmlrpc.client.util.ClientFactory;

public class Client {

	static String path = "public_client/files/";
	
    public static String ReadFromFile(String fileName){
		
		String fileText = "";
		
		try {
			BufferedReader reader = new BufferedReader(new FileReader(path + fileName));
			
			String line;

			while ((line = reader.readLine()) != null)
				fileText += line + "\n";
			
			reader.close();
			
		} catch (FileNotFoundException e) {
			fileText = e.getMessage();
		} catch (IOException e) {
			fileText  = e.getMessage();
		}
		
		return fileText;
	}

	public static void main(String[] args) {

		try {

			XmlRpcClientConfigImpl config = new XmlRpcClientConfigImpl();

			// Here we set the server URL

			config.setServerURL(new URL(
					"http://app3.cc.puv.fi/e1401209_as41_12/myxmlrpc"));

			config.setEnabledForExtensions(true);

			config.setEnabledForExceptions(true);

			XmlRpcClient client = new XmlRpcClient();

			client.setConfig(config);

			
			// In the following we call the method in the classic way.

			//Object[] params = new Object[] { word, locale };

			//String translation = (String) client.execute("service.getWord",
			//		params);

			//System.out.println("Translation of " + word + ": " + translation);

			
			// In the following we call the methods through dynamic proxy

			ClientFactory factory = new ClientFactory(client);

			IWebService myService = (IWebService) factory
					.newInstance(IWebService.class);

			String fileNameClient = "c1";
			String content = ReadFromFile(fileNameClient);
			
			System.out.println("File to upload (File Name: " + fileNameClient + "):\n" + content);
			
			byte [] uncompressed = content.getBytes();
			
			
			String ret = myService.uploadFile(fileNameClient, uncompressed);
			
			System.out.println(ret);
			
			System.out.println();
			System.out.println("---------------------");
			
			String fileNameServer = "c1";
			byte arr []= myService.downloadFile(fileNameServer);
				
			String decoded = new String(arr); 
			
			System.out.println("File downloaded from server (File Name: " + fileNameServer + "):");
			System.out.println(decoded);
		}
		catch (Exception e)
		{
			System.out.println("Exception: " + e.getMessage());
		}
	}

}
