using UnityEngine.Advertisements;

namespace Services.Ads.UnityAds
{
    internal sealed class RewardedAdsPlayer : UnityAdsPlayer
    {
        public RewardedAdsPlayer(string id) : base(id)
        { }

        protected override void OnPlaying() => Advertisement.Show(Id);
        protected override void Load() => Advertisement.Load(Id);
    }
}
