using code_in.Views.NodalView.Nodes;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
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

namespace code_in.Views.NodalView
{
    /// <summary>
    /// Interaction logic for NodalView.xaml
    /// </summary>
    public partial class NodalView : UserControl, IVisualNodeContainer, ICodeInVisual
    {
        private BaseNode[] _transformingNodes = null;
        public TransformationMode _transformationMode = TransformationMode.NONE;

        public void SetTransformationMode(TransformationMode mode)
        {
            _transformationMode = mode;
        }
        public void ResetTransformationMode()
        {
            _transformationMode = TransformationMode.NONE;
        }
        public void SetTransformingNodes(params BaseNode[] transform)
        {
            _transformingNodes = transform;
        }
        public void ResetTransformingNode()
        {
            _transformingNodes = null;
        }
        private ResourceDictionary _resourceDictionary;
        public ResourceDictionary GetResourceDictionary() { return _resourceDictionary; }
        public NodalView() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {}
        public NodalView(ResourceDictionary resDict)
        {
            this.Resources.MergedDictionaries.Add(resDict);
            InitializeComponent();
            _root = new NamespaceNode();
            _root.SetName("RootNode");
        }

        public NodeAnchor destAnchor; // when drawin a line, stock the other destination on the link

        void MainView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.ResetTransformationMode();
            this.ResetTransformingNode();
            MessageBox.Show("yeh");

            //// if the mode is drawing a line
            //if (Nodes.TransformingNode.Transformation == Nodes.TransformingNode.TransformationMode.LINE)
            //{
            //    Nodes.Items.Base.NodeAnchor n = ((Nodes.Items.Base.NodeAnchor)Nodes.TransformingNode.TransformingObject);
            //    if (n._parentItem.Orientation == Nodes.Items.Base.IOItem.EOrientation.RIGHT)
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
            //    else if (n._parentItem.Orientation == Nodes.Items.Base.IOItem.EOrientation.LEFT)
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
            int step = 2;
            Rect tmp = (Rect)code_in.Resources.SharedDictionaryManager.MainResourceDictionary["RectDims"];
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
            //if (e.Key == Key.T)
            //{

            //    this._code_inMgr._themeMgr.setTheme((themeSelect ? (Models.Theme.ThemeData)themeA : (Models.Theme.ThemeData)themeA));
            //    themeSelect = !themeSelect;
            //}
            //if (false && e.Key == Key.S) // to not show this feature during follow up
            //{

            //    System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();

            //    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //        _code_inMgr._codeMgr.SaveFile(dialog.FileName);
            //}


            //code_in.Resources.SharedDictionaryManager.MainResourceDictionary["RectDims"] = tmp;
            //((DrawingBrush)code_in.Resources.SharedDictionaryManager.MainResourceDictionary["GridTile"]).Viewport = tmp;
        }

        void MainView_MouseWheel(object sender, MouseWheelEventArgs e)
        {
        }

        Point lastPosition = new Point(0, 0);

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            //  System.Diagnostics.Trace.WriteLine(enterOutput);
            bool gridMagnet = true;
            Vector diff;
            if ((lastPosition.X + lastPosition.Y) < 0.01)
                diff = new Vector(0, 0);
            else
            {
                diff = lastPosition - e.GetPosition(this.MainGrid);
            }
            lastPosition = e.GetPosition(this.MainGrid);

            if (_transformingNodes != null && _transformingNodes.Count() > 0)
            {

                //((ScrollViewer)((Grid)sender).Parent).ScrollToHorizontalOffset(((ScrollViewer)((Grid)sender).Parent).HorizontalOffset + (diff.X < 0 ? -.1 : .1));
                if (_transformationMode == TransformationMode.RESIZE)
                {
                    double sizeX = (double)_transformingNodes[0].GetType().GetProperty("ActualWidth").GetValue(_transformingNodes[0]);
                    double sizeY = (double)_transformingNodes[0].GetType().GetProperty("ActualHeight").GetValue(_transformingNodes[0]);
                    double nSizeX = sizeX - diff.X;
                    double nSizeY = sizeY - diff.Y;

                    //MessageBox.Show((sizeX + diff.X).ToString());
                    _transformingNodes[0].GetType().GetProperty("Width").SetValue(_transformingNodes[0], nSizeX);
                    _transformingNodes[0].GetType().GetProperty("Height").SetValue(_transformingNodes[0], nSizeY);
                    //((Nodes.TransformingNode.TransformingObject.GetType().get)Nodes.TransformingNode.TransformingObject)
                }
                else if (_transformationMode == TransformationMode.MOVE || _transformationMode == TransformationMode.MOVEORDERED)
                {
                    Thickness margin = (Thickness)_transformingNodes[0].GetType().GetProperty("Margin").GetValue(_transformingNodes[0]);
                    double marginLeft = margin.Left;
                    double marginTop = margin.Top;
                    Thickness newMargin = margin;
                    if (_transformationMode == TransformationMode.MOVE)
                        newMargin.Left -= diff.X;
                    newMargin.Top -= diff.Y;

                    _transformingNodes[0].GetType().GetProperty("Margin").SetValue(_transformingNodes[0], newMargin);


                    // move the link if exist

                    //Line lineOutput = ((Nodes.BaseNode)Nodes.TransformingNode.TransformingObject).lineOutput;
                    //Line lineIntput = ((Nodes.BaseNode)Nodes.TransformingNode.TransformingObject).lineInput;

                    //    Nodes.BaseNode test = ((Nodes.BaseNode)Nodes.TransformingNode.TransformingObject);

                    //if (lineOutput != null)
                    //{
                    //    lineOutput.X1 -= diff.X;
                    //    lineOutput.Y1 -= diff.Y;
                    //}
                    //if (lineIntput != null)
                    //{
                    //    lineIntput.X2 -= diff.X;
                    //    lineIntput.Y2 -= diff.Y;
                    //}


                }
                //else if (Nodes.TransformingNode.Transformation == Nodes.TransformingNode.TransformationMode.LINE)
                //{
                //    Nodes.Items.NodeAnchor n = ((Nodes.Items.NodeAnchor)Nodes.TransformingNode.TransformingObject);

                //    n.IOLine.X1 = n.lineBegin.X;
                //    n.IOLine.Y1 = n.lineBegin.Y;
                //    n.IOLine.X2 = e.GetPosition(MainGrid).X;
                //    n.IOLine.Y2 = e.GetPosition(MainGrid).Y;


                //}
            }
        }
        public T CreateAndAddNode<T>() where T : UIElement, INode
        {
            T node = (T)Activator.CreateInstance(typeof(T), code_in.Resources.SharedDictionaryManager.MainResourceDictionary);//(MainView == null ? code_in.Resources.SharedDictionaryManager.MainResourceDictionary : MainView.ResourceDict)) as T;

            node.SetNodalView(this);
            node.SetParentNode(null);

            this.AddNode(node);
            return node;
        }
        public void AddNode<T>(T node) where T : UIElement, INode
        {
            //if (node.GetType() != RootNode || this.Root != null) // TODO
            //  throw new Exception();
            this.MainGrid.Children.Add(node as UIElement);
        }
        private void MainGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // This automatically updates the list of accessible nodes
            // Need to be optimized (compute only the first time, as it uses reflection)
            // Make the click on the Item Creates the right instance
            List<Type> listOfBs = new List<Type>();
            foreach (var t in typeof(BaseNode).Assembly.GetTypes())
            {

                if (t.IsSubclassOf(typeof(BaseNode)) && !t.IsAbstract)
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
            // Setting the position of the node if we create one to the place the menu has been opened
            _newNodePos.X = e.GetPosition(this).X;
            _newNodePos.Y = e.GetPosition(this).Y;
        }

        Point _newNodePos;
        NamespaceNode _root = null;
        void m1_Click(object sender, RoutedEventArgs e)
        {
            var node = this._root.CreateAndAddNode<FuncDeclNode>();
            node.Margin = new Thickness(_newNodePos.X, _newNodePos.Y, 0, 0);
        }
    }
}
