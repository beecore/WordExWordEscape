/***************************************************************************************
 * THIS IS CENTER OF ENTIRE GAMEPLAY HANDLING. THIS SCRIPT PERFORMS LOADING LEVEL, GAMEPLAY #endregion
 * RELATED UI FLOW.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : Singleton<GamePlay> 
{
	[SerializeField] GameObject btnBack;
    [SerializeField] GameObject header;
    
    public LevelReader levelReader;
    public AnswerPanel answerPanel;
	public InputPanel inputPanel;

    public static Level thisLevel = null;
    [SerializeField] GameObject helpPanel;
   bool isHelpActive = false;
   bool isHelpLevel = false;
   int currentHelpStep = 0;

    void OnEnable() {
        Invoke("LoadLevel",0.1F);
    }

    // Loads the level.
    public void LoadLevel() 
    {
        Debug.Log("load level");
       // txtLevel.text = UIController.Instance.currentPlayingLevel.ToString();
        thisLevel = levelReader.ReadLevel();
        CheckForHelp();
    }

    // If help not ever, then it will show help on loading a level.
    void CheckForHelp() {
        if(PlayerPrefs.HasKey("HelpShown")) {
            return;
        }
        isHelpActive = true;
        currentHelpStep = 1;
        helpPanel.SetActive(true);
        isHelpLevel = true;
    }

    // Handles device back button on android.
	public void OnBackButtonPressed () 
	{
        if(InputManager.Instance.canInput()) {
           StartCoroutine(DisableGamePlayScreen());
        }
    }

    // Disables gameplay and loads home screen, will get called on pressing back button of the app.
    IEnumerator DisableGamePlayScreen() {
		btnBack.GetComponent<Animator>().SetTrigger("Exit");
        header.GetComponent<Animator>().SetTrigger("Exit");
        yield return StartCoroutine(levelReader.UnloadLevel());
        Invoke("ActivateHomeScreen",0.2F);
    }

    // Delayed call to activate home screen.
    void ActivateHomeScreen() {
        gameObject.Deactivate();
        UIController.Instance.homeScreen.Activate();
    }

    // Activates Level Complete screen.
    void OnLevelComplete() {
        UIController.Instance.levelCompleteScreen.Activate();
    }

    // Add Character to answer panel, will be called from input panel.
    public void AddCharacterToAnswer(InputChar inputChar) {
        if(inputChar != null && !inputChar.isEmpty()) {
            AnswerChar emptyChar = answerPanel.GetEmptyChar();

            if(emptyChar != null && emptyChar.isEmpty()) {
                emptyChar.SetChacter((char)inputChar.GetCharacter(), inputChar.characterId);
                inputChar.SetEmpty();
                AudioController.Instance.PlayInputSound();
                if(answerPanel.isAnswerPanelFull()) {
                    VerifyAnswer();
                }
            }

            if(isHelpActive && currentHelpStep == 1) {
                currentHelpStep = 2;
                helpPanel.GetComponent<Help>().ShowHelp(currentHelpStep);
            }
        }
    }

    // Removes the selected character from answer panel.
    public void RemoveCharacterFromAnswer(AnswerChar answerChar) {
        if(answerPanel.isAnswerPanelFull()) {
            answerPanel.ResetWrongAnswer();
        }
        if(answerChar != null && !answerChar.isEmpty()) {
            inputPanel.ResetInputChar(answerChar.inputCharacterId);
            answerChar.ResetCharacter();
            AudioController.Instance.PlayInputRevertSound();
            
            if(isHelpActive && currentHelpStep == 2) {
                currentHelpStep = 3;
                helpPanel.GetComponent<Help>().ShowHelp(currentHelpStep);
                inputPanel.HightlightForHelp();
            }
        }
    }

    // Checks if answer given by user is correct or not. Correct answer will load the level complete screen.
    public void VerifyAnswer() {
        string userAnswer = answerPanel.GetAnswerString();
        if(userAnswer.ToUpper() == thisLevel.answer.ToUpper()) {
            if(UIController.Instance.currentLevel == 1 && isHelpLevel) {
                inputPanel.ResetHelp();
                isHelpLevel = false;
            }
            UIController.Instance.OnLevelCompleted();
        } else {
            answerPanel.OnWrongAnswerEntered();
        }
    }
}
