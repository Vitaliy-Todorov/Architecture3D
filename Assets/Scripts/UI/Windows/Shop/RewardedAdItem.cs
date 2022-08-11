using Scripts.Infrastructure.Services.Ads;
using Scripts.Infrastructure.Services.PersistentProgress;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Windows.Shop
{
    public class RewardedAdItem : MonoBehaviour
    {
        public Button ShowAdButton;
        public GameObject[] AdActiveObjects;
        public GameObject[] AdInactiveObjects;

        private IAdsService _adsService;
        private IPersistentProgressService _persistentProgress;

        public void Construct(IAdsService adsService, IPersistentProgressService progressService)
        {
            _adsService = adsService;
            _persistentProgress = progressService;
        }

        internal void Initialize()
        {
            ShowAdButton.onClick.AddListener(OnShowAdClicked);
            RefreshAvailableAd();
        }

        internal void Subscribe() =>
            _adsService.RewardedVideoRedy += RefreshAvailableAd;

        internal void Cleanup() =>
            _adsService.RewardedVideoRedy -= RefreshAvailableAd;

        private void OnShowAdClicked() =>
            _adsService.ShowRewardedVideo(OnVideoFinish);

        private void OnVideoFinish() =>
            _persistentProgress.Progress.WorldData.LootData.Add(_adsService.Reward);

        private void RefreshAvailableAd()
        {
            bool videoReady = _adsService.IsRewardedVideoReady;

            foreach (GameObject adActiveObject in AdActiveObjects)
                adActiveObject.SetActive(videoReady);

            foreach (GameObject adInactiveObject in AdInactiveObjects)
                adInactiveObject.SetActive(!videoReady);
        }
    }
}