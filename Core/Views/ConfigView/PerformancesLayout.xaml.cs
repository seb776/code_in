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
    /// Logique d'interaction pour PerformancesLayout.xaml
    /// </summary>
    public partial class PerformancesLayout : UserControl, ICodeInVisual
    {
        private ResourceDictionary _resourceDictionary;
        public ResourceDictionary GetResourceDictionary() { return _resourceDictionary; }
        public PerformancesLayout(ResourceDictionary resDict)
        {
            this._resourceDictionary = resDict;
            this.Resources.MergedDictionaries.Add(this._resourceDictionary);
            InitializeComponent();
        }
        public PerformancesLayout() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
        }
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
