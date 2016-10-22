using System;
using System.Windows;
using System.Windows.Controls;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Core;

namespace ProjectManager.WPFComponents
{
    public static class DialogHelper
    {
        public static MessageBoxResult ShowDialog(string message)
        {
            return ShowDialog(String.Empty, message);
        }

        public static MessageBoxResult ShowDialog(string title, string message)
        {
            return ShowDialog(title, message, MessageBoxButton.OK);
        }

        public static MessageBoxResult ShowDialog(string title, string message, MessageBoxButton buttons)
        {

            return Application.Current.Dispatcher.Invoke<MessageBoxResult>(() =>
            {
                DXDialog d = new DXDialog(title);
                ScrollViewer sv = new ScrollViewer { MaxHeight = 400, MaxWidth = 800, HorizontalScrollBarVisibility = ScrollBarVisibility.Auto, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
                sv.Content = new System.Windows.Controls.TextBlock { Text = message, Padding = new Thickness(10) };
                d.Content = sv;
                d.SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
                d.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                d.WindowStyle = WindowStyle.ToolWindow;
                d.MaxHeight = 400;
                d.MaxWidth = 800;
                d.Activate();
                return d.ShowDialog(buttons);
            });
        }
    }
}
