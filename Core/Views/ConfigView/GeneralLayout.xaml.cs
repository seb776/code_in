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
    /// Logique d'interaction pour GeneralLayout.xaml
    /// </summary>
    public partial class GeneralLayout : UserControl, ICodeInVisual
    {
        private ResourceDictionary _resourceDictionary;
        public ResourceDictionary GetResourceDictionary() { return _resourceDictionary; }
        public GeneralLayout(ResourceDictionary resDict)
        {
            this._resourceDictionary = resDict;
            this.Resources.MergedDictionaries.Add(this._resourceDictionary);
            InitializeComponent();
        }
        public GeneralLayout() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
        }
        // The next 3 functions are for the wheckbox of the catégory "General->Activate tutorial"

        //if box checked -> send
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Handle(sender as CheckBox);
        }
        // if box unchecked -> send anyway ^^
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Handle(sender as CheckBox);
        }

        // receive event, verify if checked or not -> different behaviour depending on result
        void Handle(CheckBox checkBox)
        {
            // Use IsChecked.
            bool flag = checkBox.IsChecked.Value;

            // if box checked --> do something (but not my part)
            if (flag == true)
            {
                MessageBox.Show("Hello ! You just checked the tutorial mode !", "Confirmation"); // Juste une petite fenêtre qui s'ouvre pour vérifier que la checkbox fonctionne bien ;)
            }
            
            // if box unchecked (doesn't work if it wasn't checked before)
            else
            {
                MessageBox.Show("Oh, enough of the tutorial ? I hope it have helped you :D");
            }
        }

        // Event for maj button of "General" menu, mboxes only for testing :)
        private void maj_menu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Votre version est à jour :D (ou pas ?)");
        }

        // Here we open a windows where we can lookinf for a file (file browsing)
        private void bOpenFileDialog_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".txt"; // change filter with our proper extensions #1
            dlg.Filter = "Text documents (.txt)|*.txt"; // same than #1

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                Path.Text = filename;
                // filename = the path of the choosen file
            }
        }

        // The two Cancel/Confirm buttons
        private void Button_Confirm(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Vous avez validé votre choix !");
        }
        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Vous avez annulé votre choix !");
        }

    }
}
