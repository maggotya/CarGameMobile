using System;

namespace Services.Ads
{
    internal interface IAdsPlayer
    {
        event Action Started;
        event Action Finished;
        event Action Failed;
        event Action Skipped;
        event Action BecomeReady;

        void Play();
    }
}
