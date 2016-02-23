package com.kandoumobile_v3.menu;

import java.util.Arrays;

import com.example.kandoumobile_v3.R;
import com.kandoumobile_v3.menu.vocabulary.VocabularyGameLayoutActivity;
import com.kandoumobile_v3.menu.vocabulary.VocabularyInfoLayoutActivity;
import com.kandoumobile_v3.menu.vocabulary.VocabularySelectedInfoLayoutActivity;
import com.kandoumobile_v3.menu.vocabulary.VocabularyGameLayoutActivity.Mode;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import aplication.data.SelectiveAdditionalIndexes;
import aplication.data.SelectiveData;
import aplication.data.KanjiComposition;
import aplication.data.VocabularyList_array;

public class VocabularyMenuLayoutActivity extends Activity{
	
	protected static final int EDIT_RESPONSE = 100;
	KanjiComposition [] VOC = null;
	Boolean [] VOC_selected = null;
	SelectiveData [] VOC_description = null;
	
	Button btn_1 = null;
	Button btn_2 = null;
	Button btn_3 = null;
	Button btn_4 = null;
	
	public Boolean SAVE = false;

	VocabularyList_array VOC_additionalList = null;
	SelectiveAdditionalIndexes [] VOC_additionalList_acceptedIndexes = null;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		setContentView(R.layout.vocabulary_menu_layout);
		
		Object[] array = (Object[]) getIntent().getSerializableExtra("voc_data");
		if (array == null) this.VOC = null;
		else this.VOC = Arrays.copyOf(array, array.length, KanjiComposition[].class);
		
		array = (Object[]) getIntent().getSerializableExtra("vocdes_data");
		if (array == null) this.VOC_description = null;
		else this.VOC_description = Arrays.copyOf(array, array.length, SelectiveData[].class);
		
		array = (Object[]) getIntent().getSerializableExtra("vocsel_data");
		if (array == null) this.VOC_selected = null;
		else this.VOC_selected = Arrays.copyOf(array, array.length, Boolean[].class);
		
		this.VOC_additionalList = (VocabularyList_array) getIntent().getSerializableExtra("vocadd_data");
		
		array = (Object[]) getIntent().getSerializableExtra("vocadd_index");
		if (array == null) this.VOC_additionalList_acceptedIndexes = null;
		else this.VOC_additionalList_acceptedIndexes = Arrays.copyOf(array, array.length, SelectiveAdditionalIndexes[].class);
		
		openLayoutActivity();
	}
	
	public void setLayoutContent()
	{
		btn_1 = (Button)findViewById(R.id.button_vocGame);
		btn_1.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {

				startActivityForResult(new Intent(VocabularyMenuLayoutActivity.this, VocabularyGameLayoutActivity.class).putExtra("voc_data", VOC)
						.putExtra("vocdesc_data", VOC_description).putExtra("vocsel_data", VOC_selected).putExtra("mode", Mode.JAP_POL)
						.putExtra("vocadd_data", VOC_additionalList).putExtra("vocadd_index", VOC_additionalList_acceptedIndexes)
						, EDIT_RESPONSE);
			}
		});
		
		btn_3 = (Button)findViewById(R.id.button_PL_JAP_game);
		btn_3.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				startActivityForResult(new Intent(VocabularyMenuLayoutActivity.this, VocabularyGameLayoutActivity.class).putExtra("voc_data", VOC)
						.putExtra("vocdesc_data", VOC_description).putExtra("vocsel_data", VOC_selected).putExtra("mode", Mode.POL_JAP)
						.putExtra("vocadd_data", VOC_additionalList).putExtra("vocadd_index", VOC_additionalList_acceptedIndexes)
						, EDIT_RESPONSE);
			}
		});
		
		btn_2 = (Button)findViewById(R.id.button_vocList);
		btn_2.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				startActivityForResult(new Intent(VocabularyMenuLayoutActivity.this, VocabularyInfoLayoutActivity.class).putExtra("voc_data", VOC)
						.putExtra("vocdesc_data", VOC_description).putExtra("vocsel_data", VOC_selected).putExtra("index", 0)
						.putExtra("vocadd_data", VOC_additionalList).putExtra("vocadd_index", VOC_additionalList_acceptedIndexes)
						, EDIT_RESPONSE);
			}
		});
		
		btn_4 = (Button)findViewById(R.id.button_vocSelectedList);
		btn_4.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
			
				startActivityForResult(new Intent(VocabularyMenuLayoutActivity.this, VocabularySelectedInfoLayoutActivity.class).putExtra("voc_data", VOC)
						.putExtra("vocdesc_data", VOC_description).putExtra("vocsel_data", VOC_selected)
						.putExtra("vocadd_data", VOC_additionalList).putExtra("vocadd_index", VOC_additionalList_acceptedIndexes)
						, EDIT_RESPONSE);
			}
		});
	}
	
	public void openLayoutActivity()
    {
        setLayoutContent();
    }
	
	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (requestCode == EDIT_RESPONSE) {
	         if (resultCode == RESULT_OK) {

	        	Object [] array = (Object[]) data.getSerializableExtra("vocsel_data");
	        	this.VOC_selected = Arrays.copyOf(array, array.length, Boolean[].class);
	     			
	        	array = (Object[]) data.getSerializableExtra("vocdesc_data");
	        	if (array == null) this.VOC_description = null;
	        	else this.VOC_description = Arrays.copyOf(array, array.length, SelectiveData[].class);

	     		this.VOC_additionalList = (VocabularyList_array) data.getSerializableExtra("vocadd_data");
	     				
	        	SAVE = true;
	        }
	    }
	}
	
	@Override
	public void onBackPressed() {
		if (SAVE)
		{
			Intent intent = new Intent();
			intent.putExtra("vocsel_data", this.VOC_selected);
			intent.putExtra("vocdesc_data", this.VOC_description);
			intent.putExtra("vocadd_data", this.VOC_additionalList);
			setResult(RESULT_OK, intent);
		}
		
	    super.onBackPressed();
	}
}
