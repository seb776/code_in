﻿using code_in.Managers;
using code_in.Models;
using code_in.Models.NodalModel;
using code_in.Presenters.Nodal;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Assets;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Expressions;
using code_in.Views.NodalView.NodesElems.Nodes.Statements;
using code_in.Views.Utils;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    public partial class NodalView : UserControl, INodalView, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private ResourceDictionary _languageResourceDictionary = null;
        private INodeElem _draggingNode = null;
        private TransformationMode _nodeTransform = TransformationMode.NONE;
        private LineMode _lineMode = LineMode.LINE;
        private INodalPresenter _nodalPresenter = null;
        private Point _newNodePos = new Point(0, 0);
        private Line _currentLineDrawing;
        private Tuple<Line, Line> _currentSquareLineDrawing;
        private Point _lastPosition = new Point(0, 0);

        private Code_inLink _link;

        public NodalView(ResourceDictionary themeResDict)
        {
            this._nodalPresenter = new NodalPresenterLocal(this);
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(this._languageResourceDictionary);
            InitializeComponent();


        }
        public NodalView() :
            this(Code_inApplication.MainResourceDictionary)
        { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }

        #region IVisualNodeContainerDragNDrop
        public void SelectNode(INodeElem node) { }
        public void UnSelectNode(INodeElem node) { }
        public void UnSelectAll() { }

        public void DragNodes(TransformationMode transform, INodeElem node, LineMode lm)
        {
            _nodeTransform = transform;
            _draggingNode = node;
            if (_draggingNode != null && _draggingNode.GetParentView() != null)
            {
                if (_nodeTransform == TransformationMode.MOVE)
                {
                    if (_draggingNode.GetParentView().GetType().IsSubclassOf(typeof(AOrderedContentNode)))
                    {
                        Point relativeCoord = ((UIElement)_draggingNode).TransformToAncestor((_draggingNode.GetParentView() as BaseNode).ContentLayout).Transform(new Point(0, 0));
                        _draggingNode.GetParentView().RemoveNode(_draggingNode);
                        ((AOrderedContentNode)_draggingNode.GetParentView()).ContentLayout.Children.Add(_draggingNode as UIElement);
                        (_draggingNode as UserControl).Margin = new Thickness(0, relativeCoord.Y, 0, 0);
                    }
                }

                else if (_nodeTransform == TransformationMode.LINE)
                {
                    Point nodeAnchorRelativeCoord;
                    if (((_draggingNode.GetParentView() as BaseNode).GetParentView() as BaseNode) != null)
                        nodeAnchorRelativeCoord = (_draggingNode as AOItem)._nodeAnchor.TransformToAncestor((_draggingNode.GetParentView() as BaseNode)).Transform(new Point(0, 0));
                    else
                        nodeAnchorRelativeCoord = (_draggingNode as AOItem)._nodeAnchor.TransformToAncestor(this.MainGrid).Transform(new Point(0, 0));

                    this._link = new Code_inLink();

                    Canvas.SetZIndex(_link, -9999999); // TODO Beuark
                    _link._x1 = nodeAnchorRelativeCoord.X;
                    _link._y1 = nodeAnchorRelativeCoord.Y + (_draggingNode as AOItem)._nodeAnchor.ActualHeight / 2;
                    _link._x2 = _link._x1;
                    _link._y2 = _link._y1;

                    if (_lineMode == LineMode.LINE)
                        _link.changeLineMode(Code_inLink.ELineMode.LINE);
                    else if (_lineMode == LineMode.BEZIER)
                        _link.changeLineMode(Code_inLink.ELineMode.BEZIER);

                    this._link.MouseRightButtonDown += _currentLineDrawing_MouseRightButtonDown;

                    this.MainGrid.Children.Add(_link);
                        

                    if (_lineMode == LineMode.LINE)
                    {
                     /*   _currentLineDrawing = new Line();

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
                            // this.RemoveLink(_draggingNode as IOItem);
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
                    else if (_lineMode == LineMode.SQUARE)
                    {
                        Line sl1 = new Line();
                        Line sl2 = new Line();
                        _currentSquareLineDrawing = new Tuple<Line, Line>(sl1, sl2);

                        sl1.Stroke = sl2.Stroke = new SolidColorBrush(Colors.GreenYellow);
                        sl2.Stroke = new SolidColorBrush(Colors.AliceBlue);
                        sl1.StrokeThickness = sl2.StrokeThickness = 3;
                        Canvas.SetZIndex(sl1, -9999999);
                        Canvas.SetZIndex(sl2, -9999999);
                       
                        Point nodeAnchorRelativeCoord;
                        if (((_draggingNode.GetParentView() as BaseNode).GetParentView() as BaseNode) != null)
                        {
                            nodeAnchorRelativeCoord = (_draggingNode as IOItem)._nodeAnchor.TransformToAncestor((_draggingNode.GetParentView() as BaseNode)).Transform(new Point(0, 0));
                        }
                        else
                        {
                            nodeAnchorRelativeCoord = (_draggingNode as IOItem)._nodeAnchor.TransformToAncestor(this.MainGrid).Transform(new Point(0, 0));
                        }
                        sl1.X1 = sl2.X1 = sl2.X2 = nodeAnchorRelativeCoord.X;
                        sl1.Y1 = sl2.Y1 = sl2.Y2 = nodeAnchorRelativeCoord.Y + (_draggingNode as IOItem)._nodeAnchor.ActualHeight / 2;
                        
                        if (((_draggingNode.GetParentView() as BaseNode).GetParentView() as BaseNode) != null)
                        {
                            ((_draggingNode.GetParentView() as BaseNode).GetParentView() as BaseNode).ContentGrid.Children.Add(sl1);
                            ((_draggingNode.GetParentView() as BaseNode).GetParentView() as BaseNode).ContentGrid.Children.Add(sl2);
                        }
                        else
                        {
                            this.MainGrid.Children.Add(sl1);
                            this.MainGrid.Children.Add(sl2);
                        }*/
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
                        ((AOrderedContentNode)_draggingNode.GetParentView()).ContentLayout.Children.Remove(_draggingNode as UIElement);
                        MethodInfo mi = ((AOrderedContentNode)_draggingNode.GetParentView()).GetType().GetMethod("AddNode");
                        MethodInfo gmi = mi.MakeGenericMethod(_draggingNode.GetType());
                        Object[] prm = { _draggingNode, ((AOrderedContentNode)_draggingNode.GetParentView()).GetDropIndex(new Point(0, (_draggingNode as UserControl).Margin.Top)) };
                        gmi.Invoke(_draggingNode.GetParentView(), prm);
                        ((UserControl)_draggingNode).Margin = new Thickness();
                    }
                }

                else if (_nodeTransform == TransformationMode.LINE)
                {
                    if (node == null ||
                            ((_draggingNode as AOItem).Orientation == AOItem.EOrientation.LEFT) && (node as AOItem).Orientation == AOItem.EOrientation.LEFT || // line from input to input
                            ((_draggingNode as AOItem).Orientation == AOItem.EOrientation.RIGHT) && (node as AOItem).Orientation == AOItem.EOrientation.RIGHT || // line from output to output
                            _draggingNode.GetParentView() == node.GetParentView())
                    {
                       
                        this.MainGrid.Children.Remove(_link);
                    }
                    else
                    {
                        if ((_draggingNode as AOItem)._nodeAnchor._parentItem.Orientation == AOItem.EOrientation.LEFT)
                        {
                            Point tmpPoint = new Point();
                            tmpPoint.X = _link._x1;
                            tmpPoint.Y = _link._y1;
                            _link._x1 = _link._x2;
                            _link._y1 = _link._y2;
                            _link._x2 = tmpPoint.X;
                            _link._y2 = tmpPoint.Y;
                        }

                        // storing line in nodeanchor
                        (_draggingNode as AOItem)._nodeAnchor.IOLine.Add(_link);
                        (node as AOItem)._nodeAnchor.IOLine.Add(_link);
                        (_draggingNode as AOItem).IOAttached = node as AOItem;
                        (node as AOItem).IOAttached = (_draggingNode as AOItem);
                    }


                   /* if (_lineMode == LineMode.LINE)
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

                    else if (_lineMode == LineMode.SQUARE)
                    {
                        Canvas.SetZIndex(_currentSquareLineDrawing.Item1, 9999999);
                        Canvas.SetZIndex(_currentSquareLineDrawing.Item2, 9999999);

                        if (node == null ||
                            ((_draggingNode as IOItem).Orientation == IOItem.EOrientation.LEFT) && (node as IOItem).Orientation == IOItem.EOrientation.LEFT || // line from input to input
                            ((_draggingNode as IOItem).Orientation == IOItem.EOrientation.RIGHT) && (node as IOItem).Orientation == IOItem.EOrientation.RIGHT || // line from output to output
                            _draggingNode.GetParentView() == node.GetParentView())
                        {
                            // remove line
                            if (((_draggingNode.GetParentView() as BaseNode).GetParentView() as BaseNode) != null)
                            {
                                ((_draggingNode.GetParentView() as BaseNode).GetParentView() as BaseNode).ContentGrid.Children.Remove(_currentSquareLineDrawing.Item1);
                                ((_draggingNode.GetParentView() as BaseNode).GetParentView() as BaseNode).ContentGrid.Children.Remove(_currentSquareLineDrawing.Item2);
                            }
                            else
                            {
                                this.MainGrid.Children.Remove(_currentSquareLineDrawing.Item1);
                                this.MainGrid.Children.Remove(_currentSquareLineDrawing.Item2);
                            }
                        }
                        else
                        {
                            if (node.GetType().IsSubclassOf(typeof(IOItem)))
                            {
                               // if ((node as IOItem).IOAttached != null)
                                //{
                                 //   this.RemoveLink(node as IOItem);
                                //}
                                // if create link from output to input, swap begin and end point of the line
                                if ((_draggingNode as IOItem)._nodeAnchor._parentItem.Orientation == IOItem.EOrientation.LEFT)
                                {
                                    Point tmpPoint = new Point();
                                    tmpPoint.X = _currentSquareLineDrawing.Item1.X1; // sl1.X1
                                    tmpPoint.Y = _currentSquareLineDrawing.Item1.Y1; // sl1.Y1
                                    _currentSquareLineDrawing.Item1.X1 = _currentSquareLineDrawing.Item2.X2;
                                    _currentSquareLineDrawing.Item1.Y1 = _currentSquareLineDrawing.Item2.Y2;
                                    _currentSquareLineDrawing.Item2.X2 = tmpPoint.X;
                                    _currentSquareLineDrawing.Item2.Y2 = tmpPoint.Y;
                                }

                                // storing line in nodeanchor
                                (_draggingNode as IOItem)._nodeAnchor.IOSquare = _currentSquareLineDrawing;
                                (node as IOItem)._nodeAnchor.IOSquare = _currentSquareLineDrawing;
                                (_draggingNode as IOItem).IOAttached = node as IOItem;
                                (node as IOItem).IOAttached = (_draggingNode as IOItem);
                            }
                        }

                    }*/
                    
                }
            }

            // Reset transformation
            _draggingNode = null;
            _nodeTransform = TransformationMode.NONE;
        }
        public void RemoveLink(AOItem node)
        {
           /* if (((node.GetParentView() as BaseNode).GetParentView() as BaseNode) != null)
            {
              //  ((node.GetParentView() as BaseNode).GetParentView() as BaseNode).ContentGrid.Children.Remove(node._nodeAnchor.IOLine);
            }
            else
            {*/
            for (int i = 0; i < node._nodeAnchor.IOLine.Count(); ++i)
            {
                this.MainGrid.Children.Remove(node._nodeAnchor.IOLine[i]);
            }
           // }
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

        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public ResourceDictionary GetLanguageResourceDictionary() { return _languageResourceDictionary; }
        public void SetThemeResources(String keyPrefix)
        {
            throw new NotImplementedException();
        }
        public void SetLanguageResources(String keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInVisual

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
                if (_lineMode == LineMode.LINE) {
                    _lineMode = LineMode.BEZIER;
                }
                else
                    _lineMode = LineMode.LINE;
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
                else if (_nodeTransform == TransformationMode.LINE)
                {
                    _link._x2 = e.GetPosition(this.MainGrid).X;
                    _link._y2 = e.GetPosition(this.MainGrid).Y;
                }

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

        private void MainGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var im = new HexagonalMenu(this.GetThemeResourceDictionary());
            
            List<Type> listOfBs = new List<Type>();
            foreach (var t in typeof(BaseNode).Assembly.GetTypes())
            {

                if (t.IsSubclassOf(typeof(BaseNode)) && !t.IsAbstract)
                {
                    listOfBs.Add(t);
                }
            }
            int xCount = (int)Math.Sqrt(listOfBs.Count);
            int iX = 0;
            int iY = 0;
            Color[] testColors = {
                                     Colors.DodgerBlue,
                                     Color.FromArgb(0xFF, 0x42, 0x42, 0x42),
                                     Colors.GreenYellow,
                                     Colors.OrangeRed,
                                 };
            //foreach (var t in listOfBs)
            //{
            //    im.AddHexagonButton(iX, iY, testColors[iX % 4], null);
            //    ++iX;
            //    if (iX == xCount)
            //    {
            //        iX = 0;
            //        iY++;
            //    }
            //}
            var options = _nodalPresenter.GetMenuOptions();
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
                        imageSrc.UriSource = new Uri("pack://application:,,,/code_inCore;component/Resources/Graphics/edit.png");
                        break;
                }
                imageSrc.EndInit();

                im.AddHexagonButtonCircle(buttonName, imageSrc, opt.Item2);
            }

            im.Placement = PlacementMode.Absolute;
            var tC = System.Windows.Forms.Control.MousePosition;
            im.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            im.HorizontalOffset = tC.X -(im.DesiredSize.Width / 2);
            im.VerticalOffset = tC.Y -(im.DesiredSize.Height / 2);
            im.IsOpen = true;

            return;
            //var hexMenu = new HexagonalMenu();

            //hexMenu.AddHexagonButton(0, 0);
            //hexMenu.AddHexagonButton(-1, -1);
            //hexMenu.AddHexagonButton(-1, 0);
            //hexMenu.AddHexagonButton(-1, 1);
            //hexMenu.AddHexagonButton(1, 1);
            //hexMenu.Margin = new Thickness(e.GetPosition((this.Parent as FrameworkElement).Parent as FrameworkElement).X, e.GetPosition((this.Parent as FrameworkElement).Parent as FrameworkElement).Y, 0, 0);
            //this.MainGrid.Children.Add(hexMenu);
            //hexMenu.ShowMenu();

            // This automatically updates the list of accessible nodes
            // Need to be optimized (compute only the first time, as it uses reflection)
            //List<Type> listOfBs = new List<Type>();
            //foreach (var t in typeof(BaseNode).Assembly.GetTypes())
            //{

            //    if (t.IsSubclassOf(typeof(BaseNode)) && !t.IsAbstract)
            //    {
            //        listOfBs.Add(t);
            //    }
            //}
            //var cm = new ContextMenu();
            //foreach (var t in listOfBs)
            //{
            //    var m1 = new MenuItem();
            //    m1.Header = t.Name;
            //    m1.DataContext = t;
            //    m1.Click += m1_Click;
            //    cm.Items.Add(m1);
            //}

            //var m2 = new MenuItem();
            //m2.Header = "change line mode";
            //m2.Click += clickChangeMode;
            //cm.Items.Add(m2);

            //cm.Margin = new Thickness(e.GetPosition((this.Parent as FrameworkElement).Parent as FrameworkElement).X, e.GetPosition((this.Parent as FrameworkElement).Parent as FrameworkElement).Y, 0, 0);
            //cm.IsOpen = true;
            //// Setting the position of the node if we create one to the place the menu has been opened
            //_newNodePos.X = e.GetPosition(this).X;
            //_newNodePos.Y = e.GetPosition(this).Y;
        }

        void clickChangeMode(object sender, RoutedEventArgs e)
        {
            var lineType = Code_inApplication.MainResourceDictionary["linkType"];
            if (lineType == null)
                Code_inApplication.MainResourceDictionary["linkType"] = 0;
            else
                Code_inApplication.MainResourceDictionary["linkType"] = ((int)(lineType) == 0 ? 1 : 0);
           
            if (_lineMode == LineMode.LINE)
                _lineMode = LineMode.BEZIER;
            else
                _lineMode = LineMode.LINE;
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

        private void changeResourceLink(object sender, ResourcesEventArgs e)
        {
            foreach (var t in this.MainGrid.Children) {
                if (t.GetType() == typeof(Code_inLink))
                {
                    _link = (Code_inLink) t;
                    _link.changeLineMode();
                }
            }
        }




    }
}
