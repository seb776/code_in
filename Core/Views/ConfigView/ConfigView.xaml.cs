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
using System.Windows.Controls;
using System.Windows;

namespace code_in.Views.ConfigView
{
    /// <summary>
    /// Logique d'interaction pour ConfigView.xaml
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public partial class ConfigView : UserControl, stdole.IDispatch
    {
        public ConfigView()
        {
            InitializeComponent();
        }
    }
}