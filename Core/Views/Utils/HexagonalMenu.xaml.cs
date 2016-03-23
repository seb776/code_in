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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace code_in.Views.Utils
{
    /// <summary>
    /// Interaction logic for HexagonalMenu.xaml
    /// </summary>
    public partial class HexagonalMenu : UserControl
    {
        public HexagonalMenu()
        {
            InitializeComponent();
            this.Opacity = 0;
        }

        public void AddHexagonButton(int x, int y)
        {
            var hexBtn = new HexagonalButton();
            this.GridHexa.Children.Add(hexBtn);
            hexBtn.Margin = new Thickness(x * (65 * ((y % 2) == 0 ? 2 : 1)), y * 120, 0, 0);
        }

        public void ShowMenu()
        {
            DoubleAnimation da = new DoubleAnimation();

            da.From = 0;
            da.To = 1;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(150));
            this.BeginAnimation(OpacityProperty, da);
        }
    }
}
