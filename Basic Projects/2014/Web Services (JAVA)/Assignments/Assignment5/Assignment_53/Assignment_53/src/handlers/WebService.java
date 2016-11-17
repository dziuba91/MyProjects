package handlers;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.Hashtable;
import java.util.Vector;

import javax.activation.MimetypesFileTypeMap;
import javax.imageio.ImageIO;

public class WebService implements IWebService {
	
	public static String path ;
	
	@Override
	public Hashtable<String, byte[]> downloadFile(Vector<String> fileName) {
		
		Hashtable<String, byte[]> content = new Hashtable<String, byte[]>();
		
		byte [] fileContent = null;
		
		for (int i=0; i<fileName.size(); i++)
			try {
				File file=new File(path + fileName.get(i));
			
				FileInputStream fileInputStream = new FileInputStream(file.getPath());
			
				fileContent= new byte[(int)file.length()];

				fileInputStream.read(fileContent);
			
				fileInputStream.close();
				
				content.put(fileName.get(i), fileContent);
	
			} 
			catch (FileNotFoundException e) {
				content.put(fileName.get(i), e.getMessage().getBytes());
			} 
			catch (IOException e) {
				content.put(fileName.get(i), e.getMessage().getBytes());
			}
		
		return content;
	}

	@Override
	public String uploadFile(Hashtable<String, byte[]> file) {
		
		File destinationFile;
		String ret = "";
		
		//for(int i=0; i<file.size(); i++)
		//{
		Enumeration<String> out = file.keys();
		String fileName;
		byte [] fileContent;
		int i = 0;
		while (out.hasMoreElements()) {

			fileName = out.nextElement();
			fileContent = file.get(fileName);
			
				try {
					destinationFile=new File(path + fileName);
					FileOutputStream fileOutputStream = new FileOutputStream(destinationFile);
			
					fileOutputStream.write(fileContent);
			
					fileOutputStream.close();
			
				} catch (FileNotFoundException e) {
					return e.getLocalizedMessage();
			
				} catch (IOException e) {
					return e.getLocalizedMessage();
			}
			
			ret += destinationFile.getAbsolutePath() + "  exists? " + destinationFile.exists() + "\n";
		}
		
		return ret;
	}

	@Override
	public String getFileList() {
		File folder = new File(path);
		File[] fileList = folder.listFiles();
		
		StringBuffer files = new StringBuffer();
		files.append("\n");
				
		for(File f : fileList)
		{
			files.append(f.getName() + "\n");
		}
		
		return files.toString();
	}

}
