package com.kandoumobile_v3.settings;

import java.util.ArrayList;

import com.example.kandoumobile_v3.R;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentManager;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnLongClickListener;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.Toast;
import android.widget.CompoundButton.OnCheckedChangeListener;
import android.widget.LinearLayout;
import android.widget.TextView;
import aplication.code.fragments.AddNewVocabularyFragment;
import aplication.data.KanjiComposition_extended;
import aplication.data.VocabularyList_array;
import aplication.file.manager.ObjectFileManager;

//'OPCJE'/(additional list)
public class SettingsVocabularyLayoutActivity_extended extends FragmentActivity {

	KanjiComposition_extended [] VOC_extended = null;
	
	int site = 0;
	int site_max = 0;
	
	ArrayList <CheckBox> cb = new ArrayList <CheckBox>();
	
	Button b_prev = null;
	Button b_next = null;
	Button b_save = null;
	TextView text = null;
	TextView infoText = null;
	
	public Boolean SAVE = false;
	
	Boolean checkedAllAction = false;

	int INDEX;
	
	VocabularyList_array VOC_additionalList = null;
	
	ObjectFileManager ofm = new ObjectFileManager();

	public enum Direction {
	    NEXT, PREVIOUS, NONE
	}
	
	public enum ItemAction {
	    SELLECT_ALL, UNCHECK_ALL
	}
	
	private enum WindowStatus {
	    EMPTY, NORMAL
	}
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		setContentView(R.layout.voc_list_settings_info_layout);
		
		INDEX = getIntent().getIntExtra("index_data", -1);
		
		this.VOC_additionalList = (VocabularyList_array) getIntent().getSerializableExtra("voclist_data");
		this.VOC_extended = VOC_additionalList.get()[INDEX].getVocabularyArray();
		
		openLayoutActivity(VOC_extended);
	}
	
	public void setMenuAction(ItemAction ia)
	{
		if (ia == ItemAction.SELLECT_ALL)
		{
			if (cb != null)
			{
				for (int i=0; i<cb.size(); i++)
				{
					checkedAllAction = true;
					cb.get(i).setChecked(true);
				}
			}
			else
			{
				Toast toast = Toast.makeText(this, "Brak rekordów do zaznaczenia! Lista pusta.", Toast.LENGTH_LONG);
    			toast.show();
			}
		}
		
		if (ia == ItemAction.UNCHECK_ALL)
		{
			if (cb != null)
			{
				for (int i=0; i<cb.size(); i++) 
				{
					checkedAllAction = true;
					cb.get(i).setChecked(false);
				}
			}
			else
			{
				Toast toast = Toast.makeText(this, "Brak rekordów do odznaczenia! Lista pusta.", Toast.LENGTH_LONG);
    			toast.show();
			}
		}
	}
	
	private void save()
	{
		this.VOC_additionalList.get()[INDEX].setVocabularyArray(this.VOC_extended);
		ofm.saveObject(this.VOC_additionalList, "V_addList.dat");
		
		SAVE = true;
	}
	
	public void setLayoutContent(WindowStatus ws)
	{
		b_save = (Button)findViewById(R.id.button_save);
		b_save.setVisibility(View.GONE);
		b_save.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				save();
				
				b_save.setVisibility(View.GONE);
			}
		});
		
		if (ws == WindowStatus.EMPTY)
		{
			infoText = (TextView)findViewById(R.id.infoText);
			infoText.setText("PUSTA LISTA");
			
			b_prev = (Button)findViewById(R.id.button_previous);
			b_next = (Button)findViewById(R.id.button_next);
			text = (TextView)findViewById(R.id.text_score);
			
			b_prev.setVisibility(View.GONE);
			b_next.setVisibility(View.GONE);
			text.setVisibility(View.GONE);
		}
		else
		{
			infoText = (TextView)findViewById(R.id.infoText);
			infoText.setText("");
			
			b_prev = (Button)findViewById(R.id.button_previous);
			b_prev.setVisibility(View.VISIBLE);
			b_next = (Button)findViewById(R.id.button_next);
			b_next.setVisibility(View.VISIBLE);
		
			b_prev.setOnClickListener(new OnClickListener(){

				@Override
				public void onClick(View arg0) {
				
					setCurrentPositionOnList(Direction.PREVIOUS);
					setLayoutData();
				}
			});
		
			b_next.setOnClickListener(new OnClickListener(){

				@Override
				public void onClick(View arg0) {
				
					setCurrentPositionOnList(Direction.NEXT);
					setLayoutData();
				}
			});
		

			int end_loop = 0;
			if (VOC_extended.length < 100)
				end_loop = VOC_extended.length;
			else
				end_loop = 100;
		
			for (int i = 0; i < end_loop; i++)
			{
				addNewItem(i);
				
				cb.get(i).setText(VOC_extended[i].reading + " - " + VOC_extended[i].signs + " (" + VOC_extended[i].meaning + ")");
				cb.get(i).setChecked(VOC_extended[i].activity);
			}
        
			text = (TextView)findViewById(R.id.text_score);
			text.setVisibility(View.VISIBLE);
			text.setText((site+1) + "/" + (site_max));
		}
	}
	
	private void addNewItem(int i) // add new item (for new vocabulary)
	{
		LinearLayout list = (LinearLayout)findViewById(R.id.ll);
		
		cb.add(new CheckBox(this));
		cb.get(i).setId(i);

		cb.get(i).setOnCheckedChangeListener(new OnCheckedChangeListener(){

			@Override
			public void onCheckedChanged(CompoundButton arg0, boolean arg1) {
			
				if (checkedAllAction)
				{
					VOC_extended[arg0.getId() + (site*100)].activity = arg1;
			
					b_save.setVisibility(View.VISIBLE);
				
					checkedAllAction = false;
				}
			}});
    
		cb.get(i).setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
			
				VOC_extended[arg0.getId() + (site*100)].activity = !VOC_extended[arg0.getId() + (site*100)].activity;
			
				b_save.setVisibility(View.VISIBLE);
			}
		});
		
		cb.get(i).setOnLongClickListener(new OnLongClickListener(){

			@Override
			public boolean onLongClick(final View arg0) {
				
				new AlertDialog.Builder(arg0.getContext())
			    .setTitle("Usuwanie...")
			    .setMessage("Czy napewno chcesz usun¹æ element: " + ((Button)arg0).getText() + " ?")
			    .setPositiveButton("TAK", new DialogInterface.OnClickListener() {
			        public void onClick(DialogInterface dialog, int which) { 
			            // continue with delete
			        	deleteVocabulary(((CheckBox)arg0).getId());
			        }
			     })
			    .setNegativeButton("NIE", new DialogInterface.OnClickListener() {
			        public void onClick(DialogInterface dialog, int which) { 
			            // do nothing
			        }
			     })
			    .setIcon(android.R.drawable.ic_dialog_alert)
			     .show();
				
				return false;
		}});
		
		list.addView(cb.get(i));
	}
	
	public void setCurrentPositionOnList(Direction D)
	{
		if (D == Direction.NEXT)	
		{
			if (site < site_max) site++;
		}
		else if (D == Direction.PREVIOUS)
		{
			if (site > 0) site--;
		}
		
		if (site == site_max - 1) b_next.setVisibility(View.INVISIBLE);
		else b_next.setVisibility(View.VISIBLE);
		
		if (site == 0) b_prev.setVisibility(View.INVISIBLE);
		else b_prev.setVisibility(View.VISIBLE);
	}
	
	public void setLayoutData()
	{
		if (VOC_extended == null)
		{
			infoText.setText("PUSTA LISTA");
			
			b_prev.setVisibility(View.GONE);
			b_next.setVisibility(View.GONE);
			text.setVisibility(View.GONE);
		}
		else
		{
			infoText.setText("");
			
			text.setVisibility(View.VISIBLE);
			
			for (int i=0; i<100; i++)
			{
				if (i+(site*100)<VOC_extended.length)
				{
					cb.get(i).setText(VOC_extended[i+(site*100)].reading + " - " + VOC_extended[i+(site*100)].signs + " (" + VOC_extended[i+(site*100)].meaning + ")");
					cb.get(i).setChecked(VOC_extended[i+(site*100)].activity);
					cb.get(i).setVisibility(View.VISIBLE);
				}
				else
				{
					if (VOC_extended.length<100) break;
				
					cb.get(i).setVisibility(View.GONE);
				}
			}
		
			text.setText((site+1) + "/" + (site_max));
		}
	}
	
	public void openLayoutActivity(KanjiComposition_extended [] voc)
    {
		if (voc != null)
		{
			this.site_max = VOC_extended.length/100;
			if ((VOC_extended.length - (site_max * 100)) > 0) this.site_max++;
		
			setLayoutContent(WindowStatus.NORMAL);
			setCurrentPositionOnList(Direction.NONE);
		}
		else
		{
			setLayoutContent(WindowStatus.EMPTY);
		}

		//setCurrentPositionOnList(Direction.NONE);
    }
	
	@Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
       
    	getMenuInflater().inflate(R.menu.settings_vocabulary_extended_list1, menu);

        return true;
    }
	
	@Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle item selection
    	
    	switch (item.getItemId()) {
        	case R.id.action_sellectAll:
        		setMenuAction(ItemAction.SELLECT_ALL);
        		return true;
        	case R.id.action_uncheckAll:
        		setMenuAction(ItemAction.UNCHECK_ALL);
        		return true;
        	case R.id.action_addNew:
        		FragmentManager fm = getSupportFragmentManager();
        		AddNewVocabularyFragment dialogFragment = new AddNewVocabularyFragment ();
        		dialogFragment.show(fm, "New");
        		return true;
        	default:
        		return super.onOptionsItemSelected(item);
    	}
    }
	
	@Override
	public void onBackPressed() {
		if (SAVE)
		{
			Intent intent = new Intent();
			intent.putExtra("voc_data", this.VOC_additionalList);
			setResult(RESULT_OK, intent);
		}
		
	    super.onBackPressed();
	}
	
	public void addNewVocabulary(String signs, String meaning, String reading) //actualize data objects (after adding vocabulary)
	{
		if (VOC_extended != null)
		{
			KanjiComposition_extended [] array = new KanjiComposition_extended [VOC_extended.length+1];
			for (int i=0; i<VOC_extended.length; i++)
				array[i] = VOC_extended[i];
		
			array[VOC_extended.length] = new KanjiComposition_extended(signs, meaning, reading);
			VOC_extended = array;
		}
		else
		{
			VOC_extended = new KanjiComposition_extended[1];
			VOC_extended[0] = new KanjiComposition_extended(signs, meaning, reading);
		}
		
		this.VOC_additionalList.get()[INDEX].setVocabularyArray(this.VOC_extended);
		ofm.saveObject(this.VOC_additionalList, "V_addList.dat");
		
		if (cb.size() < 100)
			addNewItem(cb.size());
		
		// aktualizacja 
		site = 0;
		this.site_max = VOC_extended.length/100;
		if ((VOC_extended.length - (site_max * 100)) > 0) this.site_max++;
		
		setLayoutData();
		setCurrentPositionOnList(Direction.NONE);
		
		this.SAVE = true;
	}
	
	public void deleteVocabulary(int itemID) //actualize data objects (after deleting vocabulary)
	{
		if (VOC_extended.length > 1)
		{
			KanjiComposition_extended [] array = new KanjiComposition_extended [VOC_extended.length-1];
			for (int i=0, j=0; i<VOC_extended.length; i++)
			{
				if (i == (site * 100 + itemID)) continue;
				
				array[j++] = VOC_extended[i];
			}
	
			VOC_extended = array;
		}
		else
			VOC_extended = null;
		
		this.VOC_additionalList.get()[INDEX].setVocabularyArray(this.VOC_extended);
		ofm.saveObject(this.VOC_additionalList, "V_addList.dat");
		
		if (VOC_extended == null || VOC_extended.length < 100)
		{
			LinearLayout list = (LinearLayout)findViewById(R.id.ll);
			list.removeViewAt(cb.size()-1);
			
			cb.remove(cb.size()-1);
		}
		
		if (VOC_extended != null)
		{
			// aktualizacja 
			site = 0;
			this.site_max = VOC_extended.length/100;
			if ((VOC_extended.length - (site_max * 100)) > 0) this.site_max++;
		}
		
		setLayoutData();
		setCurrentPositionOnList(Direction.NONE);
				
		this.SAVE = true;
	}
}
