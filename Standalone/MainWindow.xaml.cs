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

namespace code_in
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //_application = new App(this);
        }

        private void MainGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C)
            {
                Code_inApplication.StartApplication(Application.Current as IEnvironmentWrapper);
                var config = new code_in.Views.ConfigView.ConfigView();
                TabItem nItem = new TabItem();
                nItem.Header = "Parameters";
                nItem.Content = config;
                this.TabCtrl.Items.Add(nItem);
            }
        }
    }
}
