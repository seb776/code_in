using code_in.Presenters.Nodal;
using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Nodes.Expressions;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Base;
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
    public partial class ExpressionItem : UserControl, ITileItem, IVisualNodeContainerDragNDrop, ICodeInVisual, ICodeInTextLanguage
    {
        List<AExpressionNode> _expression = null;
        List<INodeElem> _visualNodes = null;
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
                    //testAlign();
               //     test();
                }
                else
                {
                    this.ExpressionsGrid.Visibility = System.Windows.Visibility.Collapsed;
                    this.PreviewCode.Visibility = System.Windows.Visibility.Visible;
                }

            }
        }

        private void testAlign() {
             for (int i = 0; i < 100; ++i)
                    {
                        this.AlignNode(0.1);
                    }
        }

        

        public DataFlowAnchor ExprOut = null;
        private ResourceDictionary _themeResourceDictionary = null;

        private void test()
        {
            int x = 0;
            int y = 0;
            bool f = true;
            AValueNode first = null;
            AValueNode rightNode = null;
            foreach (var t in _expression)
            {
                //if (f)
                //    first = t as AValueNode;
                //f = false;
                x += 100;
                y += 100;
                if (t != null)
                 t.SetPosition(x, y);
                if (f)
                 rightNode = t as AValueNode;
                f = false;
                //if (t as AExpressionNode != null)
                //    (t as AExpressionNode)._subGrid.Background = Brushes.Blue;
                
                //try
                //{
                //    if (!f)
                //        continue;
                //    t.SetName("test 1");
                //    (t._inputs.Children[0] as DataFlowAnchor)._links[0].Output.ParentNode.SetName("test 2");
                //    //MessageBox.Show(t.GetName() + "\n" + (t._inputs.Children[0] as DataFlowAnchor)._links[0].Output.ParentNode.GetName());
                //}
                //catch { }
                //f = false;
            }
            
            rightNode.SetName("first");
            try
            {
               // rightNode._inputs
                rightNode.ExprOut._links[0].Output._links[0].Input.ParentNode.SetName("sedonc");

            }
            catch { }
           // rightNode.ExprOut._links[0].Output._links[0].Output._links[0].Output.ParentNode.SetName("second");
            
        }

        public ExpressionItem(ResourceDictionary themeResourceDictionary)
        {
            Debug.Assert(themeResourceDictionary != null);
            _themeResourceDictionary = themeResourceDictionary;
            this.Resources.MergedDictionaries.Add(_themeResourceDictionary);
            InitializeComponent();
            ExprOut = new DataFlowAnchor(themeResourceDictionary);
            this.ExpressionsGrid.Children.Add(ExprOut);
            IsExpanded = false;
            _expression = new List<AExpressionNode>();
            _visualNodes = new List<INodeElem>();
        }
        public ExpressionItem() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }
        #region This
        public void SetName(string name)
        {
            this.ItemName.Content = name;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.IsExpanded = !this.IsExpanded;
        }
        #endregion This

        #region IVisualNodeContainerDragNDrop
        public bool IsDropNodeValid()
        {
            throw new NotImplementedException();
        }

        public int GetDropNodeIndex(Point pos)
        {
            throw new NotImplementedException();
        }

        public void HighLightDropNodePlace(Point pos)
        {
            throw new NotImplementedException();
        }

        public T CreateAndAddNode<T>(Presenters.Nodal.Nodes.INodePresenter nodePresenter) where T : UIElement, Presenters.Nodal.INodeElem
        {
            System.Diagnostics.Debug.Assert(nodePresenter != null, "nodePresenter must be a non-null value");
            T node = (T)Activator.CreateInstance(typeof(T), this._themeResourceDictionary);

            node.SetParentView(null);
            //node.SetRootView(this); // TODO @Seb to see
            node.SetNodePresenter(nodePresenter);
            nodePresenter.SetView(node);
            if (typeof(AIONode).IsAssignableFrom(typeof(T)))
            {
                //(node as AIONode).SetParentLinksContainer(this);
            }
            _visualNodes.Add(node); // TODO @Seb @Steph For automatic placement
            this.AddNode(node);
            _expression.Add(node as AExpressionNode);
            //if ((node as AExpressionNode) != null) {
            //    MessageBox.Show(node.GetName());
            //  //  node.SetName("pas null");
            //}
            //else
            //{
            //    MessageBox.Show(node.GetName());
            //   //MessageBox.Show("gros nase");
            //   // node.SetName("gros nase");
            //}
            return node;
        }

        public void AddNode<T>(T node, int idx = -1) where T : UIElement, Presenters.Nodal.INodeElem
        {
            this.ExpressionsGrid.Children.Add(node as UIElement);
        }


        public void RemoveNode(Presenters.Nodal.INodeElem node)
        {
            this.ExpressionsGrid.Children.Remove(node as UIElement);
            _expression.Remove(node as AExpressionNode);
        }

        public void AlignNode(double deltaTime)
        {
            
            
            
            
            const double pixelsBySec = 25.0;
            const double expressionLinksWidth = 100.0;
            const double expressionsHeightDiff = 25.0;
            Dictionary<INodeElem, Point> _statementNodes = new Dictionary<INodeElem, Point>();

           /* foreach (var curNode in _visualNodes)
            {
                if (curNode is AStatementNode)
                {
                    _expressionsUnderStatement[curNode as AStatementNode] = GetExpressionsAttachedToStatement(curNode as AStatementNode);
                }
            }*/

            Dictionary<INodeElem, Point> calculatedPositions = new Dictionary<INodeElem, Point>();
            foreach (var curNode in _expression)
            {
                calculatedPositions[curNode] = curNode.GetPosition();
            }
            foreach (var curNode in _expression)
            {
                if (curNode.ExprOut != null && curNode.ExprOut._links.Count != 0)
                {
                    AValueNode rightNode = curNode.ExprOut._links[0].Output.ParentNode as AValueNode;
                    rightNode.SetName("PESTE");
                    MessageBox.Show("");
                        
                        int sizeX = 0, sizeY = 0;
                        curNode.GetSize(out sizeX, out sizeY);
                        double deltaX = rightNode.GetPosition().X - (sizeX + curNode.GetPosition().X + expressionLinksWidth);
                        deltaX = deltaX / (deltaTime * pixelsBySec);
                        deltaX *= 0.5;
                    
                        double deltaY = 0.0;
                        if (rightNode._inputs.Children != null)
                        {
                            double totalSizeYNode = 0.0;
                            for (int i = 0; i < rightNode._inputs.Children.Count; ++i)
                            {
                                if ((rightNode._inputs.Children[i] as AIOAnchor)._links.Count != 0)
                                {
                                    AValueNode leftNode = (rightNode._inputs.Children[i] as AIOAnchor)._links[0].Output.ParentNode as AValueNode;
                                    int sizeXLeftNode, sizeYLeftNode = 0;
                                    leftNode.GetSize(out sizeXLeftNode, out sizeYLeftNode);

                                    if (leftNode == curNode)
                                    {
                                        deltaY = rightNode.GetPosition().Y - leftNode.GetPosition().Y + expressionsHeightDiff * i + totalSizeYNode;
                                        deltaY = deltaY / (deltaTime * pixelsBySec);
                                        deltaY *= 0.5;
                                        calculatedPositions[curNode] = (Point)(calculatedPositions[curNode] - new Point(-deltaX, -deltaY));
                                        calculatedPositions[curNode] = new Point(Math.Max(calculatedPositions[curNode].X, 0.0), Math.Max(calculatedPositions[curNode].Y, 0.0));
                                    }
                                    totalSizeYNode += sizeYLeftNode;
                                }
                            }
                        }
                    }
                }

            foreach (var n in calculatedPositions)
            {
            //    MessageBox.Show(n.Value.X.ToString() + "      " + n.Value.Y.ToString());
                n.Key.SetPosition((int)n.Value.X, (int)n.Value.Y);
            }

          /*  foreach (var couple in _expressionsUnderStatement)
            {
                Dictionary<INodeElem, Point> calculatedPositions = new Dictionary<INodeElem, Point>();
                // TODO calculate size of Expression block
                foreach (var curNode in couple.Value)
                    calculatedPositions[curNode] = curNode.GetPosition();
                bool first = true;
                foreach (var curNode in couple.Value)
                {
                    if (curNode.ExprOut != null && curNode.ExprOut._links.Count != 0)
                    {
                        if (curNode.ExprOut._links[0].Input.ParentNode is AValueNode)
                        {
                            AValueNode rightNode = curNode.ExprOut._links[0].Input.ParentNode as AValueNode;
                            if (first)
                            {
                                first = false;
                                var parentStatementNode = (rightNode._outputs.Children[0] as AIOAnchor)._links[0].Input.ParentNode;
                                Point parentStatementPos = parentStatementNode.GetPosition();
                                int parentStatementSizeX, parentStatementSizeY = 0;
                                parentStatementNode.GetSize(out parentStatementSizeX, out parentStatementSizeY);
                                int tmpSizeX, tmpSizeY = 0;
                                rightNode.GetSize(out tmpSizeX, out tmpSizeY);
                                double deltaXParentStatement = parentStatementPos.X - (tmpSizeX + rightNode.GetPosition().X + expressionLinksWidth);
                                deltaXParentStatement = deltaXParentStatement / (deltaTime * pixelsBySec);
                                deltaXParentStatement *= 0.5;
                                double deltaYParentStatement = parentStatementPos.Y - rightNode.GetPosition().Y + parentStatementSizeY;
                                calculatedPositions[rightNode] = (Point)(calculatedPositions[rightNode] - new Point(-deltaXParentStatement, -deltaYParentStatement));
                            }
                            int sizeX = 0, sizeY = 0;
                            curNode.GetSize(out sizeX, out sizeY);
                            double deltaX = rightNode.GetPosition().X - (sizeX + curNode.GetPosition().X + expressionLinksWidth);
                            deltaX = deltaX / (deltaTime * pixelsBySec);
                            deltaX *= 0.5;

                            double deltaY = 0.0;
                            if (rightNode._inputs.Children != null)
                            {
                                double totalSizeYNode = 0.0;
                                for (int i = 0; i < rightNode._inputs.Children.Count; ++i)
                                {
                                    if ((rightNode._inputs.Children[i] as AIOAnchor)._links.Count != 0)
                                    {
                                        AValueNode leftNode = (rightNode._inputs.Children[i] as AIOAnchor)._links[0].Output.ParentNode as AValueNode;
                                        int sizeXLeftNode, sizeYLeftNode = 0;
                                        leftNode.GetSize(out sizeXLeftNode, out sizeYLeftNode);

                                        if (leftNode == curNode)
                                        {
                                            deltaY = rightNode.GetPosition().Y - leftNode.GetPosition().Y + expressionsHeightDiff * i + totalSizeYNode;
                                            deltaY = deltaY / (deltaTime * pixelsBySec);
                                            deltaY *= 0.5;
                                            calculatedPositions[curNode] = (Point)(calculatedPositions[curNode] - new Point(-deltaX, -deltaY));
                                            calculatedPositions[curNode] = new Point(Math.Max(calculatedPositions[curNode].X, 0.0), Math.Max(calculatedPositions[curNode].Y, 0.0));
                                        }
                                        totalSizeYNode += sizeYLeftNode;
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (var n in calculatedPositions)
                {
                    n.Key.SetPosition((int)n.Value.X, (int)n.Value.Y);
                }

                foreach (var n in _expressionsUnderStatement)
                {
                    _statementNodes[n.Key] = n.Key.GetPosition();

                    Point topLeftCorner = new Point();
                    Point bottomRightCorner = new Point();
                    first = true;

                    foreach (var expr in _expressionsUnderStatement[n.Key])
                    {
                        Point exprPos = expr.GetPosition();
                        int exprSizeX, exprSizeY = 0;
                        expr.GetSize(out exprSizeX, out exprSizeY);
                        if (first)
                        {
                            first = false;
                            topLeftCorner = exprPos;

                            bottomRightCorner = (Point)(exprPos - new Point(-exprSizeX, -exprSizeY));
                            continue;
                        }
                        if (exprPos.X < topLeftCorner.X)
                            topLeftCorner.X = exprPos.X;
                        if (exprPos.Y < topLeftCorner.Y)
                            topLeftCorner.Y = exprPos.Y;
                        if ((exprPos.X + exprSizeX) > bottomRightCorner.X)
                            bottomRightCorner.X = exprPos.X + exprSizeX;
                        if ((exprPos.Y + exprSizeY) > bottomRightCorner.Y)
                            bottomRightCorner.Y = exprPos.Y + exprSizeY;

                    }

                    double deltaX = 0.0;
                    if ((n.Key._inputs.Children[0] as FlowNodeAnchor)._links.Count != 0)
                    {
                        int sizeY = 0;
                        (n.Key._inputs.Children[0] as FlowNodeAnchor)._links[0].Output.ParentNode.GetSize(out sizeXleftStatementNode, out sizeY);

                        posLeftStatementNode = (n.Key._inputs.Children[0] as FlowNodeAnchor)._links[0].Output.ParentNode.GetPosition().X;
                        deltaX = posLeftStatementNode + sizeXleftStatementNode + expressionLinksWidth - n.Key.GetPosition().X + (bottomRightCorner.X - topLeftCorner.X);

                    }

                    deltaX = deltaX / (deltaTime * pixelsBySec);
                    deltaX *= 0.5;

                    _statementNodes[n.Key] = (Point)(_statementNodes[n.Key] - new Point(-deltaX, 0.0));
                }

                double deltaStatementY = 0.0;

                foreach (var n in _expressionsUnderStatement)
                {
                    for (int i = 0; i < n.Key._outputs.Children.Count; ++i)
                    {
                        if ((n.Key._outputs.Children[i] as AIOAnchor)._links.Count != 0)
                        {
                            AIONode rightNode = (n.Key._outputs.Children[i] as AIOAnchor)._links[0].Input.ParentNode;
                            int sizeX, sizeY = 0;
                            rightNode.GetSize(out sizeX, out sizeY);
                            deltaStatementY = n.Key.GetPosition().Y - rightNode.GetPosition().Y + statementHeightDiff * i;
                            deltaStatementY = deltaStatementY / (deltaTime * pixelsBySec);
                            deltaStatementY *= 0.5;
                            _statementNodes[rightNode] = (Point)(_statementNodes[rightNode] - new Point(0.0, -deltaStatementY));
                        }
                    }
                }

                foreach (var n in _statementNodes)
                {
                    n.Key.SetPosition((int)n.Value.X, (int)n.Value.Y);
                }
            }*/
        }

        #endregion IVisualNodeContainerDragNDrop

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
