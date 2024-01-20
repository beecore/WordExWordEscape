/***************************************************************************************
 * THIS SCRIPT ATTACHED TO ALL THE ELEMENTS IN THE EPISODE SCREEN. 
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EpisodeContent : MonoBehaviour 
{
	 [SerializeField] Image[] pictureFrames;
	 [SerializeField] Text txtEpisodeName;
	 [SerializeField] GameObject imgLock;
	 [SerializeField] GameObject imgSign;

	bool isInitialized = false;
	int episodeNumber = -1;

	// Sets given level images to the highlight thumnnail image list.
	 public void SetEpisodeUI(int _episodeNumber, string episodeName, Level thisLevel) {
		 if(thisLevel != null) {
            pictureFrames[0].sprite= thisLevel.allPictures[0];
            pictureFrames[1].sprite= thisLevel.allPictures[1];
            pictureFrames[2].sprite= thisLevel.allPictures[2];
            pictureFrames[3].sprite= thisLevel.allPictures[3];
        }
		txtEpisodeName.text = episodeName;
		episodeNumber = _episodeNumber;
		isInitialized = true;
	 }

	 void OnEnable() {
		 if(isInitialized) {
			// Checks if episode is locked or not.  
			if(episodeNumber <= UIController.Instance.currentProgressEpisode) {
				GetComponent<CanvasGroup>().alpha = 1F;
				imgLock.SetActive(false);
				imgSign.SetActive(true);

				if(episodeNumber < UIController.Instance.currentProgressEpisode) {
					imgSign.GetComponent<Image>().sprite = EpisodeScreen.Instance.imgCheckMark;
				}

				GetComponent<Button>().enabled = true;
			} else {
				GetComponent<CanvasGroup>().alpha = 0.5F;
				imgLock.SetActive(true);
				imgSign.SetActive(false);
				GetComponent<Button>().enabled = false;
			}

			string localizedEpisodeName = LocalizationManager.Instance.GetLocalizedTextForTag("txtEpisode");
			txtEpisodeName.text = localizedEpisodeName + " " + episodeNumber;
		 }
	 }

	 public void OnOpenEpisodeButtoPressed() {
		if(InputManager.Instance.canInput()) {
			EpisodeScreen.Instance.OpenLevelScreen(episodeNumber);
		}
	 }
}
