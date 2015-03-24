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

namespace code_in_test.WPF
{
    /// <summary>
    /// Interaction logic for testNode.xaml
    /// </summary>
    public partial class testNode : UserControl
    {
        Grid _win;

        public testNode(Grid win, double posX, double posY)
        {
            InitializeComponent();
            this._win = win;
            node.Margin = new Thickness(posX, posY, 0, 0);
        }

        

        private void onCLickDelete(object sender, RoutedEventArgs e)
        {
            this._win.Children.Remove(this);
        }



        
    }
}
