//This is client.Client.java file

package client;

import java.io.BufferedReader;
import java.io.File;
import java.io.InputStreamReader;
import java.net.URL;
import java.util.Enumeration;
import java.util.Hashtable;
import java.util.Iterator;
import java.util.Scanner;
import java.util.Map.Entry;

import org.apache.xmlrpc.client.XmlRpcClient;
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;
import org.apache.xmlrpc.client.XmlRpcCommonsTransportFactory;
import org.apache.xmlrpc.client.util.ClientFactory;

import handlers.IActionHandler;

;

public class Client {

	public static void main(String[] args) {

		if (args.length < 2) {

			System.out.println("Usage: java Client [server] [port]");
			System.exit(-1);

		}

		XmlRpcClientConfigImpl config = new XmlRpcClientConfigImpl();
		try {

			config.setServerURL(new URL("http://" + args[0] + ":"
					+ Integer.parseInt(args[1])));

			XmlRpcClient client = new XmlRpcClient();
			client.setTransportFactory(new XmlRpcCommonsTransportFactory(client));

			client.setConfig(config);

			ClientFactory clientFactory = new ClientFactory(client);

			IActionHandler webServive = (IActionHandler) clientFactory
					.newInstance(IActionHandler.class);
			
			//
			System.out.printf("Type text to search : ");
			
			String word = "";
			Scanner scanner = new Scanner(System.in);
			word = scanner.nextLine();
			
			Hashtable<String, Integer> hashTable = webServive
					.search(word);;
			
			System.out.printf("Word to Search : " + word);
			System.out.println();
			System.out.println();
					
			System.out.printf("%-10s%5s\n", "File", "Count");
			System.out.println("------------------");
			
			Enumeration<String> enu = hashTable.keys();
			String key;
			Integer value;
			while (enu.hasMoreElements()) {

				key = enu.nextElement();
				value = hashTable.get(key);

				System.out.printf("%-10s%5s\n", key, value);
			}

			System.out.println("------------------");

		} catch (Exception e) {
			System.out.println("Exception: " + e.getMessage());
		}
	}
}