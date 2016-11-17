
package ws;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.Arrays;
import java.util.Collections;
import java.util.Hashtable;
import java.util.Random;
import java.util.StringTokenizer;
import java.util.Vector;

public class ServiceHandler implements IServiceHandler {

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
	
	public Vector<String> sortVectorContent(Vector<String> v, String a) { 
		// "a" variable should equal to character: "a" (ascending), "d" (descending) 

		Vector<String> v1 = v;
		
		String[] strArray = new String[v1.size()];
			
		for (int i=0; i<v1.size(); i++) strArray[i] = v1.get(i);
			
		if (a.equals("a"))
			Arrays.sort(strArray, String.CASE_INSENSITIVE_ORDER);
		else if (a.equals("d"))
		{
			Arrays.sort(strArray, String.CASE_INSENSITIVE_ORDER);
			Collections.reverse(Arrays.asList(strArray));
		}
			
		for (int i=0; i<v1.size(); i++) v1.set(i, strArray[i]);

		return v1;
	}

}