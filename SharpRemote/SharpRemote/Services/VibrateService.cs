using SharpRemote.Services;
using SharpRemote.Services.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(VibrateService))]
namespace SharpRemote.Services
{
    /// <summary>
    /// API for accessing vibration services on the device.
    /// </summary>
    public class VibrateService : IVibrateService
    {
        /// <summary>
        /// Cancel any current vibrations.
        /// </summary>
        public void Cancel() => Vibration.Cancel();

        /// <summary>
        /// Vibrate the device for 500ms.
        /// </summary>
        public void Vibrate() => Vibration.Vibrate();

        /// <summary>
        /// Vibrate the device for the specified number of milliseconds in the range[0, 5000].
        /// </summary>
        /// <param name="duration">The number of milliseconds to vibrate.</param>
        public void Vibrate(double duration) => Vibration.Vibrate(duration);
    }
}
