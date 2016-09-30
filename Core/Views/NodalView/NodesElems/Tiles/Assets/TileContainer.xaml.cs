using code_in.Presenters.Nodal.Nodes;
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
    /// Logique d'interaction pour TileContainer.xaml
    /// </summary>
    public partial class TileContainer : UserControl, ITileContainer, ICodeInVisual
    {
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
                    this.Visibility = System.Windows.Visibility.Visible;
                else
                    this.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private ResourceDictionary _themeResourceDictionary = null;
        public TileContainer(ResourceDictionary themeResDict)
        {
            _themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(themeResDict);
            InitializeComponent();
        }
        public TileContainer() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exceptions.DefaultCtorVisualException();
        }

        public T CreateAndAddTile<T>(INodePresenter nodePresenter) where T : ITile
        {
            T tile = (T)Activator.CreateInstance(typeof(T), _themeResourceDictionary);

            tile.SetParentView(null);
            tile.SetPresenter(nodePresenter);
            /* tile.SetRootView(this);
             tile.SetNodePresenter(nodePresenter);
             nodePresenter.SetView(node);
             if (typeof(AIONode).IsAssignableFrom(typeof(T)))
             {
                 (node as AIONode).SetParentLinksContainer(this);
             }
             _visualNodes.Add(node);*/
            this.AddTile(tile);
            return tile;
        }


        public void AddTile<T>(T tile, int index = -1) where T : ITile
        {
            dynamic uiTile = tile;
            if (index < 0)
                this.TileStackPannel.Children.Add(uiTile);
            else
                this.TileStackPannel.Children.Insert(index + 1, uiTile);
        }

        public void RemoveTile(ITile tile)
        {
            throw new NotImplementedException();
        }

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



        public void UpdateDisplayedInfosFromPresenter()
        {
            foreach (var v in TileStackPannel.Children)
            {
                (v as ITile).UpdateDisplayedInfosFromPresenter();
            }
        }
    }
}
