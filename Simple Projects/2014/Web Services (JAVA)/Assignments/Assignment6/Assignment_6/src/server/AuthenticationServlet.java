//This is server.XmlRpcServlet.java file.

package server;

import java.io.IOException;
import java.io.Writer;

import javax.servlet.ServletException;

import javax.servlet.http.HttpServlet;

import javax.servlet.http.HttpServletRequest;

import javax.servlet.http.HttpServletResponse;

import org.apache.xmlrpc.XmlRpcException;

import org.apache.xmlrpc.server.PropertyHandlerMapping;

import org.apache.xmlrpc.server.XmlRpcServerConfigImpl;

import org.apache.xmlrpc.webserver.XmlRpcServletServer;

public class AuthenticationServlet extends HttpServlet {

	private static final long serialVersionUID = 1L;

	private XmlRpcServletServer server = null;

	private XmlRpcServerConfigImpl serverConfig;

	private PropertyHandlerMapping mapping = null;

	// Here we refer to WEB-INF/classes/properties/ directory

	private String propertyFilePath = "properties/";

	private String propertyFile = "basicPropertyHandler.properties";

	private ClassLoader cl = null;

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

		} catch (XmlRpcException e) {

			e.getMessage();

		} catch (IOException e) {

			e.getMessage();

		}

	}

	protected void doGet(HttpServletRequest request,

	HttpServletResponse response) throws ServletException, IOException {

		Writer out = response.getWriter();
		out.write("<html><head><title>XMLRPC Servlet Response</title></head></body>");
		out.write("<h2>Info</h2><p>This XMLRPC servlet requires POST request</p>");
		out.write("</body></html>");

		out.close();

	}

	protected void doPost(HttpServletRequest request,

	HttpServletResponse response) throws ServletException, IOException {

		// Here we call the XmlRpcServletServer to execute the request.

		server.execute(request, response);

	}

}