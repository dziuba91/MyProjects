//This is servlet.ServletServer.java file.

package server;

import org.apache.xmlrpc.webserver.ServletWebServer;

import org.apache.xmlrpc.webserver.XmlRpcServlet;

public class ServletServer {

	// Here we define the port number as a constant

	private static final int port = 9500;

	public static void main(String[] args) throws Exception {

		// Here we create an instance of XmlRpcServlet.

		XmlRpcServlet servlet = new XmlRpcServlet();

		// Here we define an instance of ServletWebServer, which requires an

		// instance of XmlRpcServlet and the port number as parameters.

		ServletWebServer webServer = new ServletWebServer(servlet, port);

		System.out.println("Trying to start the web server...");

		// Here we start the webServer.

		webServer.start();

		System.out
				.println("The webserver has been successfully started at port "
						+ port);

	}

}