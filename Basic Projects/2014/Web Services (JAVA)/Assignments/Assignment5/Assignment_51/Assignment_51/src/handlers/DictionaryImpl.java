//This is handlers.DictionaryImpl.java file

package handlers;

import java.util.Locale;

import java.util.ResourceBundle;

public class DictionaryImpl implements Dictionary {

	public String getWord(String word, Locale locale) {

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

		String translation = resourceBundle.getString(word.replace(' ', '_'));

		return translation;

	}

}