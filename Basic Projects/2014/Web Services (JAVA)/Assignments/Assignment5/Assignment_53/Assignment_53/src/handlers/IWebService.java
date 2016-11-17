package handlers;

import java.util.Hashtable;
import java.util.Vector;

public interface IWebService {

	Hashtable<String, byte[]> downloadFile(Vector<String> fileName);
	String uploadFile(Hashtable<String, byte[]> file);
	String getFileList();
}
