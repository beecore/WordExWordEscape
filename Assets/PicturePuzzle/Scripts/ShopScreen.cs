/***************************************************************************************
 * SHOP SCREEN SCRIPT COMPONENT.
 ***************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScreen : MonoBehaviour 
{
	public static event Action<bool> OnShopScreenStatusChanged;
	
	[SerializeField] GameObject removeAdsContent;
	[SerializeField] RectTransform mainRoot;


	public void OnCloseButtonPressed() 
	{
		if(InputManager.Instance.canInput()) {
			gameObject.Deactivate();
		}
	}


	void OnEnable() {
		if(OnShopScreenStatusChanged != null) {
			OnShopScreenStatusChanged.Invoke(true);
		}

		Invoke("SetShopPanel",0.1F);
	}

	void OnDisable() {
		if(OnShopScreenStatusChanged != null) {
			OnShopScreenStatusChanged.Invoke(false);
		}
	}

	void SetShopPanel() {
		if(removeAdsContent.activeSelf) {
			mainRoot.sizeDelta = new Vector2(900,1375);
		} else {
			mainRoot.sizeDelta = new Vector2(900,1215);
		}
	}

	public void OnWatchVideoButtonPressed() {
		if(InputManager.Instance.canInput()) {
			AdManager.Instance.ShowRewarded();
		}
	}
}
