/***************************************************************************************
 * THIS SCRIPT IS ATTACHED TO ALL THE ELEMENT OF THE LEVEL SELECTION SCREEN.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelContent : MonoBehaviour 
{
	[SerializeField] Image[] pictureFrames;
	[SerializeField] GameObject imgLock;
	[SerializeField] GameObject imgSign;

	[SerializeField] int levelNumber;

	[SerializeField] Text txtLevel;
	
	// Sets the level pictures to the level select screen.
	public void SetLevelUI(Level thisLevel) {
		if(thisLevel != null) {
			pictureFrames[0].sprite= thisLevel.allPictures[0];
			pictureFrames[1].sprite= thisLevel.allPictures[1];
			pictureFrames[2].sprite= thisLevel.allPictures[2];
			pictureFrames[3].sprite= thisLevel.allPictures[3];
		}
	}

	// Initializes the level screen for the selected episode.
	public void InitializeLevel(bool isLocked, bool isLastUnlockedLevel = false) {
		if(!isLocked) {
			GetComponent<CanvasGroup>().alpha = 1F;
			imgLock.SetActive(false);
			imgSign.SetActive(true);

			if(isLastUnlockedLevel) {
				imgSign.GetComponent<Image>().sprite = EpisodeScreen.Instance.imgRightArrow;
			} else {
				imgSign.GetComponent<Image>().sprite = EpisodeScreen.Instance.imgCheckMark;
			}
			GetComponent<Button>().enabled = true;
		} else {
			GetComponent<CanvasGroup>().alpha = 0.5F;
			imgLock.SetActive(true);
			imgSign.SetActive(false);
			GetComponent<Button>().enabled = false;
		}

		string localizedLevelName = LocalizationManager.Instance.GetLocalizedTextForTag("txtLevel");
		txtLevel.text = localizedLevelName + " "+levelNumber;
	}
}
