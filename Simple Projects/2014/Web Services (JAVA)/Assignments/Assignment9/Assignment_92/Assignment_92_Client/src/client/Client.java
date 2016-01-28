package client;

import java.util.Iterator;
import java.util.Vector;

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
				"http://app3.cc.puv.fi/axis2/services/e1401209_as10_2_10");

		Options options = new Options();

		options.setTo(targetEPR);

		ServiceClient client = new ServiceClient();

		client.setOptions(options);

		String sortMode = "ascending";

		OMFactory omFactory = OMAbstractFactory.getOMFactory();

		OMNamespace omNameSpace = omFactory.createOMNamespace(
				"http://service.greet/xsd", "OMEsort_numbers");

		OMElement methodElement = omFactory.createOMElement("OMEsort_numbers",
				omNameSpace);

		OMElement argumentElement = omFactory.createOMElement("sortMode",
				omNameSpace);
		
		argumentElement.setText(sortMode);
		
		methodElement.addChild(argumentElement);

		
		Vector<Integer> values = new Vector<Integer>();
		values.add(2);
		values.add(1);
		values.add(8);
		values.add(6);
		values.add(10);
		values.add(5);
		
		for (int i = 0; i < values.size(); i++)
		{
			OMElement argumentElementList = omFactory.createOMElement("value",
					omNameSpace);
		
			argumentElementList.addAttribute("index", i + "", omNameSpace);
			
			argumentElementList.setText(values.get(i).toString());
		
			methodElement.addChild(argumentElementList);
		}
		
		/*
		Iterator<OMNode> children = methodElement.getChildren();

		while (children.hasNext())

			System.out.println(children.next());
		 * */

		System.out.println("Numbers before sorting: ");
		
		boolean firstElementToPrint = true;
		for(int i=0; i<values.size(); i++)
		{
			if (firstElementToPrint)
				firstElementToPrint = false;
			else
				System.out.print(", ");
			
			System.out.print(values.get(i));
		}
		
		System.out.println();
		System.out.println();
		
		
		System.out.println("Numbers sorted in " + sortMode + " mode: ");
		
		OMElement responseElement = client.sendReceive(methodElement);
		
		Iterator<OMNode> children = responseElement.getChildren();

		firstElementToPrint = true;
		while (children.hasNext())
		{
			if (firstElementToPrint)
				firstElementToPrint = false;
			else
				System.out.print(", ");
			
			System.out.print(((OMElement)children.next()).getText());
		}
			
		System.out.println();
		System.out.println();
		
		
		sortMode = "descending";
		methodElement = omFactory.createOMElement("OMEsort_numbers",
				omNameSpace);

		argumentElement = omFactory.createOMElement("sortMode",
				omNameSpace);
		
		argumentElement.setText(sortMode);
		
		methodElement.addChild(argumentElement);
		
		for (int i = 0; i < values.size(); i++)
		{
			OMElement argumentElementList = omFactory.createOMElement("value",
					omNameSpace);
		
			argumentElementList.addAttribute("index", i + "", omNameSpace);
			
			argumentElementList.setText(values.get(i).toString());
		
			methodElement.addChild(argumentElementList);
		}
		
		
		System.out.println("Numbers sorted in " + sortMode + " mode: ");
		
		responseElement = client.sendReceive(methodElement);
		
		children = responseElement.getChildren();

		firstElementToPrint = true;
		while (children.hasNext())
		{
			if (firstElementToPrint)
				firstElementToPrint = false;
			else
				System.out.print(", ");
			
			System.out.print(((OMElement)children.next()).getText());
		}
		
		
		System.out.println();
		
		/*  // testing
		System.out.println();
		
		children = methodElement.getChildren();
		while (children.hasNext())
			System.out.println(children.next());
		
		System.out.println();
		
		children = responseElement.getChildren();
		while (children.hasNext())
			System.out.println(children.next());
		
		children = methodElement.getChildren();
		while (children.hasNext())
			System.out.println(((OMElement)children.next()).getLocalName().equals("sortMode") + "");
		
		 * */
	}
}