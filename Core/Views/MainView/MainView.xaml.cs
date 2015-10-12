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

        void MainView_MouseWheel(object sender, MouseWheelEventArgs e)
        {
        }

        Point lastPosition = new Point(0,0);

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            bool gridMagnet = true;
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
                    double nSizeX = sizeX - diff.X;
                    double nSizeY = sizeY - diff.Y;

                    //MessageBox.Show((sizeX + diff.X).ToString());
                    Nodes.TransformingNode.TransformingObject.GetType().GetProperty("Width").SetValue(Nodes.TransformingNode.TransformingObject, nSizeX);
                    Nodes.TransformingNode.TransformingObject.GetType().GetProperty("Height").SetValue(Nodes.TransformingNode.TransformingObject, nSizeY);
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

                    //newMargin.Left = (double)(((int)newMargin.Left / 20) * 20); // Temporary test for magnetGrid
                    //newMargin.Top = (double)(((int)newMargin.Top / 20) * 20);
                    Nodes.TransformingNode.TransformingObject.GetType().GetProperty("Margin").SetValue(Nodes.TransformingNode.TransformingObject, newMargin);
                }
            }
        }

        private void MainGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var cm = new ContextMenu();
            var m1 = new MenuItem();
            m1.Header = "New BaseNode";
            m1.Click += m1_Click;
            cm.Items.Add(m1);
            cm.IsOpen = true;
            cm.Margin = new Thickness(e.GetPosition(this).X, e.GetPosition(this).Y, 0, 0);
            //this.WinGrid.Children.Add(cm);
        }

        void m1_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("fail");
            var node = new Nodes.BaseNode();
//            node.Margin = new Thickness(e.GetPosition(this.MainGrid).X, e.GetPosition(this.MainGrid).Y, 0, 0);
            node.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            node.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.MainGrid.Children.Add(node);
        }
    }
}
