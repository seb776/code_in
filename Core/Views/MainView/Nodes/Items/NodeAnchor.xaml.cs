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
    /// This class is used to represent the bind box to which the links will be attached
    /// It triggers the event for creating the links.
    /// </summary>
    public partial class NodeAnchor : UserControl
    {

        // protected to public?
        public IOItem _parentItem;

        // Line of the NodeAnchor
        public Line IOLine;

        // Position begin for the Line
        public Point lineBegin;

      //  public ILinkDraw line;

        public NodeAnchor()
        {
            // We need a default constructor to centralize the call to InitializeComponent();
            // and also to be able to create controls from XAML without passing parameters
            // and then be able to preview in the XAML editor
            InitializeComponent();
            _parentItem = null;
            IOLine = null;
        }
        public NodeAnchor(IOItem parent) :
            this() // Mandatory to have a call to InitializeComponent();
        {
            _parentItem = parent;
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // preview
            _parentItem.ParentNode.CreateLink(this);
            lineBegin = e.GetPosition(_parentItem.ParentNode.MainView.MainGrid);
            e.Handled = true;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_parentItem.Orientation == NodeItem.EOrientation.LEFT)
                _parentItem.ParentNode.MainView.enterInput = this;
            else
                _parentItem.ParentNode.MainView.enterOutput = this;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            _parentItem.ParentNode.MainView.enterInput = null;
            _parentItem.ParentNode.MainView.enterOutput = null;
        }
    }
}
