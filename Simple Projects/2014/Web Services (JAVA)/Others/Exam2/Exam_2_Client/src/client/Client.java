
package client;

import org.apache.axiom.om.OMAbstractFactory;

import org.apache.axiom.om.OMElement;

import org.apache.axiom.om.OMFactory;

import org.apache.axiom.om.OMNamespace;

import org.apache.axis2.AxisFault;

import org.apache.axis2.addressing.EndpointReference;

import org.apache.axis2.client.Options;

import org.apache.axis2.client.ServiceClient;

public class Client {

	static ServiceClient client = null;
	
	static String userName = "e1401209";
	static String password = "fDD6H8shxWyQ";
	
	//public static void main(String[] args) throws AxisFault {
	public Client() {

		// Here we set the service address

		EndpointReference targetEPR = new EndpointReference(
				"http://app3.cc.puv.fi/axis2/services/e1401209_exam2_6");

		Options options = new Options();

		options.setTo(targetEPR);

		try {
			
			client = new ServiceClient();
			
		} catch (AxisFault e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		client.setOptions(options);
		
		// test
		System.out.println(executeQuery("select * from guestbook"));

	}
	
	public static String executeQuery(String query) {


		String results = "Results of executeQuery()";
		try {
			OMFactory omFactory = OMAbstractFactory.getOMFactory();

			OMNamespace omNameSpace = omFactory.createOMNamespace(
					"http://service.greet/xsd", "executeQuery");

			OMElement methodElement = omFactory.createOMElement("executeQuery",
					omNameSpace);
			
			// username
			OMElement argumentElement = omFactory.createOMElement("userName",
					omNameSpace);

			argumentElement.setText(userName);
			
			methodElement.addChild(argumentElement);
			
			// password
			argumentElement = omFactory.createOMElement("password",
					omNameSpace);

			argumentElement.setText(password);
			
			methodElement.addChild(argumentElement);
			
			// query
			argumentElement = omFactory.createOMElement("query",
					omNameSpace);

			argumentElement.setText(query);
			
			methodElement.addChild(argumentElement);

			OMElement responseElement = client.sendReceive(methodElement);

			String response = responseElement.getFirstElement().getText();
			
			results = response;

		} catch (Exception e) {
			results = e.getMessage();
		}

		return results;
	}
	
	
	public String executeInsertQuery(String query, String ar1, String ar2, String ar3) {

		String results = "Results of executeQuery()";
		try {
			OMFactory omFactory = OMAbstractFactory.getOMFactory();

			OMNamespace omNameSpace = omFactory.createOMNamespace(
					"http://service.greet/xsd", "executeInsertQuery");

			OMElement methodElement = omFactory.createOMElement("executeInsertQuery",
					omNameSpace);
			
			// username
			OMElement argumentElement = omFactory.createOMElement("userName",
					omNameSpace);

			argumentElement.setText(userName);
			
			methodElement.addChild(argumentElement);
			
			// password
			argumentElement = omFactory.createOMElement("password",
					omNameSpace);

			argumentElement.setText(password);
			
			methodElement.addChild(argumentElement);
			
			// query
			argumentElement = omFactory.createOMElement("query",
					omNameSpace);

			argumentElement.setText(query);
			
			methodElement.addChild(argumentElement);
			
			// ar1
			argumentElement = omFactory.createOMElement("argument1",
					omNameSpace);

			argumentElement.setText(ar1);
			
			// ar2
			argumentElement = omFactory.createOMElement("argument2",
					omNameSpace);

			argumentElement.setText(ar2);
			
			// ar3
			argumentElement = omFactory.createOMElement("argument3",
					omNameSpace);

			argumentElement.setText(ar3);
			
			methodElement.addChild(argumentElement);

			OMElement responseElement = client.sendReceive(methodElement);

			String response = responseElement.getFirstElement().getText();
			
			results = response;

		} catch (Exception e) {
			results = e.getMessage();
		}

		return results;
	}
}
