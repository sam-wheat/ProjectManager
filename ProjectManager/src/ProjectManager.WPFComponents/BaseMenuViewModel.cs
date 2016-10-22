using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaderAnalytics.Core;
using DevExpress.Xpf.LayoutControl;
using DevExpress.Xpf.Mvvm;
using ProjectManager.Core;

namespace ProjectManager.WPFComponents
{
    public class BaseMenuViewModel : BaseViewModel
    {
        private const string imageRoot = "pack://application:,,,/ProjectManager.WPFComponents;component/Resources/";

        private ObservableCollection<Tile> _Tiles;
        public ObservableCollection<Tile> Tiles
        {
            get { return _Tiles; }
            set
            {
                if (_Tiles != value)
                {
                    _Tiles = value;
                    RaisePropertyChanged("Tiles");
                }
            }
        }

        private string _MenuTitle;
        public string MenuTitle
        {
            get { return _MenuTitle; }
            set
            {
                if (_MenuTitle != value)
                {
                    _MenuTitle = value;
                    RaisePropertyChanged("MenuTitle");
                }
            }
        }

        private string _BackgroundImageName;
        public string BackgroundImageName
        {
            get { return _BackgroundImageName; }
            set
            {
                if (_BackgroundImageName != value)
                {
                    _BackgroundImageName = value;
                    RaisePropertyChanged("BackgroundImageName");
                }
            }
        }

        public BaseMenuViewModel(IServiceClient serviceClient, IStateManager stateManager) : base(serviceClient, stateManager)
        {
            Tiles = new ObservableCollection<Tile>();
        }

        public void CreateTile(string caption, string imageName, ICommand command, object commandParm)
        {
            Tiles.Add(new Tile
            {
                Header = caption,
                Background = LeaderAnalytics.Core.Utilities.GetImageBrush(imageRoot + imageName),
                Command = command,
                CommandParameter = commandParm
            });
        }
    }
}
