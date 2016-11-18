using code_in.Presenters.Nodal;
using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Nodes.Expressions;
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

namespace code_in.Views.NodalView.NodesElems.Tiles.Items
{
    /// <summary>
    /// A TileItem to contain expressions (nodes).
    /// </summary>
    public partial class ExpressionItem : UserControl, ITileItem, IVisualNodeContainer, IContainerDragNDrop, ICodeInTextLanguage, ILinkContainer
    {
        private Point _lastPosition;
        List<AExpressionNode> _expression = null;
        List<INodeElem> _visualNodes = null;
        public DataFlowAnchor ExprOut = null;
        private ResourceDictionary _themeResourceDictionary = null;

        public INodalView NodalView
        {
            get;
            set;
        }
        public ExpressionItem(ResourceDictionary themeResourceDictionary, INodalView nodalView)
        {
            this.NodalView = nodalView;
            Debug.Assert(themeResourceDictionary != null);
            _themeResourceDictionary = themeResourceDictionary;
            this.Resources.MergedDictionaries.Add(_themeResourceDictionary);
            InitializeComponent();
            ExprOut = new DataFlowAnchor(themeResourceDictionary, this);
            ExprOut.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            this.ExpressionsGrid.Children.Add(ExprOut);
            IsExpanded = false;
            _expression = new List<AExpressionNode>();
            _visualNodes = new List<INodeElem>();
        }
        public ExpressionItem() :
            this(Code_inApplication.MainResourceDictionary, null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }
        #region This
        /// <summary>
        /// Gets or set the expanded state of the item.
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.ExpressionsGrid.IsEnabled;
            }
            set
            {
                this.ExpressionsGrid.IsEnabled = value;
                if (value)
                {
                    this.ExpressionsGrid.Visibility = System.Windows.Visibility.Visible;
                    this.PreviewCode.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    this.ExpressionsGrid.Visibility = System.Windows.Visibility.Collapsed;
                    this.PreviewCode.Visibility = System.Windows.Visibility.Visible;
                }

            }
        }

        public void SetName(string name)
        {
            this.ItemName.Content = name;
        }
        #region Events
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.IsExpanded = !this.IsExpanded;
        }

        private void ItemName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.IsExpanded = !this.IsExpanded;
        }

        private void ExpressionsGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                EDragMode dragMode = (Keyboard.IsKeyDown(Key.LeftCtrl) ? EDragMode.MOVEOUT : EDragMode.STAYINCONTEXT);
                Code_inApplication.RootDragNDrop.UpdateDragInfos(dragMode, e.GetPosition((this.NodalView as NodalView).MainGrid));
            }
            e.Handled = true;
        }

        private void ExpressionsGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion Events
        #endregion This

        #region IVisualNodeContainer
        public T CreateAndAddNode<T>(Presenters.Nodal.Nodes.INodePresenter nodePresenter) where T : UIElement, code_in.Views.NodalView.INode
        {
            System.Diagnostics.Debug.Assert(nodePresenter != null, "nodePresenter must be a non-null value");
            T node = (T)Activator.CreateInstance(typeof(T), this._themeResourceDictionary, this.NodalView, this); // this here is the link container

            node.SetParentView(this);
            node.SetNodePresenter(nodePresenter);
            nodePresenter.SetView(node);
            _visualNodes.Add(node); // TODO @Seb @Steph For automatic placement
            this.AddNode(node);
            _expression.Add(node as AExpressionNode);
            return node;
        }

        public void AddNode<T>(T node, int idx = -1) where T : UIElement, code_in.Views.NodalView.INode
        {
            this.ExpressionsGrid.Children.Add(node as UIElement);
        }


        public void RemoveNode(Presenters.Nodal.INodeElem node)
        {
            this.ExpressionsGrid.Children.Remove(node as UIElement);
            _expression.Remove(node as AExpressionNode);
        }
        #endregion IVisualNodeContainer
        #region ILinkContainer
        public bool DraggingLink
        {
            get;
            set;
        }

        Code_inLink _currentDraggingLink;

        public void DragLink(AIOAnchor from, bool isGenerated)
        {
            DraggingLink = true;
            _currentDraggingLink = new Code_inLink();
            this.ExpressionsGrid.Children.Add(_currentDraggingLink);
        }

        public void DropLink(AIOAnchor to, bool isGenerated)
        {
            DraggingLink = false;
            this.ExpressionsGrid.Children.Remove(_currentDraggingLink);
            _currentDraggingLink = null;
        }

        public void UpdateLinkDraw(Point mousePosRelToParentLinkContainer)
        {
        }
        #endregion ILinkContainer
        #region IContainerDragNDrop
        public void UpdateDragInfos(Point mousePosToMainGrid)
        {
            var selectedNodes = Code_inApplication.RootDragNDrop.SelectedItems;
            if (selectedNodes.Count == 0)
                return;
            Vector diff;
            if ((_lastPosition.X + _lastPosition.Y) < 0.01)
                diff = new Vector(0, 0);
            else
                diff = _lastPosition - mousePosToMainGrid;
            _lastPosition = mousePosToMainGrid;

            //MessageBox.Show(_selectedNodes.GroupBy(n => n).Any(c => c.Count() > 1).ToString()); // Checks for doublons
            foreach (var selNode in selectedNodes)
            {
                dynamic draggingNode = selNode;
                Thickness margin = (Thickness)draggingNode.GetType().GetProperty("Margin").GetValue(draggingNode);
                double marginLeft = margin.Left;
                double marginTop = margin.Top;
                Thickness newMargin = new Thickness();

                newMargin.Left = margin.Left;
                newMargin.Top = margin.Top;
                newMargin.Right = margin.Right;
                newMargin.Bottom = margin.Bottom;
                newMargin.Left -= diff.X;
                newMargin.Top -= diff.Y;
                newMargin.Left = Math.Max(newMargin.Left, 0);
                newMargin.Top = Math.Max(newMargin.Top, 0);
                draggingNode.SetPosition((int)newMargin.Left, (int)newMargin.Top);
            }
        }
        public void Drag(EDragMode dragMode)
        {
            _lastPosition = new Point(0.0, 0.0);
        }

        public new void Drop(IEnumerable<IDragNDropItem> items)
        {

            //throw new NotImplementedException();
        }

        public bool IsDropValid(IEnumerable<IDragNDropItem> items)
        {
            if (Code_inApplication.RootDragNDrop.DragMode == EDragMode.STAYINCONTEXT)
                return true;
            // TODO @Seb
            return false;
        }

        #endregion IContainerDragNDrop
        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary()
        {
            return _themeResourceDictionary;
        }

        public void SetThemeResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInVisual
        #region ICodeInTextLanguage
        public ResourceDictionary GetLanguageResourceDictionary()
        {
            throw new NotImplementedException();
        }

        public void SetLanguageResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInTextLanguage
    }
}
