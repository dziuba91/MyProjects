//This is ws.customer.service.CustomerService.java file.

package ws.customer.service;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.Hashtable;

import ws.customer.data.Customer;

public class CustomerService {

	// Here we define a collection for customers.

	//Hashtable<Integer, Customer> customers = new Hashtable<Integer, Customer>();
	
	String homeDir=System.getProperty("user.dir");
	String filePathName = homeDir + "/webapps/";
	
	
	// we will save data to file: "product.dat" (set it on CLient side)
	public int setCustomer(Customer customer, String fileName) {

		//customers.put(customer.getCustomerID(), customer);
		
		File f = new File(filePathName + fileName);
		
		BufferedWriter writer = null;
		try {
			if (!f.exists())
			{
				f.createNewFile();
			}
			
			writer = new BufferedWriter(new FileWriter(filePathName + fileName,true));
		} catch (IOException e) {
			e.printStackTrace();

			return -1;
		}	
		
		try {
			// write Customer
			writer.write(customer.getCustomerName() + "\n");
			writer.write(customer.getCustomerID() + "\n");
			writer.write(customer.getShoppingAmount() + "\n");
			writer.write(customer.getPrivileged() + "\n");
			writer.write(customer.getDiscountPercentage() + "\n");
			
			writer.close();
		} catch (IOException e) {
			e.printStackTrace();

			return -1;
		}

		return 1;
	}

	public Customer [] getCustomer(float shoppingAmount_lower, float shoppingAmount_upper, String fileName) {
		
		ArrayList<Customer> customer_list = new ArrayList<Customer>();
		
		String customerName = null;
		Integer customerID = null;
		float shoppingAmount = 0;
		boolean privileged = false;
		int discountPercentage = 0;
		
		int i = 0;
		
		BufferedReader reader = null;
		String fileText = "";
		
		File f = new File(filePathName + fileName);
		
		if (f.exists())
		{
			try {
				reader = new BufferedReader(new FileReader(filePathName + fileName));
			
				String line;
				while ((line = reader.readLine()) != null)
				{
					if (i == 0) customerName = line;
					else if (i == 1) customerID = Integer.parseInt(line);
					else if (i == 2) shoppingAmount = Float.parseFloat(line);
					else if (i == 3) 
					{
						if (line.equals("true")) privileged = true;
						else privileged = false;
					}
					else if (i == 4)
					{
						discountPercentage = Integer.parseInt(line);
						
						Customer value = new Customer();
						value.setCustomerName(customerName);
						value.setCustomerID(customerID);
						value.setShoppingAmount(shoppingAmount);
						value.setPrivileged(privileged);
						value.setDiscountPercentage(discountPercentage);
						
						float getShoppingAmount = value.getShoppingAmount();
						if ((getShoppingAmount >=  shoppingAmount_lower) && (getShoppingAmount <= shoppingAmount_upper))
						{
							customer_list.add(value);
						}
						
						i = 0;
						
						continue;
					}
					
					i++;
				}
				
				reader.close();
				
			} catch (FileNotFoundException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
				
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		
		
		// convert ArrayList to Customer []
		Customer [] customer = null;

		if (customer_list.size() > 0)
		{
			customer = new Customer[customer_list.size()];
			
			for (i=0; i<customer_list.size(); i++)
			{
				customer[i] = customer_list.get(i);
			}
		}
		
		if (customer == null)
		{
			customer = new Customer[0];
		}
		
		return customer;
		
		//return customer;
	}
	
	
	//////////////////////////////////////////////////////////////
	/////////////////
	/////// This methods implements possibility of set and get Customers 
	/////// (between the interval of shopping amount)
	//////  in normal ways: using hashtable, without using file
	/////////////////
	/*
	public int setCustomer(Customer customer) {

		customers.put(customer.getCustomerID(), customer);

		return customers.size();

	}

	public Customer [] getCustomer(float shoppingAmount_lower, float shoppingAmount_upper) {

		ArrayList<Customer> customer_list = new ArrayList<Customer>();
		
		//Customer customer = null;
		
		Enumeration<Integer> enu = customers.keys();
		Integer key;
		Customer value;
		while (enu.hasMoreElements()) {
			
			key = enu.nextElement();
			value = customers.get(key);
		
			float shoppingAmount = value.getShoppingAmount();
			
			if ((shoppingAmount >=  shoppingAmount_lower) && (shoppingAmount <= shoppingAmount_upper))
			{
				customer_list.add(value);
			}
		}
		
		Customer [] customer = null;

		if (customer_list.size() > 0)
		{
			customer = new Customer[customer_list.size()];
			
			for (int i=0; i<customer_list.size(); i++)
			{
				customer[i] = customer_list.get(i);
			}
		}
		
		if (customer == null)
		{
			customer = new Customer[0];
		}
		
		return customer;
	}
	 * */

}