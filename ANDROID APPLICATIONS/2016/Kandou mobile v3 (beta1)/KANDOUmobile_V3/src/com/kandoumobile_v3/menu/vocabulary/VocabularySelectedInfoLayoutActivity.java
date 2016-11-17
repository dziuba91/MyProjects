package com.kandoumobile_v3.menu.vocabulary;

import java.util.Arrays;

import com.example.kandoumobile_v3.R;
import com.kandoumobile_v3.menu.vocabulary.VocabularyGameLayoutActivity.Mode;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
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

//'S£ÓWKA'/'WYBRANE S³ówka'
public class VocabularySelectedInfoLayoutActivity extends Activity {

	protected static final int EDIT_RESPONSE = 100;
	KanjiComposition[] VOC = null;
	SelectiveData [] VOC_description = null;
	public Boolean [] VOC_selected = null;
	Activity app = null;
	
	Button btn_Previous = null;
	Button btn_Next = null;
	
	Button b_save = null;
	
	public Boolean SAVE = false;
	
	TextView txt_PositionNr = null;
	TextView txt_Main = null;
	TextView txt_MainPart2 = null;
	TextView txt_Answer = null;
	
	CheckBox cbx = null;
	
	VocabularyList_array VOC_additionalList = null;
	SelectiveAdditionalIndexes [] VOC_additionalList_acceptedIndexes = null;

	AdditionalVocabularyData_Extensions ext = null; // extension
	
	Integer [] VOC_testIndexes = null;
	
	int INDEX;
	boolean CLEAR = false;
	
	int MAX_INDEX = 0;
	
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
		
		this.VOC_additionalList = (VocabularyList_array) getIntent().getSerializableExtra("vocadd_data");
		
		array = (Object[]) getIntent().getSerializableExtra("vocadd_index");
		if (array == null) this.VOC_additionalList_acceptedIndexes = null;
		else this.VOC_additionalList_acceptedIndexes = Arrays.copyOf(array, array.length, SelectiveAdditionalIndexes[].class);
		
		ext = new AdditionalVocabularyData_Extensions (VOC, VOC_additionalList, VOC_additionalList_acceptedIndexes, VOC_description, VOC_selected);
		
		openLayoutActivity(0);
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
		b_save.setText("TEST");
		b_save.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				//
				startActivityForResult(new Intent(VocabularySelectedInfoLayoutActivity.this, VocabularySelectedGameLayoutActivity.class).putExtra("voc_data", VOC)
						.putExtra("vocdesc_data", VOC_description).putExtra("vocsel_data", VOC_selected).putExtra("mode", Mode.JAP_POL).putExtra("voctest_data", VOC_testIndexes)
						.putExtra("vocadd_data", VOC_additionalList).putExtra("vocadd_index", VOC_additionalList_acceptedIndexes)
						, EDIT_RESPONSE);
			}
		});
		
		cbx = (CheckBox)findViewById(R.id.checkBox_sellected);
		cbx.setVisibility(View.GONE);
	}
	
	public void setLayoutData()
	{		
		if (VOC_testIndexes == null)
		{
			btn_Next.setVisibility(View.GONE);
			btn_Previous.setVisibility(View.GONE);
			b_save.setVisibility(View.GONE);
			txt_PositionNr.setVisibility(View.INVISIBLE);

			txt_Main.setText("PUSTA LISTA");
			
			txt_MainPart2.setVisibility(View.INVISIBLE);
			txt_Answer.setVisibility(View.INVISIBLE);
		}
		else
		{
			txt_PositionNr.setText((INDEX+1) + "/" + MAX_INDEX);

			txt_Main.setText(ext.getDataByIndex(VOC_testIndexes[INDEX]).reading);
			txt_MainPart2.setText(ext.getDataByIndex(VOC_testIndexes[INDEX]).signs);
		
			txt_Answer.setText(ext.getDataByIndex(VOC_testIndexes[INDEX]).meaning);
		}
	}
	
	public void setCurrentPositionOnList(Direction D)
	{
		if (D == Direction.NEXT)	
		{
			if (INDEX < MAX_INDEX-1) INDEX++;
		}
		else if (D == Direction.PREVIOUS)
		{
			if (INDEX > 0) INDEX--;
		}
		
		if ((INDEX == MAX_INDEX-1) || (MAX_INDEX == 0)) btn_Next.setVisibility(View.INVISIBLE);
		else btn_Next.setVisibility(View.VISIBLE);
		
		if (INDEX == 0) btn_Previous.setVisibility(View.INVISIBLE);
		else btn_Previous.setVisibility(View.VISIBLE);
	}
	
	public void openLayoutActivity(int index)
	{
		VOC_testIndexes = ext.createSelectedIndexes();
		if (VOC_testIndexes != null)
			MAX_INDEX = VOC_testIndexes.length;
		
		//
		this.INDEX = index;
		
		setLayoutContent();
		setLayoutData();
		
		if (((VOC != null) && (VOC.length > 0)) || ((VOC_additionalList_acceptedIndexes != null) && (VOC_additionalList_acceptedIndexes.length > 0)) || (VOC_testIndexes == null)) 
			setCurrentPositionOnList(Direction.NONE);
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
				
				ext.actualizePermissionArray(VOC_selected, VOC_additionalList);
				
				openLayoutActivity(0);
					
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
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
	   
		getMenuInflater().inflate(R.menu.selected_list_submenu, menu);

		return true;
	}
	
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle item selection
		
		switch (item.getItemId()) {
			case R.id.action_clear:
				ext.uncheckAllPermission();
				ext.save();
				
				VOC = ext.getStaticListArray_Data();
				VOC_additionalList_acceptedIndexes = ext.getIndexesOfAdditionalListArray_Data();
				VOC_testIndexes = null;
				this.MAX_INDEX = 0;
				
				setLayoutData();
				
				SAVE = true;
				
				return true;
				
			default:
				return super.onOptionsItemSelected(item);
		}
	}
}
