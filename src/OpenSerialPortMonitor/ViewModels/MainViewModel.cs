﻿using Caliburn.Micro; 
using System.ComponentModel.Composition; 
using System.Threading;
using System.Threading.Tasks;
using System.Windows; 
using Whitestone.OpenSerialPortMonitor.Main.Framework;
using Whitestone.OpenSerialPortMonitor.Main.Messages;

namespace Whitestone.OpenSerialPortMonitor.Main.ViewModels
{
    [Export(typeof(IShell))]
    public class MainViewModel : Screen, IShell
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;

        private bool _isAutoscroll = true;

        private SerialConnectorViewModel _serialConnectorView;
        public SerialConnectorViewModel SerialConnectorView
        {
            get
            {
                return _serialConnectorView;
            }
            set
            {
                _serialConnectorView = value;
                NotifyOfPropertyChange(() => SerialConnectorView);
            }
        }

        private SerialDataViewModel _serialDataView;
        public SerialDataViewModel SerialDataView
        {
            get
            {
                return _serialDataView;
            }
            set
            {
                _serialDataView = value;
                NotifyOfPropertyChange(() => SerialDataView);
            }
        }

        private SerialDataSendViewModel _serialDataSendView;
        public SerialDataSendViewModel SerialDataSendView
        {
            get
            {
                return _serialDataSendView;
            }
            set
            {
                _serialDataSendView = value;
                NotifyOfPropertyChange(() => SerialDataSendView);
            }
        }

        [ImportingConstructor]
        public MainViewModel(IEventAggregator eventAggregator, IWindowManager windowManager)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;

            base.DisplayName = "Open Serial Port Monitor";// +Assembly.GetExecutingAssembly().GetName().Version;
        }

        protected override async Task OnInitializeAsync(CancellationToken token)
        {
            SerialConnectorView = new SerialConnectorViewModel(_eventAggregator);
            SerialDataView = new SerialDataViewModel(_eventAggregator);
            SerialDataSendView = new SerialDataSendViewModel(_eventAggregator);
            await base.OnInitializeAsync(token);
        }

        public void FileExit()
        {
            Application.Current.Shutdown();
        }

        public async Task Autoscroll()
        {
            _isAutoscroll = !_isAutoscroll;
            await _eventAggregator.PublishOnUIThreadAsync(new Autoscroll { IsTurnedOn = _isAutoscroll });
        }

        public async Task OpenAbout()
        {
            await _windowManager.ShowDialogAsync(new AboutViewModel());
        }
    }
}
