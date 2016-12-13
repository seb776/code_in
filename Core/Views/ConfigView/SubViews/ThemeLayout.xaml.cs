using code_in.Exceptions;
using code_in.Models.Theme;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace code_in.Views.ConfigView.SubViews
{
    /// <summary>
    /// Logique d'interaction pour CreateTheme.xaml
    /// </summary>
    public partial class ThemeLayout : UserControl, ICodeInVisual, ICodeInTextLanguage
    {

        private ResourceDictionary _themeResourceDictionary = null;
        private ResourceDictionary _languageResourceDictionary = null;
        private NodalView.DeclarationsNodalView _preview = null;

        public ThemeLayout(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(this._languageResourceDictionary);
            InitializeComponent();


            _preview = new NodalView.DeclarationsNodalView(Code_inApplication.ThemePreviewResourceDictionary);
            _preview.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            _preview.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.PreviewArea.Content = _preview;

            this.SetThemeResources("");
            this.SetLanguageResources("ConfigTheme");
        }
        public ThemeLayout() :
            this(Code_inApplication.MainResourceDictionary)
        { throw new DefaultCtorVisualException(); }

        #region ICode_inVisual

        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public ResourceDictionary GetLanguageResourceDictionary() { return _languageResourceDictionary; }
        public void SetThemeResources(String keyPrefix)
        {
            this.SetResourceReference(UserControl.StyleProperty, "StyleTabItems");
        }
        public void SetLanguageResources(String keyPrefix)
        {
            this.HeaderGeneral.SetResourceReference(TabItem.HeaderProperty, keyPrefix + "GeneralHeader");
            this.HeaderNodal.SetResourceReference(TabItem.HeaderProperty, keyPrefix + "NodalHeader");
            CurrentThemeLabel.SetResourceReference(Label.ContentProperty, "CurrentTheme");
            DefaultTheme.SetResourceReference(ComboBoxItem.ContentProperty, "DefaultTheme");
            SaveCurrentTheme.SetResourceReference(Button.ContentProperty, "SaveTheme");
            RemoveCurrentTheme.SetResourceReference(Button.ContentProperty, "RemoveTheme");
            NodalThemeBackground.SetResourceReference(Label.ContentProperty, "ThemeBackground");
            NodalThemeGeneral.SetResourceReference(Label.ContentProperty, "ThemeGeneral");
            NodalThemeGrid.SetResourceReference(Label.ContentProperty, "ThemeGrid");
            NodalThemeNodes.SetResourceReference(Label.ContentProperty, "ThemeNodes");
            NodalThemeSettings.SetResourceReference(Label.ContentProperty, "ThemeSettings");
            NodalThemePreview.SetResourceReference(Label.ContentProperty, "ThemePreview");
        }
        #endregion ICode_inVisual

        private void ComboBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            var item = sender as ComboBox;
            string selectedName = (item.SelectedItem as ComboBoxItem).Name;
            string path;
            if (selectedName == "LightTheme")
                path = "LightThemeResourcesDictionary.xaml";
            else
                path = "DarkThemeResourcesDictionary.xaml";
            ResourceDictionary retResDict = null;
            try
            {
                var reader = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                retResDict = XamlReader.Load(reader) as ResourceDictionary;
            }
            catch (Exception except)
            {
                Console.Error.WriteLine(except.Message);
            }
            Code_inApplication.ThemePresenter.ApplyTheme(retResDict);
        }
    }
}
