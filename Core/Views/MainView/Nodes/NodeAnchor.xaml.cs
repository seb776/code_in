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
    /// Interaction logic for NodeAnchor.xaml
    /// </summary>
    public partial class NodeAnchor : UserControl
    {
        protected BaseNode _parentNode;
        public NodeAnchor()
        {
            InitializeComponent();
            _parentNode = null;
        }
        public NodeAnchor(BaseNode parent)
        {
            InitializeComponent();
            _parentNode = parent;
        }
    }
}
