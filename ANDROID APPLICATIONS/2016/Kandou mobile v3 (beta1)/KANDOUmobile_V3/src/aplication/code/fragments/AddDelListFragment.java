package aplication.code.fragments;

import com.example.kandoumobile_v3.R;
import com.kandoumobile_v3.menu.SettingsMenuLayoutActivity;

import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

// Settings/'Options Item':"Dodaj"or"Usuń"
public class AddDelListFragment extends DialogFragment { // Dialog Fragment which adds or deletes list of vocabulary (additional vocabulary list extension)
    
	public String TXT = "";
	
	private TypeOfOperation dialogType;

	public enum TypeOfOperation {
		ADD, DEL
	}
	
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        
    	Bundle args = getArguments();
    	dialogType = (TypeOfOperation) args.getSerializable("key");
    	
    	View rootView = inflater.inflate(R.layout.fragment_dialog, container, false);
    	if (dialogType == TypeOfOperation.ADD)
    		getDialog().setTitle("Nowa Lista");
    	else
    		getDialog().setTitle("Usuń Listę");
        
        final TextView text = (TextView) rootView.findViewById(R.id.editText1);
        
        Button dismiss = (Button) rootView.findViewById(R.id.dismiss);
        
    	if (dialogType == TypeOfOperation.DEL)
    	{
    		TextView setText = (TextView) rootView.findViewById(R.id.title);
    		setText.setText("Wpisz nazwę listy jaką chcesz usunąć...");
    		
    		dismiss.setText("Usuń");
    	}
    	
        dismiss.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
            	
            	SettingsMenuLayoutActivity callingActivity = (SettingsMenuLayoutActivity) getActivity();
            	
            	String txt = text.getText().toString();
            	if (dialogType == TypeOfOperation.ADD)
            	{
            		if (callingActivity.findList(txt) == -1)
            		{
            			callingActivity.createNewLayoutItem(txt);
            			callingActivity.addNewListToSave(txt);

            			dismiss();
            		}
            		else
            		{
            			Toast toast = Toast.makeText(callingActivity, "Podana nazwa już istnieje! Wybierz inną nazwę...", Toast.LENGTH_LONG);
            			toast.show();
            		}	
            	}
            	else
            	{
            		int index;
            		if ((index = callingActivity.findList(txt)) >= 0)
            		{
            			//
            			callingActivity.deleteListToSave(index);
            			
            			Toast toast = Toast.makeText(callingActivity, "Lista o nazwie: " + txt + " została usunięta!", Toast.LENGTH_LONG);
            			toast.show();
            			
            			dismiss();
            		}
            		else
            		{
            			Toast toast = Toast.makeText(callingActivity, "Lista o nazwie: " + txt + " nie istnieje! Wybierz inną nazwę...", Toast.LENGTH_LONG);
            			toast.show();
            		}
            	}
            }
        });

        return rootView;
    }
}
