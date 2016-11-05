﻿using code_in.Exceptions;
using code_in.Managers;
using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Tiles;
using code_in.Views.Utils;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace code_in.Views.NodalView
{
    /// <summary>
    /// The Nodal view is the layout that is able to display Nodes From the NodalPresenter;
    /// </summary>
    public partial class NodalView : UserControl, INodalView
    {
        private List<INodeElem> _selectedNodes = null;
        public INodalPresenter _nodalPresenter = null;
        public bool IsDeclarative = true; // Defines if the view stores declarations or execution code
        private ResourceDictionary _themeResourceDictionary = null;
        
        private Point _lastPosition;
        private AIOAnchor _linkStart = null;
        private Code_inLink _currentLink = null;
        public ITileContainer RootTileContainer
        {
            get;
            set; // TODO From seb set it to private
        }

        public NodalView(ResourceDictionary themeResDict)
        {
            this._nodalPresenter = new NodalPresenterLocal(this);
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
            _selectedNodes = new List<INodeElem>();
            _lastPosition = new Point();
            RootTileContainer = new TileContainer(_themeResourceDictionary) as ITileContainer;
            this.MainGrid.Children.Add(RootTileContainer as TileContainer);
        }
        public NodalView() :
            this(Code_inApplication.MainResourceDictionary)
        { throw new DefaultCtorVisualException(); }

        #region This
        public void OpenFile(String path)
        {
            this._nodalPresenter.OpenFile(path);
        }
        public void EditFunction(FuncDeclItem node)
        {
            this._nodalPresenter.EditFunction(node);
        }
        public void EditProperty(PropertyItem node, bool isGetter)
        {
            if (isGetter)
                this._nodalPresenter.EditAccessor(node.PropertyNode.Getter);
            else
                this._nodalPresenter.EditAccessor(node.PropertyNode.Setter);
        }
        public void EditConstructor(ConstructorItem node)
        {
            this._nodalPresenter.EditConstructor(node);
        }
        #region Events
        void MainView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.DropNodes(null);
            this.DropLink(null, false);
            //this.DropNodes(null);
        }

        void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            int step = 2;
            Rect tmp = (Rect)Code_inApplication.MainResourceDictionary["RectDims"];
            if (e.Key == Key.S && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                this._nodalPresenter.SaveFile("TestOutput.cs");
            }
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

            if (e.Key == Key.L)
            {
                var lineType = Code_inApplication.MainResourceDictionary["linkType"];
                if (lineType == null)
                    Code_inApplication.MainResourceDictionary["linkType"] = 0;
                else
                    Code_inApplication.MainResourceDictionary["linkType"] = ((int)(lineType) == 0 ? 1 : 0);
            }
        }

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (_currentLink == null)
                    this.UpdateDragState(e.GetPosition(this.MainGrid));
                else
                    this.UpdateLinkDraw(e.GetPosition(this.MainGrid));
            }
        }

        void _currentLineDrawing_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Code_inLink aLine = sender as Code_inLink;
            e.Handled = true;
            var cm = new ContextMenu();
            var m1 = new MenuItem();
            m1.Header = "remove link";
            m1.Click += testClick;
            m1.DataContext = aLine;
            cm.Items.Add(m1);
            cm.Margin = new Thickness(e.GetPosition(this.MainGrid).X, e.GetPosition(this.MainGrid).Y, 0, 0);
            cm.IsOpen = true;
        }

        void testClick(object sender, RoutedEventArgs e)
        {
            Code_inLink aLine = (sender as MenuItem).DataContext as Code_inLink; // Datacontext isn't for that but it's easier
            // remove aLine
            this.MainGrid.Children.Remove(aLine);
            // need to remove line in nodeAnchor also
        }

        static public void CreateContextMenuFromOptions(Tuple<EContextMenuOptions, Action<object[]>>[] options, ResourceDictionary themeResDict, object presenter)
        {
            var im = new HexagonalMenu(themeResDict);

            foreach (var opt in options)
            {
                String buttonName = "Default";
                var imageSrc = new BitmapImage();
                imageSrc.BeginInit();
                switch (opt.Item1)
                {
                    case EContextMenuOptions.ADD:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/add.png");
                        buttonName = "Add";
                        break;
                    case EContextMenuOptions.ALIGN:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/align.png");
                        break;
                    case EContextMenuOptions.CLOSE:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/remove.png");
                        break;
                    case EContextMenuOptions.COLLAPSE:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/collapse.png");
                        break;
                    case EContextMenuOptions.COLLAPSEALL:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/collapse.png");
                        break;
                    case EContextMenuOptions.DUPLICATE:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/duplicate.png");
                        break;
                    case EContextMenuOptions.EDIT:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/edit.png");
                        break;
                    case EContextMenuOptions.EXPAND:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/expand.png");
                        break;
                    case EContextMenuOptions.EXPANDALL:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/expand.png");
                        break;
                    case EContextMenuOptions.GOINTO:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/go_into.png");
                        break;
                    case EContextMenuOptions.HELP:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/help.png");
                        break;
                    case EContextMenuOptions.REMOVE:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/remove.png");
                        buttonName = "Remove";
                        break;
                    case EContextMenuOptions.SAVE:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/save.png");
                        break;
                }
                imageSrc.EndInit();

                im.AddHexagonButtonCircle(buttonName, imageSrc, opt.Item2, presenter);
                //im.AddHexagonButtonCircle(buttonName, imageSrc, (parameters) =>
                //{
                //    var lineType = Code_inApplication.MainResourceDictionary["linkType"];
                //    if (lineType == null)
                //        Code_inApplication.MainResourceDictionary["linkType"] = 0;
                //    else
                //        Code_inApplication.MainResourceDictionary["linkType"] = ((int)(lineType) == 0 ? 1 : 0);
                //});
            }

            im.Placement = PlacementMode.Absolute;
            im.PlacementTarget = null;
            var tC = System.Windows.Forms.Control.MousePosition;
            im.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            im.HorizontalOffset = tC.X - (im.DesiredSize.Width / 2);
            im.VerticalOffset = tC.Y - (im.DesiredSize.Height / 2);
            im.IsOpen = true;
        }

        private void MainGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var options = _nodalPresenter.GetMenuOptions();
            CreateContextMenuFromOptions(options, this.GetThemeResourceDictionary(), this._nodalPresenter);
        }

        void clickChangeMode(object sender, RoutedEventArgs e)
        {
            var lineType = Code_inApplication.MainResourceDictionary["linkType"];
            if (lineType == null)
                Code_inApplication.MainResourceDictionary["linkType"] = 0;
            else
                Code_inApplication.MainResourceDictionary["linkType"] = ((int)(lineType) == 0 ? 1 : 0);
        }

        private void MainGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            //this.UnSelectAllNodes();
            this.DropLink(null, false);
            //this.DropNodes(null);
        }

        private void changeResourceLink(object sender, ResourcesEventArgs e)
        {
            //if (this.MainGrid != null)
            //    foreach (var t in this.MainGrid.Children)
            //    {
            //        if (t.GetType() == typeof(Code_inLink))
            //        {
            //            _link = (Code_inLink)t;
            //            _link.changeLineMode();
            //        }
            //    }
        }
        #endregion Events
        #endregion This
        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public void SetThemeResources(String keyPrefix) { throw new NotImplementedException(); }
        #endregion ICodeInVisual
        #region IVisualNodeContainerDragNDrop
        /*
        public void SelectNode(INodeElem node)
        {
            // TODO if correct to select these nodes together
            if (_selectedNodes.Count > 0)
                if (_selectedNodes[0].GetParentView() != node.GetParentView())
                    throw new Exception("Cannot select multiple elements that have not the same parent.");
            _selectedNodes.Add(node);
            node.SetSelected(true);
            //_selectedNodes = _selectedNodes.Distinct().ToList(); // Quick fix @z0rg
        }
        public void UnSelectNode(INodeElem node)
        {
            node.SetSelected(false);
            int idx = _selectedNodes.FindIndex(n => n == node);
            if (idx != -1)
            {
                _selectedNodes.RemoveAt(idx);
            }

        }
        public void UnSelectAllNodes()
        {
            foreach (var n in _selectedNodes)
                n.SetSelected(false);
            _selectedNodes.Clear();
        }
         */
        public void UpdateDragState(Point mousePosition)
        {
            Vector diff;
            if ((_lastPosition.X + _lastPosition.Y) < 0.01)
                diff = new Vector(0, 0);
            else
                diff = _lastPosition - mousePosition;
            _lastPosition = mousePosition;

            //MessageBox.Show(_selectedNodes.GroupBy(n => n).Any(c => c.Count() > 1).ToString()); // Checks for doublons
            for (int i = 0; i < _selectedNodes.Count; ++i)
            {
                dynamic draggingNode = _selectedNodes[i];
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
        //public void DragNodes(TransformationMode transform, INodeElem node, LineMode lm)
        //{
        //_nodeTransform = transform;
        //_draggingNode = node;
        //if (_draggingNode != null && _draggingNode.GetParentView() != null)
        //{
        //    if (_nodeTransform == TransformationMode.MOVE)
        //    {
        //        if (_draggingNode.GetParentView().GetType().IsSubclassOf(typeof(AOrderedContentNode)))
        //        {
        //            Point relativeCoord = ((UIElement)_draggingNode).TransformToAncestor((_draggingNode.GetParentView() as BaseNode).ContentLayout).Transform(new Point(0, 0));
        //            _draggingNode.GetParentView().RemoveNode(_draggingNode);
        //            ((AOrderedContentNode)_draggingNode.GetParentView()).ContentLayout.Children.Add(_draggingNode as UIElement);
        //            (_draggingNode as UserControl).Margin = new Thickness(0, relativeCoord.Y, 0, 0);
        //        }
        //    }

        //    else if (_nodeTransform == TransformationMode.LINE)
        //    {
        //        Point nodeAnchorRelativeCoord;
        //        if (((_draggingNode.GetParentView() as BaseNode).GetParentView() as BaseNode) != null)
        //            nodeAnchorRelativeCoord = (_draggingNode as AOItem)._nodeAnchor.TransformToAncestor((_draggingNode.GetParentView() as BaseNode)).Transform(new Point(0, 0));
        //        else
        //            nodeAnchorRelativeCoord = (_draggingNode as AOItem)._nodeAnchor.TransformToAncestor(this.MainGrid).Transform(new Point(0, 0));

        //        this._link = new Code_inLink();

        //        Canvas.SetZIndex(_link, -9999999); // TODO Beuark
        //        _link._x1 = nodeAnchorRelativeCoord.X;
        //        _link._y1 = nodeAnchorRelativeCoord.Y + (_draggingNode as AOItem)._nodeAnchor.ActualHeight / 2;
        //        _link._x2 = _link._x1;
        //        _link._y2 = _link._y1;

        //        if (_lineMode == LineMode.LINE)
        //            _link.changeLineMode(Code_inLink.ELineMode.LINE);
        //        else if (_lineMode == LineMode.BEZIER)
        //            _link.changeLineMode(Code_inLink.ELineMode.BEZIER);

        //        this._link.MouseRightButtonDown += _currentLineDrawing_MouseRightButtonDown;

        //        this.MainGrid.Children.Add(_link);
        //    }
        //}
        //}

        //public void DropNodes(INodeElem node)
        //{
        //// Moving inside orderedContentNode
        //if (_draggingNode != null && _draggingNode.GetParentView() != null)
        //{

        //    if (_nodeTransform == TransformationMode.MOVE)
        //    {
        //        if (_draggingNode.GetParentView().GetType().IsSubclassOf(typeof(AOrderedContentNode)))
        //        {
        //            ((AOrderedContentNode)_draggingNode.GetParentView()).ContentLayout.Children.Remove(_draggingNode as UIElement);
        //            MethodInfo mi = ((AOrderedContentNode)_draggingNode.GetParentView()).GetType().GetMethod("AddNode");
        //            MethodInfo gmi = mi.MakeGenericMethod(_draggingNode.GetType());
        //            Object[] prm = { _draggingNode, ((AOrderedContentNode)_draggingNode.GetParentView()).GetDropIndex(new Point(0, (_draggingNode as UserControl).Margin.Top)) };
        //            gmi.Invoke(_draggingNode.GetParentView(), prm);
        //            ((UserControl)_draggingNode).Margin = new Thickness();
        //        }
        //    }

        //    else if (_nodeTransform == TransformationMode.LINE)
        //    {
        //        if (node == null ||
        //                ((_draggingNode as AOItem).Orientation == AOItem.EOrientation.LEFT) && (node as AOItem).Orientation == AOItem.EOrientation.LEFT || // line from input to input
        //                ((_draggingNode as AOItem).Orientation == AOItem.EOrientation.RIGHT) && (node as AOItem).Orientation == AOItem.EOrientation.RIGHT || // line from output to output
        //                _draggingNode.GetParentView() == node.GetParentView())
        //        {

        //            this.MainGrid.Children.Remove(_link);
        //        }
        //        else
        //        {
        //            if ((_draggingNode as AOItem)._nodeAnchor._parentItem.Orientation == AOItem.EOrientation.LEFT)
        //            {
        //                Point tmpPoint = new Point();
        //                tmpPoint.X = _link._x1;
        //                tmpPoint.Y = _link._y1;
        //                _link._x1 = _link._x2;
        //                _link._y1 = _link._y2;
        //                _link._x2 = tmpPoint.X;
        //                _link._y2 = tmpPoint.Y;
        //            }

        //            // storing line in nodeanchor
        //            (_draggingNode as AOItem)._nodeAnchor.IOLine.Add(_link);
        //            (node as AOItem)._nodeAnchor.IOLine.Add(_link);
        //            (_draggingNode as AOItem).IOAttached = node as AOItem;
        //            (node as AOItem).IOAttached = (_draggingNode as AOItem);
        //        }
        //    }
        //}

        //// Reset transformation
        //_draggingNode = null;
        //_nodeTransform = TransformationMode.NONE;
        //}
        public void HighLightDropPlace(Point pos) { }
        public int GetDropIndex(Point pos) { return 0; }
        public void DropNodes(IVisualNodeContainerDragNDrop container)
        {
            if (container == null)
            {
                _lastPosition = new Point(0, 0);
            }
        }
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
        #endregion IVisualNodeContainerDragNDrop
        #region IVisualNodeContainer
        public T CreateAndAddNode<T>(INodePresenter nodePresenter) where T : UIElement, code_in.Views.NodalView.INode
        {
            System.Diagnostics.Debug.Assert(nodePresenter != null, "nodePresenter must be a non-null value");
            T node = (T)Activator.CreateInstance(typeof(T), this._themeResourceDictionary);

            node.SetParentView(null);
            node.SetNodePresenter(nodePresenter);
            nodePresenter.SetView(node);
            if (typeof(AIONode).IsAssignableFrom(typeof(T)))
            {
                (node as AIONode).SetParentLinksContainer(this);
            }
            this.AddNode(node);
            return node;
        }
        public void AddNode<T>(T node, int index = -1) where T : UIElement, code_in.Views.NodalView.INode
        {
            this.MainGrid.Children.Add(node as UIElement);
        }

        public void RemoveNode(INodeElem node)
        {
            throw new NotImplementedException();
        }
        #endregion IVisualNodeContainer
        #region INodalView
        public void RemoveLink(AIOAnchor anchor)
        {
            if (anchor._links.Count > 0)
            {
                var ioLink = anchor._links[0];
                var currentVisualLink = ioLink.Link;
                var output = anchor._links[0].Output;
                var input = anchor._links[0].Input;
                output.RemoveLink(ioLink, false);
                input.RemoveLink(ioLink, false);
                this.MainGrid.Children.Remove(currentVisualLink);
            }
        }
        public void DragLink(AIOAnchor from, bool isGenerated)
        {
            _linkStart = from;
            if (_linkStart._links.Count != 0)
            {
                _currentLink = _linkStart._links[0].Link;
                var ioLink = _linkStart._links[0];
                var output = _linkStart._links[0].Output;
                var input = _linkStart._links[0].Input;
                output.RemoveLink(ioLink, true);
                input.RemoveLink(ioLink, false);
                this.MainGrid.Children.Remove(_currentLink);
                this.MainGrid.Children.Add(_currentLink);
            }
            else
            {
                _currentLink = new Code_inLink();
                _currentLink.Stroke = new SolidColorBrush(Colors.GreenYellow);
                _currentLink.StrokeThickness = 3;
                this.MainGrid.Children.Add(_currentLink);
            }
            this.UpdateLinkDraw(Mouse.GetPosition(this.MainGrid));
        }
        public void DropLink(AIOAnchor to, bool isGenerated)
        {
            if (to == null)
            {
                _linkStart = null;
                if (_currentLink != null)
                    this.MainGrid.Children.Remove(_currentLink);
                _currentLink = null;
            }
            else
            {
                if (_currentLink != null)
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
                        this.MainGrid.Children.Remove(_currentLink);
                        _linkStart = null;
                        _currentLink = null;
                        throw except;
                    }
                    IOLink link = new IOLink();
                    link.Input = (_linkStart.Orientation == AIOAnchor.EOrientation.LEFT ? _linkStart : to);
                    link.Output = (_linkStart.Orientation == AIOAnchor.EOrientation.LEFT ? to : _linkStart);
                    link.Link = _currentLink;
                    if (link.Input._links.Count != 0)
                        this.MainGrid.Children.Remove(link.Input._links[0].Link);
                    link.Input._links.Clear();
                    if (link.Output._links.Count != 0)
                    {
                        this.MainGrid.Children.Remove(link.Output._links[0].Link);
                        link.Output.RemoveLink(link.Output._links[0], true);
                    }
                    link.Output._links.Clear();
                    if (link.Input is DataFlowAnchor && link.Output is DataFlowAnchor) // To apply links creation to AST for expressions
                        (link.Input as DataFlowAnchor).MethodAttachASTExpr((ICSharpCode.NRefactory.CSharp.Expression)((link.Output as DataFlowAnchor).ParentNode.GetNodePresenter().GetASTNode()));
                    else if (link.Input is FlowNodeAnchor && link.Output is FlowNodeAnchor && !isGenerated)
                        (link.Output as FlowNodeAnchor).AttachASTStmt(link.Input as FlowNodeAnchor);

                    _linkStart.AttachNewLink(link);
                    to.AttachNewLink(link);
                    this.UpdateLinkDraw(to.GetAnchorPosition(this.MainGrid));
                    _linkStart = null;
                    _currentLink = null;
                }
            }
        }
        public void UpdateLinkDraw(Point curPosUnattachedLinkSide)
        {
            System.Diagnostics.Debug.Assert(_currentLink != null);
            if (_linkStart != null)
            {
                Point startPosition = _linkStart.GetAnchorPosition(this.MainGrid);
                if (_linkStart.Orientation == AIOAnchor.EOrientation.LEFT) // Input
                {
                    _currentLink._x2 = startPosition.X;
                    _currentLink._y2 = startPosition.Y;
                    _currentLink._x1 = curPosUnattachedLinkSide.X;
                    _currentLink._y1 = curPosUnattachedLinkSide.Y;
                }
                else
                {
                    _currentLink._x1 = startPosition.X;
                    _currentLink._y1 = startPosition.Y;
                    _currentLink._x2 = curPosUnattachedLinkSide.X;
                    _currentLink._y2 = curPosUnattachedLinkSide.Y;
                }
                _currentLink.InvalidateVisual();
            }
        }
        #endregion INodalView

        public void AddSelectNode(IDragNDropItem item)
        {
            throw new NotImplementedException();
        }

        public void AddSelectNodes(List<IDragNDropItem> items)
        {
            throw new NotImplementedException();
        }

        public void Drag(EDragMode dragMode)
        {
            throw new NotImplementedException();
        }

        public void UpdateDragInfos()
        {
            throw new NotImplementedException();
        }

        public new void Drop(List<IDragNDropItem> items)
        {
            throw new NotImplementedException();
        }
    } // Class
} // Namespace
