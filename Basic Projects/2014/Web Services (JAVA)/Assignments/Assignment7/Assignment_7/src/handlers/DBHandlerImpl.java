//This is handlers.DBHandlerImpl.java file.

package handlers;

import java.sql.Connection;
import java.sql.PreparedStatement;

import java.sql.DriverManager;

import java.sql.ResultSet;

import java.sql.ResultSetMetaData;

import java.sql.SQLException;

import java.sql.Statement;

public class DBHandlerImpl implements DBHandler {

	private static String userName, password, url;
	private Connection connection = null;

	public String connectToDB(String userName, String password) {

		DBHandlerImpl.userName = userName;
		DBHandlerImpl.password = password;

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

	public String executeQuery(String query) {

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

			return ex.getMessage();

		} finally {

			try {

				// Here we close all open streams

				if (statement != null)

					statement.close();

				if (connection != null)

					connection.close();

			} catch (SQLException sqlexc) {

				return sqlexc.getMessage();

			}

		}

		return columnHeadings + columnValues;

	}
	
	
	public Boolean executeInsertQuery(String query, Integer id, String name, Float price) {

		PreparedStatement statement = null;
		Boolean resultSet = null;

		try {

			connection = DriverManager.getConnection(url, userName, password);

			statement = connection.prepareStatement(query);
			statement.setInt(1, id);
			statement.setString(2, name);
			statement.setFloat(3, price);
			
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

		return resultSet;

	}
	
	public Boolean executeDeleteQuery(String query, Integer id) {

		PreparedStatement statement = null;
		Boolean resultSet = null;

		try {

			connection = DriverManager.getConnection(url, userName, password);

			statement = connection.prepareStatement(query);
			statement.setInt(1, id);
			
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

		return resultSet;

	}

}