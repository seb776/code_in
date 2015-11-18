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

namespace code_in.Views.MainView.Nodes.Items
{
    /// <summary>
    /// Interaction logic for NodeItem.xaml
    /// </summary>
    public partial class NodeItem : UserControl, INodeItem
    {
        public NodeItem()
        {
            InitializeComponent();
        }

        public enum EOrientation
        {
            LEFT = 0,
            RIGHT = 1,
        }
        private EOrientation _orientation;
        public EOrientation Orientation
        {
            get
            { return _orientation; }
            set
            {
                this.Container.FlowDirection = (value == EOrientation.LEFT ?
                    System.Windows.FlowDirection.LeftToRight :
                    System.Windows.FlowDirection.RightToLeft);
                // to avoid the type to be reversed recursively
                this.TypeField.TypePanel.FlowDirection = System.Windows.FlowDirection.LeftToRight;
                _orientation = value;
            }
        }
    }
}
