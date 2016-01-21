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

namespace code_in.Views.MainView.Nodes
{
    // This is used for managing these actions: Resize, Move
    public static class TransformingNode
    {
        public static Object TransformingObject = null; // Used to move, resize or link nodes
        public enum TransformationMode
        {
            NONE = 0,
            RESIZE = 1,
            MOVE = 2,
            LINE = 3
        }
        public static TransformationMode Transformation;

        /*public static Point begin;
        public static Line lineInput;
        public static Line lineOutput;
        public static int orientationStart = 0; // 0 : none, 1 : input, 2 : output
        */
    }
    /// <summary>
    /// The visual representation of an AST node.
    /// This class contains all the features that may be used by all the other kind of nodes.
    /// </summary>
    public partial class BaseNode : UserControl, IVisualNodeContainer
    {
        protected System.Windows.Media.Brush _currentResource;
        public MainView MainView;
        public BaseNode _parent;

        public Line lineInput;
        public Line lineOutput;

        T IVisualNodeContainer.AddNode<T>()
        {
            T node = Activator.CreateInstance(typeof(T), (MainView == null ? code_in.Resources.SharedDictionaryManager.MainResourceDictionary : MainView.ResourceDict)) as T;
            node.SetMainView(this.MainView);
            this._addNode(node);
            return node;
        }

        protected virtual void _addNode(BaseNode n)
        {
            System.Diagnostics.Debug.Assert(MainView != null && n != null && n != this);
            n.SetMainView(MainView);
            n.SetParent(this);
            this.OrderedContent.Children.Add(n);
        }

        public void SetParent(BaseNode parent)
        {
            _parent = parent;
        }
        public ResourceDictionary ResourceDict = null;
        public BaseNode(ResourceDictionary resourceDict)
        {
            this.ResourceDict = resourceDict;
            this.Resources.MergedDictionaries.Add(this.ResourceDict);
            InitializeComponent();
            { // We set all the features to true
                int maxEFeaturesVal = (int)Enum.GetValues(typeof(EFeatures)).Cast<EFeatures>().Last();
                this._features = new bool[maxEFeaturesVal + 1];
                for (int i = 0; i <= maxEFeaturesVal; i++)
                    this._features[i] = true;
            }

            this.MainView = null;
            this._parent = null;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            this.SetColorResource("BaseNodeColor");
        }
        public BaseNode() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
        }

        /// <summary>
        /// This function is used to attach a node to a MainView for the theme to be handled properly.
        /// </summary>
        /// <param name="mv">The MainView</param>
        public void SetMainView(MainView mv)
        {
            MainView = mv;
        }

        /// <summary>
        /// This function sets the width and height of the node.
        /// </summary>
        /// <param name="width">The width of the node Excluding Modifiers.</param>
        /// <param name="height">The height of the node Excluding Qualifiers.</param>
        /// <returns>Returns false if the size is invalid.</returns>
        public bool SetSize(Int32 width, Int32 height)
        {
            if (width < 1 || height < 1) // (z0rg)TODO: Take care of the min width and height (cannot be as small as it hides informations)
                return false;
            this.Width = width;
            this.Height = height;
            return true;
        }

        public BaseNode(MainView view)
            : this()
        {
            MainView = view;
        }

        private void Polygon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TransformingNode.TransformingObject = this;
            TransformingNode.Transformation = TransformingNode.TransformationMode.RESIZE;
            e.Handled = true; // To avoid bubbling http://www.codeproject.com/Articles/464926/To-bubble-or-tunnel-basic-WPF-events
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TransformingNode.TransformingObject = this;
            TransformingNode.Transformation = TransformingNode.TransformationMode.MOVE;
            e.Handled = true; // To avoid bubbling http://www.codeproject.com/Articles/464926/To-bubble-or-tunnel-basic-WPF-events
        }

        private void Grid_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            ((Panel)this.Parent).Children.Remove(this);
            MainView.MainGrid.Children.Remove(lineInput);
            MainView.MainGrid.Children.Remove(lineOutput);
            e.Handled = true; // To avoid bubbling http://www.codeproject.com/Articles/464926/To-bubble-or-tunnel-basic-WPF-events
        }

        public void CreateLink(Nodes.Items.NodeAnchor n)
        {
            TransformingNode.TransformingObject = n;
            TransformingNode.Transformation = TransformingNode.TransformationMode.LINE;
            MainView.MainGrid.Children.Remove(n.IOLine);
            n.IOLine = new Line();
            n.IOLine.Stroke = System.Windows.Media.Brushes.Red;
            n.IOLine.StrokeThickness = 5;
            if (n._parentItem.Orientation == Nodes.Items.NodeItem.EOrientation.LEFT)
                lineInput = n.IOLine;
            else
                lineOutput = n.IOLine;
            Canvas.SetZIndex(n.IOLine, -1);

            MainView.MainGrid.Children.Add(n.IOLine);
        }
        public enum EFeatures
        {
            MOVABLE = 0, // The node is Movable
            RESIZABLE = 1, // The node is Resizable
            CONTAINSMODIFIERS = 2, // The node will be able to contains modifiers upon the node
            ISFLOWNODE = 3, // The node will have input and output flow
            ISREMOVABLE = 4, // The node is Removable
            EXPENDABLES = 5 // (The node can display it's content) "Chuck Norris was bitten by a king cobra, and after five days of excruciating pain... the cobra died."
        }
        public void DisableFeatures(params EFeatures[] features)
        {
            foreach (EFeatures f in features)
            {
                switch (f)
                { // Some features need special operations to be disabled
                    case EFeatures.RESIZABLE:
                        {
                            this.ResizeControl.Visibility = System.Windows.Visibility.Hidden;
                            this.ResizeControl.IsEnabled = false;
                            break;
                        }
                    case EFeatures.CONTAINSMODIFIERS:
                        {
                            break;
                        }
                    case EFeatures.ISFLOWNODE:
                        {
                            break;
                        }
                    case EFeatures.EXPENDABLES:
                        {
                            this.ExpandButton.Visibility = this.CollapseButton.Visibility = System.Windows.Visibility.Hidden;
                            this.ExpandButton.IsEnabled = this.CollapseButton.IsEnabled = false;
                            break;
                        }
                }
                _features[(int)f] = false;
            }
        }

        public void EnableFeatures(params EFeatures[] features)
        {
            foreach (EFeatures f in features)
            {
                switch (f)
                { // Some features need special operations to be enabled
                    case EFeatures.RESIZABLE:
                        {
                            this.ResizeControl.Visibility = System.Windows.Visibility.Visible;
                            this.ResizeControl.IsEnabled = true;
                            break;
                        }
                    case EFeatures.CONTAINSMODIFIERS:
                        {
                            break;
                        }
                    case EFeatures.ISFLOWNODE:
                        {
                            Items.FlowNodeItem input = new Items.FlowNodeItem();
                            input.ItemName.Text = "Input flow"; // TODO make a function
                            this.AddInput(input);

                            Items.FlowNodeItem output = new Items.FlowNodeItem();
                            output.ItemName.Text = "Output flow"; // TODO make a function
                            this.AddOutput(output);
                            break;
                        }
                    case EFeatures.EXPENDABLES:
                        {
                            this.ExpandButton.IsEnabled = true;
                            this.ExpandButton.Visibility = System.Windows.Visibility.Visible;
                            break;
                        }
                }
                _features[(int)f] = true;
            }
        } // EndFunc

        public void AddNodeModifiers(String type)
        {
            System.Diagnostics.Debug.Assert(this._features[(int)EFeatures.CONTAINSMODIFIERS], "The node cannot contain modifiers.");
            Label lblType = new Label();
            lblType.Content = type;
            lblType.Foreground = this._currentResource;
            this.NodeModifiers.Children.Add(lblType);
        }

        public void AddInput(Items.NodeItem item)
        {
            item.Orientation = Items.NodeItem.EOrientation.LEFT;
            if (item.GetType().IsSubclassOf(typeof(Items.IOItem)))
                item.Margin = new Thickness(-13, 0, 0, 0); // TODO: apply resources.AnchorOffsetLeft
            item.ParentNode = this;

            this.Inputs.Children.Add(item);
        }

        public void RemoveInput(int idx)
        {
            System.Diagnostics.Debug.Assert(idx >= 0 && idx < this.Outputs.Children.Count, "Trying to remove an input that does not exist " + idx.ToString() + ".");
            this.Inputs.Children.RemoveAt(idx);
        }

        public void AddOutput(Items.NodeItem item)
        {
            item.Orientation = Items.NodeItem.EOrientation.RIGHT;
            if (item.GetType().IsSubclassOf(typeof(Items.IOItem)))
                item.Margin = new Thickness(0, 0, -13, 0); // TODO: apply resources.AnchorOffsetLeft
            item.ParentNode = this;

            this.Outputs.Children.Add(item);
        }

        public void RemoveOutput(int idx)
        {
            System.Diagnostics.Debug.Assert(idx >= 0 && idx < this.Outputs.Children.Count, "Trying to remove an output that does not exist " + idx.ToString() + ".");
            this.Outputs.Children.RemoveAt(idx);
        }

        /// <summary>
        /// This function is used to set the resource that will be used as the main color of the node. Usefull to distinguish different nodes.
        /// This function have to be called before setting any value as the resource is used for each modification.
        /// </summary>
        /// <param name="resourceName">The name of the resource that have to be used</param>
        public void SetColorResource(String resourceName)
        {
            System.Windows.Media.Brush resource = (System.Windows.Media.Brush)this.Resources[resourceName];
            System.Diagnostics.Debug.Assert(resource != null); // Just to check if the resource exists

            _currentResource = resource;
            this.NodeBorder.SetResourceReference(BorderBrushProperty, resourceName);
            this.NodeHeader.SetResourceReference(BackgroundProperty, resourceName);
            this.ResizeControl.SetResourceReference(System.Windows.Shapes.Shape.FillProperty, resourceName);
            ColorResource = resourceName;
        }
        // Gets the ColorResource String of the currentNode
        public String ColorResource;

        public void SetNodeType(String type)
        {
            this.NodeType.Content = type;
        }

        public void SetNodeName(String name)
        {
            this.NodeName.Text = name;
        }


        private bool[] _features;

    } // Class BaseNode
} // Namespace
