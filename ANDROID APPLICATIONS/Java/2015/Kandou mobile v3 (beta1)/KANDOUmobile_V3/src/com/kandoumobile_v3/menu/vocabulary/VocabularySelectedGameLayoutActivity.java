package com.kandoumobile_v3.menu.vocabulary;

import java.util.ArrayList;
import java.util.Arrays;

import android.content.Intent;
import android.os.Bundle;
import aplication.data.SelectiveData;

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
        int max_tab = VOC.length;
        
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
        
        subGame = this;
    }
    
    @Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
	     if (requestCode == EDIT_RESPONSE) {
	         if (resultCode == RESULT_OK) {
	     		
	        	 Object [] array = (Object[]) data.getSerializableExtra("vocsel_data");
	     		 this.VOC_selected = Arrays.copyOf(array, array.length, Boolean[].class);
	     			
	     		 array = (Object[]) data.getSerializableExtra("vocdesc_data");
	     		 this.VOC_description = Arrays.copyOf(array, array.length, SelectiveData[].class);

	     		 ArrayList<Integer> arr = new ArrayList<Integer>();
				 for (int i=0; i<VOC_description.length; i++) if (VOC_description[i].perm) arr.add(i);
				 this.VOC_testIndexes = new Integer[arr.size()];
				 for (int i=0; i<arr.size(); i++) VOC_testIndexes[i] = (Integer)arr.get(i);
	     		 
	     		 SAVE = true;
	         }
	     }
	}
}
