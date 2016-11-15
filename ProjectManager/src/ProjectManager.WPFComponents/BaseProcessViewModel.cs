using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectManager.Core;

namespace ProjectManager.WPFComponents
{
    public abstract class BaseProcessViewModel : BaseViewModel
    {
        private string _CloseButtonText;
        public string CloseButtonText
        {
            get { return _CloseButtonText; }
            set
            {
                if (_CloseButtonText != value)
                {
                    _CloseButtonText = value;
                    RaisePropertyChanged("CloseButtonText");
                }
            }
        }

        protected StringBuilder _StatusMessage;
        public string StatusMessage
        {
            get { lock (sync) { return _StatusMessage.ToString(); } }
            set
            {
                lock (sync) { _StatusMessage.AppendLine(DateTime.Now.ToShortTimeString() + " - " + value); }
                RaisePropertyChanged("StatusMessage");
            }
        }

        private bool _IsRunning;
        public bool IsRunning
        {
            get { return _IsRunning; }
            set
            {
                if (_IsRunning != value)
                {
                    _IsRunning = value;
                    CloseButtonText = IsRunning ? "Cancel" : "Close Panel";
                    RaisePropertyChanged("IsRunning");
                }
            }
        }
        private bool _IsCanceling;
        public bool IsCanceling
        {
            get { return _IsCanceling; }
            set
            {
                if (_IsCanceling != value)
                {
                    _IsCanceling = value;
                    RaisePropertyChanged("IsCanceling");
                }
            }
        }

        protected object sync = new object();
        public RoutedCommand RunProcessCommand { get; set; }
        public RoutedCommand CancelProcessCommand { get; set; }
        protected CancellationTokenSource CancelTokenSource;

        public BaseProcessViewModel(IStateManager stateManager) : base(stateManager)
        {
            _StatusMessage = new StringBuilder(10000);
            IsRunning = true;
            IsRunning = false; // force property change

            RunProcessCommand = new RoutedCommand();
            CancelProcessCommand = new RoutedCommand();

            CommandBindings.Add(new CommandBinding(RunProcessCommand, RunProcessCommandHandler, RunProcessCommandCanExecute));
            CommandBindings.Add(new CommandBinding(CancelProcessCommand, CancelProcessCommandHandler, CancelProcessCommandCanExecute));

        }

        protected virtual void RunProcessCommandHandler(object sender, ExecutedRoutedEventArgs e) { }
        protected virtual void CancelProcessCommandHandler(object sender, ExecutedRoutedEventArgs e) { }
        protected virtual void RunProcessCommandCanExecute(object sender, CanExecuteRoutedEventArgs e) { }
        protected virtual void CancelProcessCommandCanExecute(object sender, CanExecuteRoutedEventArgs e) { }
    }
}
