using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class BaseAdManager : Singleton<BaseAdManager> 
{

    [Header("AdMob Keys for Google")]
    [SerializeField]
    string appId_google = "ca-app-pub-3940256099942544~3347511713";
    [SerializeField]
    string bannerAdUnitId_google = "ca-app-pub-3940256099942544/6300978111";
    [SerializeField]
    string interstitialAdUnitId_google = "ca-app-pub-3940256099942544/1033173712";     [SerializeField]
    string rewardedAdUnitId_google = "ca-app-pub-3940256099942544/5224354917";


    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardBasedVideo;

    //Initialize Unity Ads. 
    void Start()
    {
        Invoke("InitializeAdMob", 1F);
    }

    void InitializeAdMob()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            Invoke("InitBanner", 1F);
            Invoke("InitInterstitial", 1F);
            Invoke("InitRewarded", 1F);
        });


    }

    #region hienthi
    //Show banner ads. Not supported in this version.
    public void InitBanner()
    {
        if (AdManager.Instance.isAdsAllowed())
        {
            bannerView = new BannerView(bannerAdUnitId_google, AdSize.SmartBanner, AdPosition.Bottom);
            AdRequest request = new AdRequest.Builder().Build();
            bannerView.LoadAd(request);
            bannerView.Hide();

            ShowBanner();
        }
    }

    public void ShowBanner()
    {
        if (bannerView != null)
        {
            bannerView.Show();
        }
    }
    //Show banner ads. Not supported in this version.
    public void HideBanner()
    {
        if (bannerView != null)
        {
            bannerView.Hide();
        }
    }

    void InitInterstitial()
    {

        //   this.interstitial = new InterstitialAd(interstitialAdUnitId_google);
        if (interstitial != null)
        {
            interstitial.Destroy();
            interstitial = null;
        }

        // send the request to load the ad.
        var adRequest = new AdRequest();
        InterstitialAd.Load(interstitialAdUnitId_google, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitial = ad;
            });

        // Raised when an impression is recorded for an ad.
        interstitial.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Called when an ad request failed to load.
        interstitial.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
        // Called when an ad is shown.
        interstitial.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Called when the ad is closed.
        interstitial.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
            StartCoroutine(OnInterstitialFinished(true));
        };
    }

    private void RequestInterstitial()
    {
        InitInterstitial();
    }

    //Checks if interstial is loaded and ready to be shown.
    public bool isInterstitialAvailable()
    {
        return interstitial.CanShowAd();
    }

    //Show Intestitial. Call this method when you want to interstitial.
    public void ShowInterstitial()
    {
        if (AdManager.Instance.isAdsAllowed())
        {
            Invoke("StartInterstitialWithDelay", 0.1F);
        }
    }
    //A safe delay before starting an ad.
    void StartInterstitialWithDelay()
    {
        if (interstitial != null && interstitial.CanShowAd())
        {
            interstitial.Show();
        }
    }

    //A Delay based result forward just be safe. Added as just a safety bridge to prevent app from being unresponsive.
    IEnumerator OnInterstitialFinished(bool isCompleted)
    {
        yield return new WaitForSeconds(0.1F);
        AdManager.Instance.OnInterstitialFinished(isCompleted);
    }

    void InitRewarded()
    {
        this.RequestRewardBasedVideo();
        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
        };

        // Called when an ad is shown.
        // Raised when an ad opened full screen content.
        rewardBasedVideo.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };

        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdPaid += (AdValue adValue) =>
        {
            StartCoroutine(OnRewardedFinished(true));
        };
        // Called when the ad is closed.
        // Raised when the ad closed full screen content.
        rewardBasedVideo.OnAdFullScreenContentClosed += () =>
        {
            this.RequestRewardBasedVideo();
        };

       
    }

    private void RequestRewardBasedVideo()
    {
        // Clean up the old ad before loading a new one.
        if (rewardBasedVideo != null)
        {
            rewardBasedVideo.Destroy();
            rewardBasedVideo = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(rewardedAdUnitId_google, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardBasedVideo = ad;
            });

    }

    public bool isRewardedAvailable()
    {
        return rewardBasedVideo.CanShowAd();
    }

    //Show Intestitial. Call this method when you want to interstitial.
    public void ShowRewarded()
    {
        Invoke("StartRewardedWithDelay", 0.1F);
    }

    #endregion




    //Checks if rewarded video is loaded and ready to be shown.

    //A safe delay before starting an ad.
    void StartRewardedWithDelay()
    {
        if (rewardBasedVideo != null && rewardBasedVideo.CanShowAd())
        {
            rewardBasedVideo.Show((Reward reward) =>
            {
              
            });
        }
           
    }


//A Delay based result forward just be safe. Added as just a safety bridge to prevent app from being unresponsive.
IEnumerator OnRewardedFinished(bool isCompleted)
{
    yield return new WaitForSeconds(0.1F);
    AdManager.Instance.OnRewardedFinished(isCompleted);
}

}