//This is handlers. ServiceHandlerImpl.java file

package handlers;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.Arrays;
import java.util.Hashtable;
import java.util.Collections;

public class ServiceHandlerImpl implements ServiceHandler {

	public Hashtable<String,Integer> getFileLength() {

		Hashtable<String,Integer> hashTable = new Hashtable<String,Integer>();
		
		try {
			File folder = new File(path);

			File[] fileList = folder.listFiles();
		
			//StringBuffer files = new StringBuffer();
		
			for(File f : fileList)
			{
				hashTable.put(f.getName(), (int)f.length());
			}
				//files.append(f + "\n");
		} catch (Exception e) {
			hashTable.put(e.getMessage(), 0);
		}
		
		return hashTable;

	}

}