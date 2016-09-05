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
    /// Logique d'interaction pour NewFlowTileItem.xaml
    /// </summary>
    public partial class NewFlowTileItem : UserControl, ITileContainer, ITileItem, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        public NewFlowTileItem() :
            this(Code_inApplication.MainResourceDictionary)
        { /*throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); */}

        public NewFlowTileItem(ResourceDictionary themeResDict)
        {
            _themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(themeResDict);
            InitializeComponent();
        }

        public T CreateAndAddTile<T>() where T : ITile
        {
            T tile = (T)Activator.CreateInstance(typeof(T), this._themeResourceDictionary);  // TODO From Seb: This may crash if the constructor is not implemented
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
                this.ItemContent.Children.Add(uiTile);
            else
                this.ItemContent.Children.Insert(index + 1, uiTile);
        }

        public ResourceDictionary GetThemeResourceDictionary()
        {
            return _themeResourceDictionary;
        }

        public void SetThemeResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }
    }
}
