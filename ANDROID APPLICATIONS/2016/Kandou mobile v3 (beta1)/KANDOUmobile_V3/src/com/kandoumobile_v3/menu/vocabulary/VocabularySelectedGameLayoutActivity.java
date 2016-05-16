package com.kandoumobile_v3.menu.vocabulary;

import java.util.Arrays;

import android.content.Intent;
import android.os.Bundle;
import aplication.data.SelectiveData;
import aplication.data.VocabularyList_array;

//'S£ÓWKA'/'WYBRANE S³ówka'/'TEST'
public class VocabularySelectedGameLayoutActivity extends VocabularyGameLayoutActivity {

	Integer [] VOC_testIndexes = null;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		
		Object [] array = (Object[]) getIntent().getSerializableExtra("voctest_data");
		this.VOC_testIndexes = Arrays.copyOf(array, array.length, Integer[].class);
		
		super.onCreate(savedInstanceState);
	}
	
    @Override
 	public void setGameRound()
    {
        int max_tab = 0;
        if (VOC != null) max_tab += VOC.length;
        if (VOC_additionalList_acceptedIndexes != null) max_tab += VOC_additionalList_acceptedIndexes[VOC_additionalList_acceptedIndexes.length-1].ID;
        
        for (int i = 0; i < 6; i++)
        {
            int number = random.nextInt(max_tab);

          check:
            for (int j = 0; j < i; j++)
            {
                if (vocIndex[j] == number || number == CorectVoc)
                {
                    number++;
                    if (number == max_tab) number = 0;
                    
                    break check;
                }
            }

            vocIndex[i] = number;
        }
        
        CorectVocIndex = random.nextInt(6);
        int index = random.nextInt(VOC_testIndexes.length);
        int tmp = vocIndex[CorectVocIndex];
        vocIndex[CorectVocIndex] = VOC_testIndexes[index];   // change existed index to one of tested index
        if (vocIndex[CorectVocIndex] == CorectVoc)
        {
            index++;
            if (index >= VOC_testIndexes.length)
                index = 0;

            vocIndex[CorectVocIndex] = VOC_testIndexes[index];
        }
        CorectVoc = vocIndex[CorectVocIndex];

        for (int i = 0; i < 6; i++)
        {
            if (i == CorectVocIndex) continue;

            if (vocIndex[i] == CorectVoc) vocIndex[i] = tmp;
        }

        setLayoutData();
    }
    
    @Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
    	if (requestCode == EDIT_RESPONSE) {
    		if (resultCode == RESULT_OK) {
	     		
    			Object [] array = (Object[]) data.getSerializableExtra("vocsel_data");
    			if (array == null) this.VOC_selected = null;
    			else this.VOC_selected = Arrays.copyOf(array, array.length, Boolean[].class);
	     			
    			array = (Object[]) data.getSerializableExtra("vocdesc_data");
    			if (array == null) this.VOC_description = null;
    			else this.VOC_description = Arrays.copyOf(array, array.length, SelectiveData[].class);

	     		this.VOC_additionalList = (VocabularyList_array) data.getSerializableExtra("vocadd_data");
	     		
	     		// *Actualize game for new data, if something change (not need at the moment - maybe in next actualization)
	     		//ext.actualizePermissionArray(VOC_selected, VOC_additionalList);
	     		//this.VOC_testIndexes = ext.createSelectedIndexes();
	     		
    			SAVE = true;
    		}
    	}
	}
}
