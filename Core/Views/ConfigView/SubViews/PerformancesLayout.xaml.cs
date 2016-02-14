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

namespace code_in.Views.ConfigView.SubViews
{
    /// <summary>
    /// Logique d'interaction pour PerformancesLayout.xaml
    /// </summary>
    public partial class PerformancesLayout : UserControl, ICodeInVisual
    {
        public void SetDynamicResources(String keyPrefix)
        {

        }
        private ResourceDictionary _themeResourceDictionary;
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public PerformancesLayout(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
        }
        public PerformancesLayout() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }

         // The two buttons confirm/Cancel
        private void Button_Confirm(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {

        }

        // The two function for the checkbox "Store Ast"
        private void StorAst_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void StorAst_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        // The two functions for the checkbox "Activate Multithreading"
        private void MultiThread_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void MultiThread_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void DropShadow_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void DropShadow_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void BgTask_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void BgTask_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Expand_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Expand_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
