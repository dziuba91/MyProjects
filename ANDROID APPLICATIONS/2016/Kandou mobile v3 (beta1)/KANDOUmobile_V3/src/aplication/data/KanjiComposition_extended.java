package aplication.data;


@SuppressWarnings("serial")
public class KanjiComposition_extended extends KanjiComposition { // extended version for Vocabulary data object (only additional lists)

	public String [] sentences = null;
	public Boolean activity;
	public Boolean selected;
	
    public KanjiComposition_extended(String signs, String meaning, String reading, int priority, String [] sentences)
    {
        this.signs = signs;
        this.meaning = meaning;
        this.priority = priority;
        this.reading = reading;
        this.sentences = sentences;
        this.activity = true;
        this.selected = false;
    }
    
    public KanjiComposition_extended(String signs, String meaning, String reading)
    {
        this.signs = signs;
        this.meaning = meaning;
        this.priority = 80;
        this.reading = reading;
        this.sentences = null;
        this.activity = true;
        this.selected = false;
    }
}
