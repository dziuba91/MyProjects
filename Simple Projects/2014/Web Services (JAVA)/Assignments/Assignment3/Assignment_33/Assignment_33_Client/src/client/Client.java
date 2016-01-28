//WinSCP !!!

package client;

import handlers.ServiceHandler;

import java.net.URL;

import org.apache.xmlrpc.client.XmlRpcClient;
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;
import org.apache.xmlrpc.client.util.ClientFactory;

import java.util.Enumeration;
import java.util.Hashtable;

public class Client {

	public static void main(String[] args) {

		try {

			XmlRpcClientConfigImpl config = new XmlRpcClientConfigImpl();

			// config.setServerURL(new
			// URL("http://localhost:8080/mg_XmlRpcServlet/xmlrpc"));

			config.setServerURL(new URL(
					"http://app2.cc.puv.fi/e1401209_as33/xmlrpc"));

			XmlRpcClient client = new XmlRpcClient();

			client.setConfig(config);

			// In the following we use dynamic proxy
			ClientFactory clientFactory = new ClientFactory(client);
			ServiceHandler servicHandler = (ServiceHandler) clientFactory
					.newInstance(ServiceHandler.class);

			System.out.println("Dynamic Proxy:");
			Hashtable<String, Integer> results = servicHandler.getFileLength();
			
			System.out.printf("%-10s%8s\n", "File", "Length");
			System.out.println("------------------");
			
			Enumeration<String> enu = results.keys();
			String key;
			Integer value;
			while (enu.hasMoreElements()) {
			
				key = enu.nextElement();
				value = results.get(key);

				System.out.printf("%-10s%8s\n", key, value);
			}

		}

		catch (Exception e)

		{

			System.out.println("Exception: " + e.getMessage());

		}

	}

}