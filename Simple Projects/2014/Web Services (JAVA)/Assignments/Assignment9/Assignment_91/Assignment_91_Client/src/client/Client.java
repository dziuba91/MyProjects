package client;

import java.util.Iterator;

import javax.xml.namespace.QName;

import org.apache.axiom.om.OMAbstractFactory;
import org.apache.axiom.om.OMNode;

import org.apache.axiom.om.OMElement;

import org.apache.axiom.om.OMFactory;

import org.apache.axiom.om.OMNamespace;

import org.apache.axis2.AxisFault;

import org.apache.axis2.addressing.EndpointReference;

import org.apache.axis2.client.Options;

import org.apache.axis2.client.ServiceClient;

public class Client {

	public static void main(String[] args) throws AxisFault {

		// Here we set the service address

		EndpointReference targetEPR = new EndpointReference(
				"http://app3.cc.puv.fi/axis2/services/e1401209_as10_1_3");

		Options options = new Options();

		options.setTo(targetEPR);

		ServiceClient client = new ServiceClient();

		client.setOptions(options);

		Integer lower = 1;
		Integer upper = 30;

		OMFactory omFactory = OMAbstractFactory.getOMFactory();

		OMNamespace omNameSpace = omFactory.createOMNamespace(
				"http://service.greet/xsd", "prime_numbers");

		OMElement methodElement = omFactory.createOMElement("prime_numbers",
				omNameSpace);

		OMElement argumentElement1 = omFactory.createOMElement("lowerBound",
				omNameSpace);
		
		argumentElement1.setText(lower.toString());

		methodElement.addChild(argumentElement1);

		
		OMElement argumentElement2 = omFactory.createOMElement("upperBound",
				omNameSpace);

		argumentElement2.setText(upper.toString());

		methodElement.addChild(argumentElement2);
		
		
		/*
		Iterator<OMNode> children = methodElement.getChildren();

		while (children.hasNext())

			System.out.println(children.next());
		 * */
		
		/*
		OMElement firstElement = methodElement;
		
		String nameSpaceURI = firstElement.getQName().getNamespaceURI();
		
		Iterator<OMNode> iterator = firstElement.getChildrenWithName(new QName(
				nameSpaceURI, "lowerBound"));
		
		Integer lower1 = null;
		if (iterator.hasNext())
		{
			lower1 = Integer.parseInt(((OMElement) iterator.next()).getText());
			System.out.println(lower1 + "");
		}
		else
			System.out.println("False");
		 * */
		
		
		OMElement responseElement = client.sendReceive(methodElement);

		//String response = responseElement.getFirstElement().getText();

		//System.out.println(response);

		
		
		System.out.println("Prime numbers from " + lower + " to " + upper + " :");
		
		Iterator<OMNode> children1 = responseElement.getChildren();

		boolean firstElementToPrint = true;
		while (children1.hasNext())
		{
			if (firstElementToPrint)
				firstElementToPrint = false;
			else
				System.out.print(", ");
			
			System.out.print(((OMElement)children1.next()).getText());
		}
			
	
	}
}