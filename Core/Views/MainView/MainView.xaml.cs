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

namespace code_in.Views.MainView
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public partial class MainView : UserControl, stdole.IDispatch
    {
        private ViewModels.code_inMgr _code_inMgr;

        public void OpenFile(String filePath)
        {
            //MessageBox.Show(filePath);
            _code_inMgr._codeMgr.GenerateTreeFromFile(filePath);
        }
        public MainView()
        {
            InitializeComponent();

            _code_inMgr = new ViewModels.code_inMgr();

            this.MouseWheel += MainView_MouseWheel;
            this.MouseDown += MainView_MouseDown;
            this.KeyDown += MainView_KeyDown;
        }

        void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            int step = 2;
            Rect tmp = (Rect)this.Resources["RectDims"];   
            if (e.Key == Key.Add)
            {
                tmp.Width += step;
                tmp.Height += step;
            }
            if (e.Key == Key.Subtract && tmp.Width > 15)
            {
                tmp.Width -= step;
                tmp.Height -= step;
            }
            this.Resources["RectDims"] = tmp;
            ((DrawingBrush)this.Resources["GridTile"]).Viewport = tmp;
            //MessageBox.Show("Do you want to close this window?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        void MainView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Resources["RectDims"] = new Rect(0, 0, 20, 20);
            MessageBox.Show("Do you want to close this window?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        void MainView_MouseWheel(object sender, MouseWheelEventArgs e)
        {
        }
    }
}
