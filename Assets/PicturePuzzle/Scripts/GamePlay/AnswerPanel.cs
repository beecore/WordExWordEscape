/***************************************************************************************
 * THIS SCRIPT IS ATTACHED TO THE ROOT OF ANSWER PANEL. THIS SCRIPT HANDLES INPUT GIVEN 
 * FROM THE INPUT PANEL AND SET SELECTED LETTER TO ANSWER CHARATCTES IN THE ANSWER PANEL.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

public class AnswerPanel : MonoBehaviour 
{
	[SerializeField] GameObject answerButtonTemplate;
	[SerializeField] List<AnswerChar> answerChars = new List<AnswerChar>();
	GameObject answerPanel;

	// Initialize the instance of answerpanel.
	void Awake() {
		answerPanel = gameObject;
	}

	// Prepares the answer panel and create (Instantialte) answer characters of same amount of given number.
	public void PrepareAnswerPanel(int answerCharacters) 
	{
		answerChars = new List<AnswerChar>();
		answerPanel.ClearAllChild();
		
		int answerButtonSize = 110;
		int maxFittingChar = 8;

		if(answerCharacters > 8) {
			answerButtonSize = ((answerButtonSize * maxFittingChar) / answerCharacters);
		}

		answerButtonTemplate.GetComponent<RectTransform>().sizeDelta = new Vector2(answerButtonSize,answerButtonSize);

		for(int index = 0; index < answerCharacters; index ++) 
		{
			GameObject answerButton = (GameObject) Instantiate(answerButtonTemplate,Vector3.zero,Quaternion.identity,answerPanel.transform) as GameObject;
			answerButton.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
			answerButton.GetComponent<AnswerChar>().characterId = index;
			answerButton.SetActive(true);
			answerButton.name = "btn-char-" + index.ToString();
			answerChars.Add(answerButton.GetComponent<AnswerChar>());
		}
	}

	// Prepares the answer panel and create (Instantialte) answer characters of same amount as the length of level answer.
	public void PrepareAnswerPanel(string answer) 
	{
		answerPanel.ClearAllChild();
		int index = 0;

		int answerButtonSize = 110;
		int maxFittingChar = 8;

		if(answer.Length > 8) {
			answerButtonSize = ((answerButtonSize * maxFittingChar) / answer.Length);
		}

		answerButtonTemplate.GetComponent<RectTransform>().sizeDelta = new Vector2(answerButtonSize,answerButtonSize);
		
		foreach (char c in answer) {
			GameObject answerButton = (GameObject) Instantiate(answerButtonTemplate,Vector3.zero,Quaternion.identity,answerPanel.transform) as GameObject;
			answerButton.SetActive(true);
			answerButton.name = "btn-char-" + index.ToString();
			answerButton.GetComponent<AnswerChar>().SetChacter(c, index);
			index++;
		}
	}
	
	// Returns the next empry character from the Answer Panel.
	public AnswerChar GetEmptyChar() {
		return answerChars.Find( o => o.isEmpty());
	}

	// Checks if Answer Panel is full or not.
	public bool isAnswerPanelFull() {
		if(answerChars.FindAll( o => o.isEmpty()).Count <= 0) {
			return true;
		} return false;
	}

	// Returns the User's entered answer.
	public string GetAnswerString() {
		StringBuilder stringBuilder = new StringBuilder();
		foreach(AnswerChar answerChar in answerChars) {
			stringBuilder.Append(answerChar.GetCharacter());
		}
		return stringBuilder.ToString();
	}

	// Play animation prompting it's wrong answer.
	public void OnWrongAnswerEntered() {
		AudioController.Instance.PlayWrongAnswerSound();
		foreach(AnswerChar answerChar in answerChars) { 
			answerChar.PlayWrongAnswerAnimation();
		}
	}

	// Reset current answer panel anser animating for wrong answer.
	public void ResetWrongAnswer() {
		foreach(AnswerChar answerChar in answerChars) { 
			answerChar.ResetWrongAnswerAnimation();
		}
	}	

	// Returns of the empty characters button from the answer panel.
	public List<AnswerChar> GetAllEmptyChar() {
		return answerChars.FindAll( o => o.isEmpty());
	}

	public void Clear() {
		answerPanel.ClearAllChild();
	}

    // Returns of the input fields which are filled with wrong answer characters.
    public List<AnswerChar> GetAllWrongAnswerInputs()
    {
        List<AnswerChar> allWrongInputs = new List<AnswerChar>();

        for (int index = 0; index < GamePlay.thisLevel.answer.Length; index++)
        {
            if (answerChars[index].GetCharacter() != GamePlay.thisLevel.answer.ToCharArray()[index])
            {
                allWrongInputs.Add(answerChars[index]);
            }
        }
        return allWrongInputs;
    }
}