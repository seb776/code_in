using code_in.Models.Theme;
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
    /// Logique d'interaction pour CreateTheme.xaml
    /// </summary>
    public partial class ThemeLayout : UserControl, ICodeInVisual
    {

        private ResourceDictionary _themeResourceDictionary = null;

        public MainView.MainView _preview = null;

        public ThemeLayout(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
            _preview = new MainView.MainView(Code_inApplication.ThemePreviewResourceDictionary);
            //this.previewThemeLayout.Children.Add(_preview);
        }
        public ThemeLayout() :
            this(Code_inApplication.MainResourceDictionary)
        { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }

        #region ICode_inVisual
        public void SetDynamicResources(String keyPrefix)
        {

        }
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public ResourceDictionary GetLanguageResourceDictionary()
        {
            throw new NotImplementedException();
        }

        public void SetLanguageResources()
        {
            throw new NotImplementedException();
        }
        #endregion ICode_inVisual
    }
}
