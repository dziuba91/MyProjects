//This is handlers.DictionaryImpl.java file

package handlers;

import java.util.Hashtable;
import java.util.Locale;
import java.util.Vector;

import java.util.ResourceBundle;

public class DictionaryImpl implements Dictionary {

	public Hashtable<String, String> getWord(Vector<String> word, Locale locale) {

		Hashtable<String, String> wordList = new Hashtable<String, String>();
		/*
		 * Here we create a ResourceBundle object, whose base name is dictionary
		 * 
		 * and resides under WEB-INF/classes/dict/ directory of the web
		 * application.
		 * 
		 * The locale parameter specifies the name of the ResourceBundle object
		 * precisely,
		 * 
		 * like dictionary_fi_FI, or dictionary_en_GB.
		 */

		ResourceBundle resourceBundle = ResourceBundle.getBundle(
				"dict.dictionary", locale);

		/*
		 * Here we get the equivalent of 'word' from the resource bundle file.
		 * Note that we use word.replace(' ', '_') to replace white spaces with
		 * underscore since the keys in the properties file cannot contain white
		 * spaces!
		 */

		String translation;
		
		for(int i=0; i<word.size(); i++)
		{
			try
			{
				translation= resourceBundle.getString(word.get(i).replace(' ', '_'));
			}
			catch (Exception e)
			{
				translation= "";
			}
			
			wordList.put(word.get(i), translation);
		}

		return wordList;

	}
}