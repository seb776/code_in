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

namespace code_in.Views.NodalView.NodesElems.Nodes.Base
{
    // This is used for managing these actions: Resize, Move

    /// <summary>
    /// The visual representation of an AST node.
    /// This class contains all the features that may be used by all the other kind of nodes.
    /// </summary>
    public abstract partial class BaseNode : UserControl, INode, IVisualNodeContainer, ICodeInVisual
    {
        protected BaseNode _parentNode = null;
        private ResourceDictionary _resourceDictionary = null;
        private NodalView _nodalView = null;
        public ResourceDictionary GetResourceDictionary() { return _resourceDictionary; }
        public void SetParentNode(BaseNode parent) { _parentNode = parent; }
        public BaseNode GetParentNode() { return _parentNode; }
        public void SetNodalView(NodalView nv) { _nodalView = nv; }
        public NodalView GetNodalView() { return _nodalView; }

        public Line lineInput;
        public Line lineOutput;

        public abstract void SetDynamicResources(String keyPrefix);

        public BaseNode(ResourceDictionary resourceDict)
        {
            this._resourceDictionary = resourceDict;
            this.Resources.MergedDictionaries.Add(this._resourceDictionary);
            InitializeComponent();

            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;
        }
        public BaseNode() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        { }

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

        private void OnDragResize(object sender, MouseButtonEventArgs e)
        {
            this._nodalView.SetTransformingNodes(this);
            this._nodalView.SetTransformationMode(TransformationMode.RESIZE);
            e.Handled = true; // To avoid bubbling http://www.codeproject.com/Articles/464926/To-bubble-or-tunnel-basic-WPF-events
        }

        private void OnDragNode(object sender, MouseButtonEventArgs e)
        {
            this._nodalView.SetTransformingNodes(this);
            this._nodalView.SetTransformationMode(TransformationMode.MOVE);
            //if (this._parentNode.GetType() == typeof(OrderedContentNode))
            //{
            //    var orderParent = this._parentNode as OrderedContentNode;
            //    orderParent._orderedLayout.Children.Remove(this);
            //    this.ContentGrid.Children.Add(this);
            //    this.Opacity = 0.5f;
            //}
            e.Handled = true; // To avoid bubbling http://www.codeproject.com/Articles/464926/To-bubble-or-tunnel-basic-WPF-events
        }
        private void OnDropNode(object sender, MouseButtonEventArgs e)
        {
            //if (this._nodalView._transformationMode == TransformationMode.MOVEORDERED)
            //{
            //    var orderParent = this._parentNode as OrderedContentNode;
            //    this.ContentGrid.Children.Remove(this);
            //    orderParent._orderedLayout.Children.Add(this);
            //    this.Opacity = 1f;
            //}
            this._nodalView.ResetTransformationMode();
            this._nodalView.ResetTransformingNode();
        }

        private void OnRemoveNode(object sender, MouseButtonEventArgs e)
        {
            ((Panel)this.Parent).Children.Remove(this);
            //MainView.MainGrid.Children.Remove(lineInput);
            //MainView.MainGrid.Children.Remove(lineOutput);
            e.Handled = true; // To avoid bubbling http://www.codeproject.com/Articles/464926/To-bubble-or-tunnel-basic-WPF-events
        }

        //public void CreateLink(Nodes.Items.Base.NodeAnchor n)
        //{
        //    TransformingNode.TransformingObject = n;
        //    TransformingNode.Transformation = TransformingNode.TransformationMode.LINE;

        //    MainView.MainGrid.Children.Remove(n.IOLine);
        //    n.IOLine = new Line();
        //    n.IOLine.Stroke = System.Windows.Media.Brushes.Red;
        //    n.IOLine.StrokeThickness = 5;
        //    if (n._parentItem.Orientation == Nodes.Items.NodeItem.EOrientation.LEFT)
        //        lineInput = n.IOLine;
        //    else
        //        lineOutput = n.IOLine;
        //    Canvas.SetZIndex(n.IOLine, -1);

        //    MainView.MainGrid.Children.Add(n.IOLine);
        //}

        //public void AddNodeModifiers(String type)
        //{
        //    System.Diagnostics.Debug.Assert(this._features[(int)EFeatures.CONTAINSMODIFIERS], "The node cannot contain modifiers.");
        //    Label lblType = new Label();
        //    lblType.Content = type;
        //    this.NodeModifiers.Children.Add(lblType);
        //}

        /// <summary>
        /// This function is used to set the resource that will be used as the main color of the node. Usefull to distinguish different nodes.
        /// This function have to be called before setting any value as the resource is used for each modification.
        /// </summary>
        /// <param name="resourceName">The name of the resource that have to be used</param>
        public void SetColorResource(String resourceName)
        {
            System.Windows.Media.Brush resource = (System.Windows.Media.Brush)this.Resources[resourceName];
            System.Diagnostics.Debug.Assert(resource != null); // Just to check if the resource exists

            this.NodeBorder.SetResourceReference(BorderBrushProperty, resourceName);
            this.NodeHeader.SetResourceReference(BackgroundProperty, resourceName);
            this.ResizeControl.SetResourceReference(System.Windows.Shapes.Shape.FillProperty, resourceName);
        }

        public void SetNodeType(String type)
        {
            this.NodeType.Content = type;
        }


        //private bool[] _features;

        public void SetName(String name)
        {
            this.NodeName.Text = name;
        }

        public String GetName()
        {
            return this.NodeName.Text;
        }

        public abstract void AddNode<T>(T node) where T : UIElement, INode;

        // The function is virtual so it can be overriden, usefull if there is a check to do
        public virtual T CreateAndAddNode<T>() where T : UIElement, INode
        {
            T node = (T)Activator.CreateInstance(typeof(T), _resourceDictionary);
            node.SetNodalView(this._nodalView);
            node.SetParentNode(this);
            try
            {
                this.AddNode(node);
            }
            catch (Exception e)
            {
                return default(T);
            }
            return node;
        }



    } // Class BaseNode
} // Namespace
