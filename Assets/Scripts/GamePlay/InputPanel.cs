/***************************************************************************************
 * THIS SCRIPT IS ATTACHED TO THE ROOT OF INPUT PANEL. THIS SCRIPT HANDLES INPUT GIVEN 
 * BY USER AND SET SELECTED LETTER TO ANSWER CHARATCTES IN THE ANSWER PANEL.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPanel : MonoBehaviour {

	[SerializeField] List<InputChar> puzzleCharaterButtons = new List<InputChar>();
	[SerializeField] Color textDefaultColor;
	[SerializeField] Color textHighlightColor;

	[SerializeField] UnityEngine.UI.Button btnRemoveLetters;
	
	List<char> characterSet = new List<char>{'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
	string thisLevelAnswer = "";

	// Prepares the input panel and create (Instantialte) input characters based on answer.
	public void PrepareInputPanel(string answer) {
		thisLevelAnswer = answer;
		List<char> puzzleSet = new List<char>();
		characterSet.Shuffle();

		int characterIndex = 0;
		foreach(char c in answer) {
			puzzleSet.Add(c);
			characterIndex++;
		}

		for(int index = characterIndex; index < puzzleCharaterButtons.Count; index ++) {
			puzzleSet.Add(characterSet[index]);
		} 

		if(PlayerPrefs.HasKey("HelpShown")) {
			puzzleSet.Shuffle();
		}
		
		int charIndex = 0;
		foreach(InputChar btnCharacter in puzzleCharaterButtons) {
			btnCharacter.SetChacter(puzzleSet[charIndex]);
			btnCharacter.Activate();
			charIndex++;
		}

		btnRemoveLetters.interactable = true;
		btnRemoveLetters.GetComponent<CanvasGroup>().alpha = 1;
	}

	// Resets input panel.
	public void ResetInputChar(int characterId) {
		InputChar inputChar = puzzleCharaterButtons.Find( o => o.characterId == characterId);
		if(inputChar != null) {
			inputChar.ResetCharacter();
		}
	}

	// Highlight input panel for help.
	public void HightlightForHelp() {
		for(int index = 0; index < GamePlay.thisLevel.answer.Length; index++) {
			puzzleCharaterButtons[index].transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().color = textHighlightColor;
		}
	}

	// Reset's on completion of help.
	public void ResetHelp() {
		for(int index = 0; index < GamePlay.thisLevel.answer.Length; index++) {
			puzzleCharaterButtons[index].transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().color = textDefaultColor;
		}
	}

	// This is power button. This will remove extra letters from input panel using coins.
	public void OnRemoveExtraLetterButtonPressed() {
		if(InputManager.Instance.canInput()) {
			UIController.Instance.removeLettersPopup.Activate();
		}
	}

	// This is power button. This reveal one random character in answer panel using coin.
	public void OnRevealLetterButtonPressed() {
		if(InputManager.Instance.canInput()) {
			UIController.Instance.revealLetterPopup.Activate();
		}
	}

	public void RemoveExtraLettersAfterDelay(float delay = 0.5F) {
		Invoke("RemoveExtraLetters",delay);
	}

	// Executes Remove Letter Operation.
	void RemoveExtraLetters() {
		List<InputChar> puzzleCharaterAnswerButtons = new List<InputChar>();

		foreach(char c in thisLevelAnswer) {
			InputChar inputChar = puzzleCharaterButtons.Find( o => o.GetCharacter() == c && !puzzleCharaterAnswerButtons.Contains(o));

			if(inputChar != null) {
				puzzleCharaterAnswerButtons.Add(inputChar);
			}
		}

		foreach(InputChar btnCharacter in puzzleCharaterButtons) {
			if(!puzzleCharaterAnswerButtons.Contains(btnCharacter)) {
				btnCharacter.SetEmpty();
				btnCharacter.Deactivate();
			}
		}

		btnRemoveLetters.interactable = false;
		AudioController.Instance.PlayRemoveLetterSound();
		btnRemoveLetters.GetComponent<CanvasGroup>().alpha = 0.7F;
	}

	public void RevealLetterAfterDelay(float delay = 0.5F) {
		Invoke("RevealLetter", delay);
	}

    // Executes Reveal Letter Operation.
    // Executes Reveal Letter Operation.
    void RevealLetter()
    {
        if (!GamePlay.Instance.answerPanel.isAnswerPanelFull())
        {
            List<AnswerChar> allEmptyAnswerChars = GamePlay.Instance.answerPanel.GetAllEmptyChar();

            AnswerChar randomAnswerChar = allEmptyAnswerChars[UnityEngine.Random.Range(0, allEmptyAnswerChars.Count)];
            if (randomAnswerChar != null)
            {
                int characterIndex = randomAnswerChar.characterId;

                char answeChar = thisLevelAnswer.ToCharArray()[characterIndex];
                InputChar inputChar = puzzleCharaterButtons.Find(o => o.GetCharacter() == answeChar);

                if (inputChar != null)
                {
                    inputChar.SetEmpty();
                    randomAnswerChar.SetChacter(answeChar, inputChar.characterId);
                    randomAnswerChar.SetInteractable(false);
                    randomAnswerChar.transform.Find("ring-reveal").gameObject.SetActive(true);
                    AudioController.Instance.PlayReveaLetterSound();
                    if (GamePlay.Instance.answerPanel.isAnswerPanelFull())
                    {
                        GamePlay.Instance.VerifyAnswer();
                    }
                }
            }
        }
        else
        {
            List<AnswerChar> allWrongAnswerChars = GamePlay.Instance.answerPanel.GetAllWrongAnswerInputs();
            AnswerChar randomAnswerChar = allWrongAnswerChars[UnityEngine.Random.Range(0, allWrongAnswerChars.Count)];

            if (randomAnswerChar != null)
            {
                int characterIndex = randomAnswerChar.characterId;

                char answeChar = thisLevelAnswer.ToCharArray()[characterIndex];
                InputChar inputChar = puzzleCharaterButtons.Find(o => o.GetCharacter() == answeChar);

                if (inputChar != null)
                {
                    inputChar.SetEmpty();
                    randomAnswerChar.SetChacter(answeChar, inputChar.characterId);
                    randomAnswerChar.SetInteractable(false);
                    randomAnswerChar.transform.Find("ring-reveal").gameObject.SetActive(true);
                    AudioController.Instance.PlayReveaLetterSound();
                    if (GamePlay.Instance.answerPanel.isAnswerPanelFull())
                    {
                        GamePlay.Instance.VerifyAnswer();
                    }
                }
            }
        }
    }
}
