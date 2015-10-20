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
        public BaseNode()
        {
            InitializeComponent();
            { // We set all the features to true
                int maxEFeaturesVal = (int)Enum.GetValues(typeof(EFeatures)).Cast<EFeatures>().Last();
                this._features = new bool[maxEFeaturesVal + 1];
                for (int i = 0; i <= maxEFeaturesVal; i++)
                    this._features[i] = true;
            }
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

                            break;
                        }
                }
                _features[(int)f] = true;
            }
        } // EndFunc

        public void AddNodeModifiers(String type)
        {
            if (this._features[(int)EFeatures.CONTAINSMODIFIERS])
            {
                Label lblType = new Label();
                lblType.Content = type;
                lblType.Foreground = this._currentResource; // TODO does not work yet
                this.NodeModifiers.Children.Add(lblType);
            }
            else
            {
                throw new Exception("Trying to add a modifier on a node that does not allow it (" + type + ").");
            }
        }

        public void AddInput(IOItem item)
        {
            item.Orientation = 0;
            this.Inputs.Children.Add(item);
        }

        public void RemoveInput(int idx)
        {
            System.Diagnostics.Debug.Assert(idx >= 0);
            this.Inputs.Children.RemoveAt(idx);
        }

        public void AddOutput(IOItem item)
        {
            item.Orientation = 1;
            this.Outputs.Children.Add(item);
        }

        public void RemoveOutput(int idx)
        {
            System.Diagnostics.Debug.Assert(idx >= 0);
            this.Outputs.Children.RemoveAt(idx);
        }

        /// <summary>
        /// This function is used to set the resource that will be used by the node. Usefull to distinguish different nodes.
        /// </summary>
        /// <param name="resourceName">The name of the resource that have to be used</param>
        public void SetColorResource(String resourceName)
        {
            System.Windows.Media.Brush resource = (System.Windows.Media.Brush)this.Resources[resourceName];
            System.Diagnostics.Debug.Assert(resource != null);

            _currentResource = resource;
            this.NodeBorder.BorderBrush = resource;
            this.NodeHeader.Background = resource;
            this.ResizeControl.Fill = resource;
        }

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
