//This is client.Client.java file.

package client;

import java.net.URL;

import org.apache.xmlrpc.client.XmlRpcClient;
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;
import org.apache.xmlrpc.client.XmlRpcCommonsTransportFactory;
import java.util.Vector;

public class Client {

	public static void main(String[] args) {

		XmlRpcClientConfigImpl config = new XmlRpcClientConfigImpl();

		try {

			config.setServerURL(new URL("http://shell.puv.fi:"
					+ Integer.parseInt(args[0]) + "/"));

			XmlRpcClient client = new XmlRpcClient();

			client.setTransportFactory(new XmlRpcCommonsTransportFactory(client));

			client.setConfig(config);

			Object[] params = new Object[] { new String("Amanda") };
			String msg = (String) client.execute(
					"handlers.ActionHandler.getDate", params);
			System.out.println(msg);

			Vector<Integer> vec = new Vector<Integer>(4);

			// use add() method to add elements in the vector
			vec.add(41);
			vec.add(33);
			vec.add(22);
			vec.add(1342);

			params = new Object[] { vec };
			Integer numb = (Integer) client.execute(
					"handlers.ActionHandler.getMin", params);
			System.out.println("MIN = " + numb);

		} catch (Exception e) {
			System.out.println("Exception: " + e.getMessage());
		}
	}
}