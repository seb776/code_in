using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;
using ICSharpCode.NRefactory;
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

namespace code_in.Views.NodalView.NodesElems.Tiles
{
    /// <summary>
    /// Logique d'interaction pour BaseTile.xaml
    /// </summary>
    public partial class BaseTile : UserControl, INodeElem
    {
        protected INodePresenter _presenter = null;
        private ResourceDictionary _themeResourceDictionary = null;
        private bool _isBreakpointActive = false;
        private Statement _breakpoint = null;
        IContainerDragNDrop _parentView = null;

        public BaseTile(ResourceDictionary themeResDict, INodalView nodalView)
        {
            this.NodalView = nodalView;
            _themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(themeResDict);
            InitializeComponent();

        }

        public BaseTile() :
            this(Code_inApplication.MainResourceDictionary,null)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public void SetName(string name)
        {
            if (name == "")
                this.TileName.Visibility = System.Windows.Visibility.Collapsed;
            else
                this.TileName.Visibility = System.Windows.Visibility.Visible;
            this.TileName.Content = name;
        }

        public T CreateAndAddItem<T>(bool addAfterKeyword = false) where T : UIElement, ITileItem
        {
            T item = (T)Activator.CreateInstance(typeof(T), this._themeResourceDictionary, this.NodalView);
            item.NodalView = this.NodalView;
            if (addAfterKeyword)
                this.FieldAfterKeyWord.Children.Add(item);
            else
                this.AddItem(item);
            return item;
        }

        public virtual void AddItem<T>(T item) where T : UIElement, ITileItem
        {
            this.TileContent.Children.Add(item);
        }



        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary()
        {
            return _themeResourceDictionary;
            throw new NotImplementedException();
        }

        public void SetThemeResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInVisual


        #region ITile
        public void SetPresenter(INodePresenter presenter)
        {
            _presenter = presenter;
        }

        public virtual void UpdateDisplayedInfosFromPresenter()
        {
            // TODO @Seb uncomment this
            //throw new NotImplementedException("A method implementation has been forgotten.");
        }
        #endregion ITile

        private void TileEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_isBreakpointActive)
            {
                var thisASTNode = _presenter.GetASTNode();
                if (thisASTNode is Statement)
                {
                    var thisStmt = thisASTNode as Statement;
                    _breakpoint.Remove();
                    _breakpoint = null;
                    this.TileEllipse.Fill = new SolidColorBrush(Color.FromRgb(0x00, 0x88, 0xD6));
                }
            }
            else
            {
                BlockStatement blockstmt = new BlockStatement();
                ICSharpCode.NRefactory.CSharp.CSharpParser parser = new ICSharpCode.NRefactory.CSharp.CSharpParser();

                var breakpointStmts = parser.ParseStatements("if(System.Diagnostics.Debugger.IsAttached)  System.Diagnostics.Debugger.Break();");
                _breakpoint = breakpointStmts.ElementAt(0);
                var thisASTNode = _presenter.GetASTNode();
                if (thisASTNode is Statement)
                {
                    var thisStmt = thisASTNode as Statement;
                    thisStmt.Parent.InsertChildBefore(thisASTNode, _breakpoint, BlockStatement.StatementRole); // From Seb Not sure
                }
                this.TileEllipse.Fill = new SolidColorBrush(Colors.Red);
            }
            e.Handled = true;
            _isBreakpointActive = !_isBreakpointActive;
        }

        public void InstantiateASTNode()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public void AddGeneric(string name, Nodes.Assets.EGenericVariance variance)
        {
            throw new NotImplementedException();
        }
        public void SetParentView(IContainerDragNDrop vc)
        {
            Debug.Assert(vc != null);
            _parentView = vc;
        }
        public IContainerDragNDrop GetParentView()
        {
            return _parentView;
        }

        public void SetNodePresenter(INodePresenter nodePresenter)
        {
            throw new NotImplementedException();
        }

        EditNodePanel _editNodePanel;
        public void ShowEditMenu()
        {
            this.EditMenuLayout.Children.Clear();
            var editMenu = new EditNodePanel(_themeResourceDictionary);
            editMenu.SetFields(_presenter);
            this.EditMenuLayout.Children.Add(editMenu);
        }

        public void SetPosition(int posX, int posY)
        {
            this.Margin = new Thickness(posX, posY, 0.0, 0.0);
        }

        public Point GetPosition()
        {
            throw new NotImplementedException();
        }

        public void GetSize(out int x, out int y)
        {
            throw new NotImplementedException();
        }


        public void Remove()
        {
            (this.GetParentView() as IVisualNodeContainer).RemoveNode(this);
        }

        public void SelectHighLight(bool highlighetd)
        {
            if (highlighetd)
                this.BackGrid.Background = new SolidColorBrush(Color.FromArgb(0xA5, 0xE2, 0x4E, 0x42));
            else
                this.BackGrid.Background = new SolidColorBrush(Color.FromArgb(0xA5, 0xFF, 0x98, 0x3D));
        }


        public void MustBeRemovedFromContext()
        {
            throw new NotImplementedException();
        }

        public void RemoveFromContext()
        {
            throw new NotImplementedException();
        }

        public INodalView NodalView
        {
            get;
            set;
        }

        private void BackGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Keyboard.IsKeyDown(Key.LeftShift))
                Code_inApplication.RootDragNDrop.UnselectAllNodes();
            Code_inApplication.RootDragNDrop.AddSelectItem(this);
            e.Handled = true;
        }

        private void BackGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            code_in.Views.NodalView.NodalView.CreateContextMenuFromOptions(this._presenter.GetMenuOptions(), this.GetThemeResourceDictionary(), this._presenter);
            e.Handled = true;
        }
    }
}
