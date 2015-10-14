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
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public partial class GeneralLayout : UserControl, stdole.IDispatch
    {
        public GeneralLayout()
        {
            InitializeComponent();
        }
        // Les 3 fonctions suivantes sont pour la check box de "Général->Activer tuto"
        //if box checked -> send
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Handle(sender as CheckBox);
        }
        // if box unchecked -> send
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Handle(sender as CheckBox);
        }
        // reçoit l'event(?), vérifie si coché ou non -> different comportement en fonction du résultat
        void Handle(CheckBox checkBox)
        {
            // Use IsChecked.
            bool flag = checkBox.IsChecked.Value;
            if (flag == true)
            {
                MessageBox.Show("Hello ! You just checked the tutorial mode !", "Confirmation"); // Juste une petite fenêtre qui s'ouvre pour vérifier que la checkbox fonctionne bien ;)
                // si la box est cochée --> do something (but not my part)
            }
            // Si elle est décochée (Ne fonctionne pas si elle n'a pas été cochée avant)
            else
            {
                MessageBox.Show("Oh, enough of the tutorial ? I hope it have helped you :D");
            }
        }

        // Event pour le boutton maj du menu Général (pour le menu déroulant) les messages box seront a virer, c'était seulement pour les tests
        private void maj_menu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Votre version est à jour :D (ou pas ?)");
        }

        // Méthode pour ouvrir une fenêtre pour parcourir les dossiers (dans le but de choisir un fichier)
        private void bOpenFileDialog_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".txt"; // à changer en fonction du nom de nos extensions de fichier #1
            dlg.Filter = "Text documents (.txt)|*.txt"; // same than #1

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                Path.Text = filename;
                // filename = le path du fichier
            }
        }

        // Méthodes des deux boutons Valider/Annuler
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
