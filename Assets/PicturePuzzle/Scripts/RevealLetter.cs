/***************************************************************************************
 * THIS SCRIPT IS ATTACHED TO THE REVEAL LETTER POPUP.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealLetter : MonoBehaviour 
{
	[SerializeField] UnityEngine.UI.Text txtMessage;

	void OnEnable() {
		if(txtMessage != null) {
			txtMessage.text = "You want to reveal a correct letter for <color=#EEDC33>" + CurrencyManager.Instance.revealLetterCoinPrice +" coins</color>?";
		}
	}

	// Will close this popup.
	public void OnCloseButtonPressed()
	{
		if(InputManager.Instance.canInput())
		{
			gameObject.Deactivate();
		}
	}

	// Called on pressing yes button on revealletter popup.
	public void OnYesButtonPressed()
	{
		if(InputManager.Instance.canInput())
		{
           
			if(CurrencyManager.Instance.GetCoinBalance() >= CurrencyManager.Instance.revealLetterCoinPrice) {
				CurrencyManager.Instance.deductBalance(CurrencyManager.Instance.revealLetterCoinPrice);
                if (UIController.Instance.typeGame == 0)
                {
                    GamePlay.Instance.inputPanel.RevealLetterAfterDelay();
                }
                else if(UIController.Instance.typeGame == 1)
                {
                    QuestionGamePlay.Instance.inputPanel.RevealLetterAfterDelay();
                }
				
			} else {
				UIController.Instance.getMoreCoinsPopup.Activate();
				UIController.Instance.getMoreCoinsPopup.GetComponent<GetMoreCoins>().SetCoinUsePurpose(CoinUsePurpose.REVEAL_LETTER);
			}
			gameObject.Deactivate();
        }
        { }
	}
}
