//This is client.Client.java file

package client;

import handlers.Dictionary;

import java.net.MalformedURLException;
import java.net.URL;

import java.util.Locale;

import org.apache.xmlrpc.XmlRpcConfig;
import org.apache.xmlrpc.client.XmlRpcClient;

import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;

import org.apache.xmlrpc.client.util.ClientFactory;

public class Client {

	XmlRpcClientConfigImpl config = null;
	XmlRpcClient client = null;
	ClientFactory factory = null;
	Dictionary myDictionary = null;
	//String separator = System.getProperty("file.separator");
	public byte arr [] = null;
	
	public Client(String login, String password) {

		try {

			config = new XmlRpcClientConfigImpl();

			try {
				config.setServerURL(new URL(
						"http://app3.cc.puv.fi/e1401209_as51/auth_xmlrpc"));
			} catch (MalformedURLException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}

			config.setEnabledForExtensions(true);

			config.setEnabledForExceptions(true);

			// Here we set the username and password

			config.setBasicUserName(login);

			config.setBasicPassword(password);

			client = new XmlRpcClient();

			client.setConfig(config);

			factory = new ClientFactory(client);

			myDictionary = (Dictionary) factory
					.newInstance(Dictionary.class);
		}
		catch (Exception e)
		{
			System.out.println("Problem with conection to server!!!");
			System.out.println("Exception: " + e.getMessage());
		}	
	}
	
	public String translateToFi(String word) // need Eng word
	{
		Locale locale = new Locale("fi", "FI");
		
		try
		{
			return myDictionary.getWord(word, locale);
		}
		catch (Exception e)
		{}
		
		return "";
	}
	
	public String translateToEng(String word) // need Fi word
	{
		Locale locale = new Locale("en", "GB");
		
		try
		{
			return myDictionary.getWord(word, locale);
		}
		catch (Exception e)
		{}
		
		return "";
	}
	
	public Boolean getLogInStatus() // test if we can execute some service (if yes login and password is correct)
	{
		try
		{
			// SERVER TEST
			String word = "Thank you";
			Locale locale = new Locale("fi", "FI");
			
			String translation = myDictionary.getWord(word, locale);
			
			return true;
		}
		catch (Exception e) { } // if some exceptions then probably password or login is not correct
		
		return false;
	}
}