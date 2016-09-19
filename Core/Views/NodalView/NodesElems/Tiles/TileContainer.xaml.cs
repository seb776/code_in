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
    public partial class TileContainer : UserControl, ITileContainer
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
            }
        }
        private ResourceDictionary _themeResourceDictionary = null;
        public TileContainer() // TODO From Seb resource dictionary constructor
        {
            InitializeComponent();
        }

        public T CreateAndAddTile<T>() where T : ITile
        {
            T tile = (T)Activator.CreateInstance(typeof(T));//, _themeResourceDictionary); // TODO from seb make it work

            tile.SetParentView(null);
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
    }
}
