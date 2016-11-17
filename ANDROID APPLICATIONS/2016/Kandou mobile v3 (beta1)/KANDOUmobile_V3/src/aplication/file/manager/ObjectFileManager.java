package aplication.file.manager;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.io.UnsupportedEncodingException;

import android.os.Environment;
import aplication.data.VocabularyList_array;

public class ObjectFileManager { // read from (save to) file (all methods)
	
	public void saveObjectArray(String object1, String path)
	{
		byte [] arr = object1.getBytes();
		
		try {
			
			@SuppressWarnings("resource")
			FileOutputStream fileOutputStream = new FileOutputStream(pathBuilder(path));
			
			fileOutputStream.write(arr);
			
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	public Boolean [] readObjectFromFile_Boolean(String path)
	{
		byte [] array = null;
		
		try {
			File f1 = new File(pathBuilder(path));
			array = new byte[(int) f1.length()];
			
			@SuppressWarnings("resource")
			FileInputStream fileInputStream = new FileInputStream(f1);
			
			if (fileInputStream != null) fileInputStream.read(array);
			
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		String ret1 = "";
		try {
			ret1 = new String(array, "UTF-8");
		} catch (UnsupportedEncodingException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		String [] ret2 = ret1.split("-");
		
		Boolean [] ret3 = new Boolean[ret2.length];
		for (int i=0; i < ret2.length; i++) ret3[i] = Boolean.parseBoolean(ret2[i]);
		
		return ret3;
	}
	
	public VocabularyList_array readObjectFromFile(String path)
	{
		VocabularyList_array getClass = null;
		
		try {
			File f1 = new File(pathBuilder(path));
			
			FileInputStream fileInputStream = new FileInputStream(f1);
			ObjectInputStream is = new ObjectInputStream(fileInputStream);
			
			getClass = (VocabularyList_array) is.readObject();
			
			is.close();
			fileInputStream.close();
			
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return getClass;
	}
	
	public void saveObject(VocabularyList_array object1, String path)
	{
		try {
			FileOutputStream fileOutputStream = new FileOutputStream(pathBuilder(path));
			
			ObjectOutputStream os = new ObjectOutputStream(fileOutputStream);
			os.writeObject(object1);
			
			os.close();
			fileOutputStream.close();
			
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	private String pathBuilder(String path)
	{
		String sdCardPath = Environment.getExternalStorageDirectory().getPath();
		File f1 = new File(sdCardPath + "/KandouData_V3/" + path);
		
		return f1.getPath();
	}
}