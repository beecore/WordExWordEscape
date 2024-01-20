/***************************************************************************************
 * THIS SCRIPT IS ATTACHED TO EACH CHARACTER OF THE ANSWER PANEL SELECTED CHARACTER FROM
 * THE INPUT WILL SET TO NEXT EMPTY ANSWECHAR.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerChar : MonoBehaviour 
{
	public int characterId;
	[HideInInspector] public int inputCharacterId = -1;
	[SerializeField] Animator animator;
	[SerializeField] GameObject letterHighlight;
	[SerializeField] Text txtChar;

	// Sets the given character to answer panel.
	public void SetChacter(char c, int _inputCharacterId) {
		txtChar.text = c.ToString();
		inputCharacterId = _inputCharacterId;
		if(letterHighlight != null) {
			letterHighlight.SetActive(true);
		}
	}

	// Get the current character from the answer panel for the selected character button.
	public char? GetCharacter() {
		if(txtChar.text.Length > 0) {
			return (char) txtChar.text[0];
		}
		return null;
	}

	//Resets character.
	public void ResetCharacter() {
		txtChar.text = "";
		inputCharacterId = -1;
	}

	// Checks if selected character button is empty.
	public bool isEmpty() {
		return (txtChar.text.Length > 0) ? false : true;
	}

	// Make the character button empty.
	public void SetEmpty() {
		txtChar.text = "";
	}

	//Animate the character when the full answer is wrong.
	public void PlayWrongAnswerAnimation() {
		animator.SetTrigger ("WrongAnswer");
	}

	// Resets the animation.
	public void ResetWrongAnswerAnimation() {
		animator.SetTrigger ("Reset");
	}

	// Toggle Button Interactable true or false.
	public void SetInteractable(bool status) {
		GetComponent<Button>().interactable = status;
	}
}