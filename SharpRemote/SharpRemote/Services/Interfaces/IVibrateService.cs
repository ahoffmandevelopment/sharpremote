using System;

namespace SharpRemote.Services.Interfaces
{
    public interface IVibrateService
    {
        void Cancel();
        void Vibrate();
        void Vibrate(double duration);
        void Vibrate(TimeSpan duration);
    }
}