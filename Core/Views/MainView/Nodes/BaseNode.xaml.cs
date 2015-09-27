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

namespace code_in.Views.MainView.Nodes
{
    public static class TransformingNode
    {
        public static Object TransformingObject = null; // Used to move or resize nodes
    }
    /// <summary>
    /// Interaction logic for BaseNode.xaml
    /// </summary>
    public partial class BaseNode : UserControl
    {
        public BaseNode()
        {
            InitializeComponent();
            this.InFlow.Label.SetValue(Label.ContentProperty, "Input flow");
            this.OutFlow.Label.SetValue(Label.ContentProperty, "Output flow");
            this.InFlow.Label.Foreground = new SolidColorBrush(Colors.GreenYellow);
            this.OutFlow.Label.Foreground = new SolidColorBrush(Colors.GreenYellow);
        }

        private void Polygon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TransformingNode.TransformingObject = this;
        }
    }
}
