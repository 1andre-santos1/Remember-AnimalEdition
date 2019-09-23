using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    private InterstitialAd interstitial;
    public static AdManager adManager;

    private void Awake() {
        if (!adManager)
        {
            adManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        RequestInterstitial();
    }

    private void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-3345994290034136~5985019348";
        string testAdUnitId = "ca-app-pub-3940256099942544/1033173712";

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(testAdUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

        this.interstitial.OnAdClosed += Interstitial_OnAdClosed;
    }
    private void Interstitial_OnAdClosed(object sender, System.EventArgs e)
    {
        RequestInterstitial();
    }
    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
            RequestInterstitial();
        }
    }
}
