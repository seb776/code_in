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
    /// Logique d'interaction pour FlowTileItem.xaml
    /// </summary>
    public partial class FlowTileItem : UserControl, ITileContainer, ITileItem, ICodeInVisual
    {
        private TileContainer _tileContainer = null;
        private ResourceDictionary _themeResourceDictionary = null;
        /// <summary>
        /// Gets or sets the expaded state of the tile.
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return _tileContainer.IsExpanded;
            }
            set
            {
                _tileContainer.IsExpanded = value;
                //if (value == false)
                //    this.ItemGrid.Children.Remove(_tileContainer as UIElement);
                //else
                //    this.ItemGrid.Children.Add(_tileContainer);
            }
        }

        public void SetName(string name)
        {
            this.ItemName.Content = name;
        }

        public FlowTileItem() :
            this(Code_inApplication.MainResourceDictionary)
        { throw new Exceptions.DefaultCtorVisualException();  }

        public FlowTileItem(ResourceDictionary themeResDict)
        {
            _themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(themeResDict);
            InitializeComponent();
            _tileContainer = new TileContainer(_themeResourceDictionary);
            _tileContainer.SetValue(Grid.ColumnProperty, 2);
            ItemGrid.Children.Add(_tileContainer);
            this.IsExpanded = false;
        }

        public T CreateAndAddTile<T>(INodePresenter presenter) where T : ITile
        {
            //T tile = (T)Activator.CreateInstance(typeof(T), this._themeResourceDictionary);  // TODO From Seb: This may crash if the constructor is not implemented
            ////node.SetParentView(null);
            ////node.SetRootView(this); // TODO
            ////node.SetNodePresenter(nodePresenter);
            ////nodePresenter.SetView(node);
            //this.AddTile(tile);
            return _tileContainer.CreateAndAddTile<T>(presenter);
            //return tile;
        }

        public void AddTile<T>(T tile, int index = -1) where T : ITile
        {
            throw new Exception("This is useless as all is managed by CreateAndAddTile.");
            //_tileContainer.AddTile(tile, index);
        }

        public ResourceDictionary GetThemeResourceDictionary()
        {
            return _themeResourceDictionary;
        }

        public void SetThemeResources(string keyPrefix)
        {
            if (keyPrefix == "false") // From Seb temporary
                this.ItemGrid.Background = new SolidColorBrush(Color.FromArgb(0x42, 0xFF, 0x00, 0x00));
        }


        public void RemoveTile(ITile tile)
        {
            throw new NotImplementedException();
        }

        private void buttonExpand_Click(object sender, RoutedEventArgs e)
        {
            IsExpanded = !IsExpanded;
        } 
    }
}
