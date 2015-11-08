package com.kandoumobile_v3.menu.kanji;

import java.util.Arrays;

import com.example.kandoumobile_v3.R;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.TextView;
import aplication.data.SubmissionOfKanji;

public class SubmissionInfoLayoutActivity extends Activity {

	SubmissionOfKanji[] SUB = null;
	Activity app = null;
	
	Button btn_Previous = null;
	Button btn_Next = null;
	
	TextView txt_PositionNr = null;
	TextView txt_Main = null;
	TextView txt_MainPart2 = null;
	TextView txt_Answer = null;
	
	SubmissionGameLayoutActivity subGame = null;
	
	int INDEX;
	
	public enum Direction {
	    NEXT, PREVIOUS, NONE
	}
	
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		setContentView(R.layout.submission_info_layout);
		
		Object[] array = (Object[]) getIntent().getSerializableExtra("sub_data");
		this.SUB = Arrays.copyOf(array, array.length, SubmissionOfKanji[].class);
		
		this.INDEX = getIntent().getIntExtra("index", 0);
		
		openLayoutActivity(SUB, INDEX);
	}
	
	public void setLayoutContent()
	{
		CheckBox cbx = (CheckBox)findViewById(R.id.checkBox_sellected);
		cbx.setVisibility(View.GONE);
		
		Button b_save = (Button)findViewById(R.id.button_save);
		b_save.setVisibility(View.GONE);
		
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
	}
	
	public void setLayoutData()
	{
		txt_PositionNr.setText((INDEX+1) + "/" + SUB.length);

		txt_Main.setText(SUB[INDEX].signs);
		txt_MainPart2.setText(SUB[INDEX].reading);
		
		txt_Answer.setText(SUB[INDEX].meaning);
	}
	
	public void setCurrentPositionOnList(Direction D)
	{
		if (D == Direction.NEXT)	
		{
			if (INDEX < SUB.length-1) INDEX++;
		}
		else if (D == Direction.PREVIOUS)
		{
			if (INDEX > 0) INDEX--;
		}
		
		if (INDEX == SUB.length-1) btn_Next.setVisibility(View.INVISIBLE);
		else btn_Next.setVisibility(View.VISIBLE);
		
		if (INDEX == 0) btn_Previous.setVisibility(View.INVISIBLE);
		else btn_Previous.setVisibility(View.VISIBLE);
	}
	
	public void openLayoutActivity(SubmissionOfKanji [] sub, int index)
    {
		this.SUB = sub;
		this.INDEX = index;
		
        setLayoutContent();
        setLayoutData();
        setCurrentPositionOnList(Direction.NONE);
    }
}
