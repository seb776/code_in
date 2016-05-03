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
        private ResourceDictionary _languageResourceDictionary = null;
        public ConfigSubViewTemplate(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(this._languageResourceDictionary);
            this.InitializeComponent();
            this.SetThemeResources(null);
        }

        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public ResourceDictionary GetLanguageResourceDictionary() { return _languageResourceDictionary; }

        public void SetThemeResources(String keyPrefix)
        {
            this.HeaderName.SetResourceReference(Label.ForegroundProperty, "DefaultColorLight");
        }

        public void SetLanguageResources(String keyPrefix)
        {
            this.HeaderName.SetResourceReference(Label.ContentProperty, keyPrefix + "ConfigPanelTitle");
        }
        #endregion ICodeInVisual

        public void SetMenuContent(UserControl uc)
        {
            this._subViewGrid.Children.Clear();
            this._subViewGrid.Children.Add(uc);
        }
    }
}
