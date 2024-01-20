/***************************************************************************************
 * THIS SCRIPT HANDLES THE INPUT PANEL OF THE GAMEPLAY.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InputChar : MonoBehaviour 
{
	public int characterId;
	[SerializeField] Text txtChar;
	[SerializeField] GameObject highlightParticle;
	char? defaultChar;

	public void Init() {
	}

	public void Remove() {
	}

	// Sets given character on the input button.
	public void SetChacter(char c) {
		defaultChar = c;
		txtChar.text = c.ToString();
	}

	// Returns the character of this input button.
	public char? GetCharacter() {
		return defaultChar;
	}

	// Reset's the current input character to default.
	public void ResetCharacter() {
		txtChar.text = defaultChar.ToString();
	}

	// Checks if input button is empty or not.
	public bool isEmpty() {
		return (txtChar.text.Length > 0) ? false : true;
	}

	// Makes input button empty.
	public void SetEmpty() {
		txtChar.text = "";
	}

	// Deactivates input button.
	public void Deactivate() {
		txtChar.enabled = false;
		if(highlightParticle != null) {
			highlightParticle.SetActive(true);
		}
	}

	// Activates input button.
	public void Activate() {
		txtChar.enabled = true;
	}
}
