using code_in.Exceptions;
using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView.NodesElems.Nodes.Assets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public abstract partial class BaseNode : UserControl, code_in.Views.NodalView.INode
    {

        private ResourceDictionary _themeResourceDictionary = null;
        private IContainerDragNDrop _parentView = null;
        private INodePresenter _nodePresenter = null;
        private EditNodePanel EditMenu = null;
        public BaseNode(ResourceDictionary themeResDict, INodalView nodalView)
        {
            this.NodalView = nodalView;
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
        }
        public BaseNode() :
            this(Code_inApplication.MainResourceDictionary, null)
        { throw new DefaultCtorVisualException(); }


        public virtual void Remove()
        {
            Debug.Assert(this.GetNodePresenter().GetASTNode() != null);
            Debug.Assert(_parentView != null);
            (_parentView as IVisualNodeContainer).RemoveNode(this); // TODO @Seb beurak
            this.GetNodePresenter().GetASTNode().Remove();
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


        public void SetType(string type)
        {
            this.NodeType.Content = type;
        }

        public void AddGeneric(string name, EGenericVariance variance)
        {
            GenericLabel.Content += variance.ToString().ToLower() + " " + name;
        }

        #region Events
        private void MainLayout_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Code_inApplication.RootDragNDrop.IsSelectedItem(this))
                Code_inApplication.RootDragNDrop.UnselectAllNodes();
            Code_inApplication.RootDragNDrop.AddSelectItem(this);

            e.Handled = true; // To avoid bubbling http://www.codeproject.com/Articles/464926/To-bubble-or-tunnel-basic-WPF-events

        }

        private void MainLayout_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            code_in.Views.NodalView.NodalView.CreateContextMenuFromOptions(this._nodePresenter.GetMenuOptions(), this.GetThemeResourceDictionary(), this._nodePresenter);
        }
        #endregion Events
        #endregion This
        #region INodeElem
        public void SetParentView(IContainerDragNDrop vc) { _parentView = vc; }
        public IContainerDragNDrop GetParentView() { return _parentView; }
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
        public void SelectHighLight(bool highlighetd)
        {
            this.SelectionBorder.IsEnabled = highlighetd;
            this.SelectionBorder.Visibility = (highlighetd ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden);

        }


        public void UpdateDisplayedInfosFromPresenter()
        {
            throw new NotImplementedException();
        }
        public abstract void InstantiateASTNode();
        public void SetName(string name)
        {
            this.NodeName.Content = name;
        }
        public string GetName()
        {
            return this.NodeName.Content as string;
        }
        #endregion INodeElem



        public INodalView NodalView
        {
            get;
            set;
        }


        public void MustBeRemovedFromContext()
        {
            throw new NotImplementedException();
        }

        public void RemoveFromContext()
        {
            throw new NotImplementedException();
        }
    }
}
