using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Expressions;
using ICSharpCode.NRefactory.CSharp;
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
        static ExpressionItem _selfPassingThroughCallbackTmp = null; // TODO @Seb this is disgusting but faster to dev TODO set it to null after anycallback except add
        BaseTile _parentTile = null;
        private Code_inLink _currentDraggingLink;
        private AIOAnchor _linkStart = null;
        private Point _lastPosition;
        List<AExpressionNode> _expression = null;
        List<INodeElem> _visualNodes = null;
        public DataFlowAnchor ExprOut = null;
        private ResourceDictionary _themeResourceDictionary = null;


        #region Constructors
        public ExpressionItem(ResourceDictionary themeResourceDictionary, INodalView nodalView, BaseTile parentTile)
        {
            _parentTile = parentTile;
            this.NodalView = nodalView;
            Debug.Assert(themeResourceDictionary != null);
            _themeResourceDictionary = themeResourceDictionary;
            this.Resources.MergedDictionaries.Add(_themeResourceDictionary);
            InitializeComponent();
            ExprOut = new DataFlowAnchor(themeResourceDictionary, this);
            ExprOut.ParentLinksContainer = this;
            ExprOut.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            ExprOut.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            ExprOut.SetName("out");
            this.ExpressionMainGrid.Children.Add(ExprOut);
            IsExpanded = false;
            _expression = new List<AExpressionNode>();
            _visualNodes = new List<INodeElem>();
            this.SetThemeResources("");
        }

        public ExpressionItem() :
            this(Code_inApplication.MainResourceDictionary, null, null)
        { throw new Exceptions.DefaultCtorVisualException(); }
        #endregion Constructors
        #region ContextMenu_callbacks
        static void _alignNodes(object[] objects)
        {
            _selfPassingThroughCallbackTmp.AlignNodes();
            _selfPassingThroughCallbackTmp = null;
        }
        static void _addNode_Callback(object[] objects)
        {
            ContextMenu cm = new ContextMenu();
            cm.Placement = System.Windows.Controls.Primitives.PlacementMode.Mouse;
            List<Tuple<NodePresenter.ECSharpNode, String>> types = new List<Tuple<NodePresenter.ECSharpNode, String>>();

            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.ANONYMOUS_METHOD_EXPRESSION, "Anonymous method"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.ANONYMOUS_TYPE_CREATE_EXPRESSION, "Anonymous type create"));
            types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.ARRAY_CREATE_EXPRESSION, "Array create"));
            types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.OBJECT_CREATE_EXPRESSION, "Object create"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.ARRAY_INITIALIZER_EXPRESSION, "Array init"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.BASE_REFERENCE_EXPRESSION, "Base reference"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.CHECKED_EXPRESSION, "Checked"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.CONDITIONAL_EXPRESSION, "Conditional"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.DIRECTION_EXPRESSION, "Direction"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.ERROR_EXPRESSION, "Error"));
            types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.DEFAULT_VALUE_EXPRESSION, "Default"));
            types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.PRIMITIVE_EXPRESSION, "Primitive"));
            types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.IDENTIFIER_EXPRESSION, "Identifier"));
            types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.NULL_REFERENCE_EXPRESSION, "Null"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.THIS_REFERENCE_EXPRESSION, "This"));
            types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.INDEXER_EXPRESSION, "Indexer"));
            types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.INVOCATION_EXPRESSION, "Invocation"));
            types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.ASSIGNMENT_EXPRESSION, "Assignment"));
            types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.UNARY_OPERATOR_EXPRESSION, "Unary operator"));
            types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.BINARY_OPERATOR_EXPRESSION, "Binary operator"));
            types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.IS_EXPRESSION, "Is"));
            types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.AS_EXPRESSION, "As"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.CAST_EXPRESSION, "Cast"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.LAMBDA_EXPRESSION, "Lambda"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.MEMBER_REFERENCE_EXPRESSION, "Member reference"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.NAMED_ARGUMENT_EXPRESSION, "Named argument"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.NAMED_EXPRESSION, "Named"));
            types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.PARENTHESIZED_EXPRESSION, "Parenthesis"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.POINTER_REFERENCE_EXPRESSION, "Pointer reference"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.QUERY_EXPRESSION, "Query"));
            types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.SIZEOF_EXPRESSION, "Sizeof"));
            types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.TYPE_OF_EXPRESSION, "Typeof"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.STACK_ALLOC_EXPRESSION, "Stackalloc"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.TYPE_REFERENCE_EXPRESSION, "Type reference"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.UNCHECKED_EXPRESSION, "Unchecked"));
            //types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.UNDOCUMENTED_EXPRESSION, "Undocumented"));
            types.Add(new Tuple<NodePresenter.ECSharpNode, String>(NodePresenter.ECSharpNode.UNSUP_EXPR, "Unsupported"));

            foreach (var entry in types)
            {
                MenuItem mi = new MenuItem();
                mi.Header = entry.Item2;
                mi.Click += mi_Click;
                mi.DataContext = entry; // TODO beaurk
                cm.Items.Add(mi);
            }
            cm.IsOpen = true;
        }
        #endregion ContextMenu_callbacks
        #region Align
        public void AlignNodes()
        {
            if (ExprOut != null && ExprOut._links.Count != 0)
            {
                var doubleArray = new List<List<AIONode>>();
                _aligNodesStraightRecur(doubleArray, ExprOut._links[0].Output.ParentNode, 0);
                doubleArray.Reverse();
                _alignFromArray(doubleArray);
                _alignFromArrayLast(doubleArray);
            }
        }

        float _getDirectChildMiddle(AIONode node, ref bool hasChild)
        {
            bool start = true;
            float yStart = 0.0f;
            float yEnd = 0.0f;

            foreach (var v in node._inputs.Children)
            {
                var input = v as AIOAnchor;
                if (input._links.Count != 0)
                {
                    var nodePos = input._links[0].Output.ParentNode.GetPosition();
                    if (start)
                    {
                        yStart = (float)nodePos.Y;
                        start = false;
                    }
                    int sizeX = 0, sizeY = 0;
                    input._links[0].Output.ParentNode.GetSize(out sizeX, out sizeY);
                    yEnd = (float)nodePos.Y + sizeY;
                }
            }
            if (start)
            {
                hasChild = false;
                return 0.0f;
            }
            hasChild = true;
            return yStart + ((yEnd - yStart) / 2.0f);
        }
        void _aligNodesStraightRecur(List<List<AIONode>> nodes, AIONode curNode, int depth)
        {
            if (depth >= nodes.Count)
                nodes.Add(new List<AIONode>());
            nodes[depth].Add(curNode);
            foreach (var v in curNode._inputs.Children)
            {
                var input = v as AIOAnchor;
                if (input._links.Count != 0)
                    _aligNodesStraightRecur(nodes, input._links[0].Output.ParentNode, depth + 1);
            }
        }
        void _alignFromArray(List<List<AIONode>> nodes)
        {
            float vertOffset = 20.0f;
            float horizontalOffset = 100.0f;
            float curX = 20.0f;
            foreach (var level in nodes)
            {
                float maxX = 0.0f;
                float curY = 20.0f;
                foreach (var node in level)
                {
                    node.SetPosition((int)curX, (int)curY);
                    int sizeX = 0, sizeY = 0;
                    node.GetSize(out sizeX, out sizeY);
                    curY += sizeY + vertOffset;
                    if (sizeX > maxX)
                        maxX = sizeX;
                }
                curX += maxX + horizontalOffset;
            }
        }
        void _alignFromArrayLast(List<List<AIONode>> nodes)
        {
            float vertOffset = 20.0f;
            foreach (var level in nodes)
            {
                float lastY = 0.0f;
                foreach (var node in level)
                {
                    bool hasChild = true;
                    float middle = _getDirectChildMiddle(node, ref hasChild);
                    int sizeX = 0, sizeY = 0;
                    node.GetSize(out sizeX, out sizeY);
                    var nodePos = node.GetPosition();
                    if (hasChild)
                    {
                        float halfY = sizeY / 2.0f;
                        node.SetPosition((int)nodePos.X, (int)(middle - halfY));
                        lastY = middle + halfY + vertOffset;
                    }
                    else
                    {
                        node.SetPosition((int)nodePos.X, (int)lastY);
                        lastY = (float)(node.GetPosition().Y + sizeY + vertOffset);
                    }
                }
            }
        }
        #endregion Align
        #region This
        /// <summary>
        /// Gets or set the expanded state of the item.
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.ExpressionMainGrid.IsEnabled;
            }
            set
            {
                this.ExpressionMainGrid.IsEnabled = value;
                if (value)
                {
                    this.ExpressionMainGrid.Visibility = System.Windows.Visibility.Visible;
                    this.PreviewCode.Visibility = System.Windows.Visibility.Collapsed;
                    AlignNodes();
                }
                else
                {
                    this.ExpressionMainGrid.Visibility = System.Windows.Visibility.Collapsed;
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
            e.Handled = true;
        }

        private void ItemName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (Code_inApplication.RootDragNDrop.DragMode != EDragMode.NONE)
                {
                    Code_inApplication.RootDragNDrop.Drop(this._parentTile.GetParentView());
                }
                else
                    this.IsExpanded = !this.IsExpanded;
                e.Handled = true;
            }
        }

        private void ExpressionsGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (Code_inApplication.RootDragNDrop.DraggingLink)
                {
                    this.UpdateLinkDraw(e.GetPosition(this.ExpressionsGrid));
                }
                else
                {
                    EDragMode dragMode = (Keyboard.IsKeyDown(Key.LeftCtrl) ? EDragMode.MOVEOUT : EDragMode.STAYINCONTEXT);
                    Code_inApplication.RootDragNDrop.UpdateDragInfos(dragMode, e.GetPosition((this.NodalView as ANodalView).MainGrid));
                }
                e.Handled = true;
            }
        }

        private void ExpressionsGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Code_inApplication.RootDragNDrop.DraggingLink)
                this.DropLink(null, false);
            else
            {
                Code_inApplication.RootDragNDrop.Drop(this);
            }
            e.Handled = true;
        }
        #endregion Events
        #endregion This
        #region Events
        private void ExpressionsGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.ExprOut.UpdateLinksPosition();
        }
        static void mi_Click(object sender, RoutedEventArgs e)
        {
            //Dictionary<Type, NodePresenter.ECSharpNode> types = new Dictionary<Type,NodePresenter.ECSharpNode>();
            var menuItem = (sender as MenuItem);
            var nodeType = menuItem.DataContext as Tuple<NodePresenter.ECSharpNode, String>;

            //types.Add(typeof(BinaryExprNode), NodePresenter.ECSharpNode.BINARY_OPERATOR_EXPRESSION);
            //types.Add(typeof(UnaryExprNode), NodePresenter.ECSharpNode.UNARY_OPERATOR_EXPRESSION);
            //types.Add(typeof(PrimaryExprNode), NodePresenter.ECSharpNode.PRIMITIVE_EXPRESSION);
            //types.Add(typeof(AsExprNode), NodePresenter.ECSharpNode.AS_EXPRESSION);
            //types.Add(typeof(IsExprNode), NodePresenter.ECSharpNode.IS_EXPRESSION);
            //types.Add(typeof(NullRefExprNode), NodePresenter.ECSharpNode.NULL_REFERENCE_EXPRESSION);
            //types.Add(typeof(ParenthesizedExprNode), NodePresenter.ECSharpNode.PARENTHESIZED_EXPRESSION);
            //types.Add(typeof(IdentifierExprNode), NodePresenter.ECSharpNode.IDENTIFIER_EXPRESSION);
            //types.Add(typeof(IndexerExprNode), NodePresenter.ECSharpNode.INDEXER_EXPRESSION);
            //types.Add(typeof(FuncCallExprNode), NodePresenter.ECSharpNode.INVOCATION_EXPRESSION);
            //types.Add(typeof(ArrayInitExprNode), NodePresenter.ECSharpNode.ARRAY_INITIALIZER_EXPRESSION);
            //types.Add(typeof(ArrayCreateExprNode), NodePresenter.ECSharpNode.ARRAY_CREATE_EXPRESSION);

            ////if (!types.ContainsKey(nodeType))
            ////    return;
            AstNode model = NodePresenter.InstantiateASTNode(nodeType.Item1);
            var uiElem = code_in.Views.NodalView.ANodalView.InstantiateVisualNode(nodeType.Item1, _selfPassingThroughCallbackTmp.NodalView, _selfPassingThroughCallbackTmp);
            if (uiElem != null)
            {
                var presenter = new NodePresenter((_selfPassingThroughCallbackTmp.NodalView as ANodalView).Presenter, model);
                presenter.SetView(uiElem as BaseNode);
                (uiElem as BaseNode).Presenter = presenter;
                (uiElem as BaseNode).SetParentView(_selfPassingThroughCallbackTmp);
                _selfPassingThroughCallbackTmp.AddNode(uiElem as BaseNode);
                _selfPassingThroughCallbackTmp = null;
            }
        }
        private void ExpressionsGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _selfPassingThroughCallbackTmp = this;
            Tuple<EContextMenuOptions, Action<object[]>>[] options = {
                                                                         new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.ADD, _addNode_Callback),
                                                                         new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.ALIGN, _alignNodes)//,
                                                                         //new Tuple<EContextMenuOptions, Action<object[]>>(EContextMenuOptions.COLLAPSE, _addNode_Callback)
                                                                     };
            code_in.Views.NodalView.ANodalView.CreateContextMenuFromOptions(options, this.GetThemeResourceDictionary(), null);
            e.Handled = true;
        }
        #endregion Events
        #region INodalViewElement
        public INodalView NodalView
        {
            get;
            set;
        }
        #endregion INodalViewElement
        #region IVisualNodeContainer
        public T CreateAndAddNode<T>(Presenters.Nodal.Nodes.INodePresenter nodePresenter) where T : UIElement, code_in.Views.NodalView.INode
        {
            System.Diagnostics.Debug.Assert(nodePresenter != null, "nodePresenter must be a non-null value");
            T node = (T)Activator.CreateInstance(typeof(T), this._themeResourceDictionary, this.NodalView, this); // this here is the link container
            node.SetNodePresenter(nodePresenter); // TODO @Seb replace by constructor param ?

            node.SetParentView(this);
            nodePresenter.SetView(node);
            _visualNodes.Add(node); // TODO @Seb @Steph For automatic placement
            this.AddNode(node);
            _expression.Add(node as AExpressionNode);
            return node;
        }

        public void AddNode<T>(T node, int idx = -1) where T : UIElement, code_in.Views.NodalView.INode
        {
            this.ExpressionsGrid.Children.Add(node as UIElement);
            ((ANodalView)this.NodalView)._registeredNodes.Add(node);
        }


        public void RemoveNode(Presenters.Nodal.INodeElem node)
        {
            foreach (var anchorInput in (node as AIONode)._inputs.Children)
            {
                var anchor = anchorInput as AIOAnchor;
                if (anchor._links.Count != 0)
                {
                    this.RemoveVisualLink(anchor._links[0].Link);
                    anchor._links[0].Output.RemoveLink(anchor._links[0], true);
                    anchor.RemoveLink(anchor._links[0], true);
                }
            }
            foreach (var anchorOutput in (node as AIONode)._outputs.Children)
            {
                var anchor = anchorOutput as AIOAnchor;
                if (anchor._links.Count != 0)
                {
                    this.RemoveVisualLink(anchor._links[0].Link);
                    anchor._links[0].Input.RemoveLink(anchor._links[0], true);
                    anchor.RemoveLink(anchor._links[0], true);
                }

            }
            this.ExpressionsGrid.Children.Remove(node as UIElement);
            _expression.Remove(node as AExpressionNode);
            ((ANodalView)this.NodalView)._registeredNodes.Remove(node);
        }
        #endregion IVisualNodeContainer
        #region ILinkContainer
        public bool DraggingLink
        {
            get;
            set;
        }


        public void DragLink(AIOAnchor from, bool isGenerated)
        {
            if (from._links.Count != 0)
            {
                _currentDraggingLink = from._links[0].Link;
                _linkStart = (from.Orientation == AIOAnchor.EOrientation.RIGHT ? from._links[0].Input : from._links[0].Output);
                _linkStart.RemoveLink(from._links[0], true);
                from.RemoveLink(from._links[0], true);
                Code_inApplication.RootDragNDrop.ParentLinkContainer = this;
                Code_inApplication.RootDragNDrop.DraggingLink = true;
            }
            else
            {
                DraggingLink = true;
                _currentDraggingLink = new Code_inLink(_themeResourceDictionary);
                this.ExpressionsGrid.Children.Add(_currentDraggingLink);
                _currentDraggingLink.StrokeThickness = 3;
                _linkStart = from;
                Code_inApplication.RootDragNDrop.ParentLinkContainer = this;
                Code_inApplication.RootDragNDrop.DraggingLink = true;
            }
        }

        public void DropLink(AIOAnchor to, bool isGenerated)
        {
            Code_inApplication.RootDragNDrop.DraggingLink = false;
            if (to == null)
            {
                _linkStart = null;
                if (_currentDraggingLink != null)
                    this.ExpressionsGrid.Children.Remove(_currentDraggingLink);
                _currentDraggingLink = null;
            }
            else
            {
                if (_currentDraggingLink != null)
                {
                    try
                    {
                        if (_linkStart.Orientation == to.Orientation)
                            throw new Exception("Cannot link two IO of the same type (input and input, or output and output)");
                        if (_linkStart == to)
                            throw new Exception("Cannot create a link between an IO and itself.");
                        if (_linkStart.ParentNode == to.ParentNode)
                            throw new Exception("Cannot create a link between two IO that belongs to the same node.");
                    }
                    catch (Exception except)
                    {
                        this.ExpressionsGrid.Children.Remove(_currentDraggingLink);
                        _linkStart = null;
                        _currentDraggingLink = null;
                        return;
                    }
                    IOLink link = new IOLink();
                    link.Input = (_linkStart.Orientation == AIOAnchor.EOrientation.LEFT ? _linkStart : to);
                    link.Output = (_linkStart.Orientation == AIOAnchor.EOrientation.LEFT ? to : _linkStart);
                    link.Link = _currentDraggingLink;
                    if (link.Input._links.Count != 0)
                        this.ExpressionsGrid.Children.Remove(link.Input._links[0].Link);
                    link.Input._links.Clear();
                    if (link.Output._links.Count != 0)
                    {
                        this.ExpressionsGrid.Children.Remove(link.Output._links[0].Link);
                        link.Output.RemoveLink(link.Output._links[0], true);
                    }
                    link.Output._links.Clear();
                    if (link.Input is DataFlowAnchor && link.Output is DataFlowAnchor && !isGenerated) // To apply links creation to AST for expressions
                    {
                        (link.Input as DataFlowAnchor).MethodAttachASTExpr((ICSharpCode.NRefactory.CSharp.Expression)((link.Output as DataFlowAnchor).ParentNode.GetNodePresenter().GetASTNode()));
                    }

                    _linkStart.AttachNewLink(link);
                    to.AttachNewLink(link);
                    if (to.ParentNode != null)
                        this.UpdateLinkDraw(to.GetAnchorPosition(to.ParentNode.Parent as UIElement));
                    _linkStart = null;
                    _currentDraggingLink = null;
                }
            }
        }

        public void UpdateLinkDraw(Point mousePosRelToParentLinkContainer)
        {
            System.Diagnostics.Debug.Assert(_currentDraggingLink != null);
            if (_linkStart != null)
            {
                Point startPosition = _linkStart.GetAnchorPosition(this.ExpressionsGrid);
                if (_linkStart.Orientation == AIOAnchor.EOrientation.LEFT) // Input
                {
                    _currentDraggingLink._x2 = startPosition.X;
                    _currentDraggingLink._y2 = startPosition.Y;
                    _currentDraggingLink._x1 = mousePosRelToParentLinkContainer.X;
                    _currentDraggingLink._y1 = mousePosRelToParentLinkContainer.Y;
                }
                else
                {
                    _currentDraggingLink._x1 = startPosition.X;
                    _currentDraggingLink._y1 = startPosition.Y;
                    _currentDraggingLink._x2 = mousePosRelToParentLinkContainer.X;
                    _currentDraggingLink._y2 = mousePosRelToParentLinkContainer.Y;
                }
                _currentDraggingLink.InvalidateVisual();
            }
        }
        public void RemoveVisualLink(Code_inLink link)
        {
            Debug.Assert(link != null);
            this.ExpressionsGrid.Children.Remove(link);
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
            this.ItemName.SetResourceReference(Label.ForegroundProperty, "DefaultStmtNodeTypeForeGroundColor");
            this.PreviewCode.SetResourceReference(Label.ForegroundProperty, "DefaultStmtNodeTypeForeGroundColor");
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
