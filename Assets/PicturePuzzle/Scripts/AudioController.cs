/***************************************************************************************
 * THIS SCRIPTS HANDLES IN-GAME SOUNDS AND MUSIC.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>  
{
	[SerializeField] AudioSource source;

	[SerializeField] AudioClip btnPress;
	[SerializeField] AudioClip levelupSound;
	[SerializeField] AudioClip wrongSound;
	[SerializeField] AudioClip inputSound;
	[SerializeField] AudioClip inputRevertSound;
	[SerializeField] AudioClip levelCompleteSound;
	[SerializeField] AudioClip coinRewardSound;
	[SerializeField] AudioClip revealLetterSound;
	[SerializeField] AudioClip removeLetterSound;

	// Plays the requested sound.
	public void PlayButtonClickSound() {
		if(ProfileManager.Instance.isSoundEnabled) {
			source.PlayOneShot(btnPress);
		}
	}

	// Plays the requested sound.
	public void PlayLevelUpSound() {
		if(ProfileManager.Instance.isSoundEnabled) {
			source.PlayOneShot(levelupSound);
		}
	}

	// Plays the requested sound.
	public void PlayWrongAnswerSound() {
		if(ProfileManager.Instance.isSoundEnabled) {
			source.PlayOneShot(wrongSound);
		}
	}

	// Plays the requested sound.
	public void PlayInputSound() {
		if(ProfileManager.Instance.isSoundEnabled) {
			source.PlayOneShot(inputSound);
		}
	}

	// Plays the requested sound.
	public void PlayInputRevertSound() {
		if(ProfileManager.Instance.isSoundEnabled) {
			source.PlayOneShot(inputRevertSound);
		}
	}

	// Plays the requested sound.
	public void PlayLevelCompleteSound() {
		if(ProfileManager.Instance.isSoundEnabled) {
			source.PlayOneShot(levelCompleteSound);
		}
	}

	// Plays the requested sound.
	public void PlayCoinRewardSound() {
		if(ProfileManager.Instance.isSoundEnabled) {
			source.PlayOneShot(coinRewardSound);
		}
	}

	// Plays the requested sound.
	public void PlayReveaLetterSound() {
		if(ProfileManager.Instance.isSoundEnabled) {
			source.PlayOneShot(revealLetterSound);
		}
	}

	// Plays the requested sound.
	public void PlayRemoveLetterSound() {
		if(ProfileManager.Instance.isSoundEnabled) {
			source.PlayOneShot(removeLetterSound);
		}
	}
}
