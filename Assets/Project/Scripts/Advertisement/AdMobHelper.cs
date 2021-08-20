/*using System;
using System.Collections;
using System.Collections.Generic;
using EasyMobile;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobHelper : MonoBehaviour
{
    /*
    private AdPlacement curAdPlacement;

    public void Init()
    {
        if (Advertising.IsInitialized() == false)
        {
            Advertising.Initialize();
        }

        if (GameManager.IsTestMode)
        {
            curAdPlacement = AdPlacement.Achievements;
        }
        else
        {
            curAdPlacement = AdPlacement.Default;
        }

        // if (Advertising.TapjoyClient.IsInitialized == false)
        // {
        //     Advertising.TapjoyClient.Init();
        // }
        //
        // if (Advertising.ChartboostClient.IsInitialized == false)
        // {
        //     Advertising.ChartboostClient.Init();
        // }

        LoadInterstitialAd();
        LoadRewardedAd();
        LoadRewardedInterstitialAd();

        Advertising.AdMobClient.RewardedAdProvider.ClosedEvent += RewardedAdProviderClosedEvent;
        Advertising.AdMobClient.RewardedAdProvider.RewardEvent += RewardedAdProviderRewardEvent;

        Advertising.AdMobClient.RewardedInterstitialAdProvider.ClosedEvent += RewardedInterstitialAdProviderClosedEvent;
        Advertising.AdMobClient.RewardedInterstitialAdProvider.RewardEvent += RewardedInterstitialAdProviderRewardEvent;
    }
    
    public bool IsRewardedAdReady()
    {
        bool res = Advertising.AdMobClient.IsRewardedAdReady();
        return res;
    }

    public bool IsRewardedInterstitialAdReady()
    {
        return Advertising.AdMobClient.IsRewardedInterstitialAdReady();
    }

    public bool IsInterstitialAdReady()
    {
        return Advertising.AdMobClient.IsInterstitialAdReady();
    }

    public void ShowInterstitialAd()
    {
        Advertising.AdMobClient.ShowInterstitialAd();
    }

    public void ShowRewardedInterstitialAd()
    {
        Advertising.AdMobClient.ShowRewardedInterstitialAd();
    }

    public void ShowRewardedAd()
    {
        Advertising.AdMobClient.ShowRewardedAd();
    }

    private void RewardedInterstitialAdProviderRewardEvent(AdMobClientImpl.RewardedInterstitial.Instance arg1,
        Reward arg2)
    {
        SaveManager.Instance.SaveMap.Jetton.Value +=
            GameManager.Instance.AD.InsertAwardJetton(SaveManager.Instance.SaveMap.Level.Value);
        Log.LogPrint("RewardedInterstitialAdProviderRewardEvent");
    }

    private void RewardedInterstitialAdProviderClosedEvent(AdMobClientImpl.RewardedInterstitial.Instance arg1,
        object arg2, EventArgs arg3)
    {
        LoadRewardedInterstitialAd();
    }

    private void RewardedAdProviderRewardEvent(AdMobClientImpl.Rewarded.Instance arg1, object arg2, Reward arg3)
    {
        Incident.SendEvent(new WatchAwardVideoSuccess());
        Log.LogPrint("RewardedAdProviderRewardEvent");
    }

    private void RewardedAdProviderClosedEvent(AdMobClientImpl.Rewarded.Instance arg1, object arg2, EventArgs arg3)
    {
        LoadRewardedAd();
    }

    private int LoadMaxCount = 3336;

    private void LoadInterstitialAd()
    {
        Advertising.AdMobClient.LoadInterstitialAd(curAdPlacement);

        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        var b = false;
        while (true)
        {
            b = IsInterstitialAdReady();
            Log.LogPrint(b);
            yield return new WaitForSeconds(1f);
        }
    }

    private void LoadRewardedAd()
    {
        Advertising.AdMobClient.LoadRewardedAd(curAdPlacement);
    }

    private void LoadRewardedInterstitialAd()
    {
        Advertising.AdMobClient.LoadRewardedInterstitialAd(curAdPlacement);
    }
    #1#

    private string InterstitialAdID;
    private string RewardedAdID;
    private string RewardedInterstitialAdID;

    private RewardedInterstitialAd rewardedInterstitialAd { get; set; }
    private RewardedAd rewardedAd { get; set; }
    private InterstitialAd interstitial { get; set; }

    public void Init()
    {
#if UNITY_ANDROID
        if (GameManager.IsTempAD)
        {
            RewardedInterstitialAdID = "ca-app-pub-3940256099942544/5354046379";
            RewardedAdID = "ca-app-pub-3940256099942544/5224354917";
            InterstitialAdID = "ca-app-pub-3940256099942544/1033173712";
        }
        else
        {
            RewardedInterstitialAdID = "ca-app-pub-8166170171748846/9250459745";
            RewardedAdID = "ca-app-pub-8166170171748846/3679047799";
            InterstitialAdID = "ca-app-pub-8166170171748846/9964775612";
        }
#elif UNITY_IPHONE
       if (GameManager.IsTempAD)
        {
            RewardedInterstitialAdID = "ca-app-pub-3940256099942544/6978759866";
            RewardedAdID = "ca-app-pub-3940256099942544/1712485313";
            InterstitialAdID = "ca-app-pub-3940256099942544/4411468910";
        }
        else
        {
            RewardedInterstitialAdID = "ca-app-pub-6843782436793396/9426629692";
            RewardedAdID = "ca-app-pub-6843782436793396/2637520752";
            InterstitialAdID = "ca-app-pub-6843782436793396/5563539752";
        }
#endif
        try
        {
            MobileAds.Initialize(initStatus => { });

            RequestRewardedInterstitialAd();
            RequestRewardedAd();
            RequestInterstitialAd();
        }
        catch (Exception e)
        {
            Log.LogError("ad init fail");
        }
    }

    private void RequestRewardedInterstitialAd()
    {
        rewardedInterstitialAd?.Destroy();
        rewardedInterstitialAd = null;

        var request = new AdRequest.Builder().Build();
        RewardedInterstitialAd.LoadAd(RewardedInterstitialAdID, request, adLoadCallback);
    }

    private void adLoadCallback(RewardedInterstitialAd ad, AdFailedToLoadEventArgs arg2)
    {
        if (arg2 == null)
        {
            rewardedInterstitialAd = ad;
            rewardedInterstitialAd.OnAdDidDismissFullScreenContent += HandleAdDidDismiss;
        }
    }

    private void RequestRewardedAd()
    {
        rewardedAd?.Destroy();
        rewardedAd = new RewardedAd(RewardedAdID);

        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        var request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }

    private void RequestInterstitialAd()
    {
        interstitial?.Destroy();
        interstitial = new InterstitialAd(InterstitialAdID);

        interstitial.OnAdFailedToLoad += OnAdFailedToLoad;

        var request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    private int count = 0;

    private void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        if (count < 3)
        {
            count += 1;
            RequestInterstitialAd();
        }

        Log.LogPrint("interstitial load fail count" + count);
    }

    private void HandleAdDidDismiss(object sender, EventArgs args)
    {
        RequestRewardedInterstitialAd();
        Log.LogPrint("Rewarded interstitial ad has dismissed presentation.");
    }

    private void userEarnedRewardCallback(Reward reward)
    {
        Log.LogParas("Rewarded interstitial ad has ", reward.Amount, reward.Type);
        SaveManager.Instance.SaveMap.Jetton.Value +=
            GameManager.Instance.AD.InsertAwardJetton(SaveManager.Instance.SaveMap.Level.Value);
    }

    private void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        print("HandleRewardedAdClosed event received");
        RequestRewardedAd();
    }

    private void HandleUserEarnedReward(object sender, Reward args)
    {
        var type = args.Type;
        var amount = args.Amount;
        print(
            "HandleRewardedAdRewarded event received for "
            + amount + " " + type);

        Incident.SendEvent(new WatchAwardVideoSuccess());
    }


    public bool IsRewardedAdReady()
    {
        return rewardedAd != null && rewardedAd.IsLoaded();
    }

    public bool IsRewardedInterstitialAdReady()
    {
        return rewardedInterstitialAd != null;
    }

    public bool IsInterstitialAdReady()
    {
        return interstitial != null && interstitial.IsLoaded();
    }

    public void ShowInterstitialAd()
    {
        interstitial.Show();
    }

    public void ShowRewardedAd()
    {
        rewardedAd.Show();
    }

    public void ShowRewardedInterstitialAd()
    {
        rewardedInterstitialAd.Show(userEarnedRewardCallback);
    }
}*/