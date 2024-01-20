/***************************************************************************************
 * THIS SCRIPT SHOULD BE ATTACHED TO ALL THE INAPP PURCHASE BUTTON.
 * SET INAPP SKU AND OTHER DETAILS TO MAKE IT WORK. FOR MORE DETAILS SEE THE INAPP
 * INTEGRATION DOCUMENTATION.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InAppButtton : MonoBehaviour 
{
	[SerializeField] string productId;
	[SerializeField] Text txtPrice;
	[SerializeField] Button btnPurchase;

	ProductInfo thisProductInfo;

	void Start() {
		IAPManager.OnIAPInitialized += OnIAPInitialized;
		
		if(IAPManager.Instance.isIAPInitialized) {
			InitializeIAPButton();
		}
	}

	// Initialize the InAppButton after successfully initialization of In-App Purchase SDK.
	void OnIAPInitialized() {
		InitializeIAPButton();
	}

	// Initializes inapp button. Text of price on the given text will be localize by this script.
	void InitializeIAPButton() 
	{
		thisProductInfo = IAPManager.Instance.GetProductFromSKU(productId);
	
		if(thisProductInfo != null) {
			if(btnPurchase != null) {			
				if(thisProductInfo.price != "") {
					txtPrice.text = thisProductInfo.price;
				}
				btnPurchase.onClick.AddListener(OnPurchaseButtonClicked);
			}
		}
	}

	// Forwards in-app purchase request on pressing purchase button.
	void OnPurchaseButtonClicked() {
		IAPManager.Instance.PurchaseProduct(productId);
	}
}
