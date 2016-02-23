package aplication.vocabulary_part.classes;

import java.util.ArrayList;

import aplication.data.KanjiComposition;
import aplication.data.SelectiveAdditionalIndexes;
import aplication.data.SelectiveData;
import aplication.data.VocabularyList_array;
import aplication.file.manager.ObjectFileManager;

// Class is to sharing data from two list methods ('static' and 'additional')
public class AdditionalVocabularyData_Extensions {
	
	KanjiComposition [] VOC; 
	VocabularyList_array VOC_additionalList; 
	SelectiveAdditionalIndexes [] VOC_additionalList_acceptedIndexes;
	SelectiveData [] VOC_description = null;
	Boolean [] VOC_selected = null;

	public Boolean SAVE = false;
	
	public AdditionalVocabularyData_Extensions(KanjiComposition [] VOC, 
			VocabularyList_array VOC_additionalList, 
			SelectiveAdditionalIndexes [] VOC_additionalList_acceptedIndexes, 
			SelectiveData [] VOC_description, 
			Boolean [] VOC_selected)
	{
		this.VOC = VOC; 
		this.VOC_additionalList = VOC_additionalList; 
		this.VOC_additionalList_acceptedIndexes = VOC_additionalList_acceptedIndexes;
		this.VOC_description = VOC_description;
		this.VOC_selected = VOC_selected;
	}
	
	public KanjiComposition getDataByIndex(int index)
	{
		if ((VOC != null) && (index < VOC.length))
			return VOC[index];
		else
		{
			int voc_length = 0;
			if (VOC != null) voc_length = VOC.length;
			int [] tmp = getIndexOfVocabularyList(index-voc_length);
			return VOC_additionalList.get()[tmp[0]].getVocabularyArray()[tmp[1]];
		}
	}
	
	private int [] getIndexOfVocabularyList(int index)
	{
		int tmp = 0;
		int tmpPrev = 0;
		for (int i=0; i<VOC_additionalList_acceptedIndexes.length-1; i++)
		{
			tmp+=VOC_additionalList_acceptedIndexes[i].indexes.size();
			if (index<tmp)
				return new int[]{VOC_additionalList_acceptedIndexes[i].ID, VOC_additionalList_acceptedIndexes[i].indexes.get(index-tmpPrev)}; // 0 - index of list; 1 - index of vocabulary on this list 
			
			tmpPrev = tmp;
		}
		
		return null;
	}
	
	public Boolean getPermission(int index)
	{
		if ((VOC != null) && (index < VOC.length))
			return VOC_description[index].perm;
		else
		{
			int voc_length = 0;
			if (VOC != null) voc_length = VOC.length;
			int [] tmp = getIndexOfVocabularyList(index-voc_length);
			return VOC_additionalList.get()[tmp[0]].getVocabularyArray()[tmp[1]].selected;
		}
	}
	
	public void changePermission(int index)
	{
		if ((VOC != null) && (index < VOC.length))
		{
			VOC_description[index].perm = !VOC_description[index].perm;
			VOC_selected[VOC_description[index].ID] = VOC_description[index].perm;
		}
		else
		{
			int voc_length = 0;
			if (VOC != null) voc_length = VOC.length;
			int [] tmp = getIndexOfVocabularyList(index-voc_length);
			VOC_additionalList.get()[tmp[0]].getVocabularyArray()[tmp[1]].selected = !VOC_additionalList.get()[tmp[0]].getVocabularyArray()[tmp[1]].selected;
		}
	}
	
	public void uncheckAllPermission()
	{
		for (int i=0; i<VOC_selected.length; i++) VOC_selected[i] = false;
		for (int i=0; i<VOC_description.length; i++) VOC_description[i].perm = false;
		this.VOC = null;
		
		if (this.VOC_additionalList_acceptedIndexes != null)
		{
			for (int i=0; i<VOC_additionalList_acceptedIndexes.length-1; i++)
				for (int j=0; j<VOC_additionalList_acceptedIndexes[i].indexes.size(); j++)
					VOC_additionalList.get()[VOC_additionalList_acceptedIndexes[i].ID].getVocabularyArray()[VOC_additionalList_acceptedIndexes[i].indexes.get(j)].selected = false;
			this.VOC_additionalList_acceptedIndexes = null;
		}
	}
	
	public void save()
	{
		ObjectFileManager ofm = new ObjectFileManager();
		
		String s = "";
    	for (int i=0; i<VOC_selected.length; i++) 
    	{
    		s += VOC_selected[i].toString();
    		if (i < VOC_selected.length-1) s += "-";
    	}
    	
    	ofm.saveObjectArray(s, "V_sel.dat");
    	ofm.saveObject(this.VOC_additionalList, "V_addList.dat");
    	
		SAVE = true;
	}
	
	//
	public Boolean [] getSelectedArray_Data()
	{
		return VOC_selected;
	}
	
	public SelectiveData [] getDescriptionArray_Data()
	{
		return VOC_description;
	}
	
	public VocabularyList_array getAdditionalListArray_Data()
	{
		return VOC_additionalList;
	}
	
	public KanjiComposition [] getStaticListArray_Data()
	{
		return VOC;
	}
	
	public SelectiveAdditionalIndexes [] getIndexesOfAdditionalListArray_Data()
	{
		return VOC_additionalList_acceptedIndexes;
	}
	
	public void actualizePermissionArray(Boolean [] VOC_selected, VocabularyList_array VOC_additionalList)
	{
		this.VOC_selected = VOC_selected;
		this.VOC_additionalList = VOC_additionalList;
	}
	
	//
	public Integer [] createSelectedIndexes()
	{
		ArrayList<Integer> arr= new ArrayList<Integer>();
		
		// get vocabulary selected indexes (from main list)
		if (VOC != null)
		{
			for (int i=0; i<VOC.length; i++) 
				if (VOC_selected[i]) arr.add(i);
		}
		
		// get indexes (from additional lists)
		if (VOC_additionalList_acceptedIndexes != null)
		{
			int count = 0;
			if (VOC != null) count = VOC.length;
			
			for (int i=0; i<VOC_additionalList_acceptedIndexes.length-1; i++)
				for (int j=0; j<VOC_additionalList_acceptedIndexes[i].indexes.size(); j++, count++)
					if (VOC_additionalList.get()[VOC_additionalList_acceptedIndexes[i].ID].getVocabularyArray()[VOC_additionalList_acceptedIndexes[i].indexes.get(j)].selected)
						arr.add(count);
		}
		
		Integer [] ret = null;
		if (arr.size() > 0)
			ret = new Integer[arr.size()];
		
		for (int i=0; i<arr.size(); i++) ret[i] = arr.get(i);
		
		return ret;
	}
}
