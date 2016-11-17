//This is ws.Calculator.java file.

package ws;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.Hashtable;
import java.util.Random;
import java.util.StringTokenizer;
import java.util.Vector;

public class Calculator implements ICalculator {

	double digits = 10000.0;

	public Vector<Double> getNumbers(int length) {

		Vector<Double> v = new Vector<Double>(length);
		Random r = new Random();

		// Here we randomly initialize the vector.
		for (int i = 0; i < length; i++)
			v.add((Math.round(digits * r.nextDouble())) / digits);

		return v;

	}

	public Vector<Double> mutateNumbers(Vector<Double> v) {

		Random r = new Random();

		// Here we randomly select an index of the vector.
		int index = r.nextInt(v.size());

		// Here we change the value of the selected index of
		// the vector.
		v.set(index, (Math.round(digits * r.nextDouble())) / digits);

		return v;

	}
	
	public Hashtable<String, Object []> exchangeNumbers(Vector<Double> v1, Vector<Double> v2) {

		Hashtable<String, Object []> exchange = new Hashtable<String, Object []>();
		
		int maxLength = v1.size();
		if (v2.size() < maxLength) maxLength = v2.size();
		
		Vector<Double> v1_1 = new Vector<Double>(v1.size());
		Vector<Double> v2_1 = new Vector<Double>(v2.size());
		
		for (int i=0; i< v1.size(); i++)
		{
			v1_1.add(v1.get(i));
		}
		
		for (int i=0; i< v2.size(); i++)
		{
			v2_1.add(v2.get(i));
		}
		
		Random r = new Random();

		int count = r.nextInt(maxLength) + 1;
		
		if (count == maxLength)
		{
			for (int i=0; i<count; i++)
			{
				double tmp = v1_1.get(i);
				v1_1.set(i, v2_1.get(i));
				v2_1.set(i, tmp);
			}
			
			exchange.put("1", v1_1.toArray());
			exchange.put("2", v2_1.toArray());
		}
		else
		{		
			Vector<Integer> numbers = new Vector<Integer>(maxLength);
			for (int i=0; i< maxLength; i++)
			{
				numbers.add(i);
			}
			
			//exchange.put("3", numbers.toArray()); // test
			
			for (int i=0; i<count; i++)
			{
				int index = r.nextInt(numbers.size());
				int indexValue = numbers.get(index);
				numbers.removeElementAt(index);
				
				double tmp = v1_1.get(indexValue);
				v1_1.set(indexValue, v2_1.get(indexValue));
				v2_1.set(indexValue, tmp);
			}

			exchange.put("1", v1_1.toArray());
			exchange.put("2", v2_1.toArray());
			//exchange.put("3", numbers.toArray()); //test
		}
		
		return exchange;
	}

}