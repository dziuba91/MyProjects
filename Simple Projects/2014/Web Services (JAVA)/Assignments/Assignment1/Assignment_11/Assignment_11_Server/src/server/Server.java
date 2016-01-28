//This is server.Server.java file

package server;

import java.io.IOException;

import org.apache.xmlrpc.XmlRpcException;
import org.apache.xmlrpc.server.PropertyHandlerMapping;
import org.apache.xmlrpc.server.XmlRpcServer;
import org.apache.xmlrpc.server.XmlRpcServerConfigImpl;
import org.apache.xmlrpc.webserver.WebServer;

public class Server {

	public static void main(String[] args) {

		if (args.length < 1) {

			System.out.println("Usage: java Server [port]");
			System.exit(-1);

		}

		try {

			WebServer webServer = new WebServer(Integer.parseInt(args[0]));

			XmlRpcServer xmlRpcServer = webServer.getXmlRpcServer();

			// Here we define a mapping of action handlers
			PropertyHandlerMapping phm = new PropertyHandlerMapping();
			phm.addHandler(handlers.ActionHandler.class.getName(),
					handlers.ActionHandler.class);
			phm.addHandler(handlers.IActionHandler.class.getName(),
					handlers.IActionHandler.class);

			// Here we append the handler mapping to the server
			xmlRpcServer.setHandlerMapping(phm);

			XmlRpcServerConfigImpl serverConfig = (XmlRpcServerConfigImpl) xmlRpcServer
					.getConfig();
			serverConfig.setEnabledForExtensions(true);
			serverConfig.setContentLengthOptional(false);

			// Here we start the server using built-in version
			System.out.println("Attempting to start XML-RPC server...");

			// webServer.setParanoid(false);
			webServer.start();

			System.out.println("Server is started at port " + args[0]);

		} catch (XmlRpcException e) {

			System.out.println(e.getMessage());
		} catch (IOException e) {
			System.out.println(e.getMessage());

		}

		System.out
				.println("Now accepting requests. (Terminate the program to stop!)");

	}

}