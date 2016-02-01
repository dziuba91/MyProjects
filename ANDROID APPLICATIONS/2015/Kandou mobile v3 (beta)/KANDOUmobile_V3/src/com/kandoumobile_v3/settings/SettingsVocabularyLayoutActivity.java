package com.kandoumobile_v3.settings;

import java.util.Arrays;

import com.example.kandoumobile_v3.R;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.CompoundButton.OnCheckedChangeListener;
import android.widget.LinearLayout;
import android.widget.TextView;
import aplication.data.SubmissionOfKanji;
import aplication.file.manager.ObjectFileManager;

public class SettingsVocabularyLayoutActivity extends Activity {

	SubmissionOfKanji [] VOC = null;
	public Boolean [] perm = null;
	public Boolean [] VOC_sel = null;
	
	int site = 0;
	int site_max = 0;
	int index = 0;
	
	CheckBox[] cb = null;
	
	Button b_prev = null;
	Button b_next = null;
	Button b_save = null;
	TextView text = null;
	
	Activity app = null;
	Menu menu = null;
	
	public Boolean SAVE = false;
	public Boolean ACTIVE = false;
	
	Boolean checkedAllAction = false;
	
	ObjectFileManager ofm = new ObjectFileManager();

	public enum Direction {
	    NEXT, PREVIOUS, NONE
	}
	
	public enum ItemAction {
	    SELLECT_ALL, UNCHECK_ALL
	}
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		setContentView(R.layout.voc_list_settings_info_layout);
		
		Object[] array = (Object[]) getIntent().getSerializableExtra("voc_data");
		this.VOC = Arrays.copyOf(array, array.length, SubmissionOfKanji[].class);
		
		array = (Object[]) getIntent().getSerializableExtra("vocper_data");
		this.perm = Arrays.copyOf(array, array.length, Boolean[].class);
		
		array = (Object[]) getIntent().getSerializableExtra("vocsel_data");
		this.VOC_sel = Arrays.copyOf(array, array.length, Boolean[].class);
		
		openLayoutActivity(VOC, perm);
	}
	
	private void save()
	{
		String s = "";
    	for (int i=0; i<perm.length; i++) 
    	{
    		s += perm[i].toString();
    		if (i < perm.length-1) s += "-";
    	}
    	
    	ofm.saveObjectArray(s, "V_per.dat");
    	
    	//
    	s = "";
    	for (int i=0; i<VOC_sel.length; i++) 
    	{
    		s += VOC_sel[i].toString();
    		if (i < VOC_sel.length-1) s += "-";
    	}
    	
    	ofm.saveObjectArray(s, "V_sel.dat");
	}
	
	public void setMenuAction(ItemAction ia)
	{
		if (ia == ItemAction.SELLECT_ALL)
		{
			for (int i=0; i<100; i++)
			{
				checkedAllAction = true;
				cb[i].setChecked(true);
			}
		}
		
		if (ia == ItemAction.UNCHECK_ALL)
		{
			for (int i=0; i<100; i++) 
			{
				checkedAllAction = true;
				cb[i].setChecked(false);
			}
		}
	}
	
	public void setLayoutContent()
	{
		b_save = (Button)findViewById(R.id.button_save);
		b_save.setVisibility(View.GONE);
		b_save.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View arg0) {
				
				save();
				SAVE = true;
				
				b_save.setVisibility(View.GONE);
			}
		});
		
		b_prev = (Button)findViewById(R.id.button_previous);
		b_next = (Button)findViewById(R.id.button_next);
		
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
		
		LinearLayout list = (LinearLayout)findViewById(R.id.ll);

		if (VOC.length < 100)
			cb = new CheckBox[VOC.length];
		else
			cb = new CheckBox[100];
		
        for (int i = 0; i < cb.length; i++)
        {
            cb[i] = new CheckBox(this);
            cb[i].setText(VOC[i].reading + " - " + VOC[i].signs + " (" + VOC[i].meaning + ")");
            cb[i].setChecked(perm[i]);
            cb[i].setId(i);
            index = cb[i].getId();

            cb[i].setOnCheckedChangeListener(new OnCheckedChangeListener(){

				@Override
				public void onCheckedChanged(CompoundButton arg0, boolean arg1) {
					
					if (checkedAllAction)
					{
						perm[arg0.getId() + (site*100)] = arg1;
						if (!arg1) VOC_sel[arg0.getId() + (site*100)] = false;
    				
						b_save.setVisibility(View.VISIBLE);
						
						checkedAllAction = false;
					}
			}});
            
            cb[i].setOnClickListener(new OnClickListener(){

    			@Override
    			public void onClick(View arg0) {
    				
    				perm[arg0.getId() + (site*100)] = !perm[arg0.getId() + (site*100)];
    				if (!perm[arg0.getId() + (site*100)]) VOC_sel[arg0.getId() + (site*100)] = false;
    				
					b_save.setVisibility(View.VISIBLE);
    			}
    		});
            
            list.addView(cb[i]);
        }
        
        text = (TextView)findViewById(R.id.text_score);
        text.setText((site+1) + "/" + (site_max));
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
		for (int i=0; i<100; i++)
		{
			if (i+(site*100)<VOC.length)
			{
				cb[i].setText(VOC[i+(site*100)].reading + " - " + VOC[i+(site*100)].signs + " (" + VOC[i+(site*100)].meaning + ")");
				cb[i].setChecked(perm[i+(site*100)]);
				cb[i].setVisibility(View.VISIBLE);
			}
			else
				cb[i].setVisibility(View.GONE);
		}
		
        text.setText((site+1) + "/" + (site_max));
	}
	
	private void setMenuItems()
    {
		if (menu != null)
		{
			for (int i=0; i<menu.size(); i++)
			{
				MenuItem MI = this.menu.getItem(i);
				MI.setVisible(true);
			}
		}
    }
	
	public void openLayoutActivity(SubmissionOfKanji [] voc, Boolean [] p)
    {
		this.VOC = voc;
		this.site_max = VOC.length/100;
		if ((VOC.length - (site_max * 100)) > 0) this.site_max++;
		
		this.perm = new Boolean[p.length];
		for (int i=0; i<perm.length; i++)
			perm[i] = p[i];

		this.ACTIVE = true;
		
        setLayoutContent();
        setCurrentPositionOnList(Direction.NONE);
        setMenuItems();
    }
	
	@Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
       
    	getMenuInflater().inflate(R.menu.settings_menu, menu);

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
        	default:
        		return super.onOptionsItemSelected(item);
    	}
    }
	
	@Override
	public void onBackPressed() {
		if (SAVE)
		{
			Intent intent = new Intent();
			intent.putExtra("vocper_data", this.perm);
			intent.putExtra("vocsel_data", this.VOC_sel);
			setResult(RESULT_OK, intent);
		}
		
	    super.onBackPressed();
	}
}
