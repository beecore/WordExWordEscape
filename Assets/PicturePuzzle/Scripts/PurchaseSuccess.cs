/***************************************************************************************
 * THIS SCRIPT IS ATTACHED TO PURCHASE SUCCESS PROMPT ALERT.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseSuccess : MonoBehaviour 
{
	public void OnCloseButtonPressed()
	{
		if(InputManager.Instance.canInput())
		{
			if(UIController.Instance.shopScreen.activeSelf) {
				UIController.Instance.shopScreen.Deactivate();
			}
			gameObject.Deactivate();
		}
	}

	public void OnOkButtonPressed()
	{
		if(InputManager.Instance.canInput())
		{
			if(UIController.Instance.shopScreen.activeSelf) {
				UIController.Instance.shopScreen.Deactivate();
			}
			gameObject.Deactivate();
		}
	}
}
