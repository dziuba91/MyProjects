
//This is client.Client.java file

package client;

import handlers.DBHandler;

import java.io.File;
import java.net.MalformedURLException;
import java.net.URL;

import org.apache.xmlrpc.client.XmlRpcClient;
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;
import org.apache.xmlrpc.client.util.ClientFactory;

public class XMLRPCClient {

	XmlRpcClientConfigImpl config = null;
	XmlRpcClient client = null;
	ClientFactory factory = null;
	DBHandler dbHanlder = null;
	//String separator = System.getProperty("file.separator");

	public XMLRPCClient(String serverURL, String userName, String password) {

		config = new XmlRpcClientConfigImpl();

		// Here we set the server URL

		try {
			config.setServerURL(new URL(serverURL));

		} catch (MalformedURLException e) {

			e.printStackTrace();
		}

		config.setEnabledForExtensions(true);

		config.setEnabledForExceptions(true);

		// Here we set the username and password
		config.setBasicUserName(userName);
		config.setBasicPassword(password);

		client = new XmlRpcClient();

		client.setConfig(config);

		// In the following we call the methods through dynamic proxy

		factory = new ClientFactory(client);

		dbHanlder = (DBHandler) factory.newInstance(DBHandler.class);

	}

	public String executeQuery(String query) {

		// Here we specify the location of the trust store
		// String trustStore=System.getProperty("user.home") +
		// "/mg_truststore".replace('/', File.separatorChar);

		//System.setProperty("javax.net.ssl.trustStore",
		//		trustStore.replace('/', File.separatorChar));

		String results = "Results of executeQuery()";
		try {
			results = dbHanlder.executeQuery(query);

		} catch (Exception e) {
			results = e.getMessage();
		}

		return results;
	}

}
