using code_in.Managers;
using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using code_in.Views.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace code_in.Views.NodalView
{

    struct NodalViewInteractionState
    {
        enum EInteractionType
        {
            NONE = 0,
            RESIZE = 1,
            LINE = 2,
            MOVE = 3
        }
        List<INodeElem> SelectedNodes;
        EInteractionType InteractionType; // Create link, move node... (ancient TransformationMode)
        // For MOVE
        private Point _newNodePos;
        private Point _lastPosition;
        // For link creation
        Code_inLink _currentDrawingLink;
    }
    /// <summary>
    /// The Nodal view is the layout that is able to display Nodes From the NodalPresenter;
    /// </summary>
    public partial class NodalView : UserControl, INodalView, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private INodalPresenter _nodalPresenter = null;

        public NodalView(ResourceDictionary themeResDict)
        {
            this._nodalPresenter = new NodalPresenterLocal(this);
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
        }
        public NodalView() :
            this(Code_inApplication.MainResourceDictionary)
        { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }

        #region This
        public void OpenFile(String path)
        {
            this._nodalPresenter.OpenFile(path);
        }

        public void EditFunction(FuncDeclItem node)
        {
            this._nodalPresenter.EditFunction(node);
        }

        #region Events
        void MainView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.DropNodes(null);
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
            //Vector diff;
            //if ((_lastPosition.X + _lastPosition.Y) < 0.01)
            //    diff = new Vector(0, 0);
            //else
            //{
            //    diff = _lastPosition - e.GetPosition(this.MainGrid);
            //}
            //_lastPosition = e.GetPosition(this.MainGrid);

            //if (_nodeTransform != TransformationMode.NONE /*&& _transformingNodes.Count() > 0*/)
            //{
            //    if (_nodeTransform == TransformationMode.MOVE)
            //    {
            //        Thickness margin = (Thickness)_draggingNode.GetType().GetProperty("Margin").GetValue(_draggingNode);
            //        double marginLeft = margin.Left;
            //        double marginTop = margin.Top;
            //        Thickness newMargin = margin;
            //        if (_draggingNode.GetParentView() == null)
            //        {
            //            newMargin.Left -= diff.X;
            //            newMargin.Top -= diff.Y;
            //        }
            //        else
            //        {
            //            if (!_draggingNode.GetParentView().GetType().IsSubclassOf(typeof(AOrderedContentNode)))
            //                newMargin.Left -= diff.X;
            //            newMargin.Top -= diff.Y;
            //        }

            //        newMargin.Left = Math.Max(newMargin.Left, 0);
            //        newMargin.Top = Math.Max(newMargin.Top, 0);

            //        (_draggingNode as BaseNode).MoveNode(new Point(newMargin.Left, newMargin.Top));


            //    }
            //    else if (_nodeTransform == TransformationMode.LINE)
            //    {
            //        _link._x2 = e.GetPosition(this.MainGrid).X;
            //        _link._y2 = e.GetPosition(this.MainGrid).Y;
            //    }

            //}
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
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/edit.png");
                        buttonName = "Add";
                        break;
                    case EContextMenuOptions.ALIGN:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/edit.png");
                        break;
                    case EContextMenuOptions.CLOSE:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/edit.png");
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
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/edit.png");
                        break;
                    case EContextMenuOptions.HELP:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/edit.png");
                        break;
                    case EContextMenuOptions.REMOVE:
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/remove.png");
                        buttonName = "Remove";
                        break;
                    case EContextMenuOptions.SAVE:
                        // remove icon to distinguish save from the others
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/remove.png");
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

        // To add new nodes
        void m1_Click(object sender, RoutedEventArgs e)
        {
            //MethodInfo mi = this.GetType().GetMethod("CreateAndAddNode");
            //MethodInfo gmi = mi.MakeGenericMethod(((sender as MenuItem).DataContext as Type));
            //BaseNode node = gmi.Invoke(this, null) as BaseNode;

            //node.Margin = new Thickness(_newNodePos.X, _newNodePos.Y, 0, 0);
            //var node = this._rootNode.CreateAndAddNode<((sender as MenuItem).DataContext as Type)>();
        }

        private void MainGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            this.DropNodes(null);
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

        #region IVisualNodeContainerDragNDrop
        public void SelectNode(INodeElem node) { }
        public void UnSelectNode(INodeElem node) { }
        public void UnSelectAll() { }

        public void DragNodes(TransformationMode transform, INodeElem node, LineMode lm)
        {
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
        }

        public void DropNodes(INodeElem node)
        {
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
        }
        public void RemoveLink(AIOAnchor node)
        {
            ///* if (((node.GetParentView() as BaseNode).GetParentView() as BaseNode) != null)
            // {
            //   //  ((node.GetParentView() as BaseNode).GetParentView() as BaseNode).ContentGrid.Children.Remove(node._nodeAnchor.IOLine);
            // }
            // else
            // {*/
            //for (int i = 0; i < node._nodeAnchor.IOLine.Count(); ++i)
            //{
            //    this.MainGrid.Children.Remove(node._nodeAnchor.IOLine[i]);
            //}
            //// }
            //node.RemoveLink();
        }
        #endregion create
        #region IVisualNodeContainer
        public T CreateAndAddNode<T>(INodePresenter nodePresenter) where T : UIElement, INodeElem
        {
            System.Diagnostics.Debug.Assert(nodePresenter != null, "nodePresenter must be a non-null value");
            T node = (T)Activator.CreateInstance(typeof(T), this._themeResourceDictionary);

            node.SetParentView(null);
            node.SetRootView(this);
            node.SetNodePresenter(nodePresenter);
            nodePresenter.SetView(node);

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

        public void drawLink(AIOAnchor outputAnchor, AIOAnchor inputAnchor)
        {
            //Code_inLink line = new Code_inLink();
            //outputAnchor._nodeAnchor.IOLine.Add(line);
            //this.MainGrid.Children.Add(line); // add the line on maingrid
            //inputAnchor._nodeAnchor.IOLine.Add(line);
        }

        public void HighLightDropPlace(Point pos) { }
        public int GetDropIndex(Point pos) { return 0; }
        #endregion IVisualNodeContainer
        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public void SetThemeResources(String keyPrefix) { throw new NotImplementedException(); }
        #endregion ICodeInVisual

    } // Class
} // Namespace
