package com.kandoumobile_v3;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.Arrays;

import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserException;
import org.xmlpull.v1.XmlPullParserFactory;

import com.example.kandoumobile_v3.R;
import com.kandoumobile_v3.menu.KanjiMenuLayoutActivity;
import com.kandoumobile_v3.menu.VocabularyMenuLayoutActivity;
import com.kandoumobile_v3.settings.SettingsVocabularyLayoutActivity;

import android.os.Bundle;
import android.os.Environment;
import android.app.Activity;
import android.content.Intent;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ImageButton;


import aplication.data.*;
import aplication.file.manager.ObjectFileManager;

public class MainActivity extends Activity {

	protected static final int EDIT_RESPONSE = 100;

	protected static final int EDIT_RESPONSE_SETTINGS = 110;

	SettingsVocabularyLayoutActivity settingsLayout = null;
	
	KanjiDataType [] KANJI = null;
	KanjiDataType [] KANJI_accepted = null;
	Boolean [] KANJI_permit = null;
	Boolean [] VOC_permit = null;
	Boolean [] VOC_selected = null;
	SelectiveData [] VOC_selectedAccepted = null;
	SubmissionOfKanji [] VOC = null;
	SubmissionOfKanji [] VOC_accepted = null;
	Activity a = null;
	ImageButton btn = null;
	ImageButton btn2 = null;
	ImageButton btn3 = null;

	VocabularyMenuLayoutActivity vocMenu = null;
	
	ObjectFileManager ofm = null;
	
	public MainActivity()
	{
		a = this;
		
		ofm = new ObjectFileManager();
	}
	
	public void setMainMenu()
	{
		setContentView(R.layout.activity_main);
		
        btn = (ImageButton)findViewById(R.id.kanji_menu); // KANJI menu
        btn.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				startActivity(new Intent(MainActivity.this, KanjiMenuLayoutActivity.class).putExtra("kanji_data", KANJI));
			}
    	});
        
        btn2 = (ImageButton)findViewById(R.id.vocabulary_menu); // VOCABULARY menu
        if (VOC_accepted == null) btn2.setEnabled(false);
        btn2.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				startActivityForResult(new Intent(MainActivity.this, VocabularyMenuLayoutActivity.class).putExtra("voc_data", VOC_accepted)
						.putExtra("vocdes_data", VOC_selectedAccepted).putExtra("vocsel_data", VOC_selected)
						, EDIT_RESPONSE);
			}
    	});
        
        btn3 = (ImageButton)findViewById(R.id.settings_menu);
        btn3.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				startActivityForResult(new Intent(MainActivity.this, SettingsVocabularyLayoutActivity.class).putExtra("voc_data", VOC)
						.putExtra("vocper_data", VOC_permit).putExtra("vocsel_data", VOC_selected), EDIT_RESPONSE_SETTINGS);
			}
    	});
	}
	
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        
        getData();

        instalAplicationPath();
        
        setMainMenu();
    }

    void instalAplicationPath()
    {
        String sdCardPath = Environment.getExternalStorageDirectory().getPath();
        File f1 = new File(sdCardPath + "/KandouData_V3");
        
        if (!f1.exists())
        {
        	f1.mkdir();
        	
        	KANJI_permit = createGameData(KANJI, "K_per.dat");
        	VOC_permit = createGameData(VOC, "V_per.dat");
        	VOC_selected = createGameData(VOC, "V_sel.dat");
        }
        else
        {
        	KANJI_permit = ofm.readObjectFromFile_Boolean("K_per.dat");
        	VOC_permit = ofm.readObjectFromFile_Boolean("V_per.dat");
        	VOC_selected = ofm.readObjectFromFile_Boolean("V_sel.dat");
        }
        
        setGameData();
    }
    
	void setGameData()
    {
    	ArrayList<SubmissionOfKanji> arr = new ArrayList<SubmissionOfKanji>();
    	ArrayList<SelectiveData> arr_sel = new ArrayList<SelectiveData>();
    	
    	for (int i=0; i<VOC.length; i++)
    	{
    		if (VOC_permit[i])
    		{
    			arr.add(VOC[i]);
    			arr_sel.add(new SelectiveData(i, VOC_selected[i]));
    		}
    	}
    	
    	if (arr.size()>0)
    	{
    		VOC_accepted = new SubmissionOfKanji[arr.size()];
    		VOC_selectedAccepted = new SelectiveData[arr.size()];
    		for (int i=0; i<arr.size(); i++)
    		{
    			VOC_accepted[i] = (SubmissionOfKanji) arr.get(i);
    			VOC_selectedAccepted[i] = (SelectiveData) arr_sel.get(i);
    		}
    	}
    	else
    		VOC_accepted = null;
    }
    
    Boolean [] createGameData(KanjiDataType [] K, String path)
    {
    	Boolean [] permit = new Boolean[K.length];
    	
    	String s = "";
    	for (int i=0; i<K.length; i++) 
    	{
    		permit[i] = false;
    		
    		s += permit[i].toString();
    		if (i < K.length-1) s += "-";
    	}
    	
    	ofm.saveObjectArray(s, path);
    	
    	return permit;
    }
    
    Boolean [] createGameData(SubmissionOfKanji [] Voc, String path)
    {
    	Boolean [] permit = new Boolean[Voc.length];
    	
    	String s = "";
    	for (int i=0; i<Voc.length; i++) 
    	{
    		permit[i] = false;
    		
    		s += permit[i].toString();
    		if (i < Voc.length-1) s += "-";
    	}
    	
    	ofm.saveObjectArray(s, path);
    	
    	return permit;
    }
    
    private void getData()
    {
    	XmlPullParserFactory pullParserFactory;
		try {
			pullParserFactory = XmlPullParserFactory.newInstance();
			XmlPullParser parser = pullParserFactory.newPullParser();

			    InputStream in_s = getApplicationContext().getAssets().open("XMLFile1.xml");
		        parser.setFeature(XmlPullParser.FEATURE_PROCESS_NAMESPACES, false);
	            parser.setInput(in_s, null);

	            parseXML1(parser);
	            
	            //
	            InputStream in_s2 = getApplicationContext().getAssets().open("XMLFile2.xml");
		        parser.setFeature(XmlPullParser.FEATURE_PROCESS_NAMESPACES, false);
	            parser.setInput(in_s2, null);
	            
	            parseXML2(parser);

		} catch (XmlPullParserException e) {

			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
    }
    
	private void parseXML2(XmlPullParser parser) throws XmlPullParserException,IOException
	{
		ArrayList<SubmissionOfKanji> vocabularyList = null;
        int eventType = parser.getEventType();
        SubmissionOfKanji currentVocabulary = null;
        
        while (eventType != XmlPullParser.END_DOCUMENT){
            String name = null;
            switch (eventType){
                case XmlPullParser.START_DOCUMENT:
                	vocabularyList = new ArrayList<SubmissionOfKanji>();
                    break;
                case XmlPullParser.START_TAG:
                    name = parser.getName();
                    if (name.equalsIgnoreCase("Item")){
                    	currentVocabulary = new SubmissionOfKanji();
                    } else if (currentVocabulary != null){
                        if (name.equalsIgnoreCase("signs")){
                        	currentVocabulary.signs = parser.nextText();
                        } else if (name.equalsIgnoreCase("reading")){
                        	currentVocabulary.reading = parser.nextText();
                        } else if (name.equalsIgnoreCase("meaning")){
                        	currentVocabulary.meaning = parser.nextText();
                        }  else if (name.equalsIgnoreCase("priority")){
                        	currentVocabulary.priority = Integer.parseInt(parser.nextText());
                        }  
                    } 
                    break;
                case XmlPullParser.END_TAG:
                    name = parser.getName();
                    if (name.equalsIgnoreCase("Item")){
                    	vocabularyList.add(currentVocabulary);
                    }
            }
            eventType = parser.next();
        }
        
        VOC = new SubmissionOfKanji[vocabularyList.size()];
        for (int i=0;i<vocabularyList.size();i++)
        {
        	VOC[i] = vocabularyList.get(i);
        }
	}
    
    private void parseXML1(XmlPullParser parser) throws XmlPullParserException,IOException
	{
		ArrayList<KanjiDataType> kanjiList = null;
		ArrayList<SubmissionOfKanji> submissions = null;
        int eventType = parser.getEventType();
        KanjiDataType currentKanji = null;
        SubmissionOfKanji currentSubmission = null;
        
        Boolean kanjiLevel = false;
        Boolean submissionLevel = false;
        
        while (eventType != XmlPullParser.END_DOCUMENT){
            String name = null;
            switch (eventType){
                case XmlPullParser.START_DOCUMENT:
                	kanjiList = new ArrayList<KanjiDataType>();
                    break;
                case XmlPullParser.START_TAG:
                    name = parser.getName();
                    if (name.equalsIgnoreCase("Kanji")){
                    	currentKanji = new KanjiDataType();
                    	kanjiLevel = true;
                    	submissionLevel = false;
                    } else if (name.equalsIgnoreCase("submissions")){
                    	submissions = new ArrayList<SubmissionOfKanji>();
                    } else if (name.equalsIgnoreCase("submission")){
                    	currentSubmission = new SubmissionOfKanji();
                    	submissionLevel = true;
                    	kanjiLevel = false;
                    } else if ((currentKanji != null) && (kanjiLevel == true)){
                        if (name.equalsIgnoreCase("sign")){
                        	currentKanji.sign = parser.nextText();
                        } else if (name.equalsIgnoreCase("reading")){
                        	currentKanji.reading = parser.nextText();
                        } else if (name.equalsIgnoreCase("meaning")){
                        	currentKanji.meaning = parser.nextText();
                        }  
                    } else if ((currentSubmission != null) && (submissionLevel == true)){
                        if (name.equalsIgnoreCase("signs")){
                        	currentSubmission.signs = parser.nextText();
                        } else if (name.equalsIgnoreCase("reading")){
                        	currentSubmission.reading = parser.nextText();
                        } else if (name.equalsIgnoreCase("meaning")){
                        	currentSubmission.meaning = parser.nextText();
                        } else if (name.equalsIgnoreCase("priority")){
                        	currentSubmission.priority = Integer.parseInt(parser.nextText());
                        }  
                    }
                    break;
                case XmlPullParser.END_TAG:
                    name = parser.getName();
                    if (name.equalsIgnoreCase("Kanji") && currentKanji != null){
                    	kanjiList.add(currentKanji);
                    } else if (name.equalsIgnoreCase("submission") && currentSubmission != null){
                    	submissions.add(currentSubmission);
                    } else if (name.equalsIgnoreCase("submissions") && submissions != null){
                    	if (submissions.size() > 0)
                    	{
                    		currentKanji.submissions = new SubmissionOfKanji[submissions.size()];
                    		for (int i=0;i<submissions.size();i++) {
                    			currentKanji.submissions[i] = submissions.get(i);
                    		}
                    	}
                    }
            }
            eventType = parser.next();
        }
        
        KANJI = new KanjiDataType[kanjiList.size()];
        for (int i=0;i<kanjiList.size();i++)
        {
        	KANJI[i] = kanjiList.get(i);
        }
	}
    
    @Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
	     if (requestCode == EDIT_RESPONSE) {
	         if (resultCode == RESULT_OK) {

	        	 	Object [] array = (Object[]) data.getSerializableExtra("vocsel_data");
	     			this.VOC_selected = Arrays.copyOf(array, array.length, Boolean[].class);
	     			
	     			array = (Object[]) data.getSerializableExtra("vocdesc_data");
	     			this.VOC_selectedAccepted = Arrays.copyOf(array, array.length, SelectiveData[].class);
	         }
	     }
	     else if (requestCode == EDIT_RESPONSE_SETTINGS) {
	         if (resultCode == RESULT_OK) {

	        	 	Object [] array = (Object[]) data.getSerializableExtra("vocper_data");
	     			this.VOC_permit = Arrays.copyOf(array, array.length, Boolean[].class);
	     			
	     			array = (Object[]) data.getSerializableExtra("vocsel_data");
	     			this.VOC_selected = Arrays.copyOf(array, array.length, Boolean[].class);
	     			
	     			setGameData();
	     			
	     			if (VOC_accepted == null) btn2.setEnabled(false);
	     			else btn2.setEnabled(true);
	         }
	     }
	}
}
