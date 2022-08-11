using System;

namespace Scripts.Infrastructure.Services.Ads
{
    public interface IAdsService : IService
    {
        public event Action RewardedVideoRedy;

        bool IsRewardedVideoReady { get; }
        int Reward { get; }

        void Initialize();
        void ShowRewardedVideo(Action onVideoFinished);
    }
}