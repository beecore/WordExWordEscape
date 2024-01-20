/***************************************************************************************
 * SCRIPT ATTACH TO EPISODE BUTTON. PRESSING EPISODE BUTTON WILL LOAD EPISODE SCREEN.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EpisodeButton : MonoBehaviour 
{
	// Opens the episode screen on pressing button.
	public void OnEpisodeButtonPressed()
	{
		if(InputManager.Instance.canInput())
		{
			UIController.Instance.episodeScreen.Activate();
		}
	}
}
