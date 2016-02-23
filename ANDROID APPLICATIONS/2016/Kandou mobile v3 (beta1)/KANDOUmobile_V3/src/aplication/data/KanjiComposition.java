package aplication.data;

import java.io.Serializable;

@SuppressWarnings("serial")
public class KanjiComposition implements Serializable{ // basic vocabulary data object (and kanji composition, as well)
	public String signs;
    public String meaning;
    public String reading;
    public int priority;

    public KanjiComposition() { }

    public KanjiComposition(String signs, String meaning, String reading, int priority)
    {
        this.signs = signs;
        this.meaning = meaning;
        this.priority = priority;
        this.reading = reading;
    }
}
