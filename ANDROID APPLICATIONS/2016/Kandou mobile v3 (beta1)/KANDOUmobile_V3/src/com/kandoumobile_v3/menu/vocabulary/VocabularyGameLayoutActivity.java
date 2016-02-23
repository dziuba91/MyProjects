package com.kandoumobile_v3.menu.vocabulary;

import java.util.Arrays;
import java.util.Random;

import com.example.kandoumobile_v3.R;

import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;
import aplication.data.SelectiveAdditionalIndexes;
import aplication.data.SelectiveData;
import aplication.data.KanjiComposition;
import aplication.data.VocabularyList_array;
import aplication.vocabulary_part.classes.AdditionalVocabularyData_Extensions;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Color;

//'S£ÓWKA'/'JAP <-> PL Gra' or 'S£ÓWKA'/'PL <-> JAP Gra'
public class VocabularyGameLayoutActivity extends Activity{

	protected static final int EDIT_RESPONSE = 100;
	Boolean textVisibility = true;
	
	KanjiComposition [] VOC = null;
	public SelectiveData [] VOC_description = null;
	public Boolean [] VOC_selected = null;
	Activity app = null;
	
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

	VocabularyList_array VOC_additionalList = null;
	SelectiveAdditionalIndexes [] VOC_additionalList_acceptedIndexes = null;
	
	AdditionalVocabularyData_Extensions ext = null; // extension
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		this.mode = (Mode)getIntent().getSerializableExtra("mode");
		
		if (mode == Mode.JAP_POL) setContentView(R.layout.submission_game_layout);
		else setContentView(R.layout.vocabulary_pl_jap_game_layout);
		
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
		
		bG = (Button)findViewById(R.id.button_good2);
		bR = (Button)findViewById(R.id.button_wrong2);
		bR.setTextSize(14);
		bG.setTextSize(14);
		
		bG.setOnClickListener(new OnClickListener(){
			
			@Override
			public void onClick(View arg0) {
				
				startActivityForResult(new Intent(VocabularyGameLayoutActivity.this, VocabularyInfoLayoutActivity.class).putExtra("voc_data", VOC)
						.putExtra("vocdesc_data", VOC_description).putExtra("vocsel_data", VOC_selected).putExtra("index", bG_id)
						.putExtra("vocadd_data", VOC_additionalList).putExtra("vocadd_index", VOC_additionalList_acceptedIndexes)
						, EDIT_RESPONSE);
			}
		});
		
		bR.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				startActivityForResult(new Intent(VocabularyGameLayoutActivity.this, VocabularyInfoLayoutActivity.class).putExtra("voc_data", VOC)
						.putExtra("vocdesc_data", VOC_description).putExtra("vocsel_data", VOC_selected).putExtra("index", bR_id)
						.putExtra("vocadd_data", VOC_additionalList).putExtra("vocadd_index", VOC_additionalList_acceptedIndexes)
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
			btn1.setText(ext.getDataByIndex(vocIndex[0]).meaning);
			btn2.setText(ext.getDataByIndex(vocIndex[1]).meaning);
			btn3.setText(ext.getDataByIndex(vocIndex[2]).meaning);
			btn4.setText(ext.getDataByIndex(vocIndex[3]).meaning);
			btn5.setText(ext.getDataByIndex(vocIndex[4]).meaning);
			btn6.setText(ext.getDataByIndex(vocIndex[5]).meaning);
		
			text1.setText(ext.getDataByIndex(CorectVoc).reading);
			
			text2.setText(ext.getDataByIndex(CorectVoc).signs);
		}
		else
		{
			btn1.setText(ext.getDataByIndex(vocIndex[0]).reading);
			btn2.setText(ext.getDataByIndex(vocIndex[1]).reading);
			btn3.setText(ext.getDataByIndex(vocIndex[2]).reading);
			btn4.setText(ext.getDataByIndex(vocIndex[3]).reading);
			btn5.setText(ext.getDataByIndex(vocIndex[4]).reading);
			btn6.setText(ext.getDataByIndex(vocIndex[5]).reading);
		
			text1.setText(ext.getDataByIndex(CorectVoc).meaning);
		}

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
        bG.setText("  " + ext.getDataByIndex(index).reading + "  ");
        bG_id = index;
    }

    public void incorrectAnswer(int indexG, int indexR)
    {
        bR.setVisibility(View.VISIBLE);
        bR.setBackgroundColor(Color.RED);
        bR.setText("  " + ext.getDataByIndex(indexR).reading + "  ");
        bR_id = indexR;

        bG.setVisibility(View.VISIBLE);;
        bG.setBackgroundColor(Color.GREEN);
        bG.setText("  " + ext.getDataByIndex(indexG).reading + "  ");
        bG_id = indexG;
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
        int max_tab = 0;
        if (VOC!=null) max_tab += VOC.length;
        if (VOC_additionalList_acceptedIndexes!=null)
        	max_tab +=  VOC_additionalList_acceptedIndexes[VOC_additionalList_acceptedIndexes.length-1].ID;
        
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
