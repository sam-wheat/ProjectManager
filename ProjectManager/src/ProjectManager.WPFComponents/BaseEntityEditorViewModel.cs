using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Data.Linq;
using DevExpress.Data.WcfLinq;
using LeaderAnalytics.Core;
using LeaderAnalytics.Core.UI;
using ProjectManager.Core;

namespace ProjectManager.WPFComponents
{
    public abstract class BaseEntityEditorViewModel<T> : BaseViewModel where T : class
    {
        public event Action ActiveTabChanged;

        private EntityServerModeSource _DataSource; //http://www.devexpress.com/Support/Center/Example/Details/E4501
        public EntityServerModeSource DataSource
        {
            get { return _DataSource; }
            set
            {
                if (_DataSource != value)
                {
                    _DataSource = value;
                    RaisePropertyChanged("DataSource");
                }
            }
        }

        private T _CurrentItem;
        public T CurrentItem
        {
            get { return _CurrentItem; }

            set
            {
                if (_CurrentItem != value)
                {
                    _CurrentItem = value;
                    RaisePropertyChanged("CurrentItem");
                    RaisePropertyChanged("CanEdit");
                }
            }
        }

        private EntityEditor.EntityEditorActiveTab _ActiveTab;
        public EntityEditor.EntityEditorActiveTab ActiveTab
        {
            get { return _ActiveTab; }
            set
            {
                if (_ActiveTab != value)
                {
                    _ActiveTab = value;
                    RaisePropertyChanged("ActiveTab");

                    if (ActiveTabChanged != null)
                        ActiveTabChanged();
                }
            }
        }

        private string _ErrorMessage;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set
            {
                if (_ErrorMessage != value)
                {
                    _ErrorMessage = value;
                    RaisePropertyChanged("ErrorMessage");
                    RaisePropertyChanged("CanSearch");
                }
            }
        }

        public virtual bool CanSearch
        {
            get { return String.IsNullOrEmpty(ErrorMessage); }
        }

        public virtual bool CanEdit
        {
            get { return CurrentItem != null; }
        }


        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand AddNewCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CopyCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public BaseEntityEditorViewModel(IStateManager stateManager) : base(stateManager)
        {
            // handle this to close the window when the app shuts down: App.Current.Dispatcher.ShutdownStarted 
            DataSource = new EntityServerModeSource();
            SaveCommand = new RoutedCommand();
            CancelCommand = new RoutedCommand();
            CloseCommand = new RoutedCommand();
            AddNewCommand = new RoutedCommand();
            DeleteCommand = new RoutedCommand();
            CopyCommand = new RoutedCommand();
            RefreshCommand = new RoutedCommand();
            CommandBindings.Add(new CommandBinding(SaveCommand, SaveCommandHandler, SaveCommandCanExecute));
            CommandBindings.Add(new CommandBinding(CancelCommand, CancelCommandHandler, CancelCommandCanExecute));
            CommandBindings.Add(new CommandBinding(CloseCommand, CloseCommandHandler, CloseCommandCanExecute));
            CommandBindings.Add(new CommandBinding(AddNewCommand, AddNewCommandHandler, AddNewCommandCanExecute));
            CommandBindings.Add(new CommandBinding(DeleteCommand, DeleteCommandHandler, DeleteCommandCanExecute));
            CommandBindings.Add(new CommandBinding(CopyCommand, CopyCommandHandler, CopyCommandCanExecute));
            CommandBindings.Add(new CommandBinding(RefreshCommand, RefreshCommandHandler, RefreshCommandCanExecute));
        }

        protected virtual void SaveCommandHandler(object sender, ExecutedRoutedEventArgs e) { }
        protected virtual void CancelCommandHandler(object sender, ExecutedRoutedEventArgs e) { }
        protected virtual void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e) { }
        protected virtual void AddNewCommandHandler(object sender, ExecutedRoutedEventArgs e) { }
        protected virtual void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e) { }
        protected virtual void CopyCommandHandler(object sender, ExecutedRoutedEventArgs e) { }
        protected virtual void RefreshCommandHandler(object sender, ExecutedRoutedEventArgs e) { }

        protected virtual void SaveCommandCanExecute(object sender, CanExecuteRoutedEventArgs e) { }
        protected virtual void CancelCommandCanExecute(object sender, CanExecuteRoutedEventArgs e) { }
        protected virtual void CloseCommandCanExecute(object sender, CanExecuteRoutedEventArgs e) { }
        protected virtual void AddNewCommandCanExecute(object sender, CanExecuteRoutedEventArgs e) { }
        protected virtual void DeleteCommandCanExecute(object sender, CanExecuteRoutedEventArgs e) { }
        protected virtual void CopyCommandCanExecute(object sender, CanExecuteRoutedEventArgs e) { }
        protected virtual void RefreshCommandCanExecute(object sender, CanExecuteRoutedEventArgs e) { }
    }
}
