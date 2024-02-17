/***************************************************************************************
 * SETTINGS SCREEN. 
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Please setup your privac policy and support URL here, If you don't need these feature 
//You can turn off respective buttons.

public class Settings : MonoBehaviour {

	[SerializeField] UnityEngine.UI.Text txtAppVersion;

	string privacyPolicyURL = "https://powegamestudio.blogspot.com/2022/03/privacy-policy-this-privacy-policy.html";	
	string supportURL = "";

	void Start() {
		if(txtAppVersion != null) {
			txtAppVersion.text = "v " + Application.version;
		}
	}

	public void OnCloseButtonPressed()
	{
		if(InputManager.Instance.canInput())
		{
			gameObject.Deactivate();
		}
	}

	// Select Preferred Game Language.
	public void OnSelectLanguageButtonPressed() {
		if(InputManager.Instance.canInput()) {
			UIController.Instance.selectLanguageScreen.Activate();
			gameObject.Deactivate();
		}
	}

	// Opens privacy policy URL.
	public void OnPrivacyPolicyButtonPressed() {
		if(InputManager.Instance.canInput()) {
			Application.OpenURL(privacyPolicyURL);
		}
	}

	// Opens support URL.
	public void OnSupportButtonPressed() {
		if(InputManager.Instance.canInput()) {
			Application.OpenURL(supportURL);
		}
	}

	// Restores all pending transactions. 
	public void OnRestoreIAPButtonPressed() {
		if(InputManager.Instance.canInput()) {
			IAPManager.Instance.RestoreAllManagedProducts();
		}
	}

}
