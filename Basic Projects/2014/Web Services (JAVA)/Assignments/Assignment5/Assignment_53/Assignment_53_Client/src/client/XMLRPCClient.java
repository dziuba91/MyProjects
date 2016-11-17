
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
import java.util.Hashtable;
import java.util.StringTokenizer;

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
					"http://app3.cc.puv.fi/e1401209_as53_2/myxmlrpc"));
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
	
	public String getFileList()
	{
		return myService.getFileList();
	}
	

    public Hashtable<String, byte[]> ReadFromFile(String fileName){
		
    	Hashtable<String, byte[]> files = new Hashtable<String, byte[]>();
    	
		byte [] fileContent = null;
		
		StringTokenizer tokenizer = new StringTokenizer(fileName, "\n");
		while (tokenizer.hasMoreElements()) {
			//v.add(tokenizer.nextElement().toString());
			
			try {
				File file=new File(tokenizer.nextElement().toString());
			
				FileInputStream fileInputStream = new FileInputStream(file.getPath());
			
				fileContent= new byte[(int)file.length()];

				fileInputStream.read(fileContent);
			
				fileInputStream.close();
				
				files.put(file.getName(), fileContent);
			} catch (FileNotFoundException e) {
				//return e.getMessage().getBytes();
				files.put(tokenizer.nextElement().toString(), e.getMessage().getBytes());
			} catch (IOException e) {
				//return e.getMessage().getBytes();
				files.put(tokenizer.nextElement().toString(), e.getMessage().getBytes());
			}
		}
		
		return files;
	}
}
