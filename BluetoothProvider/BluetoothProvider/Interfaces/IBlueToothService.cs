using BluetoothProvider.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BluetoothProvider.Interfaces
{
    public interface IBlueToothService
    {
        bool IsAdapterValid { get; }
        bool IsAdapterEnabled { get; }
        void StartScan();
        void AddDevice(BluetoothDevice device);
        event Action<BluetoothDevice> DeviceAdded;
        event Action ScanFinished;
        void RaiseScanFinishedEvent();
    }
}
