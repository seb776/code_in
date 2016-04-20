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

namespace code_in.Views.ConfigView.SubViews
{
    /// <summary>
    /// This class is the default layout for the subviews of the configuration menu.
    /// </summary>
    public partial class ConfigSubViewTemplate : UserControl, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        public ConfigSubViewTemplate(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            this.InitializeComponent();
        }

        public ResourceDictionary GetThemeResourceDictionary()
        {
            return _themeResourceDictionary;
        }

        public void SetDynamicResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }

        public void SetMenuContent(UserControl uc)
        {
            this._subViewGrid.Children.Clear();
            this._subViewGrid.Children.Add(uc);
        }

        public void SetMenuName(String resourceKey)
        {
            // TODO
            //this.HeaderName.SetResourceReference(Label.ContentProperty, resourceKey);
            this.HeaderName.Content = resourceKey;
        }


        public ResourceDictionary GetLanguageResourceDictionary()
        {
            throw new NotImplementedException();
        }

        public void SetLanguageResources()
        {
            throw new NotImplementedException();
        }
    }
}
