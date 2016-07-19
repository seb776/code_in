using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView.NodesElems.Nodes.Assets;
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
        private IRootDragNDrop _rootView = null;
        private IVisualNodeContainerDragNDrop _parentView = null;
        private INodePresenter _nodePresenter = null;
        private EditNodePanel EditMenu = null;
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

        public void AddGeneric(string name, EGenericVariance variance)
        {
            GenericLabel.Content += variance.ToString().ToLower() + " " + name;
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

        public void SetNodePresenter(INodePresenter nodePresenter)
        {
            System.Diagnostics.Debug.Assert(nodePresenter != null);
            _nodePresenter = nodePresenter;
        }
        public Point GetPosition()
        {
            return new Point(this.Margin.Left, this.Margin.Top);
        }
        public INodePresenter GetNodePresenter()
        {
            return _nodePresenter;
        }

        public void ShowEditMenu()
        {
            this.EditMenuAndAttributesLayout.Children.Clear();
            EditMenu = new EditNodePanel(_themeResourceDictionary);
            EditMenu.SetFields(_nodePresenter);
            this.EditMenuAndAttributesLayout.Children.Add(EditMenu);
        }


        public virtual void SetPosition(int left, int top)
        {
            this.Margin = new Thickness(left, top, 0, 0);
        }

        public void GetSize(out int x, out int y)
        {
            this.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            this.Arrange(new Rect(0, 0, this.DesiredSize.Width, this.DesiredSize.Height));
            x = (int)this.ActualWidth;
            y = (int)this.ActualHeight;
        }

        #region Events
        private void MainLayout_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Debug.Assert(_rootView != null);
            if (!Keyboard.IsKeyDown(Key.LeftShift))
                this._rootView.UnSelectAllNodes();
            try
            {
                this._rootView.SelectNode(this);
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
            e.Handled = true; // To avoid bubbling http://www.codeproject.com/Articles/464926/To-bubble-or-tunnel-basic-WPF-events

        }

        private void MainLayout_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            NodalView.CreateContextMenuFromOptions(this._nodePresenter.GetMenuOptions(), this.GetThemeResourceDictionary(), this._nodePresenter);
        }
        #endregion Events
        #endregion This
        #region INodeElem
        public void SetParentView(IVisualNodeContainerDragNDrop vc) { _parentView = vc; }
        public IVisualNodeContainerDragNDrop GetParentView() { return _parentView; }
        public virtual void SetRootView(IRootDragNDrop dnd) { _rootView = dnd; }
        public IRootDragNDrop GetRootView() { return _rootView; }
        #endregion INodeElem
    }
}
