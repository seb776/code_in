using code_in.Exceptions;
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
            RootTileContainer = new TileContainer(_themeResourceDictionary, this) as ITileContainer;
            RootTileContainer.SetParentView(this);
            this.MainGrid.Children.Add(RootTileContainer as TileContainer);
        }


        public NodalView() :
            this(Code_inApplication.MainResourceDictionary)
        { throw new DefaultCtorVisualException(); }

        #region This
        public void OpenFile(String path)
        {
            // TODO Show Animation loadingFile
            this._nodalPresenter.OpenFile(path);
            AlignDeclarations();
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
        public void AlignDeclarations()
        {
            if (IsDeclarative)
            {
                foreach (var nodeUi in this.MainGrid.Children)
                {
                    var nodeElem = nodeUi as INodeElem;
                }
            }
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
        #region Events
        #region Events.Keys
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
        #endregion Events.Keys
        #region Events.Mouse
        private void MainGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Code_inApplication.RootDragNDrop.UnselectAllNodes();
        }
        void MainView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Code_inApplication.RootDragNDrop.DragMode != EDragMode.NONE)
                Code_inApplication.RootDragNDrop.Drop(this);
            e.Handled = true;
        }

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            EDragMode dragMode = (Keyboard.IsKeyDown(Key.LeftCtrl) ? EDragMode.MOVEOUT : EDragMode.STAYINCONTEXT);
            if (e.LeftButton == MouseButtonState.Pressed)
                Code_inApplication.RootDragNDrop.UpdateDragInfos(dragMode, e.GetPosition(this.MainGrid));
        }

        private void MainGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var options = _nodalPresenter.GetMenuOptions();
            CreateContextMenuFromOptions(options, this.GetThemeResourceDictionary(), this._nodalPresenter);
            e.Handled = true;
        }

        private void MainGrid_MouseLeave(object sender, MouseEventArgs e)
        {
        }
        #endregion Events.Mouse
        #region Events.Others
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
        #endregion Events.Others
        #endregion Events
        #endregion This
        #region INodalView
        #endregion INodalView
        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public void SetThemeResources(String keyPrefix) { throw new NotImplementedException(); }
        #endregion ICodeInVisual
        #region IContainerDragNDrop
        public new void Drop(IEnumerable<IDragNDropItem> items)
        {
            this.UpdateDragInfos(_lastPosition);
        }

        public bool IsDropValid(IEnumerable<IDragNDropItem> items)
        {
            if (Code_inApplication.RootDragNDrop.DragMode == EDragMode.STAYINCONTEXT)
                return true;

            foreach (var i in items)
            {
                if (this.IsDeclarative)
                    return ((i is code_in.Views.NodalView.NodesElems.Nodes.ClassDeclNode) || (i is code_in.Views.NodalView.NodesElems.Nodes.NamespaceNode) || (i is code_in.Views.NodalView.NodesElems.Nodes.UsingDeclNode));
            }
            return false;

        }
        public void UpdateDragInfos(Point mousePosition) // @Seb mousePosition must be mouse position from NodalView.MainGrid
        {
            var selectedNodes = Code_inApplication.RootDragNDrop.SelectedItems;
            if (selectedNodes.Count == 0)
                return;
            Vector diff;
            if ((_lastPosition.X + _lastPosition.Y) < 0.01)
                diff = new Vector(0, 0);
            else
                diff = _lastPosition - mousePosition;
            _lastPosition = mousePosition;

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

        #endregion IContainerDragNDrop
        #region IVisualNodeContainer
        public T CreateAndAddNode<T>(INodePresenter nodePresenter) where T : UIElement, code_in.Views.NodalView.INode
        {
            System.Diagnostics.Debug.Assert(nodePresenter != null, "nodePresenter must be a non-null value");
            T node = (T)Activator.CreateInstance(typeof(T), this._themeResourceDictionary, this, nodePresenter);

            node.SetParentView(this);
            node.SetNodePresenter(nodePresenter);
            nodePresenter.SetView(node);
            this.AddNode(node);
            return node;
        }
        public void AddNode<T>(T node, int index = -1) where T : UIElement, code_in.Views.NodalView.INode
        {
            this.MainGrid.Children.Add(node as UIElement);
        }

        public void RemoveNode(INodeElem node)
        {
            this.MainGrid.Children.Remove(node as UIElement);
        }

        public void RemoveTile(INodeElem tile)
        {
            this.MainGrid.Children.Remove(tile as UIElement);
        }

        #endregion IVisualNodeContainer

    } // Class
} // Namespace
