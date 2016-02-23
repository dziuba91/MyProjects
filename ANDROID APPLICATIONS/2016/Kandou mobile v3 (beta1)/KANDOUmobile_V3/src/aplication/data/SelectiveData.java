package aplication.data;

import java.io.Serializable;

@SuppressWarnings("serial")
public class SelectiveData  implements Serializable{ // vocabulary static list -> indexes of data to game (for faster access); actualize in MainActivity

	public int ID;
	public Boolean perm;
	
	public SelectiveData(int id, Boolean p)
	{
		this.ID = id;
		this.perm = p;
	}
}
