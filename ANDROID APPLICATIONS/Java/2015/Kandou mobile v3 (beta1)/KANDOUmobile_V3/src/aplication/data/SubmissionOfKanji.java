package aplication.data;

import java.io.Serializable;

@SuppressWarnings("serial")
public class SubmissionOfKanji implements Serializable{
	public String signs;
    public String meaning;
    public String reading;
    public int priority;

    public SubmissionOfKanji() { }

    public SubmissionOfKanji(String signs, String meaning, String reading, int priority)
    {
        this.signs = signs;
        this.meaning = meaning;
        this.priority = priority;
        this.reading = reading;
    }
}
