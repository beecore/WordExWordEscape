/***************************************************************************************
 * HANDLES AND CONTROLLS THE COIN BALANCE WITHIN THE GAME. ALL THE COINS RELATED OPERATIONS
 * ARE PERFORMED BY THIS SCRIPT.
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CoinBalance : MonoBehaviour 
{
	[SerializeField] TextMeshProUGUI  txtCoinBalance;
	//[SerializeField] GameObject coinParticle;

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable() {
		CurrencyManager.OnCoinBalanceUpdated += OnCoinBalanceUpdated;
		Invoke("UpdateCoinBalance",0.1F);
	}
	
	void UpdateCoinBalance() {
		
		int coinBalance = CurrencyManager.Instance.GetCoinBalance ();
		txtCoinBalance.text = (coinBalance > 0) ? string.Format("{0:#,#.}", coinBalance) : "0";
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable() {
		CurrencyManager.OnCoinBalanceUpdated -= OnCoinBalanceUpdated;	
	}

	/// <summary>
	/// Raises the coin balance updated event.
	/// </summary>
	/// <param name="coinBalance">Coin balance.</param>
	void OnCoinBalanceUpdated (int coinBalance)
	{
		StartCoroutine(SetCoinBalance(coinBalance));
	}

	/// <summary>
	/// Sets the coin balance.
	/// </summary>
	/// <returns>The coin balance.</returns>
	/// <param name="coinBalance">Coin balance.</param>
	IEnumerator SetCoinBalance(int coinBalance)
	{
		int IterationCount = 50;
		int oldBalance = 0;
		int.TryParse (txtCoinBalance.text.Replace(",",""), out oldBalance);

		if(coinBalance > oldBalance) {
			//coinParticle.SetActive(true);
			AudioController.Instance.PlayCoinRewardSound();
			int IterationSize = (coinBalance - oldBalance) / IterationCount;

			if(IterationSize <= 0) { 
				IterationSize = 1;
				IterationCount = 10;
			}

			for (int index = 1; index < IterationCount; index++) {
				oldBalance += IterationSize;
				txtCoinBalance.text =  string.Format("{0:#,#.}", oldBalance);
				yield return new WaitForEndOfFrame ();
			}
		}
		txtCoinBalance.text = (coinBalance > 0) ? string.Format("{0:#,#.}", coinBalance) : "0";
	}
}
