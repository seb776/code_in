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
        public static Object TransformingObject = null; // Used to move or resize nodes
        public enum TransformationMode
        {
            NONE = 0,
            RESIZE = 1,
            MOVE = 2
        }
        public static TransformationMode Transformation;
    }
    /// <summary>
    /// The visual representation of an AST node.
    /// This class contains all the features that may be used by all the other kind of nodes.
    /// </summary>
    public partial class BaseNode : UserControl
    {
        protected System.Windows.Media.Brush _currentResource;
        public MainView MainView;
        public BaseNode()
        {
            this.Resources.MergedDictionaries.Add(code_in.Resources.SharedDictionaryManager.SharedDictionary);
            InitializeComponent();
            { // We set all the features to true
                int maxEFeaturesVal = (int)Enum.GetValues(typeof(EFeatures)).Cast<EFeatures>().Last();
                this._features = new bool[maxEFeaturesVal + 1];
                for (int i = 0; i <= maxEFeaturesVal; i++)
                    this._features[i] = true;
            }

            MainView = null;
            SetColorResource("BaseNodeColor");
        }

        public BaseNode(MainView view) : this()
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
            e.Handled = true; // To avoid bubbling http://www.codeproject.com/Articles/464926/To-bubble-or-tunnel-basic-WPF-events
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
                            this.AddInput(new FlowItem(this));
                            ((IOItem)this.Inputs.Children[0]).SetItemName("Input flow");
                            this.AddOutput(new FlowItem(this));
                            ((IOItem)this.Outputs.Children[0]).SetItemName("Output flow");
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

        public void AddInput(IOItem item)
        {
            item.Orientation = 0;
            this.Inputs.Children.Add(item);
        }

        public void RemoveInput(int idx)
        {
            System.Diagnostics.Debug.Assert(idx >= 0 && idx < this.Outputs.Children.Count, "Trying to remove an input that does not exist " + idx.ToString() + ".");
            this.Inputs.Children.RemoveAt(idx);
        }

        public void AddOutput(IOItem item)
        {
            item.Orientation = 1;
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
            this.NodeName.Content = name;
        }


        private bool[] _features;
    } // Class BaseNode
} // Namespace
