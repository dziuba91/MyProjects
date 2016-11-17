//This is client.Client.java file

package client;

//import handlers.Dictionary;
import BASIC_handlers.IWebService1;
import SERVICE_handlers.IWebService2;

import java.net.URL;

import java.util.Locale;

import org.apache.xmlrpc.client.XmlRpcClient;

import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;

import org.apache.xmlrpc.client.util.ClientFactory;

public class Client {

	public static void main(String[] args) {

		try {

			XmlRpcClientConfigImpl config = new XmlRpcClientConfigImpl();

			config.setServerURL(new URL(
					"http://app3.cc.puv.fi/e1401209_as6_9/auth_xmlrpc"));

			config.setEnabledForExtensions(true);

			config.setEnabledForExceptions(true);
			
			// Here we set the username and password

			/*
			 * config.setBasicUserName("Lizzie");
			 * 
			 * config.setBasicPassword("L1000");
			 */


			//config.setBasicUserName("Oliver");
			
			//config.setBasicPassword("O1000");
			

			config.setBasicUserName("tomcat");		// BASIC authentication login

			config.setBasicPassword("tomcat");		// BASIC authentication password

			XmlRpcClient client = new XmlRpcClient();

			client.setConfig(config);
			
			// In the following we call the methods through dynamic proxy

			ClientFactory factory = new ClientFactory(client);

			IWebService1 myService = (IWebService1) factory
					.newInstance(IWebService1.class);
			
			String file = "f1";
			String serviceLogin = "Taina";
			String servicePassword = "1000";
			String ret = downloadFile(myService.getServiceServletURL(), serviceLogin, servicePassword, file);
			
			if(ret != "")
			{
				System.out.println("Content of file : " + file);
				System.out.println("--------------------------");
				System.out.println(ret);
			}
			
		}
		catch (Exception e)
		{
			
			System.out.println("BASIC Authentication Problem!!!");
			
			System.out.println("Exception: " + e.getMessage());
			
		}

	}
	
	
	public static String downloadFile(String serviceURL, String serviceLogin, String servicePassword, String fileName)	// service by XMLRPC authentication
	{
		try {

			XmlRpcClientConfigImpl config = new XmlRpcClientConfigImpl();

			// Here we set the server URL

			config.setServerURL(new URL(serviceURL));

			config.setEnabledForExtensions(true);

			config.setEnabledForExceptions(true);

			// Here we set the username and password
			config.setBasicUserName(serviceLogin);
			config.setBasicPassword(servicePassword);

			XmlRpcClient client = new XmlRpcClient();
			
			client.setConfig(config);


			// In the following we call the methods through dynamic proxy

			ClientFactory factory = new ClientFactory(client);

			IWebService2 myService = (IWebService2) factory
					.newInstance(IWebService2.class);

			return new String (myService.downloadFile(fileName));

		}
		catch (Exception e)
		{
			
			System.out.println("SERVICE Authentication Problem!!!");
			
			System.out.println("Exception: " + e.getMessage());
			
		}
		
		return "";
	}

}