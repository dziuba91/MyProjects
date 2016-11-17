package client;

import java.util.Iterator;
import java.util.Vector;

import customer.data.*;

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
				"http://app3.cc.puv.fi/axis2/services/e1401209_as10_3_2");

		Options options = new Options();

		options.setTo(targetEPR);

		ServiceClient client = new ServiceClient();

		client.setOptions(options);

		String sortMode = "ascending";
		Customer [] customerList = new Customer[5];
		customerList[0] = new Customer("e1000","cust1",12.0f);
		customerList[1] = new Customer("e1001","cust2",2.0f);
		customerList[2] = new Customer("e1002","cust3",11.5f);
		customerList[3] = new Customer("e1003","cust4",15.0f);
		customerList[4] = new Customer("e1004","cust5",5.0f);

		OMFactory omFactory = OMAbstractFactory.getOMFactory();

		OMNamespace omNameSpace = omFactory.createOMNamespace(
				"http://service.greet/xsd", "OMCustomerMaxPurchase");

		OMElement methodElement = omFactory.createOMElement("OMCustomerMaxPurchase",
				omNameSpace);

		for (int i = 0; i<customerList.length; i++)
		{
			OMElement argumentElement = omFactory.createOMElement("customer",
					omNameSpace);
		
			methodElement.addChild(argumentElement);
			
			OMElement customerElement1 = omFactory.createOMElement("id", omNameSpace);
			customerElement1.setText(customerList[i].getCustomerID());
			argumentElement.addChild(customerElement1);
			
			OMElement customerElement2 = omFactory.createOMElement("name", omNameSpace);
			customerElement2.setText(customerList[i].getCustomerName());
			argumentElement.addChild(customerElement2);
			
			OMElement customerElement3 = omFactory.createOMElement("purchase", omNameSpace);
			customerElement3.setText(((Float)customerList[i].getPurchase()).toString());
			argumentElement.addChild(customerElement3);
		}
		
		
		Iterator<OMNode> children = methodElement.getChildren();
		Integer i = 1;
		
		System.out.println("Customer list: ");
		System.out.println("-------------- ");
		
		while (children.hasNext())
		{
			System.out.println("--- " + i + " ---");
			
			Iterator<OMNode> childrenCustomer = ((OMElement)children.next()).getChildren();
			while (childrenCustomer.hasNext())
			{
				OMElement nextElement = (OMElement)childrenCustomer.next();
				System.out.println(nextElement.getLocalName() + " = " + nextElement.getText());
			}
			
			i++;
		}
		
		System.out.println();
		
		
		System.out.println("Service response: ");
		System.out.println("----------------- ");
		
		OMElement responseElement = client.sendReceive(methodElement);

		String response = responseElement.getFirstElement().getText();

		System.out.println(response);

	}
}