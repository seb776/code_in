using code_in.Presenters.Nodal.Nodes;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
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
    public partial class BaseTile : UserControl, ITile, ICodeInVisual
    {
        protected INodePresenter _presenter = null;
        private ResourceDictionary _themeResourceDictionary = null;
        bool IsBreakOrNot = false;
        public BaseTile(ResourceDictionary themeResDict)
        {
            _themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(themeResDict);
            InitializeComponent();
        }

        public BaseTile() :
            this(Code_inApplication.MainResourceDictionary)
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
            T item = (T)Activator.CreateInstance(typeof(T), this._themeResourceDictionary);
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
        public void SetParentView(IVisualNodeContainerDragNDrop vc)
        {
            //throw new NotImplementedException();
        }
        public virtual void UpdateDisplayedInfosFromPresenter()
        {
            // TODO @Seb uncomment this
            //throw new NotImplementedException("A method implementation has been forgotten.");
        }
        #endregion ITile

        private void TileEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsBreakOrNot)
            {
                BlockStatement blockstmt = new BlockStatement();
                ICSharpCode.NRefactory.CSharp.CSharpParser parse = new ICSharpCode.NRefactory.CSharp.CSharpParser();

                var stmts = parse.ParseStatements("if(System.Diagnostics.Debugger.IsAttached)  System.Diagnostics.Debugger.Break();");
                blockstmt.Add(stmts.ElementAt(0)); // TODO check Z0rg
                blockstmt.Add((Statement)_presenter.GetASTNode()); // TODO check Z0rg
                _presenter.GetASTNode().ReplaceWith(blockstmt);
            }
            else
            {
                //TODO z0rg
            }
        }
    }
}
