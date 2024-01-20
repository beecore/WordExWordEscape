/***************************************************************************************
 * THIS SCRIPT IS ATTACHED TO QUIT GAME CONFIRMATION POPUP.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour 
{
	// Will cancel quitting game.
	public void OnCloseButtonPressed()
	{
		if(InputManager.Instance.canInput())
		{
			gameObject.Deactivate();
		}
	}

	// Will quit the game.
	public void OnYesButtonPressed()
	{
		if(InputManager.Instance.canInput())
		{
			UIController.Instance.QuitGame();
			gameObject.Deactivate();
		}
	}

	// Will cancel quitting game.
	public void OnNoButtonPressed()
	{
		if(InputManager.Instance.canInput())
		{
			gameObject.Deactivate();
		}
	}
}
