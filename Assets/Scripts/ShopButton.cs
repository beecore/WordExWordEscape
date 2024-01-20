/***************************************************************************************
 * ADDED THIS SCRIPT TO SHOP BUTTON. PRESSING BUTTON WITH THIS SCRIPT ATTCHED WILL OPEN
 * SHOP SCREEN.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour 
{
	public void OnShopButtonPressed()
	{
		if(InputManager.Instance.canInput())
		{
			UIController.Instance.shopScreen.Activate();
		}
	}
}