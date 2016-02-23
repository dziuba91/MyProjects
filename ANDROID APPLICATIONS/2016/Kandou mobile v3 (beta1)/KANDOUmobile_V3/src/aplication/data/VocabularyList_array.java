package aplication.data;

import java.io.Serializable;


@SuppressWarnings("serial")
public class VocabularyList_array implements Serializable { // for managing array of additional vocabulary lists and serialize

	private VocabularyList [] VOC_list = null;
	
    public VocabularyList_array(VocabularyList [] VOC_list)
    {
        this.VOC_list = VOC_list;
    }
    
    public VocabularyList [] get()
    {
		return VOC_list;
	}
    
    public void set(VocabularyList [] Arr)
    {
    	this.VOC_list = Arr;
    }
}
