using System;

namespace Services.Ads
{
    internal interface IAdsShower
    {
        void ShowInterstitial();
        void ShowVideo(Action successShow);
    }
}
