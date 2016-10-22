using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Configuration;
using System.Reflection;
using DevExpress.Xpf.Docking;
using ProjectManager.Domain;
using ProjectManager.Core;

namespace ProjectManager.WPFComponents
{
    public class StateManager : INotifyPropertyChanged, IStateManager
    {
        public int CurrentUserID { get; set; }
        public string CurrentUserName { get; set; }

        private string _StatusBarMessage;
        public string StatusBarMessage
        {
            get { return _StatusBarMessage; }
            set
            {
                if (_StatusBarMessage != value)
                {
                    _StatusBarMessage = value;
                    RaisePropertyChanged("StatusBarMessage");
                }
            }
        }
        
        private ILogger _Logger;
        public ILogger Logger { get { return _Logger; } }
        private static StateManager _Instance;
        private static StateManager Instance { get { return _Instance; } }
        
        public string AppVersion { get { return AssemblyName.GetAssemblyName(Assembly.GetEntryAssembly().Location).Version.ToString(); } } 
        
        private DockLayoutManager DockManager;

        public StateManager(ILogger logger)
        {
            // Initialize Logging
            _Logger = logger;
            // Verify database installation
            DataConfigManager.VerifyLocalDBInstallation(Logger);
        }

        public void SetDockManager(object manager)
        {
            DockManager = manager as DockLayoutManager;
        }
          
        public void LoadLayoutPanel(DocumentPanel container, bool allowMultiple = false)
        {

            if (DockManager == null)
                throw new Exception("DockManager is null.  Call SetDockManager first.");

            // Check to see if a panel already exists for the control type being loaded
            DocumentPanel dupe = DockManager.GetItem(container.Name) as DocumentPanel;


            // If the control is not loaded yet than load it otherwise bring into focus
            if (dupe == null || allowMultiple)
            {
                container.ClosingBehavior = ClosingBehavior.ImmediatelyRemove;
                container.FloatOnDoubleClick = true;
                container.FloatSize = new System.Windows.Size(1000, 600);

                var tabbedGroup = DockManager.LayoutRoot.Items.SingleOrDefault(x => x.Name == "tabbedGroup");

                if (tabbedGroup == null)
                {
                    tabbedGroup = new DocumentGroup { Name = "tabbedGroup", FloatOnDoubleClick = false, AllowFloat = false, DestroyOnClosingChildren = false, ClosingBehavior = ClosingBehavior.ImmediatelyRemove };
                    DockManager.LayoutRoot.Items.Add(tabbedGroup);  // do not call this method from the constructor of mainwindow because LayoutRoot will be null
                }
                DockManager.DockController.Dock(container, tabbedGroup, DevExpress.Xpf.Layout.Core.DockType.Fill);

            }
            else
            {
                dupe.BringIntoView();
                dupe.IsActive = true;
            }
        }

        

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
