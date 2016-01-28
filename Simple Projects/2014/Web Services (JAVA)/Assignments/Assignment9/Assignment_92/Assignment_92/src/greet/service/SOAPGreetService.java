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

	public Vector<Integer> sortContent(Vector<Integer> v, String a) { 
		// "a" variable should equal to string: ascending, descending 

		Vector<Integer> v1 = new Vector<Integer>();
		
		Integer[] numbArray = new Integer[v.size()];
		for (int i = 0; i < v.size(); i++)
			numbArray[i] = v.get(i);
		
		if (a.equals("ascending"))
			Arrays.sort(numbArray);
		else if (a.equals("descending"))
		{
			Arrays.sort(numbArray);
			Collections.reverse(Arrays.asList(numbArray));
		}
			
		for (Integer value : numbArray)
			v1.add(value);

		return v1;
	}
	
	public OMElement OMEsort_numbers(OMElement element) throws XMLStreamException {

		element.detach();
		
		Iterator<OMNode> children = element.getChildren();
		
		String sortMode = null;
		Vector<Integer> vec = new Vector<Integer>();
		while (children.hasNext())
		{
			OMElement nextElement = (OMElement)children.next();
			
			if (nextElement.getLocalName().equals("sortMode"))
			{
				sortMode = nextElement.getText();
			}
			else
				vec.add(Integer.parseInt(nextElement.getText()));
		}
		
		
		// get result
		Vector<Integer> result = sortContent(vec, sortMode);
		
		
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