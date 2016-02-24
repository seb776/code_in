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
    public partial class ConfigView : UserControl, stdole.IDispatch, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private UserControl currentMenu;
        private Dictionary<String, UserControl> menu = new Dictionary<String, UserControl>();

        public ConfigView(ResourceDictionary themeResDict)
        {
            Dictionary<String, UserControl> panels = new Dictionary<String, UserControl>() {
                {"General", new SubViews.GeneralLayout(themeResDict)},
                {"Theme", new SubViews.ThemeLayout(themeResDict)},
                {"Shortcuts", new SubViews.ShortcutsLayout(themeResDict)},
                {"Performances", new SubViews.PerformancesLayout(themeResDict)},
                {"Errors", new SubViews.ErrorsLayout(themeResDict)}
            };
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
            foreach (var i in panels)
            {
                TreeViewItem item = new TreeViewItem();
                item.Foreground = new SolidColorBrush(Color.FromRgb(0x42, 0x42, 0x42));
                item.Header = i.Key;
                item.DataContext = i.Value;
                this.TreeViewMenu.Items.Add(item);
                i.Value.Visibility = System.Windows.Visibility.Hidden;
                i.Value.IsEnabled = false;
                this.RightPanel.Children.Add(i.Value);
            }
            //menu.Add("Général", GenMenu);
            //menu.Add("Thèmes", TheMenu);
            //menu.Add("Erreurs", ErrMenu);
            //menu.Add("Raccourcis", ShortMenu);
            //menu.Add("Performances", PerfMenu);
        }
        public ConfigView() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        { /* Here we must keep the ability to instantiate from default constructor as if it's called by VSConnect, it cannot pass parameters. */}

        #region ICodeInVisual
        public void SetDynamicResources(String keyPrefix)
        {
            throw new NotImplementedException();
        }
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        #endregion ICodeInVisual

        // For handling the changement of the menu item (moving from a category to another)
        private void myTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (RightPanel != null)
            {
                TreeViewItem newItem = ((TreeViewItem)e.NewValue);
                UserControl selectedMenu = newItem.DataContext as UserControl;

                if (currentMenu != null)
                {
                    // Set the ancient item to hidden/disabled
                    currentMenu.Visibility = System.Windows.Visibility.Hidden;
                    currentMenu.IsEnabled = false;
                }

                // Set the new Item to visible/enabled
                selectedMenu.Visibility = System.Windows.Visibility.Visible;
                selectedMenu.IsEnabled = true;

                // The new item is now our current item
                currentMenu = selectedMenu;
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            currentMenu = ((TreeViewMenu.Items[0] as TreeViewItem).DataContext as UserControl);
            currentMenu.Visibility = System.Windows.Visibility.Visible;
            currentMenu.IsEnabled = true;
        }
    }
}