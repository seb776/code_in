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
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;

namespace code_in.Views.ConfigView
{
    /// <summary>
    /// Logique d'interaction pour ConfigView.xaml
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public partial class ConfigView : UserControl, stdole.IDispatch, ICodeInVisual, ICodeInTextLanguage, ICode_inWindow
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private ResourceDictionary _languageResourceDictionary = null;
        private UserControl _currentMenu = null;
        private Dictionary<String, UserControl> _menu = new Dictionary<String, UserControl>();

        public ConfigView(ResourceDictionary themeResDict)
        {
            Dictionary<String, UserControl> panels = new Dictionary<String, UserControl>() {
                {"General", new SubViews.GeneralLayout(themeResDict)},
                {"Theme", new SubViews.ThemeLayout(themeResDict)},
                {"Shortcuts", new SubViews.ShortcutsLayout(themeResDict)}
            };
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(this._languageResourceDictionary);
            InitializeComponent();

            foreach (var i in panels)
            {
                TreeViewItem item = new TreeViewItem();
                item.Foreground = new SolidColorBrush(Color.FromRgb(0x42, 0x42, 0x42));
                item.Header = i.Key;
                item.SetResourceReference(TreeViewItem.HeaderProperty, i.Key + "ConfigPanelTitle");
                this.TreeViewMenu.Items.Add(item);
                var subViewWrapper = new SubViews.ConfigSubViewTemplate(this.GetThemeResourceDictionary());
                subViewWrapper.SetMenuContent(i.Value);
                subViewWrapper.SetLanguageResources(i.Key);
                item.DataContext = subViewWrapper;
                subViewWrapper.Visibility = System.Windows.Visibility.Hidden;
                subViewWrapper.IsEnabled = false;
                this.RightPanel.Children.Add(subViewWrapper);
            }
        }
        public ConfigView() :
            this(Code_inApplication.MainResourceDictionary)
        { /* Here we must keep the ability to instantiate from default constructor as if it's called by VSConnect, it cannot pass parameters. */}

        #region ICodeInVisual

        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public ResourceDictionary GetLanguageResourceDictionary() { return _languageResourceDictionary; }

        public void SetLanguageResources(String keyPrefix)
        {
            throw new NotImplementedException();
        }
        public void SetThemeResources(String keyPrefix)
        {
            throw new NotImplementedException();
        }
        #endregion ICodeInVisual

        #region Events
        // For handling the changement of the menu item (moving from a category to another)
        private void myTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (RightPanel != null)
            {
                TreeViewItem newItem = ((TreeViewItem)e.NewValue);
                UserControl selectedMenu = newItem.DataContext as UserControl;

                if (_currentMenu != null)
                {
                    // Set the ancient item to hidden/disabled
                    _currentMenu.Visibility = System.Windows.Visibility.Hidden;
                    _currentMenu.IsEnabled = false;
                }

                // Set the new Item to visible/enabled
                selectedMenu.Visibility = System.Windows.Visibility.Visible;
                selectedMenu.IsEnabled = true;

                // The new item is now our current item
                _currentMenu = selectedMenu;
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            _currentMenu = ((TreeViewMenu.Items[0] as TreeViewItem).DataContext as UserControl);
            _currentMenu.Visibility = System.Windows.Visibility.Visible;
            _currentMenu.IsEnabled = true;
        }
        #endregion Events

        public IEnvironmentWindowWrapper EnvironmentWindowWrapper
        {
            get;
            set;
        }

        public bool IsSaved
        {
            get
            { 
                return false;
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}