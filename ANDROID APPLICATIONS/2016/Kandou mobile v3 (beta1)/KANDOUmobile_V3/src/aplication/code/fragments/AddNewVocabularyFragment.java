package aplication.code.fragments;

import com.example.kandoumobile_v3.R;
import com.kandoumobile_v3.settings.SettingsVocabularyLayoutActivity_extended;

import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

//Settings/(one of additional list)/'Options Item':"Dodaj"
public class AddNewVocabularyFragment extends DialogFragment { // Dialog Fragment which adds vocabulary (additional vocabulary list extension)
    
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        
    	View rootView = inflater.inflate(R.layout.fragment_new_vocabulary_dialog, container, false);
    	getDialog().setTitle("Nowe Słowo");
        
        final TextView text_kanji = (TextView) rootView.findViewById(R.id.editText_kanji);
        final TextView text_meaning = (TextView) rootView.findViewById(R.id.editText_meaning);
        final TextView text_reading = (TextView) rootView.findViewById(R.id.editText_reading);
        
        Button dismiss = (Button) rootView.findViewById(R.id.dismiss);
        dismiss.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
            	
            	SettingsVocabularyLayoutActivity_extended callingActivity = (SettingsVocabularyLayoutActivity_extended) getActivity();
            	
            	String txt1 = text_kanji.getText().toString();
            	String txt2 = text_meaning.getText().toString();
            	String txt3 = text_reading.getText().toString();
            	
            	if (txt1.equals("") && txt2.equals("") && txt3.equals(""))
            	{
            		Toast toast = Toast.makeText(callingActivity, "Wszystkie pola puste! Uzupełnij wskazane pola...", Toast.LENGTH_LONG);
        			toast.show();
            	}
            	else
            		callingActivity.addNewVocabulary(txt1, txt2, txt3);
            			
            	dismiss();
            }
        });

        return rootView;
    }
}
