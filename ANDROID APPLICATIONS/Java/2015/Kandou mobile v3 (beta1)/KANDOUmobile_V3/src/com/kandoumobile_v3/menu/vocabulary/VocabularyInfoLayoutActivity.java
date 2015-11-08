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
import aplication.data.SelectiveData;
import aplication.data.SubmissionOfKanji;
import aplication.file.manager.ObjectFileManager;

public class VocabularyInfoLayoutActivity extends Activity {

	SubmissionOfKanji[] VOC = null;
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
	
	ObjectFileManager ofm = new ObjectFileManager();
	
	int INDEX;
	
	Boolean OPEN = false;
	
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
		
		this.INDEX = getIntent().getIntExtra("index", 0);
		
		openLayoutActivity(VOC, VOC_description, VOC_selected, INDEX);
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
				
				save();
				
				b_save.setVisibility(View.GONE);
			}
		});
		
		cbx = (CheckBox)findViewById(R.id.checkBox_sellected);
		cbx.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				VOC_description[INDEX].perm = !VOC_description[INDEX].perm;
				VOC_selected[VOC_description[INDEX].ID] = VOC_description[INDEX].perm;
				
				b_save.setVisibility(View.VISIBLE);
			}
		});
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
    	
		SAVE = true;
	}
	
	public void setLayoutData()
	{
		txt_PositionNr.setText((INDEX+1) + "/" + VOC.length);

		txt_Main.setText(VOC[INDEX].reading);
		txt_MainPart2.setText(VOC[INDEX].signs);
		
		txt_Answer.setText(VOC[INDEX].meaning);
		
		cbx.setChecked(VOC_description[INDEX].perm);
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
		this.VOC = voc;
		this.VOC_description = new SelectiveData[sd.length];
		for (int i = 0; i < sd.length; i++) this.VOC_description[i] = new SelectiveData(sd[i].ID, sd[i].perm);
		this.VOC_selected = new Boolean[vs.length];
		for (int i = 0; i < vs.length; i++) this.VOC_selected[i] = vs[i];
		
		this.INDEX = index;
		
        setLayoutContent();
        setLayoutData();
        setCurrentPositionOnList(Direction.NONE);
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
}
