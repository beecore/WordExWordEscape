/***************************************************************************************
 * THIS SCRIPT IS ATTACHED TO THE REMOVE LETTER POPUP.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLetters : MonoBehaviour 
{
	[SerializeField] UnityEngine.UI.Text txtMessage;

	void OnEnable() {
		if(txtMessage != null) {
			txtMessage.text = "You want to remove extra letters for <color=#EEDC33>" + CurrencyManager.Instance.removeExtraLetterCoinPrice +" coins</color>?";
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

	// Called on pressing yes button on remove extra letter popup.
	public void OnYesButtonPressed()
	{
		if(InputManager.Instance.canInput())
		{
			if(CurrencyManager.Instance.GetCoinBalance() >= CurrencyManager.Instance.removeExtraLetterCoinPrice) {
				CurrencyManager.Instance.deductBalance(CurrencyManager.Instance.removeExtraLetterCoinPrice);
                if (UIController.Instance.typeGame == 0)
                {
                    GamePlay.Instance.inputPanel.RemoveExtraLettersAfterDelay();
                }
                else if (UIController.Instance.typeGame == 1)
                {
                    QuestionGamePlay.Instance.inputPanel.RemoveExtraLettersAfterDelay();
                }
                
			} else {
				UIController.Instance.getMoreCoinsPopup.Activate();
				UIController.Instance.getMoreCoinsPopup.GetComponent<GetMoreCoins>().SetCoinUsePurpose(CoinUsePurpose.REMOVE_LETTERS);
			}
			gameObject.Deactivate();
		}
	}
}
