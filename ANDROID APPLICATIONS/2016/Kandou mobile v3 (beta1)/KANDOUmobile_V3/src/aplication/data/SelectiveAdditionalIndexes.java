package aplication.data;

import java.io.Serializable;
import java.util.ArrayList;

@SuppressWarnings("serial")
public class SelectiveAdditionalIndexes	 implements Serializable{ // vocabulary additional list -> indexes of data to game (for faster access); actualize in MainActivity

	public int ID;
	public ArrayList<Integer> indexes = null;
	
	public SelectiveAdditionalIndexes(int id, ArrayList<Integer> indexes)
	{
		this.ID = id;
		this.indexes = indexes;
	}
	
	public SelectiveAdditionalIndexes(int id)
	{
		this.ID = id;
		this.indexes = null;
	}
}