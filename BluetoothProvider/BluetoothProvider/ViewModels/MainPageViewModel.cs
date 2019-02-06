using BluetoothProvider.Interfaces;
using BluetoothProvider.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BluetoothProvider.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IBlueToothService _blueToothService = null;
        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                RaisePropertyChanged();
            }
        }
        public DelegateCommand RefreshBlueTooothDevicesCommand { get; set; }
        public ObservableCollection<BluetoothDevice> Devices { get; set; } = new ObservableCollection<BluetoothDevice>();

        public MainPageViewModel(INavigationService navigationService, IBlueToothService btService)
            : base(navigationService)
        {
            RefreshBlueTooothDevicesCommand = new DelegateCommand(RefreshBlueToothExecuted, RefreshBlueToothCanExecute);
            _blueToothService = btService;
            _blueToothService.DeviceAdded += BlueToothServiceDeviceAdded;
            _blueToothService.ScanFinished += () => ToggleRunningState();
        }

        private void BlueToothServiceDeviceAdded(BluetoothDevice obj)
        {
            if(!Devices.Any(d=>d.MacAddress == obj.MacAddress))
            {
                Devices.Add(obj);
            }
            
        }

        private bool RefreshBlueToothCanExecute()
        {
            return !IsRunning && _blueToothService != null;
        }

        private void RefreshBlueToothExecuted()
        {
            ToggleRunningState();
            Devices.Clear();
            if(!_blueToothService.IsAdapterValid)
            {
                return;
            }
            if(!_blueToothService.IsAdapterEnabled)
            {
                return;
            }
            try
            {
                _blueToothService.StartScan();
            }
            catch (Exception e)
            {
                //Log exception.                
            }
        }

        private void ToggleRunningState()
        {
            IsRunning = !IsRunning;
            RefreshBlueTooothDevicesCommand.RaiseCanExecuteChanged();
        }
    }
}
