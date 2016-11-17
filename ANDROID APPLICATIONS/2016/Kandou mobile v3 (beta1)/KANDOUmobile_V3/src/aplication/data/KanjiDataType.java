package aplication.data;

import java.io.Serializable;

@SuppressWarnings("serial")
public class KanjiDataType implements Serializable{ // basic Kanji data objject
	public String sign;
	public String meaning;
	public String reading;

	public KanjiComposition[] submissions;

	public KanjiDataType()	{ }
	
	public KanjiDataType(String sign, String meaning, String reading, KanjiComposition[] submissions)
	{
		this.sign = sign;
		this.meaning = meaning;
		this.reading = reading;
		this.submissions = submissions;
	}
}