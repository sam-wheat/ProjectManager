using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectManager.Model.Domain;
using ProjectManager.Domain;
using ProjectManager.Core;

namespace ProjectManager.WPFComponents
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : DXWindow, INotifyPropertyChanged
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set 
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    RaisePropertyChanged("UserName");
                }
            }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    RaisePropertyChanged("Password");
                }
            }
        }

        private Visibility _ErrorMsgVisibility;
        public Visibility ErrorMsgVisibility
        {
            get { return _ErrorMsgVisibility; }
            set 
            {
                if (_ErrorMsgVisibility != value)
                {
                    _ErrorMsgVisibility = value;
                    RaisePropertyChanged("ErrorMsgVisibility");
                }
            }
        }
        private IStateManager stateManager;
        private IServiceClient<IUsersService> usersService;

        public LoginDialog(IServiceClient<IUsersService> usersService, IStateManager stateManager)
        {
            InitializeComponent();
            DataContext = this;
            this.usersService = usersService;
            txtUserName.Focus();
            ErrorMsgVisibility = Visibility.Hidden;
            UserName = "admin";
            Password = "admin";
        }

        public void Login_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Content.ToString() == "Cancel")
                Close();
            else
            {
                // plug
                //AsyncResult<User> userResult = await serviceClient<IUsersService>().TryAsync(x => x.GetUser(UserName, Password));
                User user = null;
                // end plug

                if (user == null)
                    ErrorMsgVisibility = Visibility.Visible;
                else
                {
                    stateManager.CurrentUserID = user.ID;
                    stateManager.CurrentUserName = user.Name;
                    Close();
                }
            }
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        public new void RaisePropertyChanged([CallerMemberNameAttribute] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
