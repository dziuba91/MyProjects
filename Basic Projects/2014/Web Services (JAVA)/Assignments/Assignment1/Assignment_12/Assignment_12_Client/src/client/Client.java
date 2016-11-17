//This is client.Client.java file

package client;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.URL;
import java.util.Scanner;

import org.apache.xmlrpc.client.XmlRpcClient;
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;
import org.apache.xmlrpc.client.XmlRpcCommonsTransportFactory;

public class Client {

	public static void main(String[] args) {

		if (args.length < 2) {

			System.out.println("Usage: java Client [server] [port]");
			System.exit(-1);

		}
		double area = 0.0;

		XmlRpcClientConfigImpl config = new XmlRpcClientConfigImpl();
		try {

			config.setServerURL(new URL("http://" + args[0] + ":"
					+ Integer.parseInt(args[1]) + "/"));

			XmlRpcClient client = new XmlRpcClient();
			client.setTransportFactory(new XmlRpcCommonsTransportFactory(client));

			client.setConfig(config);

			double radius = 0.0;

			System.out.println("Please type circle radius: ");

			/*
			 * BufferedReader in = new BufferedReader(new InputStreamReader(
			 * System.in)); try {
			 * 
			 * radius = Double.parseDouble(in.readLine());
			 * 
			 * } catch (Exception e) {
			 * 
			 * System.out.println(e.getMessage()); }
			 */

			Scanner scanner = new Scanner(System.in);

			radius = scanner.nextDouble();

			System.out.println(radius);

			Object[] params = new Object[] { new Double(radius) };

			area = (Double) client.execute("action.circleArea", params);

			System.out.println("The circle area is: " + area);

			System.out.println("Please type rectangle length and width: ");

			double length = 0.0, width = 0.0;

			System.out.print("Length: ");

			length = scanner.nextDouble();

			System.out.print("Width: ");
			width = scanner.nextDouble();

			/*
			 * try {
			 * 
			 * System.out.print("Length: "); length =
			 * Double.parseDouble(in.readLine());
			 * 
			 * System.out.print("Width: "); width =
			 * Double.parseDouble(in.readLine());
			 * 
			 * } catch (Exception e) {
			 * 
			 * System.out.println(e.getMessage()); }
			 */
			params = new Object[] { new Double(length), new Double(width) };

			area = (Double) client.execute("action.rectangleArea", params);

			System.out.println("The rectangle area is: " + area);

			System.out
					.println("Please type triangle edges and the angle betwwn them in degrees: ");

			double edge1 = 0.0, edge2 = 0.0;
			int angle = 0;

			System.out.print("Edge1: ");
			edge1 = scanner.nextDouble();

			System.out.print("Edge2: ");
			edge2 = scanner.nextDouble();

			System.out.print("Angle in degrees: ");
			angle = scanner.nextInt();

			/*
			 * try { System.out.print("Edge1: "); edge1 =
			 * Double.parseDouble(in.readLine());
			 * 
			 * System.out.print("Edge2: "); edge2 =
			 * Double.parseDouble(in.readLine());
			 * 
			 * System.out.print("Angle in degrees: "); angle = (int)
			 * Double.parseDouble(in.readLine());
			 * 
			 * } catch (Exception e) {
			 * 
			 * System.out.println(e.getMessage()); }
			 */

			params = new Object[] { new Double(edge1), new Double(edge2),
					new Integer(angle) };

			area = (Double) client.execute("action.triangleArea", params);

			System.out.println("The triangle area is: " + area);

			//
			System.out
					.println("Please type the radius of the ball in degrees: ");

			radius = 0.0;

			System.out.print("Radius of the ball : ");
			radius = scanner.nextDouble();

			params = new Object[] { new Double(radius) };

			area = (Double) client.execute("action.ballVolume", params);

			System.out.println("The volume of a ball is: " + area);

		} catch (Exception e) {
			System.out.println("Exception: " + e.getMessage());
		}
	}
}