/***************************************************************************************
 * THIS SCRIPT SIMPLY HANDLES THE LAYER OF COIN BUTTON ON LEFT TOP SIDE OF SCREEN.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBalanceVisibility : MonoBehaviour {

	[SerializeField] Canvas thisCanvas;
	
	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable() {
		ShopScreen.OnShopScreenStatusChanged += OnShopScreenStatusChanged;
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable() {
		ShopScreen.OnShopScreenStatusChanged -= OnShopScreenStatusChanged;
	}

	void OnShopScreenStatusChanged(bool isShopOpened) {
		if(thisCanvas != null) {
			thisCanvas.sortingOrder = (isShopOpened == true) ? 3 : 1;
		}
	}	
}
