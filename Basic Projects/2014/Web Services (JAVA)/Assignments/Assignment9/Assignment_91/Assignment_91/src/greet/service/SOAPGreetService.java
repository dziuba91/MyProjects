//The following is the content of

// greet.service. SOAPGreetService . java file.

package greet.service;

import java.net.Inet4Address;

import java.net.InetAddress;

import java.net.UnknownHostException;

import java.text.DateFormat;

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

	public Vector<Integer> getPrimeNumbers(int lower, int upper){

		Vector<Integer> v = new Vector<Integer>();
		
		for (int i=lower; i<=upper; i++)
		{
			int count =0;
			for (int j=1; j<=i; j++)
			{
				if(i%j == 0) count++;
				
				if(count > 2) break;
			}
			
			if ((count == 1) || (count == 2)) 
				v.add(i);
		}

		return v;
	}
	
	public OMElement prime_numbers(OMElement element) throws XMLStreamException {

		element.detach();

		OMElement firstElement = element;
		
		String nameSpaceURI = firstElement.getQName().getNamespaceURI();
		
		Iterator<OMNode> iterator = firstElement.getChildrenWithName(new QName(
				nameSpaceURI, "lowerBound"));
		
		Integer lower = null;
		if (iterator.hasNext())
			lower = Integer.parseInt(((OMElement) iterator.next()).getText());
		
		
		iterator = firstElement.getChildrenWithName(new QName(
				nameSpaceURI, "upperBound"));
		
		Integer upper = null;
		if (iterator.hasNext())
			upper = Integer.parseInt(((OMElement) iterator.next()).getText());
		
		
		// get result
		Vector<Integer> result = getPrimeNumbers(lower, upper);
		
		
		// In the following we build a response.
		OMFactory omFactory = OMAbstractFactory.getOMFactory();

		OMNamespace omNameSpace = omFactory.createOMNamespace(
				"http://service.greet/xsd", "ss");

		OMElement methodElement = omFactory.createOMElement("primeNumbersResponse",
				omNameSpace);

		
		for (int i = 0; i < result.size(); i++)
		{
			OMElement responseElement = omFactory.createOMElement("value",
					omNameSpace);
		
			responseElement.addAttribute("index", i + "", omNameSpace);
			
			responseElement.setText(result.get(i).toString());
		
			methodElement.addChild(responseElement);
		}
		
		
		return methodElement;

	}

}