using BluetoothProvider.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BluetoothProvider.Common.Messages
{
    public class DeviceAddedMessage
    {
        public BluetoothDevice NewDevice { get; set; }
    }
}
