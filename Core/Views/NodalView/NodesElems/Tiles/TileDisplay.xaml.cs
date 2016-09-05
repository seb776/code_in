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
    /// Logique d'interaction pour TileDisplay.xaml
    /// </summary>
    public partial class TileDisplay : UserControl, ITileContainer
    {
        public TileDisplay()
        {
            InitializeComponent();
        }

        public T CreateAndAddTile<T>() where T : ITile
        {
            T tile = (T)Activator.CreateInstance(typeof(T)/*, this._themeResourceDictionary*/);  // TODO From Seb: This may crash if the constructor is not implemented
            //node.SetParentView(null);
            //node.SetRootView(this); // TODO
            //node.SetNodePresenter(nodePresenter);
            //nodePresenter.SetView(node);
            this.AddTile(tile);
            return tile;
        }

        public void AddTile<T>(T tile, int index = -1) where T : ITile
        {
            dynamic uiTile = tile;
            if (index < 0)
                this.DisplayContent.Children.Add(uiTile);
            else
                this.DisplayContent.Children.Insert(index + 1, uiTile);
        }
    }
}
