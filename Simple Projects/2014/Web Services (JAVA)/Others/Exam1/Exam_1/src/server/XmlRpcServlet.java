//This is server.XmlRpcServlet.java file.

package server;

import handlers.DBHandlerImpl;

import java.io.IOException;
import java.io.Writer;
import java.util.ResourceBundle;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.xmlrpc.XmlRpcException;
import org.apache.xmlrpc.XmlRpcRequest;
import org.apache.xmlrpc.common.XmlRpcHttpRequestConfig;
import org.apache.xmlrpc.server.AbstractReflectiveHandlerMapping.AuthenticationHandler;
import org.apache.xmlrpc.server.PropertyHandlerMapping;
import org.apache.xmlrpc.server.XmlRpcServerConfigImpl;
import org.apache.xmlrpc.webserver.XmlRpcServletServer;

public class XmlRpcServlet extends HttpServlet {

	private static final long serialVersionUID = 1L;
	private XmlRpcServletServer server = null;
	private XmlRpcServerConfigImpl serverConfig;
	private PropertyHandlerMapping mapping = null;
	// Here we refer to WEB-INF/classes/properties/ directory
	private String propertyFilePath = "properties/";
	private String propertyFile = "propertyHandler.properties";
	private ClassLoader cl = null;
	DBHandlerImpl dbHandler = null;

	/*
	private String isAuthenticated(String userName, String password) {

		String result = "";

		try {
			ResourceBundle resourceBundle = ResourceBundle
					.getBundle("users.userinfo");
			result = "" + resourceBundle.getString(userName).equals(password);
		} catch (Exception e) {
			result = e.getMessage();
		}

		return result;
	}
	*/
	
	public void init() {
		
		try {
			server = new XmlRpcServletServer();

			mapping = new PropertyHandlerMapping();

			cl = Thread.currentThread().getContextClassLoader();

			// Here we load the property handler file

			mapping.load(cl, (propertyFilePath + propertyFile));

			server.setHandlerMapping(mapping);

			serverConfig = (XmlRpcServerConfigImpl) server.getConfig();

			serverConfig.setEnabledForExceptions(true);

			serverConfig.setEnabledForExtensions(true);

			server.setConfig(serverConfig);
			
			dbHandler = new DBHandlerImpl();
			dbHandler.connectToDB("e1401209","fDD6H8shxWyQ");

		} catch (XmlRpcException e) {
			e.getMessage();
		} catch (IOException e) {
			e.getMessage();
		}

	}

	protected void doGet(HttpServletRequest request,
			HttpServletResponse response) throws ServletException, IOException {
		Writer out = response.getWriter();
		response.setContentType("text/html");
		out.write("<h2>Warning</h2>");
		out.write("<p>This servlet handles only HTTP POST requests</p>");
		out.close();

	}

	protected void doPost(HttpServletRequest request,
			HttpServletResponse response) throws ServletException, IOException {
		// Here we call the XmlRpcServletServer to execute the request.
		server.execute(request, response);

	}

}