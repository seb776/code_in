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
using System.Windows.Controls.Primitives;

namespace code_in.Views.MainView
{
    /// <summary>
    /// Interaction logic for MainButton.xaml
    /// </summary>
    public partial class MainButton : UserControl, ICodeInVisual
    {
        public void SetDynamicResources(String keyPrefix)
        {

        }
        private ResourceDictionary _themeResourceDictionary = null;
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public MainButton()
        {
            InitializeComponent();
        }

        private void Polygon_MouseEnter(object sender, MouseEventArgs e)
        {
            this.GreenBtn.IsEnabled = true;
            this.GreenBtn.Visibility = System.Windows.Visibility.Visible;
            e.Handled = true;
        }

        private void Polygon_MouseLeave(object sender, MouseEventArgs e)
        {
            this.GreenBtn.IsEnabled = false;
            this.GreenBtn.Visibility = System.Windows.Visibility.Hidden;
            e.Handled = true;

        }

        private void Polygon_MouseEnter_1(object sender, MouseEventArgs e)
        {
            this.PinkBtn.IsEnabled = true;
            this.PinkBtn.Visibility = System.Windows.Visibility.Visible;
        }

        private void Polygon_MouseLeave_1(object sender, MouseEventArgs e)
        {
            this.PinkBtn.IsEnabled = false;
            this.PinkBtn.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Polygon_MouseEnter_2(object sender, MouseEventArgs e)
        {
            this.BlueBtn.IsEnabled = true;
            this.BlueBtn.Visibility = System.Windows.Visibility.Visible;
        }

        private void Polygon_MouseLeave_2(object sender, MouseEventArgs e)
        {
            this.BlueBtn.IsEnabled = false;
            this.BlueBtn.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Pink_Clicked(object sender, MouseEventArgs e)
        {
            Popup codePopup = new Popup();
            TextBlock popupText = new TextBlock();
            popupText.Text = "Pink";
            popupText.Background = Brushes.Pink;
            popupText.Foreground = Brushes.Purple;
            codePopup.Child = popupText;
            codePopup.IsOpen = true;
        }
    }
}
