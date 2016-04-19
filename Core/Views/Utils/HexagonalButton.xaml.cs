using code_in.Views.NodalView.NodesElems;
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

namespace code_in.Views.Utils
{
    /// <summary>
    /// Interaction logic for HexagonalButton.xaml
    /// </summary>
    public partial class HexagonalButton : UserControl
    {
        public HexagonalButton(Color c, ButtonAction btnAction, params object[] args)
        {
            InitializeComponent();
            _btnAction = btnAction;
            _args = args;
            this.HexaButton.Fill = new SolidColorBrush(c);
        }

        public delegate void ButtonAction(object[] args);
        ButtonAction _btnAction;
        object[] _args;

        private void HexaButton_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("yeah");
            if (_btnAction != null)
                _btnAction.Invoke(_args);
        }
    }
}
