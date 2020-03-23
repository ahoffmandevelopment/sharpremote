using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;

namespace SharpRemote.Droid
{
    [Activity(
        Label = "SharpRemote",
        Icon = "@mipmap/icon",
        Theme = "@style/MyTheme.Splash",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            PerformInits(savedInstanceState);
            
            LoadApplication(new App());

            base.SetTheme(Resource.Style.MainTheme);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void PerformInits(Bundle savedInstanceState)
        {
            // Use full namespace for clarification on what's being inited.

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState); // Must be done after Forms.Init call.
            Acr.UserDialogs.UserDialogs.Init(this);
        }
    }
}