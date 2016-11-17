package com.kandoumobile_v3.menu.kanji;

import java.util.Arrays;
import java.util.Random;

import com.example.kandoumobile_v3.R;

import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;
import aplication.data.KanjiDataType;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Color;

//'KANJI'/'KANJI Gra'
public class KanjiGameLayoutActivity extends Activity {

	KanjiDataType [] KANJI = null;
	Activity app = null;
	
	int [] kanjiIndex = new int[6];
	int CorectKanji;
	int CorectKanjiIndex;
	
	int score = 0;
	int round = 0;
	
	int bG_id = 0;
	int bR_id = 0;
	
	public Boolean goodAnswer = false;
	
	Button btn1 = null;
	Button btn2 = null;
	Button btn3 = null;
	Button btn4 = null;
	Button btn5 = null;
	Button btn6 = null;
	
	Button bG = null;
	Button bR = null;
	
	TextView text = null;
	TextView scoreText = null;
	
	Random random = new Random();
	
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		setContentView(R.layout.kanji_game_layout);
		
		Object[] array = (Object[]) getIntent().getSerializableExtra("kanji_data");
		this.KANJI = Arrays.copyOf(array, array.length, KanjiDataType[].class);
		
		openLayoutActivity();
	}
	
	public void setLayoutContent()
	{
		btn1 = (Button)findViewById(R.id.b1);
		btn2 = (Button)findViewById(R.id.b2);
		btn3 = (Button)findViewById(R.id.b3);
		btn4 = (Button)findViewById(R.id.b4);
		btn5 = (Button)findViewById(R.id.b5);
		btn6 = (Button)findViewById(R.id.b6);
		
		btn1.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				onClickAction(0);
			}
		});
		
		btn2.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				onClickAction(1);
			}
		});
		
		btn3.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				onClickAction(2);
			}
		});
		
		btn4.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				onClickAction(3);
			}
		});
		
		btn5.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				onClickAction(4);
			}
		});
		
		btn6.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				onClickAction(5);
			}
		});
		
		bG = (Button)findViewById(R.id.button_good);
		bR = (Button)findViewById(R.id.button_wrong);

		bG.setOnClickListener(new OnClickListener(){
			
			@Override
			public void onClick(View arg0) {
				
				startActivity(new Intent(KanjiGameLayoutActivity.this, KanjiInfoLayoutActivity.class).putExtra("kanji_data", KANJI).putExtra("index", bG_id));
			}
		});
		
		bR.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				startActivity(new Intent(KanjiGameLayoutActivity.this, KanjiInfoLayoutActivity.class).putExtra("kanji_data", KANJI).putExtra("index", bR_id));
			}
		});
		
		text = (TextView)findViewById(R.id.mainText);
		scoreText = (TextView)findViewById(R.id.sText);
	}
	
	public void setLayoutData()
	{
		btn1.setText(KANJI[kanjiIndex[0]].sign);
		btn2.setText(KANJI[kanjiIndex[1]].sign);
		btn3.setText(KANJI[kanjiIndex[2]].sign);
		btn4.setText(KANJI[kanjiIndex[3]].sign);
		btn5.setText(KANJI[kanjiIndex[4]].sign);
		btn6.setText(KANJI[kanjiIndex[5]].sign);
		
		text.setText(KANJI[CorectKanji].meaning + "\n" + KANJI[CorectKanji].reading);

		scoreText.setText(score + "/" + round);
	}
	
	public void openLayoutActivity()
	{
		setLayoutContent();
		setGameRound();

		bG.setVisibility(View.INVISIBLE);
		bR.setVisibility(View.INVISIBLE);
	}

	public void correctAnswer(int index)
	{
		bR.setVisibility(View.INVISIBLE);

		bG.setVisibility(View.VISIBLE);
		bG.setBackgroundColor(Color.GREEN);
		bG.setText("	" + KANJI[index].sign + "	 ");
		bG_id = index;
	}

	public void incorrectAnswer(int indexG, int indexR)
	{
		bR.setVisibility(View.VISIBLE);
		bR.setBackgroundColor(Color.RED);
		bR.setText("	" + KANJI[indexR].sign + "	  ");
		bR_id = indexR;

		bG.setVisibility(View.VISIBLE);;
		bG.setBackgroundColor(Color.GREEN);
		bG.setText("	" + KANJI[indexG].sign + "	  ");
		bG_id = indexG;
	}
	
	public void onClickAction(int index)
	{
		if (CorectKanjiIndex == index)
		{
			score++;
			correctAnswer(CorectKanji);

			goodAnswer = true;
		}
		else
		{
			incorrectAnswer(CorectKanji, kanjiIndex[index]);

			goodAnswer = false;
		}

		round++;
		setGameRound();	
	}
	
	public void setGameRound()
	{
		int max_tab = KANJI.length;
		
		for (int i = 0; i < 6; i++)
		{
			int number = random.nextInt(max_tab);

		  check:
			for (int j = 0; j < i; j++)
			{
				if (kanjiIndex[j] == number || number == CorectKanji)
				{
					number++;
					if (number == max_tab) number = 0;
					
					break check;
				}
			}

			kanjiIndex[i] = number;
		}

		CorectKanjiIndex = random.nextInt(6);
		CorectKanji = kanjiIndex[CorectKanjiIndex];

		setLayoutData();
	}
}