//This is handlers.Dictionary.java file

package handlers;

import java.util.Hashtable;
import java.util.Locale;
import java.util.Vector;

public interface Dictionary {

	public Hashtable<String, String> getWord(Vector<String> word, Locale locale);

}