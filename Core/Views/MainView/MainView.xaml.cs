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
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Items.Assets;

namespace code_in.Views.MainView
{

    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public partial class MainView : UserControl, stdole.IDispatch, ICodeInVisual
    {
        private NodalView.NodalView _nodalView = null;
        public NodeAnchor enterInput = null;
        public NodeAnchor enterOutput = null;
        public void SetDynamicResources(String keyPrefix)
        {

        }
        private Point _newNodePos;

        public void OpenFile(String filePath)
        {
            this._nodalView.OpenFile(filePath);
        }

        private ResourceDictionary _ThemeResourceDictionary = null;
        public ResourceDictionary GetThemeResourceDictionary() { return _ThemeResourceDictionary; }
        public SearchBar SearchBar = null;

        public MainView(ResourceDictionary resourceDict)
        {
            this._ThemeResourceDictionary = resourceDict;
            this.Resources.MergedDictionaries.Add(this._ThemeResourceDictionary);
            InitializeComponent();
            this.SearchBar = new SearchBar(this.GetThemeResourceDictionary());
            this.SearchBar.SetValue(Grid.HorizontalAlignmentProperty, HorizontalAlignment.Left);
            this.SearchBar.SetValue(WidthProperty, Double.NaN); // Width auto
            this.WinGrid.Children.Add(this.SearchBar);
            this._nodalView = new NodalView.NodalView(this._ThemeResourceDictionary);
            this.ZoomPanel.Child = this._nodalView;

            //this.MouseWheel += MainView_MouseWheel;
            //this.KeyDown += MainView_KeyDown;
            //this.MouseUp += MainView_MouseUp;          
        }
        public MainView() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
        }

        void MainView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //// if the mode is drawing a line
            //if (Nodes.TransformingNode.Transformation == Nodes.TransformingNode.TransformationMode.LINE)
            //{
            //    Nodes.Items.NodeAnchor n = ((Nodes.Items.NodeAnchor)Nodes.TransformingNode.TransformingObject);
            //    if (n._parentItem.Orientation == Nodes.Items.NodeItem.EOrientation.RIGHT)
            //    {
            //        // delete link if when mouse up is not from output to an input
            //        if (enterInput == null)
            //            MainGrid.Children.Remove(n.IOLine);
            //        else
            //        {
            //            enterInput.IOLine = n.IOLine;
            //            enterInput._parentItem.ParentNode.lineInput = n.IOLine;
            //        }
            //    }
            //    else if (n._parentItem.Orientation == Nodes.Items.NodeItem.EOrientation.LEFT)
            //    {
            //        // delete link if when mouse up is not from input to an output
            //        if (enterOutput == null)
            //            MainGrid.Children.Remove(n.IOLine);
            //        else
            //        {
            //            double tmpX = n.IOLine.X1;
            //            double tmpY = n.IOLine.Y1;
            //            n.IOLine.X1 = n.IOLine.X2;
            //            n.IOLine.Y1 = n.IOLine.Y2;
            //            n.IOLine.X2 = tmpX;
            //            n.IOLine.Y2 = tmpY;
            //            enterOutput.IOLine = n.IOLine;
            //            enterOutput._parentItem.ParentNode.lineOutput = n.IOLine;
            //        }
            //    }
            //}

            //// reset mode 
            //Nodes.TransformingNode.TransformingObject = null;
            //Nodes.TransformingNode.Transformation = Nodes.TransformingNode.TransformationMode.NONE;
        }

        void MainView_KeyDown(object sender, KeyEventArgs e)
        {
        }

        void MainView_MouseWheel(object sender, MouseWheelEventArgs e)
        {
        }

        Point lastPosition = new Point(0, 0);

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
        //  //  System.Diagnostics.Trace.WriteLine(enterOutput);
        //    bool gridMagnet = true;
        //    Vector diff;
        //    if ((lastPosition.X + lastPosition.Y) < 0.01)
        //        diff = new Vector(0, 0);
        //    else
        //    {
        //        diff = lastPosition - e.GetPosition(this.MainGrid);
        //    }
        //    lastPosition = e.GetPosition(this.MainGrid);

        //    if (Nodes.TransformingNode.TransformingObject != null)
        //    {

        //        //((ScrollViewer)((Grid)sender).Parent).ScrollToHorizontalOffset(((ScrollViewer)((Grid)sender).Parent).HorizontalOffset + (diff.X < 0 ? -.1 : .1));
        //        if (Nodes.TransformingNode.Transformation == Nodes.TransformingNode.TransformationMode.RESIZE)
        //        {
        //            double sizeX = (double)Nodes.TransformingNode.TransformingObject.GetType().GetProperty("ActualWidth").GetValue(Nodes.TransformingNode.TransformingObject);
        //            double sizeY = (double)Nodes.TransformingNode.TransformingObject.GetType().GetProperty("ActualHeight").GetValue(Nodes.TransformingNode.TransformingObject);
        //            double nSizeX = sizeX - diff.X;
        //            double nSizeY = sizeY - diff.Y;

        //            //MessageBox.Show((sizeX + diff.X).ToString());
        //            Nodes.TransformingNode.TransformingObject.GetType().GetProperty("Width").SetValue(Nodes.TransformingNode.TransformingObject, nSizeX);
        //            Nodes.TransformingNode.TransformingObject.GetType().GetProperty("Height").SetValue(Nodes.TransformingNode.TransformingObject, nSizeY);
        //            //((Nodes.TransformingNode.TransformingObject.GetType().get)Nodes.TransformingNode.TransformingObject)
        //        }
        //        else if (Nodes.TransformingNode.Transformation == Nodes.TransformingNode.TransformationMode.MOVE)
        //        {
        //            Thickness margin = (Thickness)Nodes.TransformingNode.TransformingObject.GetType().GetProperty("Margin").GetValue(Nodes.TransformingNode.TransformingObject);
        //            double marginLeft = margin.Left;
        //            double marginTop = margin.Top;
        //            Thickness newMargin = margin;
        //            newMargin.Left -= diff.X;
        //            newMargin.Top -= diff.Y;

        //            Nodes.TransformingNode.TransformingObject.GetType().GetProperty("Margin").SetValue(Nodes.TransformingNode.TransformingObject, newMargin);


        //            // move the link if exist
                    
        //            Line lineOutput = ((Nodes.BaseNode)Nodes.TransformingNode.TransformingObject).lineOutput;
        //            Line lineIntput = ((Nodes.BaseNode)Nodes.TransformingNode.TransformingObject).lineInput;

        //        //    Nodes.BaseNode test = ((Nodes.BaseNode)Nodes.TransformingNode.TransformingObject);

        //            if (lineOutput != null)
        //            {
        //                lineOutput.X1 -= diff.X;
        //                lineOutput.Y1 -= diff.Y;
        //        }
        //            if (lineIntput != null)
        //            {
        //                lineIntput.X2 -= diff.X;
        //                lineIntput.Y2 -= diff.Y;
        //    }
                    
                        
        //}
        //        else if (Nodes.TransformingNode.Transformation == Nodes.TransformingNode.TransformationMode.LINE)
        //        {
        //            Nodes.Items.NodeAnchor n = ((Nodes.Items.NodeAnchor)Nodes.TransformingNode.TransformingObject);

        //            n.pthFigure.StartPoint = n.lineBegin;
        //            n.IOBezier.Point1 = new Point(n.lineBegin.X + 100 , n.lineBegin.Y);
        //            n.IOBezier.Point2 = new Point(n.lineBegin.X + 0, n.lineBegin.Y + 100);
        //            n.IOBezier.Point3 = e.GetPosition(MainGrid);
                    
        //      /*      n.IOLine.X1 = n.lineBegin.X;
        //            n.IOLine.Y1 = n.lineBegin.Y;
        //            n.IOLine.X2 = e.GetPosition(MainGrid).X;
        //            n.IOLine.Y2 = e.GetPosition(MainGrid).Y;
        //            */

        //        }
        //    }
        }

        private void MainGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // This automatically updates the list of accessible nodes
            // Need to be optimized (compute only the first time, as it uses reflection)
            // Make the click on the Item Creates the right instance
            //List<Type> listOfBs = new List<Type>();
            //foreach (var t in typeof(Nodes.BaseNode).Assembly.GetTypes())
            //{

            //    if (t.IsSubclassOf(typeof(Nodes.BaseNode)))
            //    {
            //        listOfBs.Add(t);
            //    }
            //}
            //var cm = new ContextMenu();
            //foreach (var t in listOfBs)
            //{
            //    var m1 = new MenuItem();
            //    m1.Header = t.Name;
            //    m1.Click += m1_Click;
            //    cm.Items.Add(m1);
            //}
            //cm.Margin = new Thickness(e.GetPosition(this).X, e.GetPosition(this).Y, 0, 0);
            //cm.IsOpen = true;
            //// Setting the position of the node if we create one to the place the menu has been opened
            //_newNodePos.X = e.GetPosition(this).X;
            //_newNodePos.Y = e.GetPosition(this).Y;
        }

        void m1_Click(object sender, RoutedEventArgs e)
        {
            //var node = ((IVisualNodeContainer)this).AddNode<Nodes.FuncDeclNode>();
            //node.Margin = new Thickness(_newNodePos.X, _newNodePos.Y, 0, 0);
        }

        private void SliderZoom(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.ZoomPanel != null && this._nodalView != null)
            {
                if (((int)(e.NewValue * 10.0) % 2) == 0)
                {
                    this.ZoomPanel.Width = this._nodalView.MainGrid.Width * e.NewValue;
                    this.ZoomPanel.Height = this._nodalView.MainGrid.Height * e.NewValue;
                }
            }
        }
    }
}
