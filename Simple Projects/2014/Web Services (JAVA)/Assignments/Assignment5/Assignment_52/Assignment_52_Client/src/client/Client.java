//This is client.Client.java file

package client;

import handlers.Dictionary;

import java.net.MalformedURLException;
import java.net.URL;

import java.util.Hashtable;
import java.util.Locale;
import java.util.Vector;

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
						"http://app3.cc.puv.fi/e1401209_as52_1/auth_xmlrpc"));
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
	
	public Hashtable translateToFi(Vector word) // need Eng word
	{
		Locale locale = new Locale("fi", "FI");
		
		try
		{
			return myDictionary.getWord(word, locale);
		}
		catch (Exception e)
		{}
		
		return null;
	}
	
	public Hashtable translateToEng(Vector word) // need Fi word
	{
		Locale locale = new Locale("en", "GB");
		
		try
		{
			return myDictionary.getWord(word, locale);
		}
		catch (Exception e)
		{}
		
		return null;
	}
	
	public Boolean getLogInStatus() // test if we can execute some service (if yes login and password is correct)
	{
		try
		{
			// SERVER TEST
			String word = "Thank you";
			Locale locale = new Locale("fi", "FI");
			
			Vector<String> v = new Vector<String>();
			v.add(word);
			
			Hashtable translation = myDictionary.getWord(v, locale);
			
			return true;
		}
		catch (Exception e) { } // if some exceptions then probably password or login is not correct
		
		return false;
	}
}