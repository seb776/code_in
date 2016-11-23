using code_in.Presenters.Nodal.Nodes;
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

namespace code_in.Views.NodalView.NodesElems.Tiles
{
    /// <summary>
    /// Logique d'interaction pour TileContainer.xaml
    /// </summary>
    public partial class TileContainer : UserControl, ITileContainer, ICodeInVisual
    {
        IContainerDragNDrop _parentView = null;
        private ResourceDictionary _themeResourceDictionary = null;
        public TileContainer(ResourceDictionary themeResDict, INodalView nodalView)
        {
            this.NodalView = nodalView;
            _themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(themeResDict);
            InitializeComponent();
        }
        public TileContainer() :
            this(Code_inApplication.MainResourceDictionary, null)
        { throw new Exceptions.DefaultCtorVisualException(); }

        #region This
        public bool IsExpanded
        {
            get
            {
                return this.IsEnabled;
            }
            set
            {
                this.IsEnabled = value;
                if (value)
                {
                    this.Visibility = System.Windows.Visibility.Visible;


                }
                else
                    this.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        #endregion This
        #region ITileContainer
        public void UpdateDisplayedInfosFromPresenter()
        {
            foreach (var v in TileStackPannel.Children)
            {
                (v as BaseTile).UpdateDisplayedInfosFromPresenter();
            }
        }
        public T CreateAndAddTile<T>(INodePresenter nodePresenter) where T : BaseTile
        {
            T tile = (T)Activator.CreateInstance(typeof(T), _themeResourceDictionary, this.NodalView);

            nodePresenter.SetView(tile);
            tile.SetParentView(this);
            tile.SetPresenter(nodePresenter);
            this.AddTile(tile);
            return tile;
        }


        public void AddTile<T>(T tile, int index = -1) where T : BaseTile
        {
            dynamic uiTile = tile;
            if (index < 0)
                this.TileStackPannel.Children.Add(uiTile);
            else
                this.TileStackPannel.Children.Insert(index, uiTile);
        }

        public void RemoveTile(BaseTile tile)
        {
            Debug.Assert(GetParentView() != null);
            if (GetParentView() != null)
                (this.GetParentView() as ITileContainer).RemoveTile(tile);
        }
        #endregion ITileContainer
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
        #region INodalViewElement
        public INodalView NodalView
        {
            get;
            set;
        }
        #endregion INodalViewElement
        #region IContainerDragNDrop
        StackPanel CurrentMovingNodes;
        public void Drag(EDragMode dragMode)
        {
            var selItems = Code_inApplication.RootDragNDrop.SelectedItems;

            if (CurrentMovingNodes == null)
            {
                CurrentMovingNodes = new StackPanel();
                this.TileGridDragNDrop.Children.Add(CurrentMovingNodes);
            }
            foreach (var item in selItems)
            {
                this.TileStackPannel.Children.Remove(item as UIElement); // Temporary
                //item.RemoveFromContext(); // TODO @Seb
                CurrentMovingNodes.Children.Add(item as UIElement); // TODO @Seb Beuark
            }
        }

        public void UpdateDragInfos(Point mousePosToMainGrid)
        {
            var relPos = (this.NodalView as NodalView).MainGrid.TranslatePoint(mousePosToMainGrid, this);
            this.CurrentMovingNodes.Margin = new Thickness(0.0, relPos.Y, 0.0, 0.0);
        }

        public new void Drop(IEnumerable<IDragNDropItem> items)
        {
            int finalIndex = 0; // Index for inserting nodes at the right place
            double movingNodesY = this.CurrentMovingNodes.Margin.Top;
            BaseTile beforeItem = null;
            foreach (var item in this.TileStackPannel.Children)
            {
                if (((item as FrameworkElement).TranslatePoint(new Point(0, 0), this.TileStackPannel).Y + ((item as FrameworkElement).ActualHeight / 2.0f)) > movingNodesY)
                    break;
                beforeItem = item as BaseTile; // TODO modification AST
                ++finalIndex;
            }
            if (Code_inApplication.RootDragNDrop.DragMode == EDragMode.STAYINCONTEXT)
            {
                ICSharpCode.NRefactory.CSharp.AstNode astNodeParent; // TODO modification AST
                if (CurrentMovingNodes != null) // TODO @Seb Replace by assert ?
                {
                    List<UIElement> saveItems = new List<UIElement>();
                    foreach (var uiElem in CurrentMovingNodes.Children)
                        saveItems.Add(uiElem as UIElement);
                    CurrentMovingNodes.Children.Clear();
                    astNodeParent = (saveItems.First() as BaseTile).Presenter.GetASTNode().Parent;
                    foreach (var uiElem in saveItems) // TODO modification AST
                        (uiElem as BaseTile).Presenter.RemoveFromAST();
                    int endIndex = finalIndex;
                    foreach (var uiElem in saveItems)
                    {
                        dynamic item = uiElem;
                        this.AddTile(item, endIndex);
                        if (beforeItem != null)
                            astNodeParent.InsertChildAfter(beforeItem.Presenter.GetASTNode(), (item as BaseTile).Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.Statement, ICSharpCode.NRefactory.CSharp.BlockStatement.StatementRole); // TODO modification AST
                        else
                            astNodeParent.InsertChildAfter(null, (item as BaseTile).Presenter.GetASTNode() as ICSharpCode.NRefactory.CSharp.Statement, ICSharpCode.NRefactory.CSharp.BlockStatement.StatementRole); // TODO modification AST
                        endIndex++;
                        beforeItem = item;
                    }
                    this.TileGridDragNDrop.Children.Remove(CurrentMovingNodes);
                    CurrentMovingNodes = null;
                }
            }
            else if (Code_inApplication.RootDragNDrop.DragMode == EDragMode.MOVEOUT)
            {

            }
        }

        public bool IsDropValid(IEnumerable<IDragNDropItem> items)
        {
            if (Code_inApplication.RootDragNDrop.DragMode == EDragMode.STAYINCONTEXT)
                return true;
            foreach (var i in items)
            {
                if (i is BaseTile)
                    return true;
            }
            return false;
        }

        #endregion IContainerDragNDrop
        #region IDragNDropItem
        public void SelectHighLight(bool highlighetd)
        {
            throw new NotImplementedException();
        }

        public void MustBeRemovedFromContext()
        {
            throw new NotImplementedException();
        }

        public void RemoveFromContext()
        {
            throw new NotImplementedException();
        }

        public void SetParentView(IContainerDragNDrop vc)
        {
            _parentView = vc;
        }

        public IContainerDragNDrop GetParentView()
        {
            return _parentView;
        }
        #endregion IDragNDropItem

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Code_inApplication.RootDragNDrop.DragMode != EDragMode.NONE)
                Code_inApplication.RootDragNDrop.Drop(this);
            e.Handled = true;
        }
    }
}
