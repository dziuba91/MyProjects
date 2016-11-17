package handlers;

public interface IWebService {

	byte[] downloadFile(String fileName);
	String uploadFile(String fileName, byte[] fileConTent);
	String getImageList();
}
