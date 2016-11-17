
//This is client.Client.java file

package client;

import handlers.IWebService;

import java.awt.image.BufferedImage;
import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;

import javax.imageio.ImageIO;

import org.apache.xmlrpc.client.XmlRpcClient;
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;
import org.apache.xmlrpc.client.util.ClientFactory;

public class XMLRPCClient {

	String path = "public_client/files/";
	
	XmlRpcClientConfigImpl config = null;
	XmlRpcClient client = null;
	ClientFactory factory = null;
	IWebService myService = null;
	
	public byte arr [] = null;
    
	public XMLRPCClient() {

		try {

			config = new XmlRpcClientConfigImpl();

			config.setServerURL(new URL(
					"http://app3.cc.puv.fi/e1401209_as43_12/myxmlrpc"));
		}
		catch (Exception e)
		{
			System.out.println("Exception: " + e.getMessage());
		}
		
		config.setEnabledForExtensions(true);

		config.setEnabledForExceptions(true);

		client = new XmlRpcClient();

		client.setConfig(config);
		
		
		factory = new ClientFactory(client);

		myService = (IWebService) factory
				.newInstance(IWebService.class);
	}
	
	public BufferedImage getImage(String fileName){
	
		BufferedImage imag = null;
		
		try {
			byte [] arr = myService.downloadFile(fileName);
			
			if (arr != null)
				imag=ImageIO.read(new ByteArrayInputStream(arr));
			
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return imag;
	}
	
	public String getImageList(){
		
		return myService.getImageList();
	}

	/*
    public byte [] ReadFromFile(String fileName){
		
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
	}*/
}
