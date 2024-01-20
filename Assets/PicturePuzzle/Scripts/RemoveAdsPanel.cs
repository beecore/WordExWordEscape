/***************************************************************************************
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAdsPanel : MonoBehaviour {

	public static GameObject thisPanel;

	void Awake()
	{
		thisPanel = gameObject;
	}	
	void OnEnable() 
	{
		if(PlayerPrefs.HasKey("isAdFree")) {
			gameObject.SetActive(false);
		}
	}

	public static void OnRemoveAdsPurchased() {
		if(thisPanel != null) {
			thisPanel.SetActive(false);
		}
	}
}
