using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationInfo : ScriptableObject {
	public List<Langauge> LanguageList;
}

[System.Serializable]
public class Langauge
{
	public string LanguageName;
	public string LangaugeCode;
	public Sprite imgFlag;
	public bool isAvailable;
}