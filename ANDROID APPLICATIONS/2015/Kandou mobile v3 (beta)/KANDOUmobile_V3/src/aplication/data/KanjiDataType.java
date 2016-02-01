package aplication.data;

import java.io.Serializable;

@SuppressWarnings("serial")
public class KanjiDataType implements Serializable{
	public String sign;
    public String meaning;
    public String reading;

    public SubmissionOfKanji[] submissions;

    public KanjiDataType()  { }
    
    public KanjiDataType(String sign, String meaning, String reading, SubmissionOfKanji[] submissions)
    {
        this.sign = sign;
        this.meaning = meaning;
        this.reading = reading;
        this.submissions = submissions;
    }
}
