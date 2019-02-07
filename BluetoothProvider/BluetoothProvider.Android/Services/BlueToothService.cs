using System;
using System.Collections.Generic;
using Android;
using Android.Bluetooth;
using Android.Support.V4.App;
using BluetoothProvider.Common;
using BluetoothProvider.Droid.Services;
using BluetoothProvider.Interfaces;
using BluetoothProvider.Models;
using Xamarin.Forms;
using LocalBluetoothDevice = BluetoothProvider.Models.BluetoothDevice;

[assembly: Dependency(typeof(BlueToothService_Android))]
namespace BluetoothProvider.Droid.Services
{
    public class BlueToothService_Android : IBlueToothService
    {        
        private Guid id = Guid.NewGuid();

        public void StartScan()
        {
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            if (adapter.IsEnabled)
            {               
                bool result = adapter.StartDiscovery();
                Console.WriteLine($"discovery start returned {result}");
            }
        }        

        public void AddDevice(LocalBluetoothDevice device)
        {
            MessagingCenter.Send<IBlueToothService, LocalBluetoothDevice>(this, MessageTypes.DeviceFound.ToString(), device);
        }

        public void RaiseScanFinishedEvent()
        {
            MessagingCenter.Send<IBlueToothService>(this, MessageTypes.ScanFinished.ToString());
        }

        public bool IsAdapterEnabled
        {
            get
            {
                BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
                return adapter.IsEnabled;
            }
        }

        public bool IsAdapterValid
        {
            get
            {
                BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
                return adapter != null;
            }

        }
    }
}