/***************************************************************************************
 * A COMMON POPUP SCRIPT ATTACHED TO CommonPopup GAMEOBJECT. SETS THE GIVEN TEXT AND TITLE
 * TO THIS POPUP'S TEXT COMPONENT.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonPopup : MonoBehaviour 
{
	[SerializeField] UnityEngine.UI.Text txtTitle;
	[SerializeField] UnityEngine.UI.Text txtMessage;
	
	public void OnCloseButtonPressed()
	{
		if(InputManager.Instance.canInput())
		{
			gameObject.Deactivate();
		}
	}
	
	public void OnYesButtonPressed()
	{
		if(InputManager.Instance.canInput())
		{
			gameObject.Deactivate();
		}
	}

	public void OnNoButtonPressed()
	{
		if(InputManager.Instance.canInput())
		{
			gameObject.Deactivate();
		}
	}

	public void SetText(string title, string message) {
		txtTitle.text = title;
		txtMessage.text = message;
	}
}
