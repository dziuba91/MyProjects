//The following is the content of

// greet.service. SOAPGreetService . java file.

package greet.service;


import java.net.Inet4Address;

import java.net.InetAddress;

import java.net.UnknownHostException;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.DateFormat;

import java.util.Date;
import java.util.Iterator;
import java.util.Vector;

import javax.xml.stream.XMLStreamException;

import org.apache.axiom.om.OMAbstractFactory;
import org.apache.axiom.om.OMNode;

import org.apache.axiom.om.OMElement;

import org.apache.axiom.om.OMFactory;

import org.apache.axiom.om.OMNamespace;

public class SOAPGreetService {

	private static String userName, password, url;
	private Connection connection = null;

	public String connectToDB(String userName, String password) {

		SOAPGreetService.userName = userName;
		SOAPGreetService.password = password;

		String returnValue = "true";

		// Here we load the database driver for Oracle database

		// Class.forName("oracle.jdbc.driver.OracleDriver");

		// For mySQL database the above code would look like this:

		try {
			Class.forName("com.mysql.jdbc.Driver");

			// Here we set the JDBC URL for the Oracle database

			// String url = "jdbc:oracle:thin:@db.cc.puv.fi:1521:ora817";

			// For mySQL database the above code would look like

			// something this.

			// Notice that here we are accessing specified user's db database.

			url = "jdbc:mysql://mysql.cc.puv.fi:3306/" + userName + "_db";

			// Here we create a connection to the database

			// connection = DriverManager.getConnection(url, userName,
			// password);

			// For mg_db mySQL database the above code would look like

			// the following:

			connection = DriverManager.getConnection(url, userName, password);

			if (connection != null)
				connection.close();
			// return connection==null ? "true" : "false";

		} catch (ClassNotFoundException e) {
			returnValue = e.getMessage();
		} catch (SQLException e) {
			returnValue = e.getMessage();
		}

		return returnValue;

	}

	public OMElement executeQuery(OMElement element) {

		// Here we build the xml tree using AXIOM API for the request

		// element element.build();

		// Here we detach the node from its parent container

		element.detach();

		Iterator<OMNode> children = element.getChildren(); // has userName password query
		Integer k = 0;
		
		String userName = null;
		String password = null;
		String query = null;
		
		while (children.hasNext())
		{
			OMElement nextElement = (OMElement)children.next();
			
			if (k == 0)
			{
				userName = nextElement.getText();
			}
			else
			if (k == 1)
			{
				password = nextElement.getText();
			}
			else
			{
				query = nextElement.getText();
			}
			
			k++;
			//nextElement.getLocalName().equals("sortMode"))
		}
		
		connectToDB(userName, password);
		
		/*
		 * 
		 * Here we initialize tools for making the database connection
		 * 
		 * and reading from the database
		 */
		Statement statement = null;
		ResultSet resultSet = null;

		// Here we declare ResultSetMetaData object to get
		// data about the result set
		ResultSetMetaData resultSetMetaData = null;

		String columnHeadings = "", columnValues = "";

		try {

			connection = DriverManager.getConnection(url, userName, password);

			// Here we create the statement object for executing SQL commands.

			statement = connection.createStatement();

			// Here we execute the SQL query and save the results to a ResultSet
			// object

			resultSet = statement.executeQuery(query);

			// Here we get the metadata of the query results

			resultSetMetaData = resultSet.getMetaData();

			// Here we calculate the number of columns

			int columns = resultSetMetaData.getColumnCount();

			// Here we print column names in table header cells

			// Pay attention that the column index starts with 1

			for (int i = 1; i <= columns; i++) {

				columnHeadings += resultSetMetaData.getColumnName(i) + "\t";
			}

			columnHeadings += "\n";

			// for (int i = 1; i <= columns; i++)
			// columnHeadings += "-------";
			//
			// columnHeadings += "\n";

			while (resultSet.next()) {

				// Here we print the value of each column

				for (int i = 1; i <= columns; i++)
					columnValues += resultSet.getObject(i) + "\t";

				columnValues += "\n";

			}

		} catch (Exception ex) {

			//return ex.getMessage();

		} finally {

			try {

				// Here we close all open streams

				if (statement != null)

					statement.close();

				if (connection != null)

					connection.close();

			} catch (SQLException sqlexc) {

				//return sqlexc.getMessage();

			}

		}

		// In the following we build a response.

		OMFactory omFactory = OMAbstractFactory.getOMFactory();

		OMNamespace omNameSpace = omFactory.createOMNamespace(
				"http://service.greet/xsd", "ss");

		OMElement methodElement = omFactory.createOMElement("databaseResponse",
				omNameSpace);

		OMElement responseElement = omFactory.createOMElement("DBdata",
				omNameSpace);

		// Here we add the greeting text to "greeting" element.

		responseElement.addChild(omFactory.createOMText(responseElement,
				columnHeadings + columnValues)); // return table

		methodElement.addChild(responseElement);
		
		return methodElement;

	}
	
	
	public OMElement executeInsertQuery(OMElement element) {

		element.detach();

		Iterator<OMNode> children = element.getChildren(); // has userName password query name comment date
		Integer k = 0;
		
		String userName = null;
		String password = null;
		String query = null;
		String name = null;
		String comment = null;
		String date = null;
		
		while (children.hasNext())
		{
			OMElement nextElement = (OMElement)children.next();
			
			if (k == 0)
			{
				userName = nextElement.getText();
			}
			else
			if (k == 1)
			{
				password = nextElement.getText();
			}
			else
			if (k == 2)
			{
				query = nextElement.getText();
			}
			else
			if (k == 3)
			{
				name = nextElement.getText();
			}
			else
			if (k == 4)
			{
				comment = nextElement.getText();
			}
			else
			{
				date = nextElement.getText();
			}
			
			k++;
			//nextElement.getLocalName().equals("sortMode"))
		}
		
		connectToDB(userName, password);
		
		
		PreparedStatement statement = null;
		Boolean resultSet = null;

		try {
			//Class.forName("com.mysql.jdbc.Driver");
			//url = "jdbc:mysql://mysql.cc.puv.fi:3306/" + userName + "_db";
			connection = DriverManager.getConnection(url, SOAPGreetService.userName, SOAPGreetService.password);

			statement = connection.prepareStatement(query);
			statement.setString(1, name);
			statement.setString(2, comment);
			statement.setString(3, date);
			
			//resultSet = statement.executeQuery(query);
			
			statement.execute();
			
			resultSet = true;

		} catch (Exception ex) {

			resultSet = false;

		} finally {

			try {

				connection.close();

			} catch (SQLException sqlexc) {

				//return false;

			}

		}

		
		OMFactory omFactory = OMAbstractFactory.getOMFactory();

		OMNamespace omNameSpace = omFactory.createOMNamespace(
				"http://service.greet/xsd", "ss2");

		OMElement methodElement = omFactory.createOMElement("databaseResponse2",
				omNameSpace);

		OMElement responseElement = omFactory.createOMElement("DBanswer",
				omNameSpace);

		// Here we add the greeting text to "greeting" element.

		responseElement.addChild(omFactory.createOMText(responseElement,
				resultSet.toString())); // return table

		methodElement.addChild(responseElement);

		return methodElement;

	}

}