

//This is handlers.ActionHandler.java file

package handlers;

import java.io.File;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.Hashtable;
import java.util.StringTokenizer;

public class ActionHandler implements IActionHandler {

public String getFileList2(){

	File folder = new File(path);

	String[] fileList = folder.list();
	
	StringBuffer files = new StringBuffer();
	
	for(String f : fileList)
		files.append(f + "\n");

	return files.toString();
}

public String ReadFromFile(String fileName){
	
	String fileText = "";
	
	try {
		BufferedReader reader = new BufferedReader(new FileReader(path + fileName));
		
		String line;

		while ((line = reader.readLine()) != null)
			fileText += line + "\n";
		
		reader.close();
		
	} catch (FileNotFoundException e) {
		fileText = e.getMessage();
	} catch (IOException e) {
		fileText  = e.getMessage();
	}
	
	return fileText;
}

public String WriteToFile(String fileName, String text){
	
	try {
		//FileWriter fw = new FileWriter(file.getAbsoluteFile());
		//BufferedWriter bw = new BufferedWriter(new FileWriter(new File(path + fileName).getAbsoluteFile()));
		String fileText = "";
		BufferedReader reader = new BufferedReader(new FileReader(path + fileName));
		
		String line;

		while ((line = reader.readLine()) != null)
			fileText += line + "\n";
		
		//fileText += "\n";
		
		
		BufferedWriter writer = new BufferedWriter(new FileWriter(path + fileName));
		
		writer.write(fileText);
		//writer.newLine();
		//writer.newLine();
		writer.write(text);
		writer.close();
	
	} catch (FileNotFoundException e) {
		return e.getMessage();
	} catch (IOException e) {
		return e.getMessage();
	}
	
	return "Text was successfully written to file!";
}

}
