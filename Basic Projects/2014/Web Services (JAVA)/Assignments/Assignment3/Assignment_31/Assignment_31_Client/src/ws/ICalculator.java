//This is ws.ICalculator.java file.

package ws;

import java.util.Hashtable;
import java.util.Vector;

public interface ICalculator {

public Vector<Double> getNumbers(int length);

public Vector<Double> mutateNumbers(Vector<Double> v);

public Hashtable<String, Object []> exchangeNumbers(Vector<Double> v1, Vector<Double> v2);

}