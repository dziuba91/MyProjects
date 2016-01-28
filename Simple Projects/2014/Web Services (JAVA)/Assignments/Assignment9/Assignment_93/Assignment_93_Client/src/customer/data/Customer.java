
package customer.data;

public class Customer {

	private String id;

	private String name;
	
	private float purchase;


	public Customer() {

		id = "not knowm";

		name = "not knowm";

		purchase = 0.0f;

	}
	
	public Customer(String id, String name, float purchase) {

		this.id = id;

		this.name = name;

		this.purchase = purchase;

	}

	public void setCustomerID(String customerID) {

		this.id = customerID;

	}

	public String getCustomerID() {

		return this.id;

	}
	
	public void setCustomerName(String customerName) {

		this.name = customerName;

	}

	public String getCustomerName() {

		return this.name;

	}

	public void setPurchase(float purchase) {

		this.purchase = purchase;

	}

	public float getPurchase() {

		return this.purchase;

	}
}