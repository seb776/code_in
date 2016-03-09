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
        public void SetDynamicResources(String keyPrefix)
        {

        }
        private ResourceDictionary _themeResourceDictionary = null;
        private ModThemeData temp = new ModThemeData();

        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public Dictionary<String, AThemeData> _themeList = new Dictionary<string,AThemeData>();
        public MainView.MainView _preview = null;

        public ThemeLayout(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
            _preview = new MainView.MainView(code_in.Resources.SharedDictionaryManager.ThemePreviewResourceDictionary);
            _themeList["DefaultTheme"] = null;
            _themeList["DarkTheme"] = null;
            foreach (KeyValuePair<string, AThemeData> elem in _themeList)
                BoxTheme.Items.Add(elem.Key);
            ThemeName.AddHandler(TextBox.TextInputEvent,
                   new TextCompositionEventHandler(ThemeName_TextInput_1),
                   true);
            this.previewThemeLayout.Children.Add(_preview);
        }
        public ThemeLayout() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }

        private void DeleteTheme(object sender, RoutedEventArgs e)
        {
            if (BoxTheme.SelectedItem.ToString() != null)
            {
                _themeList.Remove(BoxTheme.SelectedItem.ToString());
                BoxTheme.Items.Remove(BoxTheme.SelectedItem);
            }
        }

        private void ThemeName_TextInput_1(object sender, TextCompositionEventArgs e)
        {
            //ThemeName.KeyUp += new KeyEventHandler(ThemeName_KeyDown);

            //if (e.Text != "\n")
            //{
            //    name += e.Text;
            //}
        }

        private void ThemeName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    PreviewName.Text = "Aperçu de " + name;
            //    CurrentCustomData.Name = name;
            //}
        }

        private void Button_Confirm(object sender, RoutedEventArgs e)
        {
            if (Tmp != null)
                Code_inApplication.ThemeMgr.setMainTheme(Tmp);
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WidthGridChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void HeightGridChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BgColorChanged(object sender, TextChangedEventArgs e)
        {
         
        }

        private void TraitsColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NodeBaseFrontColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NodeBaseBgColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NodeBaseNameColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NodeVarDeclFrontColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NodeVarDeclBgColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NodeVarDeclNameColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NodeClassDeclFrontColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NodeClassDeclBgColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NodeClassDeclNameColorChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void NodeFunctDeclFrontColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NodeFunctDeclBgColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NodeFunctDeclNameColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NodeStructDeclFrontColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NodeStructDeclBgColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NodeStructDeclNameColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NodeNsDeclFrontColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NodeNsDeclBgColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NodeNsDeclNameColorChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TypeColorIOIntChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void TypeColorIOFloatChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TypeColorIODoubleChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TypeColorIOShortChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TypeColorIOCharChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TypeColorIOBoolChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TypeColorIOVoidChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TypeColorIOObjectChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TypeColorIOOtherTypeChanged(object sender, TextChangedEventArgs e)
        {

        }
        private DefaultThemeData DefaultData = new DefaultThemeData();
        private DarkThemeData DarkData = new DarkThemeData();
        AThemeData Tmp = null;
        private void BoxTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0].ToString() == "DefaultTheme")
            {
                Code_inApplication.ThemeMgr.setPreviewTheme(DefaultData);
                Tmp = DefaultData;
            }
            else
            {
                Code_inApplication.ThemeMgr.setPreviewTheme(DarkData);
                Tmp = DarkData;
            }
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {

        }
    }
}
