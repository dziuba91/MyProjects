package com.kandoumobile_v3.menu.vocabulary;

import java.util.Arrays;
import java.util.Random;

import com.example.kandoumobile_v3.R;

import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;
import aplication.data.SelectiveData;
import aplication.data.SubmissionOfKanji;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Color;

public class VocabularyGameLayoutActivity extends Activity{

	protected static final int EDIT_RESPONSE = 100;
	Boolean textVisibility = true;
	
	SubmissionOfKanji [] VOC = null;
	public SelectiveData [] VOC_description = null;
	public Boolean [] VOC_selected = null;
	Activity app = null;
	
	public VocabularyInfoLayoutActivity subInfo = null;
	VocabularyGameLayoutActivity subGame = null;
	
	int [] vocIndex = new int[6];
	int CorectVoc;
	int CorectVocIndex;
	
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
	
	TextView text1 = null;
	TextView text2 = null;
	TextView scoreText = null;
	
	public Boolean SAVE = false;
	
    Random random = new Random();
    
    public enum Mode {
	    JAP_POL, POL_JAP
	}
    
    Mode mode = null;
    
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		this.mode = (Mode)getIntent().getSerializableExtra("mode");
		
		if (mode == Mode.JAP_POL) setContentView(R.layout.submission_game_layout);
		else setContentView(R.layout.vocabulary_pl_jap_game_layout);
		
		Object [] array = (Object[]) getIntent().getSerializableExtra("voc_data");
		this.VOC = Arrays.copyOf(array, array.length, SubmissionOfKanji[].class);
		
		array = (Object[]) getIntent().getSerializableExtra("vocdesc_data");
		this.VOC_description = Arrays.copyOf(array, array.length, SelectiveData[].class);
		
		array = (Object[]) getIntent().getSerializableExtra("vocsel_data");
		this.VOC_selected = Arrays.copyOf(array, array.length, Boolean[].class);
		
		openLayoutActivity(VOC, VOC_description, VOC_selected, mode);
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
		
		bG = (Button)findViewById(R.id.button_good2);
		bR = (Button)findViewById(R.id.button_wrong2);
		bR.setTextSize(14);
		bG.setTextSize(14);
		
		bG.setOnClickListener(new OnClickListener(){
			
			@Override
			public void onClick(View arg0) {
				
				startActivityForResult(new Intent(VocabularyGameLayoutActivity.this, VocabularyInfoLayoutActivity.class).putExtra("voc_data", VOC)
						.putExtra("vocdesc_data", VOC_description).putExtra("vocsel_data", VOC_selected).putExtra("index", bG_id)
						, EDIT_RESPONSE);
			}
		});
		
		bR.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				startActivityForResult(new Intent(VocabularyGameLayoutActivity.this, VocabularyInfoLayoutActivity.class).putExtra("voc_data", VOC)
						.putExtra("vocdesc_data", VOC_description).putExtra("vocsel_data", VOC_selected).putExtra("index", bR_id)
						, EDIT_RESPONSE);
			}
		});
		
		if (mode == Mode.JAP_POL)
		{
			text1 = (TextView)findViewById(R.id.mainText1);
			text2 = (TextView)findViewById(R.id.mainText2);
		
			if (!textVisibility) text2.setVisibility(View.INVISIBLE);
			else text2.setVisibility(View.VISIBLE);
			
			text1.setOnClickListener(new OnClickListener(){

				@Override
				public void onClick(View arg0) {
				
					if (text2.getVisibility() == View.VISIBLE) 
						text2.setVisibility(View.INVISIBLE);
					else
						text2.setVisibility(View.VISIBLE);
					
					textVisibility = !textVisibility;
				}
			});
		
			text2.setOnClickListener(new OnClickListener(){

				@Override
				public void onClick(View arg0) {
				
					if (text2.getVisibility() == View.VISIBLE) 
						text2.setVisibility(View.INVISIBLE);
					else
						text2.setVisibility(View.VISIBLE);

					textVisibility = !textVisibility;
				}
			});
		}
		else
			text1 = (TextView)findViewById(R.id.mainText);
		
		scoreText = (TextView)findViewById(R.id.sText);
	}
	
	public void setLayoutData()
	{	
		if (mode == Mode.JAP_POL)
		{
			btn1.setText(VOC[vocIndex[0]].meaning);
			btn2.setText(VOC[vocIndex[1]].meaning);
			btn3.setText(VOC[vocIndex[2]].meaning);
			btn4.setText(VOC[vocIndex[3]].meaning);
			btn5.setText(VOC[vocIndex[4]].meaning);
			btn6.setText(VOC[vocIndex[5]].meaning);
		
			text1.setText(VOC[CorectVoc].reading);
			
			text2.setText(VOC[CorectVoc].signs);
		}
		else
		{
			btn1.setText(VOC[vocIndex[0]].reading);
			btn2.setText(VOC[vocIndex[1]].reading);
			btn3.setText(VOC[vocIndex[2]].reading);
			btn4.setText(VOC[vocIndex[3]].reading);
			btn5.setText(VOC[vocIndex[4]].reading);
			btn6.setText(VOC[vocIndex[5]].reading);
		
			text1.setText(VOC[CorectVoc].meaning);
		}

        scoreText.setText(score + "/" + round);
	}
	
	public void openLayoutActivity(SubmissionOfKanji [] sub, SelectiveData [] sd, Boolean [] vs, Mode m)
    {
		this.VOC = sub;
		this.VOC_description = sd;
		this.VOC_selected = vs;
		this.mode = m;
		
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
        bG.setText("  " + VOC[index].reading + "  ");
        bG_id = index;
    }
    
    public void correctAnswer()
    {
        bR.setVisibility(View.INVISIBLE);

        bG.setVisibility(View.VISIBLE);
        bG.setBackgroundColor(Color.GREEN);
        bG.setText("  " + VOC[bG_id].reading + "  ");
    }

    public void incorrectAnswer(int indexG, int indexR)
    {
        bR.setVisibility(View.VISIBLE);
        bR.setBackgroundColor(Color.RED);
        bR.setText("  " + VOC[indexR].reading + "  ");
        bR_id = indexR;

        bG.setVisibility(View.VISIBLE);;
        bG.setBackgroundColor(Color.GREEN);
        bG.setText("  " + VOC[indexG].reading + "  ");
        bG_id = indexG;
    }
    
    public void incorrectAnswer()
    {
        bR.setVisibility(View.VISIBLE);
        bR.setBackgroundColor(Color.RED);
        bR.setText("  " + VOC[bR_id].reading + "  ");

        bG.setVisibility(View.VISIBLE);;
        bG.setBackgroundColor(Color.GREEN);
        bG.setText("  " + VOC[bG_id].reading + "  ");
    }

    public void onClickAction(int index)
    {
    	if (CorectVocIndex == index)
        {
            score++;
            correctAnswer(CorectVoc);

            goodAnswer = true;
        }
        else
        {
            incorrectAnswer(CorectVoc, vocIndex[index]);

            goodAnswer = false;
        }

        round++;
        setGameRound();	
    }
    
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
        CorectVoc = vocIndex[CorectVocIndex];

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
}
