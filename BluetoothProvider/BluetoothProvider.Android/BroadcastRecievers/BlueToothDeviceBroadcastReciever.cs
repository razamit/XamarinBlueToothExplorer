using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BluetoothProvider.Interfaces;

namespace BluetoothProvider.Droid.BroadcastRecievers
{
    [BroadcastReceiver]
    [IntentFilter(new[] { BluetoothDevice.ActionFound, BluetoothAdapter.ActionDiscoveryStarted, BluetoothAdapter.ActionDiscoveryFinished })]
    public class BlueToothDeviceBroadcastReciever : BroadcastReceiver
    {
        public BlueToothDeviceBroadcastReciever()
        {

        }
        public override void OnReceive(Context context, Intent intent)
        {
            IBlueToothService service =  (IBlueToothService)App.Current.Container.Resolve(typeof(IBlueToothService));
            string action = intent.Action;
            if (action == BluetoothDevice.ActionFound)
            {
                BluetoothDevice newDevice = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);
                service.AddDevice(new Models.BluetoothDevice(newDevice.Name, newDevice.Address));
            }
            if(action == BluetoothAdapter.ActionDiscoveryFinished)
            {
                service.RaiseScanFinishedEvent();
            }
        }
    }
}