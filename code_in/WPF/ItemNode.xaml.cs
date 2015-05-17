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

namespace code_in.WPF
{
    /// <summary>
    /// Interaction logic for ItemNode.xaml
    /// </summary>
    public partial class ItemNode : UserControl
    {
        public ItemNode()
        {
            InitializeComponent();
        }
        private int _orientation;
        public int Orientation
        {
            get
            {
                return _orientation;
            }
            set
            {
                _orientation = value;
                if (_orientation == 1)
                {
                    this.Circle.SetValue(Grid.ColumnProperty, 1);
                    this.Text.SetValue(Grid.ColumnProperty, 0);
                }
                else
                {
                    this.Circle.SetValue(Grid.ColumnProperty, 0);
                    this.Text.SetValue(Grid.ColumnProperty, 1);
                }
            }
        }

        public static ItemNode last = null;
        public static WPF.Bezier bezier = null;

        private void Circle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point ptDepart = this.Circle.TranslatePoint(new Point(15, 15), UserControl1._grid_win);


            if (bezier == null)
            {
                bezier = new WPF.Bezier(UserControl1._grid_win, ptDepart, e.GetPosition(UserControl1._grid_win));
                ItemNode.last = this;
            }
            else
            {
                bezier.setPositions(new Point(-1, 0), ptDepart);
                UserControl1.Links[last] = bezier;
                UserControl1.Links[this] = bezier;
                bezier = null;
            }
        }
    }
}
