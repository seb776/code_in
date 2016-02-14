using code_in.Views.NodalView.NodesElems.Items.Base;
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

namespace code_in.Views.NodalView.NodesElems.Items.Assets
{
    /// <summary>
    /// This class is used to represent the bind box to which the links will be attached
    /// It triggers the event for creating the links.
    /// </summary>
    public partial class NodeAnchor : UserControl, ICodeInVisual
    {
        public void SetDynamicResources(String keyPrefix)
        {

        }
        private ResourceDictionary _themeResourceDictionary;
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        // protected to public?
        public IOItem _parentItem;

        // Line of the NodeAnchor
        public Line IOLine;
        public BezierSegment IOBezier;
        public PathFigure pthFigure;

        // Position begin for the Line
        public Point lineBegin;

      //  public ILinkDraw line;


        public NodeAnchor() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }

        public NodeAnchor(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
            _parentItem = null;
            IOLine = null;
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // preview
            //_parentItem.ParentNode.CreateLink(this);
            //lineBegin = e.GetPosition(_parentItem.ParentNode.MainView.MainGrid);
            //e.Handled = true;

        //   this._parentItem.createLink(this);
            e.Handled = true;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            //if (_parentItem.Orientation == NodeItem.EOrientation.LEFT)
            //    _parentItem.ParentNode.MainView.enterInput = this;
            //else
            //    _parentItem.ParentNode.MainView.enterOutput = this;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            //_parentItem.ParentNode.MainView.enterInput = null;
            //_parentItem.ParentNode.MainView.enterOutput = null;
        }
    }
}
