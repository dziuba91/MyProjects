//This is handlers.IActionHandler.java file

package handlers;

public interface IActionHandler {
	
	final String path="public1/files/";

public String getFileList2();

public String ReadFromFile(String fileName);

public String WriteToFile(String fileName, String text);

}