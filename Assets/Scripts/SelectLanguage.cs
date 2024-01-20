using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLanguage : MonoBehaviour 
{
	[SerializeField] 
	private GameObject LanguageButtonTemplate;

	[SerializeField] 
	private Transform languageSelectionContent;


	void Start() {
		CreateLanguageList ();
	}

	void CreateLanguageList()
	{
		foreach (Langauge lang in LocalizationManager.Instance.localizationInfo.LanguageList) {
			if (lang.isAvailable) {
				CreateLanguageButton (lang);
			}
		}
	}

	void CreateLanguageButton(Langauge languauge)
	{
		GameObject languageButton = (GameObject)Instantiate (LanguageButtonTemplate);
		languageButton.name = "btn-" + languauge.LanguageName;
		languageButton.GetComponent<LanguageButton> ().SetLangugaeDetail (languauge);
		languageButton.transform.SetParent (languageSelectionContent);
		languageButton.transform.localScale = Vector3.one;
		languageButton.SetActive (true);
	}

	public void OnCloseButtonPressed()
	{
		if (InputManager.Instance.canInput ()) {
			AudioController.Instance.PlayButtonClickSound ();
			gameObject.Deactivate();
		}
	}
}
