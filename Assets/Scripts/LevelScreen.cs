/***************************************************************************************
 * THIS SCRIPT IS ATTACHED TO THE ROOT OF LEVEL SELECTION SCREEN. THIS SCRIPT WILL ENABLE
 * AND SETUP LEVEL CONTENT FOR SELECTED EPISODE.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScreen : MonoBehaviour 
{
	[SerializeField] List<LevelContent> allLevels;
	[SerializeField] GameObject levelContentRoot;
	[SerializeField] GameObject levelContentScroll;
	int episodeNumber = 0;

	int maxAllowedScroll = 0;
	int rowHeight = 175;

	void Start() 
	{
		int levelContentHeight = (int) levelContentRoot.transform.GetComponent<RectTransform>().rect.height;
		int scrollHeight =  (int) levelContentScroll.GetComponent<RectTransform>().sizeDelta.y;
		maxAllowedScroll = (scrollHeight - levelContentHeight);
	}

	// Prepares the level selection screen for the selected episode.
	public void PrepareLevelScreen(int _episodeNumber, LevelInfo episodeInfo) 
	{
		int levelNumber = 1;
		episodeNumber = _episodeNumber;

		foreach(LevelContent content in allLevels) {
			Level thisLevel = episodeInfo.allLevels.Find( o => o.levelNumber == levelNumber.ToString());
			content.SetLevelUI(thisLevel);

			if(episodeNumber < UIController.Instance.currentProgressEpisode) {
				content.InitializeLevel(false);
			} 
			else 
			{
				if(levelNumber <= UIController.Instance.currentProgressLevel) 
				{
					content.InitializeLevel(false, ((levelNumber == UIController.Instance.currentProgressLevel) ? true : false));
				} else {
					content.InitializeLevel(true);
				}
			}
			levelNumber++;
		}
		levelContentRoot.SetActive(true);

		Invoke("SetScrollPosition",0.1F);
	}

	public void OnCloseButtonPressed() 
	{
		if(InputManager.Instance.canInput()) {
			gameObject.Deactivate();
		}
	}

	void OnDisable() {
		levelContentRoot.SetActive(false);
	}

	// Starts the selected level.
	public void OnLevelSelected(GameObject levelContent) {
		if(InputManager.Instance.canInput()) {
			UIController.Instance.episodeScreen.Deactivate();
			int selectedLevel = 1;
			int.TryParse(levelContent.name.Replace("Level-",""), out selectedLevel);
			UIController.Instance.LoadSelectedLevel(episodeNumber, selectedLevel);
			gameObject.Deactivate();
		}
	}

	// Sets the scroll position for the level select screen based on the progress.
	void SetScrollPosition()
	{
		if(episodeNumber < UIController.Instance.currentProgressEpisode) {
			levelContentScroll.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,maxAllowedScroll);
		} else {
			int scrollPostion = ((rowHeight * (UIController.Instance.currentProgressLevel - 1))) + 20;
			scrollPostion = Mathf.Clamp(scrollPostion, 0, maxAllowedScroll);
			levelContentScroll.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,scrollPostion);
		}
	}
}
