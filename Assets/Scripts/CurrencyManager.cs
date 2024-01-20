/***************************************************************************************
 ***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CurrencyManager : Singleton<CurrencyManager> 
{
	public static event Action<int> OnCoinBalanceUpdated;

	public int InitialCoinBalance = 100;
	private int coinBalance = 0;

	public int revealLetterCoinPrice = 50;
	public int removeExtraLetterCoinPrice = 100;
	public int watchVideoRewardCoins = 50;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake() {
		coinBalance = PlayerPrefs.GetInt ("coinBalance", InitialCoinBalance);
	}

	void OnEnable() {
		IAPManager.OnPurchaseSuccess += OnPurchaseSuccess;
		IAPManager.OnPurchaseFail += OnPurchaseFailure;
		AdManager.OnRewardedFinishedEvent += OnRewardedFinished;
	}

	void OnDisable() {

		IAPManager.OnPurchaseSuccess -= OnPurchaseSuccess;
		IAPManager.OnPurchaseFail -= OnPurchaseFailure;
		AdManager.OnRewardedFinishedEvent -= OnRewardedFinished;
	}

	/// <summary>
	/// Gets the coin balance.
	/// </summary>
	/// <returns>The coin balance.</returns>
	public int GetCoinBalance()
	{
		return coinBalance;
	}

	/// <summary>
	/// Adds the coin balance.
	/// </summary>
	/// <param name="coinBalanceToIncrease">Coin balance to increase.</param>
	public void AddCoinBalance(int coinBalanceToIncrease)
	{
		coinBalance += coinBalanceToIncrease;
		PlayerPrefs.SetInt ("coinBalance", coinBalance);

		if (OnCoinBalanceUpdated != null) {
			OnCoinBalanceUpdated.Invoke (coinBalance);
		}
	}

	/// <summary>
	/// Deducts the balance.
	/// </summary>
	/// <returns><c>true</c>, if balance was deducted, <c>false</c> otherwise.</returns>
	/// <param name="coinBalanceToDeduct">Coin balance to deduct.</param>
	public bool deductBalance(int coinBalanceToDeduct)
	{
		if (coinBalance >= coinBalanceToDeduct) {
			coinBalance -= coinBalanceToDeduct;

			PlayerPrefs.SetInt ("coinBalance", coinBalance);

			if (OnCoinBalanceUpdated != null) {
				OnCoinBalanceUpdated.Invoke (coinBalance);
			}
			return true;
		}
		return false;
	}


	public void OnPurchaseSuccess(PurchaseReward reward) {
		switch(reward.rewardType) {
			case "Currency":
				AddCoinBalance((int)reward.rewardQuantity);
			break;

			case "Other":
			if(reward.rewardSubType == "adfree") {
				PlayerPrefs.SetInt("isAdFree",1);
				Invoke("MakeGameAdFree",1F);
			}
			break;
		}

		UIController.Instance.purchaseSuccessPopup.Activate();
	}

	public void OnPurchaseFailure(string productId, string reason) {
		UIController.Instance.purchaseFailurePopup.Activate();
	}

	void MakeGameAdFree() {
		RemoveAdsPanel.OnRemoveAdsPurchased();
		AdManager.Instance.SetAdFree();
	}

	void OnRewardedFinished(bool isCompleted) {
		if(isCompleted) {
			AddCoinBalance(watchVideoRewardCoins);
		}
	}
}
