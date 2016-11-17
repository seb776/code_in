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

namespace code_in.Views.NodalView.NodesElems.Nodes.Assets
{
    /// <summary>
    /// Logique d'interaction pour AttributesAsset.xaml
    /// </summary>
    public partial class AttributesAsset : UserControl, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private ResourceDictionary _languageResourceDictionary = null;
        public List<AttributesItems> ItemsList = null;
        public AttributesAsset(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(_themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(_languageResourceDictionary);
            InitializeComponent();
            ItemsList = new List<AttributesItems>();
            updateViewPanel();
        }

        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public ResourceDictionary GetLanguageResourceDictionary() { return _languageResourceDictionary; }

        public void SetThemeResources(String keyPrefix)
        {

        }

        public void SetLanguageResources(String keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInVisual

        public void updateViewPanel()
        {
            if (ItemsList.Count > 0)
            {
                AttributeField.Children.Clear();
                foreach (AttributesItems item in ItemsList)
                    AttributeField.Children.Add(item);
            }
        }
        public void addAttribute(string type, string arg)
        {
            AttributesItems attr = new AttributesItems(type, arg);
            ItemsList.Add(attr);
            updateViewPanel();
        }


        public void deleteAttribute(int index)
        {
            ItemsList.RemoveAt(index);
            updateViewPanel();
        }
    }
}
