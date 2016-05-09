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

namespace code_in.Views.NodalView.NodesElems.Nodes.Base
{
    /// <summary>
    /// Interaction logic for BaseNode.xaml
    /// </summary>
    public abstract partial class BaseNode : UserControl, INodeElem, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private IVisualNodeContainerDragNDrop _rootView = null;
        private IVisualNodeContainer _parentView = null;
        public BaseNode(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
        }
        public BaseNode() :
            this(Code_inApplication.MainResourceDictionary)
        {
        }

        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public virtual void SetThemeResources(string keyPrefix)
        {
            this.HeaderLayout.SetResourceReference(Border.BackgroundProperty, keyPrefix + "MainColor");
            this.ContentLayout.SetResourceReference(Border.BorderBrushProperty, keyPrefix + "MainColor");
            this.ContentLayout.SetResourceReference(Border.BackgroundProperty, keyPrefix + "SecondaryColor");
            this.NodeName.SetResourceReference(Label.ForegroundProperty, keyPrefix + "SecondaryColor");
            this.NodeType.SetResourceReference(Label.ForegroundProperty, keyPrefix + "SecondaryColor");
        }
        #endregion ICodeInVisual

        #region This
        public void SetName(string name)
        {
            this.NodeName.Content = name;
        }
        public string GetName()
        {
            return this.NodeName.Content as string;
        }

        public void SetType(string type)
        {
            this.NodeType.Content = type;
        }

        /// <summary>
        /// Sets the visual selected state of the node. It does not affect anything other than the visual.
        /// </summary>
        /// <param name="isSelected">
        /// - True the node is highlighted
        /// - False the node is not highlighted
        /// </param>
        public void SetSelected(bool isSelected)
        {
            this.SelectionBorder.IsEnabled = isSelected;
            this.SelectionBorder.Visibility = (isSelected ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden);
        }
        #endregion This

        #region INodeElem
        public void SetParentView(IVisualNodeContainer vc) { _parentView = vc; }
        public IVisualNodeContainer GetParentView() { return _parentView; }
        public virtual void SetRootView(IVisualNodeContainerDragNDrop dnd) { _rootView = dnd; }
        public IVisualNodeContainerDragNDrop GetRootView() { return _rootView; }
        #endregion INodeElem

        public void MoveNode(Point pos)
        {
            this.Margin = new Thickness(pos.X, pos.Y, 0, 0);
            this.MoveNodeSpecial();
        }
        public abstract void MoveNodeSpecial();

        private void MainLayout_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.GetRootView().DragNodes(TransformationMode.MOVE, this, LineMode.NONE);
            e.Handled = true; // To avoid bubbling http://www.codeproject.com/Articles/464926/To-bubble-or-tunnel-basic-WPF-events

        }
    }
}
