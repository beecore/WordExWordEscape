/***************************************************************************************
 * ADDED THIS SCRIPT TO SETTING BUTTON. PRESSING BUTTON WITH THIS SCRIPT ATTCHED WILL OPEN
 * SETTINGS SCREEN.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour 
{
	public void OnSettingsButttonPressed()
	{
		if(InputManager.Instance.canInput())
		{
			UIController.Instance.settingScreen.Activate();
		}
	}
}