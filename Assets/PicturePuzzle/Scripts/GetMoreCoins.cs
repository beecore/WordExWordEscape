/***************************************************************************************
 * THIS SCRIPT IS ATTACHED TO GETMORE COINS POPUP.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoinUsePurpose {
	REVEAL_LETTER,
	REMOVE_LETTERS,
	NOT_SPECIFIED
}

public class GetMoreCoins : MonoBehaviour 
{

	CoinUsePurpose purpose = CoinUsePurpose.NOT_SPECIFIED;

	public void SetCoinUsePurpose(CoinUsePurpose _purpose) {
		purpose = _purpose;
	}

	void OnEnable() {
		AdManager.OnRewardedFinishedEvent += OnRewardedFinished;
	}

	void OnDisable() {
		AdManager.OnRewardedFinishedEvent -= OnRewardedFinished;
		purpose = CoinUsePurpose.NOT_SPECIFIED;
	}

	public void OnCloseButtonPressed()
	{
		if(InputManager.Instance.canInput())
		{
			if(UIController.Instance.shopScreen.activeSelf) {
				UIController.Instance.shopScreen.Deactivate();
			}
			gameObject.Deactivate();
		}
	}

	// Opens shop screen.
	public void OnOpenShopkButtonPressed()
	{
		if(InputManager.Instance.canInput())
		{
			UIController.Instance.shopScreen.Activate();
			gameObject.Deactivate();
		}
	}

	// Requests watching rewarded video for getting free coins.
	public void OnWatchVideoButtonPressed() 
	{
		if(InputManager.Instance.canInput()) 
		{
			AdManager.Instance.ShowRewarded();
		}
	}

	// Handles the UI Screens and determines which popup to open after completion of rewarded video.
	void OnRewardedFinished(bool isCompleted) {
		if(isCompleted) {

			switch(purpose) {
				case CoinUsePurpose.REMOVE_LETTERS:
					if(CurrencyManager.Instance.GetCoinBalance() >= CurrencyManager.Instance.removeExtraLetterCoinPrice) {
						GamePlay.Instance.inputPanel.OnRemoveExtraLetterButtonPressed();
						gameObject.Deactivate();
					}
				break;

				case CoinUsePurpose.REVEAL_LETTER:
					if(CurrencyManager.Instance.GetCoinBalance() >= CurrencyManager.Instance.revealLetterCoinPrice) {
						GamePlay.Instance.inputPanel.OnRevealLetterButtonPressed();
						gameObject.Deactivate();
					}
				break;
			}
		}
	}
}
