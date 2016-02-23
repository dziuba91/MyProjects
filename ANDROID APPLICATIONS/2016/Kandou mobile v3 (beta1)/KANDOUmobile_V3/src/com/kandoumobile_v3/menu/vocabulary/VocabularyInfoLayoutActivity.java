package com.kandoumobile_v3.menu.vocabulary;

import java.util.Arrays;

import com.example.kandoumobile_v3.R;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.TextView;
import aplication.data.SelectiveAdditionalIndexes;
import aplication.data.SelectiveData;
import aplication.data.KanjiComposition;
import aplication.data.VocabularyList_array;
import aplication.vocabulary_part.classes.AdditionalVocabularyData_Extensions;

//'S£ÓWKA'/'LISTA S³ówek'
public class VocabularyInfoLayoutActivity extends Activity {

	KanjiComposition[] VOC = null;
	SelectiveData [] VOC_description = null;
	public Boolean [] VOC_selected = null;
	Activity app = null;
	
	Button btn_Previous = null;
	Button btn_Next = null;
	
	Button b_save = null;
	
	TextView txt_PositionNr = null;
	TextView txt_Main = null;
	TextView txt_MainPart2 = null;
	TextView txt_Answer = null;
	
	CheckBox cbx = null;
	
	int INDEX;
	
	VocabularyList_array VOC_additionalList = null;
	SelectiveAdditionalIndexes [] VOC_additionalList_acceptedIndexes = null;

	AdditionalVocabularyData_Extensions ext = null; // extension
	int max_tab = 0;
	
	public enum Direction {
	    NEXT, PREVIOUS, NONE
	}
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		setContentView(R.layout.submission_info_layout);
		
		Object [] array = (Object[]) getIntent().getSerializableExtra("voc_data");
		if (array == null) this.VOC = null;
		else this.VOC = Arrays.copyOf(array, array.length, KanjiComposition[].class);
		
		array = (Object[]) getIntent().getSerializableExtra("vocdesc_data");
		if (array == null) this.VOC_description = null;
		else this.VOC_description = Arrays.copyOf(array, array.length, SelectiveData[].class);
		
		array = (Object[]) getIntent().getSerializableExtra("vocsel_data");
		if (array == null) this.VOC_selected = null;
		else this.VOC_selected = Arrays.copyOf(array, array.length, Boolean[].class);
		
		this.INDEX = getIntent().getIntExtra("index", 0);

		this.VOC_additionalList = (VocabularyList_array) getIntent().getSerializableExtra("vocadd_data");
		
		array = (Object[]) getIntent().getSerializableExtra("vocadd_index");
		if (array == null)
			this.VOC_additionalList_acceptedIndexes = null;
		else
			this.VOC_additionalList_acceptedIndexes = Arrays.copyOf(array, array.length, SelectiveAdditionalIndexes[].class);
		
		ext = new AdditionalVocabularyData_Extensions (VOC, VOC_additionalList, VOC_additionalList_acceptedIndexes, VOC_description, VOC_selected);
		
		openLayoutActivity(INDEX);
	}
	
	public void setLayoutContent()
	{
		btn_Next = (Button)findViewById(R.id.button_next);
		btn_Previous = (Button)findViewById(R.id.button_previous);
		
		btn_Previous.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				setCurrentPositionOnList(Direction.PREVIOUS);
				setLayoutData();
			}
		});
		
		btn_Next.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				setCurrentPositionOnList(Direction.NEXT);
				setLayoutData();
			}
		});
		
		
		txt_PositionNr = (TextView)findViewById(R.id.text_score);
		
		txt_Main = (TextView)findViewById(R.id.text_main);
		
		txt_Main.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				if (txt_MainPart2.getVisibility() == View.VISIBLE) 
				{
					txt_MainPart2.setVisibility(View.INVISIBLE);
					txt_Answer.setVisibility(View.INVISIBLE);
				}
				else
				{
					txt_MainPart2.setVisibility(View.VISIBLE);
					txt_Answer.setVisibility(View.VISIBLE);
				}
			}
		});
		
		txt_MainPart2 = (TextView)findViewById(R.id.text_main_part2);
		txt_Answer = (TextView)findViewById(R.id.text_answer);
		
		b_save = (Button)findViewById(R.id.button_save);
		b_save.setVisibility(View.GONE);
		b_save.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				ext.save();
				
				b_save.setVisibility(View.GONE);
			}
		});
		
		cbx = (CheckBox)findViewById(R.id.checkBox_sellected);
		cbx.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				ext.changePermission(INDEX);
				
				b_save.setVisibility(View.VISIBLE);
			}
		});
	}
	
	public void setLayoutData()
	{
		txt_PositionNr.setText((INDEX+1) + "/" + max_tab);

		txt_Main.setText(ext.getDataByIndex(INDEX).reading);
		txt_MainPart2.setText(ext.getDataByIndex(INDEX).signs);
		
		txt_Answer.setText(ext.getDataByIndex(INDEX).meaning);
		
		cbx.setChecked(ext.getPermission(INDEX));
	}
	
	public void setCurrentPositionOnList(Direction D)
	{
		if (D == Direction.NEXT)	
		{
			if (INDEX < max_tab-1) INDEX++;
		}
		else if (D == Direction.PREVIOUS)
		{
			if (INDEX > 0) INDEX--;
		}
		
		if (INDEX == max_tab-1) btn_Next.setVisibility(View.INVISIBLE);
		else btn_Next.setVisibility(View.VISIBLE);
		
		if (INDEX == 0) btn_Previous.setVisibility(View.INVISIBLE);
		else btn_Previous.setVisibility(View.VISIBLE);
	}
	
	public void openLayoutActivity(int index)
    {
	    if (VOC!=null) max_tab += VOC.length;
	    if (VOC_additionalList_acceptedIndexes!=null)
	    	max_tab +=  VOC_additionalList_acceptedIndexes[VOC_additionalList_acceptedIndexes.length-1].ID;
	        
		this.INDEX = index;
		
        setLayoutContent();
        setLayoutData();
        setCurrentPositionOnList(Direction.NONE);
    }
	
	@Override
	public void onBackPressed() {
		if (ext.SAVE)
		{
			Intent intent = new Intent();
			intent.putExtra("vocsel_data", ext.getSelectedArray_Data());
			intent.putExtra("vocdesc_data", ext.getDescriptionArray_Data());
			intent.putExtra("vocadd_data", ext.getAdditionalListArray_Data());
			setResult(RESULT_OK, intent);
		}
		
	    super.onBackPressed();
	}
}
