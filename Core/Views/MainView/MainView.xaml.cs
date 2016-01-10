﻿using System;
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
    public partial class MainView : UserControl, stdole.IDispatch, IVisualNodeContainer
    {
        public Nodes.Items.NodeAnchor enterInput = null;
        public Nodes.Items.NodeAnchor enterOutput = null;

        private ViewModels.code_inMgr _code_inMgr;

        public void OpenFile(String filePath)
        {
            _code_inMgr.LoadFile(filePath);

        }

        void IVisualNodeContainer.AddNode(Nodes.BaseNode n)
        {
            System.Diagnostics.Debug.Assert(n != null);
            n.SetMainView(this);
            this.MainGrid.Children.Add(n);
        }

        public MainView()
        {
            this.Resources.MergedDictionaries.Add(code_in.Resources.SharedDictionaryManager.SharedDictionary);
            InitializeComponent();

            _code_inMgr = new ViewModels.code_inMgr(this);

            this.MouseWheel += MainView_MouseWheel;
            this.KeyDown += MainView_KeyDown;
            this.MouseUp += MainView_MouseUp;
            themeSelect = true;
            themeA = new Models.Theme.DefaultThemeData();
            themeB = new Models.Theme.ThemeYaya();
        }
        bool themeSelect;
        Models.Theme.DefaultThemeData themeA;
        Models.Theme.ThemeYaya themeB;

        public Nodes.Items.NodeAnchor destAnchor; // when drawin a line, stock the other destination on the link

        void MainView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Nodes.TransformingNode.Transformation == Nodes.TransformingNode.TransformationMode.LINE)
            {
                Nodes.Items.NodeAnchor n = ((Nodes.Items.NodeAnchor)Nodes.TransformingNode.TransformingObject);
                // delete link if when mouse up is not from output to an input
                if (n._parentItem.Orientation == Nodes.Items.NodeItem.EOrientation.RIGHT)
                {
                    if (enterInput == null)
                        MainGrid.Children.Remove(n.IOLine);
                    else
                    {
                        enterInput.IOLine = n.IOLine;
                        enterInput._parentItem.ParentNode.lineInput = n.IOLine;
                    }
                }
                // delete link if when mouse up is not from input to an output
                else if (n._parentItem.Orientation == Nodes.Items.NodeItem.EOrientation.LEFT)
                {
                    if (enterOutput == null)
                        MainGrid.Children.Remove(n.IOLine);
                    else
                    {
                        double tmpX = n.IOLine.X1;
                        double tmpY = n.IOLine.Y1;
                        n.IOLine.X1 = n.IOLine.X2;
                        n.IOLine.Y1 = n.IOLine.Y2;
                        n.IOLine.X2 = tmpX;
                        n.IOLine.Y2 = tmpY;
                        enterOutput.IOLine = n.IOLine;
                        enterOutput._parentItem.ParentNode.lineOutput = n.IOLine;
                    }
                }
            }

            Nodes.TransformingNode.TransformingObject = null;
            Nodes.TransformingNode.Transformation = Nodes.TransformingNode.TransformationMode.NONE;
        }

        void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            int step = 2;
            Rect tmp = (Rect)code_in.Resources.SharedDictionaryManager.SharedDictionary["RectDims"];
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
            if (e.Key == Key.T)
            {

                this._code_inMgr._themeMgr.setTheme((themeSelect ? (Models.Theme.IThemeData)themeA : (Models.Theme.IThemeData)themeB));
                themeSelect = !themeSelect;
            }
            if (e.Key == Key.S)
            {

                System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    _code_inMgr._codeMgr.SaveFile(dialog.FileName);
            }
            code_in.Resources.SharedDictionaryManager.SharedDictionary["RectDims"] = tmp;
            ((DrawingBrush)code_in.Resources.SharedDictionaryManager.SharedDictionary["GridTile"]).Viewport = tmp;
        }

        void MainView_MouseWheel(object sender, MouseWheelEventArgs e)
        {
        }

        Point lastPosition = new Point(0, 0);

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine(enterOutput);
            bool gridMagnet = true;
            Vector diff;
            if ((lastPosition.X + lastPosition.Y) < 0.01)
                diff = new Vector(0, 0);
            else
            {
                diff = lastPosition - e.GetPosition(this.MainGrid);
            }
            lastPosition = e.GetPosition(this.MainGrid);

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

                    Nodes.TransformingNode.TransformingObject.GetType().GetProperty("Margin").SetValue(Nodes.TransformingNode.TransformingObject, newMargin);


                    // move the link if exist
                    
                    Line lineOutput = ((Nodes.BaseNode)Nodes.TransformingNode.TransformingObject).lineOutput;
                    Line lineIntput = ((Nodes.BaseNode)Nodes.TransformingNode.TransformingObject).lineInput;

                //    Nodes.BaseNode test = ((Nodes.BaseNode)Nodes.TransformingNode.TransformingObject);

                    if (lineOutput != null)
                    {
                        lineOutput.X1 -= diff.X;
                        lineOutput.Y1 -= diff.Y;
                    }
                    if (lineIntput != null)
                    {
                        lineIntput.X2 -= diff.X;
                        lineIntput.Y2 -= diff.Y;
                    }
                    
                        
                }
                else if (Nodes.TransformingNode.Transformation == Nodes.TransformingNode.TransformationMode.LINE)
                {
                    Nodes.Items.NodeAnchor n = ((Nodes.Items.NodeAnchor)Nodes.TransformingNode.TransformingObject);

                    n.IOLine.X1 = n.lineBegin.X;
                    n.IOLine.Y1 = n.lineBegin.Y;
                    n.IOLine.X2 = e.GetPosition(MainGrid).X;
                    n.IOLine.Y2 = e.GetPosition(MainGrid).Y;

                    /*      if (Nodes.TransformingNode.lineOutput == null)
                          {
                              Nodes.TransformingNode.lineOutput = new Line();
                              Canvas.SetZIndex(Nodes.TransformingNode.lineOutput, -1000);
                              MainGrid.Children.Add(Nodes.TransformingNode.lineOutput);
                              Nodes.TransformingNode.lineOutput.Stroke = System.Windows.Media.Brushes.Red;
                              Nodes.TransformingNode.lineOutput.StrokeThickness = 5;
                          }

                          Nodes.TransformingNode.lineOutput.X1 = Nodes.TransformingNode.begin.X;
                          Nodes.TransformingNode.lineOutput.Y1 = Nodes.TransformingNode.begin.Y;
                          Nodes.TransformingNode.lineOutput.X2 = e.GetPosition(MainGrid).X;
                          Nodes.TransformingNode.lineOutput.Y2 = e.GetPosition(MainGrid).Y;*/

                }
            }
        }

        private void MainGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // This automatically updates the list of accessible nodes
            // Need to be optimized (compute only the first time, as it uses reflection)
            // Make the click on the Item Creates the right instance
            List<Type> listOfBs = new List<Type>();
            foreach (var t in typeof(Nodes.BaseNode).Assembly.GetTypes())
            {

                if (t.IsSubclassOf(typeof(Nodes.BaseNode)))
                {
                    listOfBs.Add(t);
                }
            }
            var cm = new ContextMenu();
            foreach (var t in listOfBs)
            {
                var m1 = new MenuItem();
                m1.Header = t.Name;
                m1.Click += m1_Click;
                cm.Items.Add(m1);
            }
            cm.Margin = new Thickness(e.GetPosition(this).X, e.GetPosition(this).Y, 0, 0);
            cm.IsOpen = true;

        }

        void m1_Click(object sender, RoutedEventArgs e)
        {

            var node = new Nodes.FuncDeclNode(this, "test");
            //            node.Margin = new Thickness(e.GetPosition(this.MainGrid).X, e.GetPosition(this.MainGrid).Y, 0, 0);
            node.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            node.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.MainGrid.Children.Add(node);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (((int)(e.NewValue * 10.0) % 2) == 0)
            {
                this.ZoomPanel.Width = this.MainGrid.Width * e.NewValue;
                this.ZoomPanel.Height = this.MainGrid.Height * e.NewValue;
            }
        }
    }
}
