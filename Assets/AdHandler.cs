// Скрипт взят с сайт http://unityblog.ru/
// Библиотека https://github.com/googleads/googleads-mobile-unity/releases


using System;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Ad
{
	public class AdHandler : MonoBehaviour
	{
		

		
		 string bannerId="";
		 string interstitialId="";
		 string rewardedId="";
		 

 				
		
		private InterstitialAd _interstitialAd;
		private BannerView _banner;
		private RewardedAd _rewardedAd;
		
		public static AdHandler instance;
		private bool _noAds;
		
		private string noAdsKey = "NoAds";
		public event Action GetReward;
		public bool ShowingBanner {get; private set;}
/*



Примеры кода
// Показать
	 if (!AdHandler.instance.ShowingBanner)
	AdHandler.instance.ShowBanner(true);

// Спрятать
	 if (AdHandler.instance.ShowingBanner)
				AdHandler.instance.ShowBanner(false);}
			
			

using Ad;

    public void Play_ReklamaMini()
    {

AdHandler.instance.ShowBanner(true);

    }
   public void PlayReklama()
    {
 AdHandler.instance.ShowInterstitialAd();

  }
  
  IEnumerator StartReklama ()
{

	yield return new WaitForSeconds(12f);
	AdHandler.instance.ShowBanner(true);			
}
		StartCoroutine("StartReklama");
*/ 		
		
// Чтобы реклама не убиралась		
		private void Awake()
		{
			
			if (instance == null)
			{
				instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else
				Destroy(gameObject);
		
		}
		
// Стартуем		
		private void Start()
		{
		
// Включить тестовые ID		
bannerId="ca-app-pub-3940256099942544/1033173712";
interstitialId="ca-app-pub-3940256099942544/6300978111";
rewardedId="ca-app-pub-3940256099942544/5224354917";
		
			_noAds = PlayerPrefs.GetInt(noAdsKey, 0) == 1;
			
// Проверяем рекламу			
			MobileAds.Initialize(status => {});

// Грузим всю рекламу

			RequestRewardedVideo();
			if (!_noAds)
			{
				RequestInterstitialAd();
				RequestBanner();
			}
		}
		
// Отключаем рекламу	(Для покупок)	
		public void RemoveAds()
		{
			PlayerPrefs.SetInt(noAdsKey, 1);
			 ShowBanner(false);
		}
		
// Показываем баннер		
		public void ShowBanner(bool show)
		{
			_noAds = PlayerPrefs.GetInt(noAdsKey, 0) == 1; 
			if (!_noAds)
			{
				if (show){
					if (!ShowingBanner)
				_banner?.Show();
					ShowingBanner=true;
				}
				else{
				_banner?.Hide();
					ShowingBanner=false;
				}
			}

		}
// Получаем баннер		
		public void RequestBanner()
		{
			_banner = new BannerView(bannerId, AdSize.Banner, AdPosition.Top);
			AdRequest newRequest = new AdRequest.Builder().Build();
			_banner?.LoadAd(newRequest);
			_banner?.Hide();
		}
		
// Получаем межстраничку		
		public void RequestInterstitialAd()
		{
			_interstitialAd = new InterstitialAd(interstitialId);
			AdRequest request = new AdRequest.Builder().Build();
			_interstitialAd?.LoadAd(request);
		}
		
// Показываем межстраничку		
		public void ShowInterstitialAd()
		{
			_noAds = PlayerPrefs.GetInt(noAdsKey, 0) == 1; 
			if (!_noAds) 
			{
				if (_interstitialAd!= null && _interstitialAd.IsLoaded())
				{
					_interstitialAd?.Show();
					RequestInterstitialAd();
				}
			}
		}
		
		
// Получаем денежку		
		public void HandleRewardBasedVideoRewarded(object sender, Reward args)
		{
	// тут код писать сюда
	print("привет");
		
		}
		
		
		public void HandleRewardedAdClosed(object sender, EventArgs args)
		{
			RequestRewardedVideo();
		}
		
			private void RequestRewardedVideo()
		{
			_rewardedAd = new RewardedAd(rewardedId);
	    
			_rewardedAd.OnUserEarnedReward += HandleRewardBasedVideoRewarded;
			_rewardedAd.OnAdClosed += HandleRewardedAdClosed;
	    
			AdRequest request = new AdRequest.Builder ().Build();
			_rewardedAd?.LoadAd(request);
		}
		
		
		public void ShowRewardAd()
		{
			_noAds = PlayerPrefs.GetInt(noAdsKey, 0) == 1; 
			if (!_noAds)
			{
			if (_rewardedAd.IsLoaded())
			{
				_rewardedAd?.Show();
					} 
		}
	}
	
}
}