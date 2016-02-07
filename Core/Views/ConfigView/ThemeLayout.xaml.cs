using code_in.ViewModels;
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

namespace code_in.Views.ConfigView
{
    /// <summary>
    /// Logique d'interaction pour CreateTheme.xaml
    /// </summary>
    public partial class ThemeLayout : UserControl, ICodeInVisual
    {
        private ResourceDictionary _resourceDictionary = null;
        public ResourceDictionary GetResourceDictionary() { return _resourceDictionary; }

        public ThemeLayout(ResourceDictionary resDict)
        {
            this._resourceDictionary = resDict;
            this.Resources.MergedDictionaries.Add(this._resourceDictionary);
            InitializeComponent();

            themeList["DefaultTheme"] = DefaultData;
            themeList["DarkTheme"] = DarkData;
            foreach (KeyValuePair<string, Models.Theme.ThemeData> elem in themeList)
                BoxTheme.Items.Add(elem.Key);
            ThemeName.AddHandler(TextBox.TextInputEvent,
                   new TextCompositionEventHandler(ThemeName_TextInput_1),
                   true);
            this.previewThemeLayout.Children.Add(preview);
        }
        public ThemeLayout() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
        }

        private void DeleteTheme(object sender, RoutedEventArgs e)
        {
            if (BoxTheme.SelectedItem.ToString() != null)
            {
                themeList.Remove(BoxTheme.SelectedItem.ToString());
                BoxTheme.Items.Remove(BoxTheme.SelectedItem);
            }
        }

        private void ThemeName_TextInput_1(object sender, TextCompositionEventArgs e)
        {
            ThemeName.KeyUp += new KeyEventHandler(ThemeName_KeyDown);

            if (e.Text != "\n")
            {
                name += e.Text;
            }
        }

        public void setCode_inMgr(code_inMgr c)
        {
            codeinMgr = c;
        }
        code_inMgr codeinMgr = null;
        private void ThemeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PreviewName.Text = "Aperçu de " + name;
                CurrentCustomData.Name = name;
            }
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            if (Tmp != null)
            {
                codeinMgr._themeMgr.setMainTheme(Tmp);
            }
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

        private void BoxTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if(e.AddedItems[0].ToString() == "DefaultTheme")
            {
                codeinMgr._themeMgr.setPreviewTheme(DefaultData);
                Tmp = DefaultData;
            }
            else
            {
                codeinMgr._themeMgr.setPreviewTheme(DarkData);
                Tmp = DarkData;
            }
        }
    }
}
