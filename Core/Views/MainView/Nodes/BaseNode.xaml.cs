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
    /// Interaction logic for BaseNode.xaml
    /// </summary>
    public partial class BaseNode : UserControl
    {
        enum Kind
        {
            EXEC = 0,
            DECL = 1
        }
        public BaseNode()
        {
            InitializeComponent();
            this.InFlow.Label.SetValue(Label.ContentProperty, "Input flow");
            this.OutFlow.Label.SetValue(Label.ContentProperty, "Output flow");
            this.InFlow.Label.Foreground = new SolidColorBrush(Colors.GreenYellow);
            this.OutFlow.Label.Foreground = new SolidColorBrush(Colors.GreenYellow);
            int maxEFeaturesVal = (int)Enum.GetValues(typeof(EFeatures)).Cast<EFeatures>().Last();
            this._features = new bool[maxEFeaturesVal + 1];
            for (int i = 0; i <= maxEFeaturesVal; i++)
                this._features[i] = true;
        }

        private void Polygon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TransformingNode.TransformingObject = this;
            TransformingNode.Transformation = TransformingNode.TransformationMode.RESIZE;
            e.Handled = true; // To avoid bubbling
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TransformingNode.TransformingObject = this;
            TransformingNode.Transformation = TransformingNode.TransformationMode.MOVE;
        }

        private void Grid_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            ((Panel)this.Parent).Children.Remove(this);
            e.Handled = true; // To avoid bubbling
        }

        public enum EFeatures
        {
            MOVABLE = 0, // The node is Movable
            RESIZABLE = 1, // The node is Resizable
            CONTAINSMODIFIERS = 2, // The node will be able to contains modifiers upon the node
            ISFLOWNODE = 3, // The node will have input and output flow
            ISREMOVABLE=4, // The node is Removable
            EXPENDABLES = 5 // (The node can display it's content) "Chuck Norris was bitten by a king cobra, and after five days of excruciating pain... the cobra died."
        }
        void DisableFeatures(params EFeatures[] features)
        {
            foreach (EFeatures f in features)
            {
                switch (f)
                {
                    case EFeatures.MOVABLE:
                        {
                            break;
                        }
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
                    default:
                        {
                            throw new Exception("BaseNode: Trying to disable a feature that does not exist.");
                        }
                }
                _features[(int)f] = false;
            }
        }

        void EnableFeatures(params EFeatures[] features)
        {
            foreach (EFeatures f in features)
            {
                switch (f)
                {
                    case EFeatures.MOVABLE:
                        {
                            break;
                        }
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
                    default:
                        {
                            throw new Exception("BaseNode: Trying to enable a feature that does not exist.");
                        }
                }
                _features[(int)f] = true;
            }
        } // Func

        private bool[] _features;
    } // Class BaseNode
} // Namespace
