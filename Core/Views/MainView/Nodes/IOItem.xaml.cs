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
    /// <summary>
    /// Interaction logic for IOItem.xaml
    /// </summary>
    public partial class IOItem : UserControl
    {
        public void SetItemName(String name)
        {
            this.Label.Content = name;
        }
        public IOItem(BaseNode parent)
        {
            InitializeComponent();
            _orientation = 0;
            _parentNode = parent;
        }

        private int _orientation; // Used to set the orientation of the node (left = 0, right = 1) TODO replace by an enum
        protected BaseNode _parentNode; // Used to access current node informations
        public int Orientation // TODO define an enum Orientation.Right .Left
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
                    this.Anchor.SetValue(Grid.ColumnProperty, 1);
                    this.Label.SetValue(Grid.ColumnProperty, 0);
                    this.Anchor.Margin = new Thickness(0, 0, -12, 0);
                    this.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                }
                if (_orientation == 0)
                {
                    this.Anchor.SetValue(Grid.ColumnProperty, 0);
                    this.Label.SetValue(Grid.ColumnProperty, 1);
                    this.Anchor.Margin = new Thickness(-12, 0, 0, 0);
                    this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                }
            }
        }
    }
}
