using System;
using System.Collections.Generic;
using System.Text;

namespace BluetoothProvider.Models
{
    public class BluetoothDevice
    {
        const string EmptyValue = @"N\A";
        public BluetoothDevice(string name, string macAddress)
        {
            if (string.IsNullOrEmpty(name))
            {
                Name = EmptyValue;
            }
            else
            {
                Name = name;
            }
            if (string.IsNullOrEmpty(macAddress))
            {
                MacAddress = EmptyValue;
            }
            else
            {
                MacAddress = macAddress;
            }

        }
        public string Name { get; }
        public string MacAddress { get; }
    }
}
