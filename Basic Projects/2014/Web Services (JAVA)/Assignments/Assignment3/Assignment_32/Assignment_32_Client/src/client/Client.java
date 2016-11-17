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

import ws.IServiceHandler;

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

			IServiceHandler myServices = (IServiceHandler) factory
					.newInstance(IServiceHandler.class);

			//1
			int s1 = 17;
			int s2 = 51;
			System.out.println("1) Prime numbers from " + s1 + " to " + s2 + " :");
			Vector<Integer> numbers = myServices.getPrimeNumbers(s1, s2);
			for (Integer d : numbers)
				System.out.print(d + " ");
			
			//2
			Vector<String> v = new Vector<String>();
			v.add("Hopefully,");
			v.add("We");
			v.add("are");
			v.add("LEARNing");
			v.add("something...");
			
			System.out.println();
			System.out.println();
			System.out.println("2) Sorting content of the vector : ");
			System.out.println("-> Vector : ");
			for (String d : v)
				System.out.print(d + " ");
			System.out.println();
			
			Vector<String> v1 = new Vector<String>();
			v1 = myServices.sortVectorContent(v, "a");
			System.out.println("-> Vector content sorted in ascending order : ");
			for (String d : v1)
				System.out.print(d + " ");
			System.out.println();
			
			Vector<String> v2 = new Vector<String>();
			v2 = myServices.sortVectorContent(v, "d");
			System.out.println("-> Vector content sorted in descending order : ");
			for (String d : v2)
				System.out.print(d + " ");
			System.out.println();
		}

		catch (Exception e)

		{

			System.out.println("Exception: " + e.getMessage());

		}

	}

}