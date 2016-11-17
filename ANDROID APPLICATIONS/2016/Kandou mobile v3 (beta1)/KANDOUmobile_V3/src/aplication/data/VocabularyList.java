package aplication.data;

import java.io.Serializable;


@SuppressWarnings("serial")
public class VocabularyList implements Serializable { // describe a single list of additional vocabularies 

	//public int ID;
	public Boolean active; // not used yet (for next actualization)
	public String name;
	private KanjiComposition_extended [] VOC_list;
	
    public VocabularyList(String name, KanjiComposition_extended [] VOC_list, Boolean active)
    {
        //this.ID = ID;
        this.name = name;
        this.VOC_list = VOC_list;
    	this.active = active;
    }
    
    public void setVocabularyArray(KanjiComposition_extended [] voc)
    {
    	this.VOC_list = voc;
    }
    
    public KanjiComposition_extended [] getVocabularyArray()
    {
    	return this.VOC_list;
    }
}