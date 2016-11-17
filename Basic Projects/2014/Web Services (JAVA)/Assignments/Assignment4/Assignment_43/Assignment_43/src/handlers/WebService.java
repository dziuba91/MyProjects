package handlers;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.ArrayList;

import javax.activation.MimetypesFileTypeMap;
import javax.imageio.ImageIO;

public class WebService implements IWebService {
	
	public static String path ;
	
	@Override
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

	@Override
	public String uploadFile(String fileName, byte[] fileContent) {
		
		File destinationFile;
		
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
		
		  return destinationFile.getAbsolutePath() + "  exists? " + destinationFile.exists();
	}

	@Override
	public String getImageList() {
		File folder = new File(path);
		File[] fileList = folder.listFiles();
		
		StringBuffer files = new StringBuffer();
		files.append("\n");
				
		for(File f : fileList)
		{
			try {
				if(null != ImageIO.read(f)){
				     files.append(f.getName() + "\n");
				}
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		
		return files.toString();
	}

}
