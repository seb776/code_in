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
    /// Logique d'interaction pour ShortcutsLayout.xaml
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public partial class ShortcutsLayout : UserControl, stdole.IDispatch
    {
        public ShortcutsLayout()
        {
            InitializeComponent();
        }

        private void Button_Confirm(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {

        }
    }
}
