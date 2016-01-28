//This is handlers.IActionHandler.java file

package handlers;

import java.util.Hashtable;

public interface IActionHandler {
	
	final String path="public1/files/";

public Hashtable<String, Integer> search(String word);

}