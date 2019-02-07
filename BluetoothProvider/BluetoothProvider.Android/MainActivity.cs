using System;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using BluetoothProvider.Droid.BroadcastRecievers;
using BluetoothProvider.Droid.Services;
using BluetoothProvider.Interfaces;
using Prism;
using Prism.Ioc;
using Prism.Services;

namespace BluetoothProvider.Droid
{
    [Activity(Label = "BluetoothProvider", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        readonly string[] _permissionsLocation =
                   {
                        Manifest.Permission.AccessFineLocation,
                        Manifest.Permission.Bluetooth,
                        Manifest.Permission.BluetoothAdmin
                    };
        const int RequestLocationId = 0;
        BlueToothDeviceBroadcastReciever bluetoothDeviceReceiver = null;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }
        protected override void OnResume()
        {
            base.OnResume();
            if (bluetoothDeviceReceiver == null)
            {
                 bluetoothDeviceReceiver = new BlueToothDeviceBroadcastReciever();
            }
            
            var intentFilter = new IntentFilter();
            intentFilter.AddAction(BluetoothDevice.ActionFound);
            intentFilter.AddAction(BluetoothAdapter.ActionDiscoveryStarted);
            intentFilter.AddAction(BluetoothAdapter.ActionDiscoveryFinished);
            RegisterReceiver(bluetoothDeviceReceiver, intentFilter);
            RequestPermissions();                       
        }

        private void RequestPermissions()
        {
            if(CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Android.Content.PM.Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, _permissionsLocation, RequestLocationId);
                return;
            }           
        }
      
        protected override void OnPause()
        {
            if (bluetoothDeviceReceiver != null)
            {
                UnregisterReceiver(bluetoothDeviceReceiver);
                bluetoothDeviceReceiver = null;
            }
            
            base.OnPause();
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IBlueToothService,BlueToothService_Android>();
        }
    }
}

