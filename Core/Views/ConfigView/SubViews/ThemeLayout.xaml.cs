﻿using code_in.Models.Theme;
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
        private ResourceDictionary _languageResourceDictionary = null;
        private MainView.MainView _preview = null;

        public ThemeLayout(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(this._languageResourceDictionary);
            InitializeComponent();


            _preview = new MainView.MainView(Code_inApplication.ThemePreviewResourceDictionary);
            _preview.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            _preview.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.PreviewArea.Content = _preview;

            this.SetThemeResources("");
            this.SetLanguageResources("ConfigTheme");
        }
        public ThemeLayout() :
            this(Code_inApplication.MainResourceDictionary)
        { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }

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
        }
        #endregion ICode_inVisual
    }
}
