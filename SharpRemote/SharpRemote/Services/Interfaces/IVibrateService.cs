namespace SharpRemote.Services.Interfaces
{
    /// <summary>
    /// API for accessing vibration services on the device.
    /// </summary>
    public interface IVibrateService
    {
        /// <summary>
        /// Cancel any current vibrations.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Vibrate the device for 500ms.
        /// </summary>
        void Vibrate();

        /// <summary>
        /// Vibrate the device for the specified number of milliseconds in the range[0, 5000].
        /// </summary>
        /// <param name="duration">The number of milliseconds to vibrate.</param>
        /// <remarks>On iOS, the device will only vibrate for 500ms, regardless of the value specified.</remarks>
        void Vibrate(double duration);
    }
}