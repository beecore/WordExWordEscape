/***************************************************************************************
 * EPISODE SCREEN HANDLES, SHOWS AND LOADS ALL EPISODE. THIS SCRIPT WILL LOAD ALL THE 
 * LEVELS ON FIRST LOAD, YOU MIGHT NOTICE SLIGHT DELAY FOR THE FIRST TIME LOADING. 
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EpisodeScreen : Singleton<EpisodeScreen> {

	[SerializeField] GameObject episodeContentTemplate;
	[SerializeField] GameObject episodeScroll;
	[SerializeField] GameObject episodePanel;
	
	[SerializeField] GameObject txtLoading;

	public Sprite imgCheckMark;
	public Sprite imgRightArrow;

	List<LevelInfo> allEpisodeInfo = new List<LevelInfo>();
	bool isInitialized = false;

	int maxAllowedScroll = 0;
	int rowHeight = 220;

	void Start() {
		StartCoroutine(InitializeEpisodeScreen());
	}

	// Initialize episode screens and loads all the episodes.
	
	IEnumerator InitializeEpisodeScreen()
	{ 
		yield return new WaitForSeconds(0.4F);
		for(int index = 1; index <= UIController.Instance.totalEpisodes; index++)
		{
			GameObject episodeContent = (GameObject) Instantiate (episodeContentTemplate,Vector3.zero, Quaternion.identity, episodeScroll.transform);
			episodeContent.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
			episodeContent.SetActive(true);
			episodeContent.name = "episode-"+index;
			string localizedEpisodeName = LocalizationManager.Instance.GetLocalizedTextForTag("txtEpisode");
			LevelInfo currentEpisodeInfo = (LevelInfo) Resources.Load("Levels/Episode-"+index) as LevelInfo;
			allEpisodeInfo.Add(currentEpisodeInfo);
			Level thisLevel = currentEpisodeInfo.allLevels.Find( o => o.levelNumber == "1");
			episodeContent.GetComponent<EpisodeContent>().SetEpisodeUI(index, localizedEpisodeName+ " " + index, thisLevel);
			yield return new WaitForFixedUpdate();
		}
		txtLoading.SetActive(false);
		episodeScroll.SetActive(true);
		isInitialized = true;
		Invoke("SetScrollPositionOnStartup",0.1F);
	}

	// Initialize the scroll position based on size of screen.
	void SetScrollPositionOnStartup() {
		int episodeContentHeight = (int) episodeScroll.transform.parent.GetComponent<RectTransform>().rect.height;
		int scrollHeight =  (int) episodeScroll.GetComponent<RectTransform>().sizeDelta.y;
		maxAllowedScroll = (scrollHeight - episodeContentHeight);

		int scrollPostion = ((rowHeight * (UIController.Instance.currentProgressEpisode - 1))) + 30;

		scrollPostion = Mathf.Clamp(scrollPostion, 0, maxAllowedScroll);
		episodeScroll.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,scrollPostion);
	}

	// Sets the position of scroll based on progress.
	void SetScrollPosition()
	{
		int scrollPostion = ((rowHeight * (UIController.Instance.currentProgressEpisode - 1))) + 30;
		scrollPostion = Mathf.Clamp(scrollPostion, 0, maxAllowedScroll);
		episodeScroll.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,scrollPostion);
	}

	void OnEnable() {
		if(isInitialized) {
			SetScrollPosition();
		}
	}

	public void OnCloseButtonPressed() {
		if(InputManager.Instance.canInput()) {
			gameObject.Deactivate();
		}
	}

	// Opens the levels screen for the selected episode.
	public void OpenLevelScreen(int episodeNumber) {
		GameObject levelPanel = UIController.Instance.levelScreen;
		levelPanel.Activate();
		levelPanel.GetComponent<LevelScreen>().PrepareLevelScreen(episodeNumber, allEpisodeInfo[episodeNumber-1]);
	}
}