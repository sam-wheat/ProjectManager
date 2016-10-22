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

namespace ProjectManager.WPFComponents
{

    public class LayoutItemX : DevExpress.Xpf.LayoutControl.LayoutItem
    {
        public double LabelWidth2
        {
            get { return (double)GetValue(LabelWidth2Property); }
            set { SetValue(LabelWidth2Property, value); }
        }

        public static readonly DependencyProperty LabelWidth2Property =
            DependencyProperty.Register("LabelWidth2", typeof(double), typeof(LayoutItemX), new PropertyMetadata(new double()));


        static LayoutItemX()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LayoutItemX), new FrameworkPropertyMetadata(typeof(LayoutItemX)));
        }
    }
}
