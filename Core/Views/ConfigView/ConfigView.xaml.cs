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
    public partial class ConfigView : UserControl, stdole.IDispatch
    {
        private UserControl currentMenu;
        private Dictionary<string, UserControl> menu = new Dictionary<string, UserControl>();
        public ConfigView()
        {
            InitializeComponent();
            menu.Add("Général", GenMenu);
            menu.Add("Thèmes", TheMenu);
            menu.Add("Erreurs", ErrMenu);
            menu.Add("Raccourcis", ShortMenu);
            menu.Add("Performances", PerfMenu);
        }
        
        // Pour handle le changement de l'item du menu (quand on change de catégorie dans le menu)
        private void myTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (RightPanel.IsLoaded)
            {
                // Set the new Item to visible/enabled
                //((UserControl)sender).Visibility = System.Windows.Visibility.Visible;
                //((UserControl)sender).IsEnabled = true;
                TheMenu.Visibility = System.Windows.Visibility.Visible;
                TheMenu.IsEnabled = true;
                // Set the ancien item to hidden/disabled
                currentMenu.Visibility = System.Windows.Visibility.Hidden;
                currentMenu.IsEnabled = false;

                // The new item is now our current item
                currentMenu = TheMenu;//((UserControl)e.NewValue);
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            currentMenu = GenMenu;
        }
    }
}