//This is handlers.DictionaryImpl.java file

package SERVICE_handlers;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.Locale;

import java.util.ResourceBundle;

public class WebService2 implements IWebService2 {

	public static String path ;

	public byte[] downloadFile(String fileName) {
		
		byte [] fileContent = null;
		
		try {
			File file=new File(path + fileName);
			
			FileInputStream fileInputStream = new FileInputStream(file.getPath());
			
			fileContent= new byte[(int)file.length()];

			fileInputStream.read(fileContent);
			
			fileInputStream.close();
	
		} catch (FileNotFoundException e) {
			return e.getMessage().getBytes();
		} catch (IOException e) {
			return e.getMessage().getBytes();
		}
		
		return fileContent;
	}
}