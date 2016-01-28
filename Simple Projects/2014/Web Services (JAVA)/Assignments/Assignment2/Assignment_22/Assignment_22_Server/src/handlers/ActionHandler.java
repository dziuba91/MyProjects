

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

	public Hashtable<String, Integer> search(String word) {

		Hashtable<String, Integer> wordList = new Hashtable<String, Integer>();

		try {
			
			File folder = new File(path);

			String[] fileList = folder.list();
			
			for(String f : fileList)
			{
				Integer a = 0;
				
				BufferedReader reader = new BufferedReader(new FileReader(path + f));

				String fileContent = "";

				String line;
				
				while ((line = reader.readLine()) != null)
					fileContent += line + "\n";

				StringTokenizer tokenizer = new StringTokenizer(fileContent,
					" \t\n\r\f.");
				
				String fileWord = "";
				while (tokenizer.hasMoreElements()) {
					fileWord = tokenizer.nextElement().toString();
					
					if (fileWord.toUpperCase().equals(word.toUpperCase())) a++;
				}

				wordList.put(f, a);
			}
		} catch (FileNotFoundException e) {
			wordList.put(e.getMessage(), 0);
		} catch (IOException e) {
			wordList.put(e.getMessage(), 0);
		}

		return wordList;

	}

}
