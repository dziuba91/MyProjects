//This is server.XmlRpcServlet.java file.

package server;

import SERVICE_handlers.WebService2;

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
import org.apache.xmlrpc.server.PropertyHandlerMapping;
import org.apache.xmlrpc.server.XmlRpcServerConfigImpl;
import org.apache.xmlrpc.server.AbstractReflectiveHandlerMapping.AuthenticationHandler;
import org.apache.xmlrpc.webserver.XmlRpcServletServer;

public class ServiceServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
	private XmlRpcServletServer server = null;
	private XmlRpcServerConfigImpl serverConfig;
	private PropertyHandlerMapping mapping = null;
	// Here we refer to WEB-INF/classes/properties/ directory
	private String propertyFilePath = "properties/";
	private String propertyFile = "servicePropertyHandler.properties";
	private ClassLoader cl = null;

	/*
	 * This is the method which compares the user information received as
	 * parameters with the ones in WEB-INF/classes/users/userinfo.properties
	 * file.
	 */

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

	public void init() {
		WebService2.path= this.getServletContext().getRealPath("/") + "public1/files/";
		
		try {
			server = new XmlRpcServletServer();
			mapping = new PropertyHandlerMapping();
			cl = Thread.currentThread().getContextClassLoader();
			// Here we load the property handler file
			mapping.load(cl, (propertyFilePath + propertyFile));
			/*
			 * Here we set the authentication handler, which requires the
			 * definition of isAuthorized() method.
			 */

			// The following piece of code shows another way of defining
			// an authentication handler and initializing the servlet
			// authentiocan
			// handler.

			/*
			 * class MyAuthHandler implements AuthenticationHandler{
			 * 
			 * private boolean isAuthenticated(String userName, String password)
			 * { String correctPassword =
			 * ResourceBundle.getBundle("users.userinfo").getString(userName);
			 * return correctPassword.equals(password); }
			 * 
			 * 
			 * public boolean isAuthorized(XmlRpcRequest pRequest) {
			 * 
			 * XmlRpcHttpRequestConfig config = (XmlRpcHttpRequestConfig)
			 * pRequest .getConfig();
			 * 
			 * return isAuthenticated(config.getBasicUserName(),
			 * config.getBasicPassword()); } }
			 * 
			 * //Here we instantiate the authentication handler class //and
			 * initialize the authentican handler of the servle //with it.
			 * MyAuthHandler authHandler = new MyAuthHandler();
			 * mapping.setAuthenticationHandler(authHandler);
			 */
			
			
			mapping.setAuthenticationHandler(new AuthenticationHandler() {
				public boolean isAuthorized(XmlRpcRequest pRequest) {

					XmlRpcHttpRequestConfig config = (XmlRpcHttpRequestConfig) pRequest
							.getConfig();

					return isAuthenticated(config.getBasicUserName(),
							config.getBasicPassword()).equals("true");
				}
			});

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