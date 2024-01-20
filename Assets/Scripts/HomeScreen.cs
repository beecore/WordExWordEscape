/***************************************************************************************
 * THIS SCRIPT IS ATTACHED TO HOMESCREEN GAMEOBJECT.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HomeScreen : MonoBehaviour 
{
    //[SerializeField] GameObject titleImage;
    [SerializeField] GameObject btnPlay;
    [SerializeField] GameObject header;
    [SerializeField] TextMeshProUGUI txtLevel;

	public void OnPlayButtonPressed() {
        if(InputManager.Instance.canInput()) {
            DisableHomeScreen();
        }
    }

    // Disables home screen with animations.
    void DisableHomeScreen()
    {
        //titleImage.GetComponent<Animator>().SetTrigger("Exit");
        header.GetComponent<Animator>().SetTrigger("Exit");
        Invoke("ActivateGamePlay",0.5F);
    }

    // Activates gameplay.
    void ActivateGamePlay()
    {
        gameObject.Deactivate();
        UIController.Instance.questionScreen.Activate();
    }

    void OnEnable() {
        Invoke("SetCurrentLevelUIOnHomeScreen",0.5F);

        LocalizationManager.OnLanguageChangedEvent += OnLanguageChanged;
    }

    void OnDisable() {
        LocalizationManager.OnLanguageChangedEvent -= OnLanguageChanged;
    }

    // Sets the UI of home screen on activing home screen.
    void SetCurrentLevelUIOnHomeScreen() 
    {        
        int totalPlayedLevel = ((UIController.Instance.currentProgressQuestionEpisode - 1) * UIController.Instance.levelsPerQuestionEpisode) + UIController.Instance.currentProgressQuestionLevel;
        txtLevel.text = totalPlayedLevel.ToString();

        LevelInfoQuestion currentEpisodeInfo = null;

        if(currentEpisodeInfo == null || currentEpisodeInfo.episodeNumber != UIController.Instance.currentProgressQuestionEpisode) {
		    currentEpisodeInfo = (LevelInfoQuestion) Resources.Load("Questions/Episode-" + UIController.Instance.currentProgressQuestionEpisode) as LevelInfoQuestion;
		}
        Debug.Log(currentEpisodeInfo);

        LevelQuestion thisLevel = currentEpisodeInfo.allLevels.Find( o => o.levelNumber == UIController.Instance.currentProgressQuestionLevel.ToString());
        UIController.Instance.currentPlayingQuestionLevel = totalPlayedLevel;
        UIController.Instance.currentQuestionLevel = UIController.Instance.currentProgressQuestionLevel;
        UIController.Instance.currentQuestionEpisode = UIController.Instance.currentProgressQuestionEpisode;
    }

    void OnLanguageChanged(string languageCode) {
        Invoke("SetCurrentLevelUIOnHomeScreen",1F);
    }
}
