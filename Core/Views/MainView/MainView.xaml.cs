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
            _code_inMgr.LoadFile(filePath);

        }

        public MainView()
        {
            InitializeComponent();

            _code_inMgr = new ViewModels.code_inMgr(this);

            this.MouseWheel += MainView_MouseWheel;
            this.MouseDown += MainView_MouseDown;
            this.KeyDown += MainView_KeyDown;
            this.MouseUp += MainView_MouseUp;
        }

        void MainView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Nodes.TransformingNode.TransformingObject = null;
            Nodes.TransformingNode.Transformation = Nodes.TransformingNode.TransformationMode.NONE;
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

        Point lastPosition = new Point(0,0);

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            Vector diff;
            if ((lastPosition.X + lastPosition.Y) < 0.01)
                diff = new Vector(0, 0);
            else
            {
                diff = lastPosition - e.GetPosition(null);
            }
            lastPosition = e.GetPosition(null);

            if (Nodes.TransformingNode.TransformingObject != null)
            {
                //((ScrollViewer)((Grid)sender).Parent).ScrollToHorizontalOffset(((ScrollViewer)((Grid)sender).Parent).HorizontalOffset + (diff.X < 0 ? -.1 : .1));
                if (Nodes.TransformingNode.Transformation == Nodes.TransformingNode.TransformationMode.RESIZE)
                {
                    double sizeX = (double)Nodes.TransformingNode.TransformingObject.GetType().GetProperty("ActualWidth").GetValue(Nodes.TransformingNode.TransformingObject);
                    double sizeY = (double)Nodes.TransformingNode.TransformingObject.GetType().GetProperty("ActualHeight").GetValue(Nodes.TransformingNode.TransformingObject);
                    //MessageBox.Show((sizeX + diff.X).ToString());
                    Nodes.TransformingNode.TransformingObject.GetType().GetProperty("Width").SetValue(Nodes.TransformingNode.TransformingObject, sizeX - diff.X);
                    Nodes.TransformingNode.TransformingObject.GetType().GetProperty("Height").SetValue(Nodes.TransformingNode.TransformingObject, sizeY - diff.Y);
                    //((Nodes.TransformingNode.TransformingObject.GetType().get)Nodes.TransformingNode.TransformingObject)
                }
                else if (Nodes.TransformingNode.Transformation == Nodes.TransformingNode.TransformationMode.MOVE)
                {
                    Thickness margin = (Thickness)Nodes.TransformingNode.TransformingObject.GetType().GetProperty("Margin").GetValue(Nodes.TransformingNode.TransformingObject);
                    double marginLeft = margin.Left;
                    double marginTop = margin.Top;
                    Thickness newMargin = margin;
                    newMargin.Left -= diff.X;
                    newMargin.Top -= diff.Y;
                    Nodes.TransformingNode.TransformingObject.GetType().GetProperty("Margin").SetValue(Nodes.TransformingNode.TransformingObject, newMargin);
                }
            }
        }
    }
}
