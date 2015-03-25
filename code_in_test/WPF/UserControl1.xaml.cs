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

namespace code_in_test
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public partial class UserControl1 : UserControl
    {
        List<Line> list = new List<Line>();
        private static Grid isSelected = null;
       // public static Boolean justClicked = false;
      //  public WPF.testNode _node;

        WPF.Bezier bezier;
        public UserControl1()
        {
            InitializeComponent();
            bezier = new WPF.Bezier(this.grid_win, new Point(0, 0), new Point(50, 50));
           // _node = new WPF.testNode(this.grid_win);
            //this.MouseMove += UserControl1_MouseMove;
            //this.grid_win.Children.Add(new Line())
            anchor = new Tuple<float, float>(10, 25);
            //this.grid_win.Focus();
        }
        Tuple<float, float> anchor;

        void UserControl1_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = e.GetPosition(this.grid_win);

            if (isSelected != null)
            {
                isSelected.Margin = new Thickness(pt.X, pt.Y, 0, 0);
            }
            bezier.setPositions(new Point(0, 0), pt);


       //     Tuple<float, float> dep = new Tuple<float, float>(0, 0);
       //     Tuple<float, float> mouse = new Tuple<float, float>((float)e.GetPosition(this.grid_win).X, (float)e.GetPosition(this.grid_win).Y);
       //     Tuple<float, float> lastPoint = dep;
       //     foreach (Line item in list)
       //         this.grid_win.Children.Remove(item);
       ////     this.grid_win.Children.Clear();
       //     Line ll = new Line();
       //     ll.IsEnabled = true;
       //     ll.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 0, 0));
       //     ll.StrokeThickness = 2;
       //     ll.X1 = dep.Item1;
       //     ll.Y1 = dep.Item2;
       //     ll.X2 = anchor.Item1;
       //     ll.Y2 = anchor.Item2;
       //     this.grid_win.Children.Add(ll);
       //     Line ll2 = new Line();
       //     ll2.IsEnabled = true;
       //     ll2.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 0, 0));
       //     ll2.StrokeThickness = 2;
       //     ll2.X1 = anchor.Item1;
       //     ll2.Y1 = anchor.Item2;
       //     ll2.X2 = mouse.Item1;
       //     ll2.Y2 = mouse.Item2;
       //     this.grid_win.Children.Add(ll2);
       //     for (float p = 0; p <= 1; p += 0.05F)
       //     {
       //         Tuple<float, float> pointA = CalcPoint(dep, anchor, p);
       //         Tuple<float, float> pointB = CalcPoint(anchor, mouse, p);
       //         Tuple<float, float> pointFinal = CalcPoint(pointA, pointB, p);
       //         Line l = new Line();
       //         l.IsEnabled = true;
       //         l.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 255, 255));
       //         l.StrokeThickness = 2;
       //         l.X1 = lastPoint.Item1;
       //         l.Y1 = lastPoint.Item2;
       //         l.X2 = pointFinal.Item1;
       //         l.Y2 = pointFinal.Item2;
       //         this.grid_win.Children.Add(l);
       //         lastPoint = pointFinal;
       //         list.Add(l);
       //     }
       //     Line lFinal = new Line();
       //     lFinal.IsEnabled = true;
       //     lFinal.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 255, 255));
       //     lFinal.StrokeThickness = 2;
       //     lFinal.X1 = lastPoint.Item1;
       //     lFinal.Y1 = lastPoint.Item2;
       //     lFinal.X2 = mouse.Item1;
       //     lFinal.Y2 = mouse.Item2;
       //     this.grid_win.Children.Add(lFinal);
       //     list.Add(lFinal);
       //     list.Add(ll);
       //     list.Add(ll2);
        }

        Tuple<float, float> CalcPoint(Tuple<float, float> a, Tuple<float, float> b, float percent)
        {
            Tuple<float, float> p = new Tuple<float, float>(a.Item1 + (b.Item1 - a.Item1) * percent, a.Item2 + (b.Item2 - a.Item2) * percent);
            return p;
        }

        private void onClickCreate(object sender, RoutedEventArgs e)
        {

            Point pt = this.MenuItemNode.TranslatePoint(new Point(0, 0), this.grid_win);
           // MessageBox.Show("X ==> " + pt.X.ToString());
          //  MessageBox.Show("Y ==> " + pt.Y.ToString());
            this.grid_win.Children.Add(new WPF.testNode(this.grid_win, pt.X, pt.Y));
        }

        public static void nodeIsSelected(Grid g)
        {
            isSelected = g;

            //isSelected.Margin = new Thickness(0);
        }

        private void clickMoveNode(object sender, MouseButtonEventArgs e)
        {
            if (isSelected != null)
            {
               // Point pt = e.GetPosition(this.grid_win);
                //isSelected.Margin = new Thickness(pt.X, pt.Y, 0, 0);
                isSelected = null;
            }
        }

    }
}
