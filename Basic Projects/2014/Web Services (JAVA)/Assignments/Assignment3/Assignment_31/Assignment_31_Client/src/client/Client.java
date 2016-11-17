//This is client.Client.java file

package client;

import java.net.URL;
import java.util.Enumeration;
import java.util.Iterator;
import java.util.Vector;
import java.util.Hashtable;
import java.util.Map.Entry;

import org.apache.xmlrpc.client.XmlRpcClient;

import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;

import org.apache.xmlrpc.client.util.ClientFactory;

import ws.ICalculator;

public class Client {

	public static void main(String[] args) {

		try {

			XmlRpcClientConfigImpl config = new XmlRpcClientConfigImpl();

			// config.setServerURL(new URL("http://127.0.0.1:8080/xmlrpc"));

			config.setServerURL(new URL("http://127.0.0.1:9500"));

			XmlRpcClient client = new XmlRpcClient();

			client.setConfig(config);

			// In the following we call the methods through dynamic proxy

			ClientFactory factory = new ClientFactory(client);

			ICalculator myCalculator = (ICalculator) factory
					.newInstance(ICalculator.class);

			System.out.println("The numbers returned by the service: ");
			Vector<Double> numbers = myCalculator.getNumbers(10);
			for (Double d : numbers)
				System.out.print(d + " ");

			System.out
					.println("\nThe mutated numbers returned by the service: ");
			numbers = myCalculator.mutateNumbers(numbers);
			for (double d : numbers)
				System.out.print(d + " ");
			
			
			///
			System.out
					.println("\n\n//////////////\nImprove content: ");
			
			System.out
					.println("\nVector1: ");
			Vector<Double> v1 = myCalculator.getNumbers(10);
			for (Double d : v1)
				System.out.print(d + " ");
			
			System.out
					.println("\nVector2: ");
			Vector<Double> v2 = myCalculator.getNumbers(10);
			for (Double d : v2)
				System.out.print(d + " ");
			
			System.out
					.println("\n\nThe vectors returned by the service: ");
			Hashtable<String, Object []> hashTable = myCalculator.exchangeNumbers(v1, v2);
			
			Enumeration<String> enu = hashTable.keys();
			String key;
			Object [] value1 = null;
			Object [] value2 = null;
			//Object [] test;
			while (enu.hasMoreElements()) {
				key = enu.nextElement();
				
				if (key.equals("1"))
				{
					value1 = hashTable.get(key);
					//System.out.print("v1 = "+value1.length + "\n");
				}
				else if (key.equals("2"))
					value2 = hashTable.get(key);
				/*
				else if (key.equals("3"))
				{
					System.out
							.println("TEST: ");
					
					test = hashTable.get(key);
					System.out.print(test.length + "\n");
					
					for (int i = 0; i<test.length; i++)
						System.out.print(test[i] + " ");
				}*/
			}
			
			System.out
				.println("Vector1 (returned): ");
			for (int i = 0; i<value1.length; i++)
				System.out.print(value1[i] + " ");
			
			System.out
				.println("\nVector2 (returned): ");
			for (int i = 0; i<value2.length; i++)
				System.out.print(value2[i] + " ");
		}

		catch (Exception e)

		{

			System.out.println("Exception: " + e.getMessage());

		}

	}

}