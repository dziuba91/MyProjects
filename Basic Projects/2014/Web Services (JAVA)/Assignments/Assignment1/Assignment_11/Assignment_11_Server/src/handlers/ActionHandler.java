
//This is handlers.ActionHandler.java

package handlers;

import java.text.DateFormat;
import java.util.Collections;
import java.util.Date;
import java.util.Vector;

public class ActionHandler implements IActionHandler {
	public String getDate(String name) {
		Date date = new Date();

		return "Hello "
				+ name
				+ "! By the way, today is "
				+ DateFormat.getDateTimeInstance(DateFormat.LONG,
						DateFormat.LONG).format(date);
	}

	public Integer getMin(Vector vec) {
		return Collections.min(vec);
	}
}
