﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.Linq;
using System;

public class LocalizationManager : Singleton<LocalizationManager>
{
	XDocument xDocLocalizedText;
	XElement xEleRoot;
	 
	[System.NonSerialized] public string defaultLangCode = "EN";
	string currentLangCode = "EN";

	public static event Action<string> OnLanguageChangedEvent;
	public LocalizationInfo localizationInfo;

	void Awake ()
	{
		///Check if manual langauges is selected by user, untill user selects language manually, device language will be set for app automatically.
		if (PlayerPrefs.HasKey ("currentLangCode")) {
			currentLangCode = PlayerPrefs.GetString ("currentLangCode", defaultLangCode);
		} else {
			currentLangCode = GetSystemLanguageCode ();
		}

		InitLanguageXML (currentLangCode, ref xDocLocalizedText);

		if (xDocLocalizedText != null) {
			xEleRoot = xDocLocalizedText.Element ("Resources");
		}
	}

	/// <summary>
	/// Sets the language.
	/// </summary>
	/// <param name="langCode">Lang code.</param>
	public void SetLanguage (string langCode)
	{
		InitLanguageXML (langCode, ref xDocLocalizedText);

		if (xDocLocalizedText != null) {
			xEleRoot = xDocLocalizedText.Element ("Resources");
			PlayerPrefs.SetString ("currentLangCode", langCode);

			currentLangCode = langCode;
			if (OnLanguageChangedEvent != null) {
				OnLanguageChangedEvent.Invoke (currentLangCode);
			}
		} else {
			xDocLocalizedText = XDocument.Parse (Resources.Load ("strings-" + currentLangCode.ToLower ()).ToString ());
		}
	}

	/// <summary>
	/// Gets the current language code.
	/// </summary>
	/// <returns>The current language code.</returns>
	public String getCurrentLanguageCode()
	{
		return currentLangCode;
	}

	public string GetLocalizedTextForTag (string tag)
	{
		if (xEleRoot != null) {
			XElement ele = xEleRoot.Descendants ("string").FirstOrDefault (el => el.Attribute ("name").Value == tag);

			if (ele != null) {
				return (ele.Attribute ("text").Value);
			}
		}
		return null;
	}

	public Sprite GetLocalizedImageForTag(string tag) {
		Sprite sp = Resources.Load<Sprite>("LocalizedImages/"+currentLangCode.ToUpper() +"/"+tag) as Sprite;
		return sp;
	}

	/// <summary>
	/// Gets the system language code.
	/// </summary>
	/// <returns>The system language code.</returns>
	public string GetSystemLanguageCode()
	{
		string SystemLanguageCode = "EN";
		string systemlanguageName = Application.systemLanguage.ToString ();

		if (systemlanguageName.Contains ("English")) {
			SystemLanguageCode = "EN";
		} else if (systemlanguageName.Contains ("French")) {
			SystemLanguageCode = "FR";
		} else if (systemlanguageName.Contains ("German")) {
			SystemLanguageCode = "DE";
		} else if (systemlanguageName.Contains ("Italian")) {
			SystemLanguageCode = "IT";
		} else if (systemlanguageName.Contains ("Spanish")) {
			SystemLanguageCode = "ES";
		} else if (systemlanguageName.Contains ("Swedish")) {
			SystemLanguageCode = "SV";
		}
			
		return SystemLanguageCode;
	}

	public void InitLanguageXML(string langCode, ref XDocument xDoc)
	{
		var txtFile = Resources.Load("Localization/Xmls/strings-" + langCode.ToLower ()) as TextAsset;

		if (txtFile == null) {
			txtFile = Resources.Load("Localization/Xmls/strings-en") as TextAsset;
		}

		xDoc = XDocument.Parse (txtFile.text.ToString ());
	}
}

public class SpecialTagReplacer
{
	public string specialTag;
	public string replaceValue;

	public SpecialTagReplacer(string _specialTag, string _replaceValue)
	{
		this.specialTag = _specialTag;
		this.replaceValue = _replaceValue;
	}
}


