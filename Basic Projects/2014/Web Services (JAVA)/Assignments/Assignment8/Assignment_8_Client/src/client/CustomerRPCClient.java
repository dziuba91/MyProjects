//This is the content of client. CustomerRPCClient.java file.

package client;

import javax.xml.namespace.QName;

import org.apache.axis2.AxisFault;
import org.apache.axis2.addressing.EndpointReference;
import org.apache.axis2.client.Options;
import org.apache.axis2.rpc.client.RPCServiceClient;

import ws.customer.data.Customer;

public class CustomerRPCClient {

	public static void main(String[] args1) throws AxisFault {

		RPCServiceClient serviceClient = new RPCServiceClient();

		Options options = serviceClient.getOptions();

	 //EndpointReference targetEPR = new
	 //   EndpointReference("http://localhost:10000/axis2/services/e1401209_CustomerService");

		EndpointReference targetEPR = new EndpointReference(
				"http://app3.cc.puv.fi/axis2/services/e1401209_assignment8_42");

		options.setTo(targetEPR);

		int[] ids = { 4000, 5000, 6000 };
		String[] names = { "Rao", "Sakari", "Laura" };
		float[] shoppinGAmounts = { 123.54f, 320.40f, 58.89f };
		boolean[] priviliged = { true, true, false };
		int[] discountPercentages = { 3, 10, 2 };

		String fileName = "product.dat";
		
		// Setting the customer information

		QName opSetCustomer = new QName("http://service.customer.ws",
				"setCustomer");
		
		Class[] returnTypes_int = new Class[] { Integer.class };
		
		Customer customer = null;
		Object[] opSetCustomerArgs = null;
		
		Object[] res = null;

		for (int i = 0; i < ids.length; i++) {
			customer = new Customer();
			customer.setCustomerName(names[i]);
			customer.setCustomerID(ids[i]);
			customer.setShoppingAmount(shoppinGAmounts[i]);
			customer.setPrivileged(priviliged[i]);
			customer.setDiscountPercentage(discountPercentages[i]);

			opSetCustomerArgs = new Object[] { customer, fileName };

			//res = serviceClient.invokeBlocking(opSetCustomer, opSetCustomerArgs, returnTypes_int);
		}
		
		//String res1 = (Integer)res[0] + "";
		//System.out.println(res1);
		
		// Here we get the customer information

		//int id = ids[2];
		QName opGetCustomer = new QName("http://service.customer.ws",
				"getCustomer");

		float lower = 50.0f;
		float upper = 200.0f;
		Object[] opGetCustomerArgs = new Object[] { lower, upper, fileName };

		Class[] returnTypes = new Class[] { Customer[].class };
		
		//Class[] returnTypes = new Class[] { String.class };

		Object[] response = serviceClient.invokeBlocking(opGetCustomer, opGetCustomerArgs, returnTypes);

		//System.out.println((String)response[0]);
		
		System.out.println("MINIMUM shopping amount set to: " + lower);
		System.out.println("MAXIMUM shopping amount set to: " + upper);
		System.out.println();
		
		String header1 = "Customer name \tShopping Amounts";
		String header2 = "--------------------------------";
		System.out.println(header1);
		System.out.println(header2);
		
		for (int j = 0; j<response.length; j++)
		{
			Customer[] result = (Customer[]) response[j];
			
			for (int i = 0; i < result.length; i++) {
				System.out.println(result[i].getCustomerName() + "\t \t" + result[i].getShoppingAmount());
			}
		}
		
		// Displaying the result
		/*
		System.out.println("Customer name : " +

		result.getCustomerName());

		System.out.println("Customer ID : " +

		result.getCustomerID());

		System.out.println("Shopping amount : " +

		result.getShoppingAmount());

		System.out.println("Customer is privileged? : " +

		result.getPrivileged());

		System.out.println("Discount amount : " +

		result.getDiscount());
		 */
	}

}