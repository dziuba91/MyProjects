package com.kandoumobile_v3.menu.vocabulary;

import java.util.ArrayList;
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
import aplication.data.SelectiveData;
import aplication.data.SubmissionOfKanji;
import aplication.file.manager.ObjectFileManager;

public class VocabularySelectedInfoLayoutActivity extends Activity {

	protected static final int EDIT_RESPONSE = 100;
	SubmissionOfKanji[] VOC = null;
	SubmissionOfKanji[] VOC_all = null;
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
	
	VocabularyGameLayoutActivity subGame = null;
	public VocabularySelectedGameLayoutActivity game_jap_pl = null;
	
	ObjectFileManager ofm = new ObjectFileManager();
	
	int INDEX;
	boolean CLEAR = false;
	
	public enum Direction {
	    NEXT, PREVIOUS, NONE
	}
	
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		setContentView(R.layout.submission_info_layout);
		
		Object [] array = (Object[]) getIntent().getSerializableExtra("voc_data");
		this.VOC = Arrays.copyOf(array, array.length, SubmissionOfKanji[].class);
		
		array = (Object[]) getIntent().getSerializableExtra("vocdesc_data");
		this.VOC_description = Arrays.copyOf(array, array.length, SelectiveData[].class);
		
		array = (Object[]) getIntent().getSerializableExtra("vocsel_data");
		this.VOC_selected = Arrays.copyOf(array, array.length, Boolean[].class);
		
		openLayoutActivity(VOC, VOC_description, VOC_selected, 0);
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
				
				ArrayList<Integer> arr = new ArrayList<Integer>();
				for (int i=0; i<VOC_description.length; i++) if (VOC_description[i].perm) arr.add(i);
				Integer [] VOC_testIndexes = new Integer[arr.size()];
				for (int i=0; i<arr.size(); i++) VOC_testIndexes[i] = (Integer)arr.get(i);
				
				startActivityForResult(new Intent(VocabularySelectedInfoLayoutActivity.this, VocabularySelectedGameLayoutActivity.class).putExtra("voc_data", VOC_all)
						.putExtra("vocdesc_data", VOC_description).putExtra("vocsel_data", VOC_selected).putExtra("mode", Mode.JAP_POL).putExtra("voctest_data", VOC_testIndexes)
						, EDIT_RESPONSE);
			}
		});
		
		cbx = (CheckBox)findViewById(R.id.checkBox_sellected);
		cbx.setVisibility(View.GONE);
	}
	
	private void save()
	{
		String s = "";
    	for (int i=0; i<VOC_selected.length; i++) 
    	{
    		s += VOC_selected[i].toString();
    		if (i < VOC_selected.length-1) s += "-";
    	}
    	
    	ofm.saveObjectArray(s, "V_sel.dat");
	}
	
	public void setLayoutData()
	{		
		if ((VOC == null) || (VOC.length==0))
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
			txt_PositionNr.setText((INDEX+1) + "/" + VOC.length);

			txt_Main.setText(VOC[INDEX].reading);
			txt_MainPart2.setText(VOC[INDEX].signs);
		
			txt_Answer.setText(VOC[INDEX].meaning);
		}
	}
	
	public void setCurrentPositionOnList(Direction D)
	{
		if (D == Direction.NEXT)	
		{
			if (INDEX < VOC.length-1) INDEX++;
		}
		else if (D == Direction.PREVIOUS)
		{
			if (INDEX > 0) INDEX--;
		}
		
		if (INDEX == VOC.length-1) btn_Next.setVisibility(View.INVISIBLE);
		else btn_Next.setVisibility(View.VISIBLE);
		
		if (INDEX == 0) btn_Previous.setVisibility(View.INVISIBLE);
		else btn_Previous.setVisibility(View.VISIBLE);
	}
	
	public void openLayoutActivity(SubmissionOfKanji [] voc, SelectiveData [] sd, Boolean [] vs, int index)
    {
		VOC_all = voc;
		
		ArrayList<SubmissionOfKanji> arr= new ArrayList<SubmissionOfKanji>();
		for (int i=0; i<voc.length; i++) if (vs[i]) arr.add(voc[i]);
		this.VOC = new SubmissionOfKanji [arr.size()];
		for (int i=0; i<VOC.length; i++) VOC[i] = arr.get(i);
		
		this.VOC_description = sd;
		this.VOC_selected = vs;
			
		this.INDEX = index;
		
        setLayoutContent();
        setLayoutData();
        
        if ((VOC != null) && (VOC.length > 0)) setCurrentPositionOnList(Direction.NONE);
    }
	
	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
	     if (requestCode == EDIT_RESPONSE) {
	         if (resultCode == RESULT_OK) {

	        	 	Object [] array = (Object[]) data.getSerializableExtra("vocsel_data");
	     			this.VOC_selected = Arrays.copyOf(array, array.length, Boolean[].class);
	     			
	     			array = (Object[]) data.getSerializableExtra("vocdesc_data");
	     			this.VOC_description = Arrays.copyOf(array, array.length, SelectiveData[].class);
	     			
	     			openLayoutActivity(VOC_all, VOC_description, VOC_selected, INDEX);
	     			
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
        		for (int i=0; i<VOC_selected.length; i++) VOC_selected[i] = false;
        		for (int i=0; i<VOC_description.length; i++) VOC_description[i].perm = false;
        		
				save();
				
				VOC = null;
				setLayoutData();
				
				SAVE = true;
				
        		return true;
        	default:
        		return super.onOptionsItemSelected(item);
    	}
    }
}
