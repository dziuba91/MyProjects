package com.kandoumobile_v3.menu;

import java.util.ArrayList;
import java.util.Arrays;

import com.example.kandoumobile_v3.R;
import com.kandoumobile_v3.menu.kanji.KanjiGameLayoutActivity;
import com.kandoumobile_v3.menu.kanji.KanjiInfoLayoutActivity;
import com.kandoumobile_v3.menu.kanji.KanjiCompositionGameLayoutActivity;
import com.kandoumobile_v3.menu.kanji.KanjiCompositionInfoLayoutActivity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import aplication.data.KanjiDataType;
import aplication.data.KanjiComposition;

public class KanjiMenuLayoutActivity extends Activity {
	
	KanjiDataType [] KANJI = null;
	KanjiComposition [] SUB = null;
	Activity app = null;
	
	Button btn_1 = null;
	Button btn_2 = null;
	Button btn_3 = null;
	Button btn_4 = null;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		setContentView(R.layout.kanji_menu_layout);
		
		Object[] array = (Object[]) getIntent().getSerializableExtra("kanji_data");
		this.KANJI = Arrays.copyOf(array, array.length, KanjiDataType[].class);
		
		openLayoutActivity(KANJI);
	}
	
	public void getSub() // get Kanji Compositions list (for KanjiCompositionGame)
	{
		ArrayList<KanjiComposition> arr = new ArrayList<KanjiComposition>();
		
		for (int i = 0; i<KANJI.length; i++)
			if (KANJI[i].submissions != null)
				for (int j = 0; j<KANJI[i].submissions.length; j++)
					if (KANJI[i].submissions[j].priority >= 90)
						arr.add(KANJI[i].submissions[j]);
		
		SUB = new KanjiComposition[arr.size()];
		for (int i=0;i<arr.size();i++)
			SUB[i] = arr.get(i);
	}
	
	public void setLayoutContent()
	{
		btn_1 = (Button)findViewById(R.id.button_kanjiGame2);
		btn_1.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				startActivity(new Intent(KanjiMenuLayoutActivity.this, KanjiGameLayoutActivity.class).putExtra("kanji_data", KANJI));
			}
		});
		
		btn_2 = (Button)findViewById(R.id.button_kanjiList);
		btn_2.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				startActivity(new Intent(KanjiMenuLayoutActivity.this, KanjiInfoLayoutActivity.class).putExtra("kanji_data", KANJI).putExtra("index", 0));
			}
		});
		
		btn_3 = (Button)findViewById(R.id.button_submissionGame);
		btn_3.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				startActivity(new Intent(KanjiMenuLayoutActivity.this, KanjiCompositionGameLayoutActivity.class).putExtra("sub_data", SUB));
			}
		});
		
		btn_4 = (Button)findViewById(R.id.button_submissionList);
		btn_4.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				startActivity(new Intent(KanjiMenuLayoutActivity.this, KanjiCompositionInfoLayoutActivity.class).putExtra("sub_data", SUB).putExtra("index", 0));
			}
		});
	}
	
	public void openLayoutActivity(KanjiDataType [] kanji)
	{
		getSub();
		
		setLayoutContent();
	}
}
