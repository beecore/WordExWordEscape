/*******************************************************************************************
	NOTE : 
	THIS IS A PLACE HOLDER EDITOR SPECIC SCRIPT CODE. TO ACTIVATE INAPP PURCHASING USING UNITY IAP,
    PLEASE IMPORT UNITY INAPP SDK FROM WINDOW -> SERVICES AND ENABLE IN-APP PURCHASING. 
    
    IF YOU ANY QUATIONS OR ISSUE, PLEASE CONTACT US VIA FORUM OR SUPPORT EMAIL AND WE WILL HELP YOU
	FIX YOUR ISSUE.	
*******************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IAPManager : Singleton<IAPManager> 
{
    [System.NonSerialized] public bool isIAPInitialized = false;

    public static event Action OnIAPInitialized;
    public static event Action<PurchaseReward> OnPurchaseSuccess;
    public static event Action<string, string> OnPurchaseFail;

    List<InAppProduct> allInAppProducts = new List<InAppProduct>();
    
    void Start() {
        InitIAP();
    }

    public void InitIAP() {
    
        allInAppProducts.Add(new InAppProduct("com.picturepuzzle.removeads", InAppProductType.NonConsumable, new PurchaseReward("com.picturepuzzle.removeads", "Other", "adfree", 1)));
        allInAppProducts.Add(new InAppProduct("com.picturepuzzle.coin1", InAppProductType.NonConsumable, new PurchaseReward("com.picturepuzzle.coin1", "Currency", "", 240)));
        allInAppProducts.Add(new InAppProduct("com.picturepuzzle.coin2", InAppProductType.NonConsumable, new PurchaseReward("com.picturepuzzle.coin2", "Currency", "", 720)));
        allInAppProducts.Add(new InAppProduct("com.picturepuzzle.coin3", InAppProductType.NonConsumable, new PurchaseReward("com.picturepuzzle.coin3", "Currency", "", 1344)));
        allInAppProducts.Add(new InAppProduct("com.picturepuzzle.coin4", InAppProductType.NonConsumable, new PurchaseReward("com.picturepuzzle.coin4", "Currency", "", 2940)));
        allInAppProducts.Add(new InAppProduct("com.picturepuzzle.coin5", InAppProductType.NonConsumable, new PurchaseReward("com.picturepuzzle.coin5", "Currency", "", 6240)));
        isIAPInitialized = true;

        if(OnIAPInitialized != null) {
            OnIAPInitialized.Invoke();
        }
    }

    public ProductInfo GetProductFromSKU(string productId) 
    {
        InAppProduct product = allInAppProducts.Find(p => p.productId == productId);
        ProductInfo info = new ProductInfo(product.productId, "");
    
        if(product != null) {
            return info;
        }
        return null;
    }

    public void PurchaseProduct(string productId) 
    {
        InAppProduct product = allInAppProducts.Find(p => p.productId == productId);
        if(product != null) {
            if(OnPurchaseSuccess != null) {
                OnPurchaseSuccess.Invoke(product.reward);
            }
        }
    }

    public void RestoreAllManagedProducts() {
        
    }
}

public class ProductInfo
{
    public string productId;
    public string price;

    public ProductInfo(string _productId, string _price) {
        this.productId = _productId;
        this.price = _price;
    }
}

public class PurchaseReward {
    public string productId;
    public string rewardType;
    public string rewardSubType;
    public double rewardQuantity;

    public PurchaseReward(string _productId, string _rewardType, string _rewardSubType, double _rewardQuantity) {
        productId = _productId;        
        rewardType = _rewardType;
        rewardSubType = _rewardSubType;
        rewardQuantity = _rewardQuantity;
    }
}

public class InAppProduct {
    public string productId;
    public InAppProductType type;
    public PurchaseReward reward;

    public InAppProduct(string _productId, InAppProductType _type, PurchaseReward _reward) {
        this.productId = _productId;
        this.type = _type;
        this.reward = _reward;
    }
}

public enum InAppProductType {
    Consumable,
    NonConsumable,
    Subscription
}