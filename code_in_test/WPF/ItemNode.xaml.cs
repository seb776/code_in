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
    /// Interaction logic for ItemNode.xaml
    /// </summary>
    public partial class ItemNode : UserControl
    {
        public ItemNode()
        {
            InitializeComponent();
        }
        private int _orientation;
        public int Orientation
        {
            get
            {
                return _orientation;
            }
            set
            {
                _orientation = value;
                if (_orientation == 1)
                {
                    this.Circle.SetValue(Grid.ColumnProperty, 1);
                    this.Text.SetValue(Grid.ColumnProperty, 0);
                }
                else
                {
                    this.Circle.SetValue(Grid.ColumnProperty, 0);
                    this.Text.SetValue(Grid.ColumnProperty, 1);
                }
            }
        }
    }
}
