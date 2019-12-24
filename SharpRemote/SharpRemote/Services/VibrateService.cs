using SharpRemote.Services;
using SharpRemote.Services.Interfaces;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(VibrateService))]
namespace SharpRemote.Services
{
    public class VibrateService : IVibrateService
    {
        public void Cancel() => Vibration.Cancel();

        public void Vibrate() => Vibration.Vibrate();

        public void Vibrate(double duration) => Vibration.Vibrate(duration);

        public void Vibrate(TimeSpan duration) => Vibration.Vibrate(duration);
    }
}
