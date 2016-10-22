using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.WindowsUI;
using DevExpress.Xpf.Mvvm;

namespace ProjectManager.WPFComponents
{
    public partial class MenuNavigator : DocumentPanel
    {
        public ICommand NavigateCommand { get; set; }

        public NavigationFrame Frame
        {
            get { return frame; }
        }

        public MenuNavigator()
        {
            InitializeComponent();
        }
    }
}
