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
        private ResourceDictionary _themeResourceDictionary = null;
        public BaseTile(ResourceDictionary themeResDict)
        {
            _themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(themeResDict);
            InitializeComponent();
        }

        public BaseTile() :
            this(Code_inApplication.MainResourceDictionary)
        {
            //throw new Exceptions.DefaultCtorVisualException();
        }

        public T CreateAndAddItem<T>() where T : UIElement, ITileItem
        {
            T item = (T)Activator.CreateInstance(typeof(T), this._themeResourceDictionary);
            this.AddItem(item);
            return item;
        }

        public void AddItem<T>(T item) where T : UIElement, ITileItem
        {
            this.TileContent.Children.Add(item);
        }

        public void SetParentView(IVisualNodeContainerDragNDrop vc)
        {
            //throw new NotImplementedException();
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

    }
}
