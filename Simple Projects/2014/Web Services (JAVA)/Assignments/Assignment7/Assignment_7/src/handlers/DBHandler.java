//This is handlers.DBHandler.java file.

package handlers;

public interface DBHandler {
public String executeQuery(String query);
public Boolean executeInsertQuery(String query, Integer id, String name, Float price);
public Boolean executeDeleteQuery(String query, Integer id);
}