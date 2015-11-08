package com.kandoumobile_v3.menu.kanji;

import java.util.Arrays;

import com.example.kandoumobile_v3.R;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;
import aplication.data.KanjiDataType;

public class KanjiInfoLayoutActivity extends Activity{

	KanjiDataType [] KANJI = null;
	Activity app = null;
	
	Button btn_Previous = null;
	Button btn_Next = null;
	
	TextView txt_PositionNr = null;
	TextView txt_BasicData = null;
	TextView txt_KanjiSign = null;
	TextView txt_Submissions = null;
	
	KanjiGameLayoutActivity kanjiGame = null;
	
	int INDEX;
	
	public enum Direction {
	    NEXT, PREVIOUS, NONE
	}
	
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		setContentView(R.layout.kanji_info_layout);
		
		Object[] array = (Object[]) getIntent().getSerializableExtra("kanji_data");
		this.KANJI = Arrays.copyOf(array, array.length, KanjiDataType[].class);
		
		this.INDEX = getIntent().getIntExtra("index", 0);
		
		openLayoutActivity();
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
		
		
		txt_PositionNr = (TextView)findViewById(R.id.text_positionNumber);
		
		txt_BasicData = (TextView)findViewById(R.id.text_basicData);
		txt_KanjiSign = (TextView)findViewById(R.id.text_kanji);
		txt_Submissions = (TextView)findViewById(R.id.text_submissions);
	}
	
	public void setLayoutData()
	{
		txt_PositionNr.setText((INDEX+1) + "/" + KANJI.length);
		
		txt_KanjiSign.setText(KANJI[INDEX].sign);
		txt_BasicData.setText(KANJI[INDEX].meaning + "\n" + KANJI[INDEX].reading);

		String txt = "";
		if (KANJI[INDEX].submissions != null)
			for (int i=0; i<KANJI[INDEX].submissions.length; i++)
			{
				txt += KANJI[INDEX].submissions[i].signs + " (" + KANJI[INDEX].submissions[i].reading + ") - " + KANJI[INDEX].submissions[i].meaning;
				if (i < (KANJI[INDEX].submissions.length-1)) txt += "\n";
			}
		
		txt_Submissions.setText(txt);
	}
	
	public void setCurrentPositionOnList(Direction D)
	{
		if (D == Direction.NEXT)	
		{
			if (INDEX < KANJI.length-1) INDEX++;
		}
		else if (D == Direction.PREVIOUS)
		{
			if (INDEX > 0) INDEX--;
		}
		
		if (INDEX == KANJI.length-1) btn_Next.setVisibility(View.INVISIBLE);
		else btn_Next.setVisibility(View.VISIBLE);
		
		if (INDEX == 0) btn_Previous.setVisibility(View.INVISIBLE);
		else btn_Previous.setVisibility(View.VISIBLE);
	}
	
	public void openLayoutActivity()
    {
        setLayoutContent();
        setLayoutData();
        setCurrentPositionOnList(Direction.NONE);
    }
}
