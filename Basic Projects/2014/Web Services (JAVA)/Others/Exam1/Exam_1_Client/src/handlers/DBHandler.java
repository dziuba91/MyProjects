

package handlers;


public interface DBHandler {
public String executeQuery(String query);
public Boolean executeInsertQuery(String query, String name, String comment, String date);
public String connectToDB(String userName, String password);
}
