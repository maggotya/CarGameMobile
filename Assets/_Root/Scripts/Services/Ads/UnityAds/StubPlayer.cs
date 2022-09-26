namespace Services.Ads.UnityAds
{
    internal class StubPlayer : UnityAdsPlayer
    {
        public StubPlayer(string id) : base(id) { }

        protected override void OnPlaying() { }
        protected override void Load() { }
    }
}
