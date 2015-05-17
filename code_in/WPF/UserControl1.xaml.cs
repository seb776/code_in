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

namespace code_in
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [GuidAttribute("9ED54F84-A89D-4fcd-A854-44251E925F09")]
    public partial class UserControl1 : UserControl, stdole.IDispatch
    {
        public static Dictionary<WPF.ItemNode, WPF.Bezier> Links;

        List<Line> list = new List<Line>();
        private static WPF.testNode isSelected = null;
        public static Grid _grid_win;
       // public static Boolean justClicked = false;
      //  public WPF.testNode _node;

        public UserControl1()
        {
            Links = new Dictionary<WPF.ItemNode, WPF.Bezier>();
            InitializeComponent();
           // _node = new WPF.testNode(this.grid_win);
            //this.MouseMove += UserControl1_MouseMove;
            //this.grid_win.Children.Add(new Line())
            _grid_win = this.grid_win;
            this.grid_win.Focus();
        }

        void UserControl1_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = e.GetPosition(this.grid_win);

            if (isSelected != null)
            {
                isSelected.Margin = new Thickness(pt.X, pt.Y, 0, 0);
                foreach (var input in isSelected.spLeft.Children)
                {
                     WPF.ItemNode _in = (input as WPF.ItemNode);
                    if (UserControl1.Links.ContainsKey(_in))
                    {
                        Point ptEnd = _in.Circle.TranslatePoint(new Point(15, 15), UserControl1._grid_win);
                        UserControl1.Links[_in].setPositions(new Point(-1, 0), ptEnd);
                    }
                }
                foreach (var output in isSelected.spRight.Children)
                {
                    WPF.ItemNode _out = (output as WPF.ItemNode);
                    if (UserControl1.Links.ContainsKey(_out))
                    {
                        Point ptStart = _out.Circle.TranslatePoint(new Point(15, 15), UserControl1._grid_win);
                        UserControl1.Links[_out].setPositions(ptStart, new Point(-1, 0));
                    }
                }
            }
            pt.X = pt.X - 1;
            if (WPF.ItemNode.bezier != null)
                WPF.ItemNode.bezier.setPositions(new Point(-1, 0), pt);
        }

        private void onClickCreate(object sender, RoutedEventArgs e)
        {

            Point pt = this.MenuItemNode.TranslatePoint(new Point(0, 0), this.grid_win);
           // MessageBox.Show("X ==> " + pt.X.ToString());
          //  MessageBox.Show("Y ==> " + pt.Y.ToString());
            this.grid_win.Children.Add(new WPF.testNode(this.grid_win, pt.X, pt.Y));
        }

        public static void nodeIsSelected(WPF.testNode g)
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

        private void MenuItem_AddImports(object sender, RoutedEventArgs e)
        {
            Point pt = this.MenuItemNode.TranslatePoint(new Point(0, 0), this.grid_win);
            string imp = WPF.PromptDialog.Prompt("Please type the name of the Import", "Imports");
            if (imp.Length != 0)
            {
                Connect.code_inInstance._parser.vbcst._imports.Add(new TestAntlr.CST.Imports(imp));
                WPF.testNode n = new WPF.testNode(this.grid_win, pt.X, pt.Y);
                n.Title.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.AliceBlue);
                n.Title.Text = "Imports";
                n.spLeft.Children.Clear();
                n.spRight.Children.Clear();
                WPF.ItemNode itemImport = new WPF.ItemNode();

                itemImport.Text.Text = imp;
                n.spLeft.Children.Add(itemImport);
                this.grid_win.Children.Add(n);
            }
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog fileDialog = new Microsoft.Win32.SaveFileDialog();

            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(fileDialog.FileName);
                StringBuilder builder = new StringBuilder();

                Connect.code_inInstance._parser.vbcst.GenerateCode(builder);
                file.Write(builder.ToString());

                file.Close();
            }
        }
    }
}
