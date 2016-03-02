using code_in.Models;
using code_in.Presenters.Nodal;
using code_in.Views.NodalView.Nodes;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Assets;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Expressions;
using code_in.Views.NodalView.NodesElems.Nodes.Statements;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace code_in.Views.NodalView
{
    /// <summary>
    /// The Nodal view is the layout that is able to display Nodes From the NodalPresenter;
    /// </summary>
    public partial class NodalView : UserControl, IVisualNodeContainer, IVisualNodeContainerDragNDrop, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private INodeElem _draggingNode = null;
        private TransformationMode _nodeTransform = TransformationMode.NONE;
        private INodalPresenter _nodalPresenter = null;
        private Point _newNodePos = new Point(0, 0);
        private Line _currentLineDrawing;
        private Point _lastPosition = new Point(0, 0);

        public NodalView(ResourceDictionary themeResDict)
        {
            this._nodalPresenter = new NodalPresenter(this);
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
        }
        public NodalView() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }

        #region IVisualNodeContainerDragNDrop
        public void SelectNode(INodeElem node) { }
        public void UnSelectNode(INodeElem node) { }
        public void UnSelectAll() { }

        public void DragNodes(TransformationMode transform, INodeElem node)
        {
            _nodeTransform = transform;
            _draggingNode = node;
            if (_draggingNode != null && _draggingNode.GetParentView() != null)
            {
                if (_nodeTransform == TransformationMode.MOVE)
                {
                    if (_draggingNode.GetParentView().GetType().IsSubclassOf(typeof(AOrderedContentNode)))
                    {
                        Point relativeCoord = ((UIElement)_draggingNode).TransformToAncestor((_draggingNode.GetParentView() as BaseNode).ContentGrid).Transform(new Point(0, 0));
                        _draggingNode.GetParentView().RemoveNode(_draggingNode);
                        ((AOrderedContentNode)_draggingNode.GetParentView()).ContentGrid.Children.Add(_draggingNode as UIElement);
                        (_draggingNode as UserControl).Margin = new Thickness(0, relativeCoord.Y, 0, 0);
                    }
                }

                else if (_nodeTransform == TransformationMode.LINE)
                {
                    _currentLineDrawing = new Line();

                    _currentLineDrawing.Stroke = new SolidColorBrush(Colors.GreenYellow);
                    _currentLineDrawing.StrokeThickness = 3;
                    Canvas.SetZIndex(_currentLineDrawing, -9999999); // TODO Beuark
                    Point nodeAnchorRelativeCoord;
                    if (((_draggingNode.GetParentView() as BaseNode).GetParentView() as BaseNode) != null)
                    {
                        nodeAnchorRelativeCoord = (_draggingNode as IOItem)._nodeAnchor.TransformToAncestor((_draggingNode.GetParentView() as BaseNode)).Transform(new Point(0, 0));
                    }
                    else
                    {
                        nodeAnchorRelativeCoord = (_draggingNode as IOItem)._nodeAnchor.TransformToAncestor(this.MainGrid).Transform(new Point(0, 0));

                    }
                    _currentLineDrawing.X1 = nodeAnchorRelativeCoord.X;
                    _currentLineDrawing.Y1 = nodeAnchorRelativeCoord.Y + (_draggingNode as IOItem)._nodeAnchor.ActualHeight / 2;
                    _currentLineDrawing.X2 = _currentLineDrawing.X1;
                    _currentLineDrawing.Y2 = _currentLineDrawing.Y1;
                    if ((_draggingNode as IOItem)._nodeAnchor.IOLine != null)
                    {
                        this.RemoveLink(_draggingNode as IOItem);
                    }
                    if (((_draggingNode.GetParentView() as BaseNode).GetParentView() as BaseNode) != null)
                    {
                        ((_draggingNode.GetParentView() as BaseNode).GetParentView() as BaseNode).ContentGrid.Children.Add(_currentLineDrawing);
                    }
                    else
                    {
                        this.MainGrid.Children.Add(_currentLineDrawing);
                    }
                }
            }
        }

        public void DropNodes(INodeElem node)
        {
            // Moving inside orderedContentNode
            if (_draggingNode != null && _draggingNode.GetParentView() != null)
            {

                if (_nodeTransform == TransformationMode.MOVE)
                {
                    if (_draggingNode.GetParentView().GetType().IsSubclassOf(typeof(AOrderedContentNode)))
                    {
                        ((AOrderedContentNode)_draggingNode.GetParentView()).ContentGrid.Children.Remove(_draggingNode as UIElement);
                        MethodInfo mi = ((AOrderedContentNode)_draggingNode.GetParentView()).GetType().GetMethod("AddNode");
                        MethodInfo gmi = mi.MakeGenericMethod(_draggingNode.GetType());
                        Object[] prm = { _draggingNode, ((AOrderedContentNode)_draggingNode.GetParentView()).GetDropIndex(new Point(0, (_draggingNode as UserControl).Margin.Top)) };
                        gmi.Invoke(_draggingNode.GetParentView(), prm);
                        ((UserControl)_draggingNode).Margin = new Thickness();
                    }
                }

                else if (_nodeTransform == TransformationMode.LINE)
                {
                    Canvas.SetZIndex(_currentLineDrawing, 9999999);

                    if (node == null ||
                        ((_draggingNode as IOItem).Orientation == IOItem.EOrientation.LEFT) && (node as IOItem).Orientation == IOItem.EOrientation.LEFT || // line from input to input
                        ((_draggingNode as IOItem).Orientation == IOItem.EOrientation.RIGHT) && (node as IOItem).Orientation == IOItem.EOrientation.RIGHT || // line from output to output
                        _draggingNode.GetParentView() == node.GetParentView())
                    {
                        // remove line
                        if (((_draggingNode.GetParentView() as BaseNode).GetParentView() as BaseNode) != null)
                        {
                            ((_draggingNode.GetParentView() as BaseNode).GetParentView() as BaseNode).ContentGrid.Children.Remove(_currentLineDrawing);
                        }
                        else
                        {
                            this.MainGrid.Children.Remove(_currentLineDrawing);
                        }
                    }
                    else
                    {
                        if (node.GetType().IsSubclassOf(typeof(IOItem)))
                        {
                            if ((node as IOItem).IOAttached != null)
                            {
                                this.RemoveLink(node as IOItem);
                            }
                            // if create link from output to input, swap begin and end point of the line
                            if ((_draggingNode as IOItem)._nodeAnchor._parentItem.Orientation == IOItem.EOrientation.LEFT)
                            {
                                Point tmpPoint = new Point();
                                tmpPoint.X = _currentLineDrawing.X1;
                                tmpPoint.Y = _currentLineDrawing.Y1;
                                _currentLineDrawing.X1 = _currentLineDrawing.X2;
                                _currentLineDrawing.Y1 = _currentLineDrawing.Y2;
                                _currentLineDrawing.X2 = tmpPoint.X;
                                _currentLineDrawing.Y2 = tmpPoint.Y;
                            }

                            // storing line in nodeanchor
                            (_draggingNode as IOItem)._nodeAnchor.IOLine = _currentLineDrawing;
                            (node as IOItem)._nodeAnchor.IOLine = _currentLineDrawing;
                            (_draggingNode as IOItem).IOAttached = node as IOItem;
                            (node as IOItem).IOAttached = (_draggingNode as IOItem);
                        }
                    }
                }
            }

            // Reset transformation
            _draggingNode = null;
            _nodeTransform = TransformationMode.NONE;
        }
        public void RemoveLink(IOItem node)
        {
            if (((node.GetParentView() as BaseNode).GetParentView() as BaseNode) != null)
            {
                ((node.GetParentView() as BaseNode).GetParentView() as BaseNode).ContentGrid.Children.Remove(node._nodeAnchor.IOLine);
            }
            else
            {
                this.MainGrid.Children.Remove(node._nodeAnchor.IOLine);
            }
            node.RemoveLink();
        }
        #endregion create
        #region IVisualNodeContainer
        public T CreateAndAddNode<T>() where T : UIElement, INodeElem
        {
            T node = (T)Activator.CreateInstance(typeof(T), this._themeResourceDictionary);

            node.SetParentView(null);
            node.SetRootView(this);

            this.AddNode(node);
            return node;
        }
        public void AddNode<T>(T node, int index = -1) where T : UIElement, INodeElem
        {
            this.MainGrid.Children.Add(node as UIElement);
        }

        public void RemoveNode(INodeElem node)
        {
            throw new NotImplementedException();
        }
        public void HighLightDropPlace(Point pos) { }
        public int GetDropIndex(Point pos) { return 0; }
        #endregion IVisualNodeContainer
        #region ICodeInVisual
        public void SetDynamicResources(String keyPrefix)
        {
            throw new NotImplementedException();
        }
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        #endregion ICodeInVisual

        public void OpenFile(String path)
        {
            this._nodalPresenter.OpenFile(path);
        }

        public void GenerateVisualNodes(NodalModel model) // TODO Do better (Give a graph of NodePresenter (can be very generic))
        {
            this._generateVisualASTRecur(model.AST, this);
        }

        private void _generateVisualASTRecur(AstNode node, IVisualNodeContainer parentContainer)
        {
            bool goDeeper = true;
            IVisualNodeContainer parentNode = null;
            if (node.Children == null)
                return;
            #region Namespace
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.NamespaceDeclaration))
            {
                NamespaceNode namespaceNode = parentContainer.CreateAndAddNode<NamespaceNode>();
                parentNode = namespaceNode;

                var tmpNode = (ICSharpCode.NRefactory.CSharp.NamespaceDeclaration)node;
                namespaceNode.SetName(tmpNode.Name);
            }
            #endregion
            #region Classes (interface, class, enum)
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.TypeDeclaration)) // Handles class, struct, enum (see further)
            {
                var tmpNode = (ICSharpCode.NRefactory.CSharp.TypeDeclaration)node;
                #region Enum
                if (tmpNode.ClassType == ICSharpCode.NRefactory.CSharp.ClassType.Enum)
                {
                    ClassDeclNode enumDeclNode = parentContainer.CreateAndAddNode<ClassDeclNode>();
                    enumDeclNode.SetName("EnumDecl " + tmpNode.Name);

                    foreach (var v in tmpNode.Members)
                    {
                        var tmp = v as EnumMemberDeclaration;
                        var item = enumDeclNode.CreateAndAddNode<DataFlowItem>();
                        if (tmp.Initializer.IsNull == false)
                            item.SetName(v.Name + " = " + tmp.Initializer.ToString());
                        else
                            item.SetName(v.Name);
                    }
                }
                #endregion Enum
                #region Class
                else if (tmpNode.ClassType == ICSharpCode.NRefactory.CSharp.ClassType.Class)
                {
                    ClassDeclNode classDeclNode = parentContainer.CreateAndAddNode<ClassDeclNode>();
                    parentNode = classDeclNode;

                    classDeclNode.SetName(tmpNode.Name);
                    // TODO protected internal
                    switch (tmpNode.Modifiers.ToString()) // Puts the right scope
                    {
                        case "Public":
                            classDeclNode.NodeScope.Scope = ScopeItem.EScope.PUBLIC;
                            break;
                        case "Private":
                            classDeclNode.NodeScope.Scope = ScopeItem.EScope.PRIVATE;
                            break;
                        case "Protected":
                            classDeclNode.NodeScope.Scope = ScopeItem.EScope.PROTECTED;
                            break;
                        default:
                            break;
                    }
                    //goDeeper = false;
                    foreach (var n in node.Children)
                    {
                        if (n.GetType() == typeof(ICSharpCode.NRefactory.CSharp.FieldDeclaration))
                        {
                            ICSharpCode.NRefactory.CSharp.FieldDeclaration field = n as ICSharpCode.NRefactory.CSharp.FieldDeclaration;

                            var item = classDeclNode.CreateAndAddNode<ClassItem>();
                            item.SetName(field.Variables.FirstOrNullObject().Name);
                            //item.SetItemType(field.ReturnType.ToString());
                            switch (field.Modifiers.ToString()) // Puts the right scope
                            {
                                case "Public":
                                    item.ItemScope.Scope = ScopeItem.EScope.PUBLIC;
                                    break;
                                case "Private":
                                    item.ItemScope.Scope = ScopeItem.EScope.PRIVATE;
                                    break;
                                case "Protected":
                                    item.ItemScope.Scope = ScopeItem.EScope.PROTECTED;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                #endregion Class
            }
            #endregion Classes (interface, class, enum)
            #region Method
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.MethodDeclaration))
            {
                FuncDeclNode funcDecl = parentContainer.CreateAndAddNode<FuncDeclNode>();
                funcDecl.MethodNode = node as ICSharpCode.NRefactory.CSharp.MethodDeclaration;
                ICSharpCode.NRefactory.CSharp.MethodDeclaration method = node as ICSharpCode.NRefactory.CSharp.MethodDeclaration;

                var parameters = method.Parameters.ToList();
                for (int i = 0; i < parameters.Count; ++i)
                {
                    var item = funcDecl.CreateAndAddNode<ClassItem>(); // TODO ArgItem
                    item.SetName(parameters[i].Name);
                    //item.SetItemType(parameters[i].Type.ToString());
                }
                funcDecl.SetName(method.Name);
                goDeeper = false;
            }
            #endregion
            //if (visualNode != null)
            //    parentContainer.AddNode(visualNode);
            if (goDeeper)
                foreach (var n in node.Children) if (n.GetType() != typeof(ICSharpCode.NRefactory.CSharp.FieldDeclaration))
                        _generateVisualASTRecur(n, (parentNode != null ? parentNode : parentContainer));
        }

        private void _generateFuncNodesBlockStmt(Statement stmtArg)
        {
            #region Block Statement
            if (stmtArg.GetType() == typeof(BlockStatement))
            {
                foreach (var stmt in (stmtArg as BlockStatement))
                {
                    this._generateFuncNodesBlockStmt(stmt);
                }
            }

            # region IfStmts
            if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.IfElseStatement))
            {
                var ifStmt = stmtArg as ICSharpCode.NRefactory.CSharp.IfElseStatement;
                var ifNode = this.CreateAndAddNode<IfStmtNode>();

                ifNode.Condition.SetName(ifStmt.Condition.ToString());

                this._generateFuncNodesBlockStmt(ifStmt.TrueStatement);
                this._generateFuncNodesBlockStmt(ifStmt.FalseStatement);
            }

            # endregion IfStmts
            # region Loops
            if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.WhileStatement))
            {
                var whileStmt = stmtArg as ICSharpCode.NRefactory.CSharp.WhileStatement;
                var nodeLoop = this.CreateAndAddNode<WhileStmtNode>();
                var cond = nodeLoop.CreateAndAddInput<DataFlowItem>();
                cond.SetName(whileStmt.Condition.ToString());
                this._generateFuncNodesBlockStmt(whileStmt.EmbeddedStatement);
            }
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ForStatement)) // TODO
            {
            }
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.DoWhileStatement)) // TODO
            {
            }
            else if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ForeachStatement)) // TODO
            {
            }
            # endregion Loops
            #endregion Block Statement
            #region Single Statement
            #region Variable Declaration
            if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.VariableDeclarationStatement))
            {
                var varStmt = (ICSharpCode.NRefactory.CSharp.VariableDeclarationStatement)stmtArg;
                var variableNode = this.CreateAndAddNode<VarDeclStmtNode>();
                variableNode.SetNodeType("VarDecl");
                foreach (var v in varStmt.Variables)
                {
                    var item = variableNode.CreateAndAddOutput<DataFlowItem>();
                    item.SetName(v.Name);
                    item.SetItemType(varStmt.Type.ToString());
                }
            }
            #endregion Variable Declaration
            #region ExpressionStatement
            if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ExpressionStatement))
            {
                var exprStmt = stmtArg as ExpressionStatement;

                var exprStmtNode = this.CreateAndAddNode<ExpressionStmtNode>();
                exprStmtNode.Expression.SetName(exprStmt.ToString());
                this._generateFuncExpressions(exprStmt.Expression);
            }
            #endregion ExpressionStatement
            #region Return Statement
            if (stmtArg.GetType() == typeof(ICSharpCode.NRefactory.CSharp.ReturnStatement))
            {
                var exprStmt = stmtArg as ExpressionStatement;

                var exprStmtNode = this.CreateAndAddNode<ReturnStmtNode>();
                if (exprStmt != null)
                {
                    this._generateFuncExpressions(exprStmt.Expression);
                }
            }
            #endregion Return Statement
            #endregion Single Statement
        }

        private void _generateFuncExpressions(ICSharpCode.NRefactory.CSharp.Expression expr)
        {
            if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.UnaryOperatorExpression))
            {
                var unaryExprOp = expr as ICSharpCode.NRefactory.CSharp.UnaryOperatorExpression;
                var unaryExpr = this.CreateAndAddNode<UnaryExprNode>();
                unaryExpr.OperandA.SetName(unaryExprOp.OperatorToken.ToString());
            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.BinaryOperatorExpression))
            {
                var binaryExpr = this.CreateAndAddNode<BinaryExprNode>();

            }
            else if (expr.GetType() == typeof(ICSharpCode.NRefactory.CSharp.InvocationExpression))
            {
                var invokExpr = this.CreateAndAddNode<FuncCallExprNode>();

            }
        }

        public void GenerateFuncNodes(MethodDeclaration method)
        {
            var entry = this.CreateAndAddNode<FuncEntryNode>();
            var exit = this.CreateAndAddNode<ReturnStmtNode>();
            exit.MakeNotRemovable();

            foreach (var i in method.Parameters)
            {
                var data = entry.CreateAndAddOutput<DataFlowItem>();
                data.SetName(i.Name);
                data.SetItemType(i.Type.ToString());
            }

            this._generateFuncNodesBlockStmt(method.Body);

            var returnType = exit.CreateAndAddInput<FlowNodeItem>();
            returnType.SetName(method.ReturnType.ToString());



            exit.Margin = new Thickness(0, 150, 0, 0);
        }


        #region Events
        void MainView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.DropNodes(null);
        }

        void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            int step = 2;
            Rect tmp = (Rect)code_in.Resources.SharedDictionaryManager.MainResourceDictionary["RectDims"];
            if (e.Key == Key.Add)
            {
                tmp.Width += step;
                tmp.Height += step;
            }
            if (e.Key == Key.Subtract && tmp.Width > 15)
            {
                tmp.Width -= step;
                tmp.Height -= step;
            }
        }

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            Vector diff;
            if ((_lastPosition.X + _lastPosition.Y) < 0.01)
                diff = new Vector(0, 0);
            else
            {
                diff = _lastPosition - e.GetPosition(this.MainGrid);
            }
            _lastPosition = e.GetPosition(this.MainGrid);

            if (_nodeTransform != TransformationMode.NONE /*&& _transformingNodes.Count() > 0*/)
            {

                //    //((ScrollViewer)((Grid)sender).Parent).ScrollToHorizontalOffset(((ScrollViewer)((Grid)sender).Parent).HorizontalOffset + (diff.X < 0 ? -.1 : .1));
                //    if (_transformationMode == TransformationMode.RESIZE)
                //    {
                //        double sizeX = (double)_transformingNodes[0].GetType().GetProperty("ActualWidth").GetValue(_transformingNodes[0]);
                //        double sizeY = (double)_transformingNodes[0].GetType().GetProperty("ActualHeight").GetValue(_transformingNodes[0]);
                //        double nSizeX = sizeX - diff.X;
                //        double nSizeY = sizeY - diff.Y;

                //        //MessageBox.Show((sizeX + diff.X).ToString());
                //        _transformingNodes[0].GetType().GetProperty("Width").SetValue(_transformingNodes[0], nSizeX);
                //        _transformingNodes[0].GetType().GetProperty("Height").SetValue(_transformingNodes[0], nSizeY);
                //        //((Nodes.TransformingNode.TransformingObject.GetType().get)Nodes.TransformingNode.TransformingObject)
                //    }
                if (_nodeTransform == TransformationMode.MOVE)
                {
                    Thickness margin = (Thickness)_draggingNode.GetType().GetProperty("Margin").GetValue(_draggingNode);

                    double marginLeft = margin.Left;
                    double marginTop = margin.Top;
                    Thickness newMargin = margin;
                    if (_draggingNode.GetParentView() == null)
                    {
                        newMargin.Left -= diff.X;
                        newMargin.Top -= diff.Y;
                    }
                    else
                    {
                        if (!_draggingNode.GetParentView().GetType().IsSubclassOf(typeof(AOrderedContentNode)))
                            newMargin.Left -= diff.X;
                        newMargin.Top -= diff.Y;
                    }

                    newMargin.Left = Math.Max(newMargin.Left, 0);
                    newMargin.Top = Math.Max(newMargin.Top, 0);

                    (_draggingNode as BaseNode).MoveNode(new Point(newMargin.Left, newMargin.Top));


                }


                // move the link if exist

                //        //    Nodes.BaseNode test = ((Nodes.BaseNode)Nodes.TransformingNode.TransformingObject);

                //        //if (lineOutput != null)
                //        //{
                //        //    lineOutput.X1 -= diff.X;
                //        //    lineOutput.Y1 -= diff.Y;
                //        //}
                //        //if (lineIntput != null)
                //        //{
                //        //    lineIntput.X2 -= diff.X;
                //        //    lineIntput.Y2 -= diff.Y;
                //        //}


                //    }
                else if (_nodeTransform == TransformationMode.LINE)
                {
                    _currentLineDrawing.X2 = e.GetPosition(this.MainGrid).X;
                    _currentLineDrawing.Y2 = e.GetPosition(this.MainGrid).Y;
                }

            }
        }

        private void MainGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // This automatically updates the list of accessible nodes
            // Need to be optimized (compute only the first time, as it uses reflection)
            List<Type> listOfBs = new List<Type>();
            foreach (var t in typeof(BaseNode).Assembly.GetTypes())
            {

                if (t.IsSubclassOf(typeof(BaseNode)) && !t.IsAbstract)
                {
                    listOfBs.Add(t);
                }
            }
            var cm = new ContextMenu();
            foreach (var t in listOfBs)
            {
                var m1 = new MenuItem();
                m1.Header = t.Name;
                m1.DataContext = t;
                m1.Click += m1_Click;
                cm.Items.Add(m1);
            }
            cm.Margin = new Thickness(e.GetPosition((this.Parent as FrameworkElement).Parent as FrameworkElement).X, e.GetPosition((this.Parent as FrameworkElement).Parent as FrameworkElement).Y, 0, 0);
            cm.IsOpen = true;
            // Setting the position of the node if we create one to the place the menu has been opened
            _newNodePos.X = e.GetPosition(this).X;
            _newNodePos.Y = e.GetPosition(this).Y;
        }

        void m1_Click(object sender, RoutedEventArgs e)
        {
            MethodInfo mi = this.GetType().GetMethod("CreateAndAddNode");
            MethodInfo gmi = mi.MakeGenericMethod(((sender as MenuItem).DataContext as Type));
            BaseNode node = gmi.Invoke(this, null) as BaseNode;

            node.Margin = new Thickness(_newNodePos.X, _newNodePos.Y, 0, 0);
            //var node = this._rootNode.CreateAndAddNode<((sender as MenuItem).DataContext as Type)>();
        }
        #endregion Events

        private void MainGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            this.DropNodes(null);
        }
    }
}
