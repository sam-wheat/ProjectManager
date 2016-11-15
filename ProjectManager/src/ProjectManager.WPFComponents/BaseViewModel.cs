using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectManager.Model;
using ProjectManager.Services;
using LeaderAnalytics.Core.UI;
using ProjectManager.Core;

namespace ProjectManager.WPFComponents
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event EventHandler CloseViewEvent;
        public CommandBindingCollection CommandBindings;

        private IStateManager _StateManager;
        public IStateManager StateManager
        {
            get { return _StateManager; }
            private set
            {
                if (_StateManager != value)
                {
                    _StateManager = value;
                    RaisePropertyChanged("StateManager");
                }
            }
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                if (_IsBusy != value)
                {
                    _IsBusy = value;
                    RaisePropertyChanged("IsBusy");
                }
            }
        }


        public BaseViewModel(IStateManager stateManager)
        {
            StateManager = stateManager;
            CommandBindings = new CommandBindingCollection();
        }

        protected void ShowStatusBarMsg(string msg)
        {
            StateManager.StatusBarMessage = msg;
        }

        protected void CloseView()
        {
            if (CloseViewEvent != null)
                CloseViewEvent(this, new EventArgs());
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberNameAttribute] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
