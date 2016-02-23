package com.kandoumobile_v3.menu;

import java.util.ArrayList;
import java.util.Arrays;
import android.view.LayoutInflater;

import com.example.kandoumobile_v3.R;
import com.kandoumobile_v3.settings.SettingsVocabularyLayoutActivity;
import com.kandoumobile_v3.settings.SettingsVocabularyLayoutActivity_extended;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentManager;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.LinearLayout;
import aplication.code.fragments.AddDelListFragment;
import aplication.data.KanjiComposition;
import aplication.data.VocabularyList;
import aplication.data.VocabularyList_array;
import aplication.file.manager.ObjectFileManager;

public class SettingsMenuLayoutActivity extends FragmentActivity{
	
	protected static final int EDIT_RESPONSE = 100;
	protected static final int EDIT_RESPONSE_EXTENDED = 110;
	
	KanjiComposition [] VOC = null;
	Boolean [] VOC_sel = null;
	Boolean [] perm = null;
	
	public Boolean SAVE = false;
	public Boolean SAVE_additional = false;
	
	//
	VocabularyList_array VOC_additionalList = null;
	VocabularyList [] VOC_additional = null;
	
	public ArrayList <LinearLayout> additionalLayout = new ArrayList <LinearLayout>();

	ObjectFileManager ofm = new ObjectFileManager();
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		setContentView(R.layout.settings_menu_layout);
		
		Object[] array = (Object[]) getIntent().getSerializableExtra("voc_data");
		if (array == null) this.VOC = null;
		else this.VOC = Arrays.copyOf(array, array.length, KanjiComposition[].class);
		
		array = (Object[]) getIntent().getSerializableExtra("vocper_data");
		if (array == null) this.perm = null;
		else this.perm = Arrays.copyOf(array, array.length, Boolean[].class);
		
		array = (Object[]) getIntent().getSerializableExtra("vocsel_data");
		if (array == null) this.VOC_sel = null;
		else this.VOC_sel = Arrays.copyOf(array, array.length, Boolean[].class);
		
		this.VOC_additionalList = (VocabularyList_array) getIntent().getSerializableExtra("addData_data");
		
		openLayoutActivity();
	}
	
	public void setLayoutContent()
	{
		Button btn_1 = (Button)findViewById(R.id.button_vocSet1);
		btn_1.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
        		
				startActivityForResult(new Intent(SettingsMenuLayoutActivity.this, SettingsVocabularyLayoutActivity.class).putExtra("voc_data", VOC)
						.putExtra("vocper_data", perm).putExtra("vocsel_data", VOC_sel), EDIT_RESPONSE);
			}
		});
		
		if (VOC_additionalList.get() != null)
		{
			VOC_additional = VOC_additionalList.get();
					
			for (int i = 0; i < VOC_additional.length; i++)
				createNewLayoutItem(VOC_additional[i].name);
		}
		else
			this.VOC_additional = null;
	}
	
	public void openLayoutActivity()
    {
        setLayoutContent();
    }
	
	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
	     if (requestCode == EDIT_RESPONSE) {
	         if (resultCode == RESULT_OK) {

	        	 Object [] array = (Object[]) data.getSerializableExtra("vocsel_data");
	        	 this.VOC_sel = Arrays.copyOf(array, array.length, Boolean[].class);
	     			
	        	 array = (Object[]) data.getSerializableExtra("vocper_data");
	        	 this.perm = Arrays.copyOf(array, array.length, Boolean[].class);
	     			
	        	 SAVE = true;
	         }
	     }
	     if (requestCode == EDIT_RESPONSE_EXTENDED) {
	    	 if (resultCode == RESULT_OK) {
	    		 
	    		 this.VOC_additionalList = (VocabularyList_array) data.getSerializableExtra("voc_data");
	    		 this.VOC_additional = VOC_additionalList.get();
	    		 
	    		 this.SAVE = true;
	    	 }
	     }
	}
	
	@Override
	public void onBackPressed() {
		if (SAVE)
		{
			if (SAVE_additional) ofm.saveObject(this.VOC_additionalList, "V_addList.dat");
			
			Intent intent = new Intent();
			intent.putExtra("vocper_data", this.perm);
			intent.putExtra("vocsel_data", this.VOC_sel);
			intent.putExtra("addData_data", this.VOC_additionalList);
			setResult(RESULT_OK, intent);
		}
		
	    super.onBackPressed();
	}
	
	@Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
       
    	getMenuInflater().inflate(R.menu.menu_settings, menu);

        return true;
    }
	
	@Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle item selection
    	
    	switch (item.getItemId()) {
        	case R.id.action_showDialog:
        		Bundle args = new Bundle();
        		args.putSerializable("key", AddDelListFragment.TypeOfOperation.ADD);
        		
				FragmentManager fm = getSupportFragmentManager();
        		AddDelListFragment dialogFragment = new AddDelListFragment ();
        		dialogFragment.setArguments(args);
        		dialogFragment.show(fm, "New");
        		return true;
        		
        	case R.id.action_deleteDialog:
        		Bundle args1 = new Bundle();
        		args1.putSerializable("key", AddDelListFragment.TypeOfOperation.DEL);
        		
				FragmentManager fm1 = getSupportFragmentManager();
        		AddDelListFragment dialogFragment1 = new AddDelListFragment ();
        		dialogFragment1.setArguments(args1);
        		dialogFragment1.show(fm1, "Delete");
        		return true;
        		
        	default:
        		return super.onOptionsItemSelected(item);
    	}
    }
	
	public void createNewLayoutItem(String value) // Adds new button (for new additional vocabulary list)
	{
		LinearLayout list = (LinearLayout)findViewById(R.id.main);
		
		LayoutInflater inflater = (LayoutInflater) this.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		LinearLayout newLayout = (LinearLayout)inflater.inflate(R.layout.fragment_button1, null);
		
		Button btn_1 = (Button)newLayout.findViewById(R.id.button_vocSet1);
		btn_1.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
        		
				startActivityForResult(new Intent(SettingsMenuLayoutActivity.this, SettingsVocabularyLayoutActivity_extended.class).putExtra("voclist_data", VOC_additionalList)
						.putExtra("index_data", findList(((Button)arg0).getText().toString())), EDIT_RESPONSE_EXTENDED);
			}
		});
		
		additionalLayout.add(newLayout);
		
		Button b = (Button)additionalLayout.get(additionalLayout.size()-1).findViewById(R.id.button_vocSet1);
		b.setText(value);
		
		list.addView(newLayout);
	}
	
	public void addNewListToSave(String value) //actualize additional lists objects (after adding)
	{
		int index = 0;
		
		if (VOC_additional != null) index = VOC_additional.length;
		VocabularyList[] tmp = new VocabularyList[index+1];
		for (int i=0; i<index; i++) tmp[i] = this.VOC_additional[i];
		tmp[index]= new VocabularyList(value, null, true);
		this.VOC_additional = tmp;
		
		VOC_additionalList.set(this.VOC_additional);
		
		this.SAVE_additional = true;
		this.SAVE = true;
	}
	
	//
	public int findList(String value)
	{
		if (VOC_additional != null)
			for (int i=0; i<VOC_additional.length; i++)
				if (VOC_additional[i].name.equals(value)) return i;
		
		return -1;
	}
	
	public void deleteListToSave(int index) //actualize additional lists objects (after deleting)
	{
		LinearLayout layout = additionalLayout.get(index);
		layout.removeAllViews();
		additionalLayout.remove(index);
		
		if (VOC_additional.length > 1)
		{
			VocabularyList[] tmp = new VocabularyList[VOC_additional.length-1];
			for (int i=0, j=0; i<VOC_additional.length; i++)
			{
				if (index == i) continue;
				
				tmp[j++] = this.VOC_additional[i];
			}
			
			this.VOC_additional = tmp;
		}
		else
			VOC_additional = null;
		
		VOC_additionalList.set(this.VOC_additional);
		
		this.SAVE_additional = true;
		this.SAVE = true;
	}
}