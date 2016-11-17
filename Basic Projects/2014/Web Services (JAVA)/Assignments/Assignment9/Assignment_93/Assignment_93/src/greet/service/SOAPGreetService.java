//The following is the content of

// greet.service. SOAPGreetService . java file.

package greet.service;

import java.net.Inet4Address;

import java.net.InetAddress;

import java.net.UnknownHostException;

import java.text.DateFormat;

import java.util.Arrays;
import java.util.Collections;
import java.util.Date;
import java.util.Iterator;
import java.util.Vector;

import javax.xml.namespace.QName;
import javax.xml.stream.XMLStreamException;

import org.apache.axiom.om.OMAbstractFactory;
import org.apache.axiom.om.OMNode;

import org.apache.axiom.om.OMElement;

import org.apache.axiom.om.OMFactory;

import org.apache.axiom.om.OMNamespace;

public class SOAPGreetService {
	
	public OMElement OMCustomerMaxPurchase(OMElement element) throws XMLStreamException {

		element.detach();

		Iterator<OMNode> children = element.getChildren();
		
		String id = null;
		String name = null;
		String customer_max = null;
		Float purchase = null;
		Float purchase_max = null;
		
		while (children.hasNext())
		{
			Iterator<OMNode> childrenCustomer = ((OMElement)children.next()).getChildren();
			while (childrenCustomer.hasNext())
			{
				OMElement nextElement = (OMElement)childrenCustomer.next();
				
				if (nextElement.getLocalName().equals("id"))
				{
					id = nextElement.getText();
				}
				else if (nextElement.getLocalName().equals("name"))
				{
					name = nextElement.getText();
				}
				else if (nextElement.getLocalName().equals("purchase"))
				{
					purchase = Float.parseFloat(nextElement.getText());
				}
			}
			
			if (purchase_max == null || purchase_max < purchase)
			{
				customer_max = "ID: " + id + "; NAME: " + name;
				purchase_max = purchase;
			}
			else if (purchase_max == purchase)
			{
				customer_max += "\nID: " + id + "; NAME: " + name;
			}
		}
		
		OMFactory omFactory = OMAbstractFactory.getOMFactory();

		OMNamespace omNameSpace = omFactory.createOMNamespace(
				"http://service.greet/xsd", "ss");

		OMElement methodElement = omFactory.createOMElement("greetResponse",
				omNameSpace);

		OMElement responseElement = omFactory.createOMElement("greeting",
				omNameSpace);
		
		responseElement.addChild(omFactory.createOMText(responseElement,
				"CUSTOMER:\n" + customer_max + "\n, got MAX PURCHASE = " + purchase_max));

		methodElement.addChild(responseElement);
		
		return methodElement;

	}

}