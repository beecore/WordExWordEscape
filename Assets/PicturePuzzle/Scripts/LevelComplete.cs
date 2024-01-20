/***************************************************************************************
 * THIS SCRIPT IS ATTACHED TO LEVEL COMPLETE POPUP. ON LEVEL COMPLETION THIS SCRIPT WILL
 * SETUP UI BASED ON COMPLETED LEVEL.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour {

	[SerializeField] GameObject header;
	[SerializeField] GameObject levelcompleteContent;
	[SerializeField] AnswerPanel answerPanel;

	[SerializeField] UnityEngine.UI.Image imgProgressCircle;
	[SerializeField] UnityEngine.UI.Text txtLevelProgress;

	void Start() {
		float fillAmount = 0.05F * (UIController.Instance.currentLevel - 2);
		imgProgressCircle.fillAmount = fillAmount;
	}

	void OnEnable() {
	 	Invoke("LevelCompleteAnimation",0.1F);
	}

	void OnDisable() {
		imgProgressCircle.enabled = false;
		txtLevelProgress.enabled = false;
		BGMusic.Instance.ResumeBgMusic();
	}

	// Shows the level complete animation.
	void LevelCompleteAnimation() 
	{
		BGMusic.Instance.PauseBgMusic();
		header.GetComponent<Animator>().SetTrigger("Enter");
        if (UIController.Instance.typeGame == 0)
        {
            answerPanel.PrepareAnswerPanel(GamePlay.thisLevel.answer);
        }else if(UIController.Instance.typeGame == 1)
        {
            Debug.Log("type " + UIController.Instance.typeGame);
            answerPanel.PrepareAnswerPanel(QuestionGamePlay.thisLevel.answer);
        }
		
		Invoke("ActivateLevelCompleteContent",0.2F);
	}

	// Activates the level complete content.
	void ActivateLevelCompleteContent() {
		levelcompleteContent.SetActive(true);
		AudioController.Instance.PlayLevelCompleteSound();

		Invoke("ShowLevelProgressAnimation",0.7F);
		Invoke("AddCoinReward",1F);
	}

	void ShowLevelProgressAnimation() {
		StartCoroutine(AnimateProgressCircle());
	}

	IEnumerator AnimateProgressCircle() {
		int lastCompletedLevel = UIController.Instance.currentLevel - 1;
		if(lastCompletedLevel == 0 ) {
			lastCompletedLevel = UIController.Instance.levelsPerEpisode;
		} else if(lastCompletedLevel == 1) {
			imgProgressCircle.fillAmount = 0;
		}

		imgProgressCircle.enabled = true;
		txtLevelProgress.text = (lastCompletedLevel * 5).ToString() + "%";
		txtLevelProgress.enabled = true;

		float fillAmount = 0.05F * lastCompletedLevel;

		while(imgProgressCircle.fillAmount < fillAmount) {
			imgProgressCircle.fillAmount = imgProgressCircle.fillAmount + 0.003F;
			yield return new WaitForFixedUpdate();
		}
	}

	// Loads the next level on pressing next button of level complete screen.
	public void OnContinueButtonPressed() {
		if(InputManager.Instance.canInput()) {
			answerPanel.Clear();
            if (UIController.Instance.typeGame == 0)
            {
                UIController.Instance.gameScreen.GetComponent<GamePlay>().LoadLevel();
            }
            else if (UIController.Instance.typeGame == 1)
            {
                Debug.Log("game " + UIController.Instance.questionScreen);
              UIController.Instance.questionScreen.GetComponent<QuestionGamePlay>().LoadLevel();
            }
            
			gameObject.Deactivate();
		}
	}

	// Adds coins reward for completing level.
	public void AddCoinReward() {
		CurrencyManager.Instance.AddCoinBalance(10);

		// You can place Interstital ads something like below. modiy it as per your rerquirement.
		//if(UIController.Instance.currentLevel % 4 == 0) 
		{
			if(AdManager.Instance.isInterstitialAvailable()) {
				AdManager.Instance.ShowInterstitial();
			}
		}

		Invoke("CleanCache",0.3F);
	}

	// Clears the unused assets from the game. you can move this to better place incase you notice lag during calling of this.
	void CleanCache() {
		UIController.Instance.ClearCache();
	}
}
