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
    public abstract partial class BaseNode : UserControl, INodeElem, ICodeInVisual
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private IVisualNodeContainer _parentView = null;
        private IVisualNodeContainerDragNDrop _rootView = null;

        public Line lineInput;
        public Line lineOutput;

        public BaseNode(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();

            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;
        }
        public BaseNode() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }

        #region INodeElem
        public void SetParentView(IVisualNodeContainer parent) { _parentView = parent; }
        public IVisualNodeContainer GetParentView() { return _parentView; }
        public void SetRootView(IVisualNodeContainerDragNDrop root) { _rootView = root; }
        public IVisualNodeContainerDragNDrop GetRootView() { return _rootView; }
        #endregion INodeElem

        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public abstract void SetDynamicResources(String keyPrefix);

        /// <summary>
        /// This function is used to set the resource that will be used as the main color of the node. Usefull to distinguish different nodes.
        /// This function have to be called before setting any value as the resource is used for each modification.
        /// </summary>
        /// <param name="resourceName">The name of the resource that have to be used</param>
        public void SetColorResource(String resourceName) // TODO remove this
        {
            System.Windows.Media.Brush resource = (System.Windows.Media.Brush)this.Resources[resourceName];
            System.Diagnostics.Debug.Assert(resource != null); // Just to check if the resource exists

            this.NodeBorder.SetResourceReference(BorderBrushProperty, resourceName);
            this.NodeHeader.SetResourceReference(BackgroundProperty, resourceName);
            this.ResizeControl.SetResourceReference(System.Windows.Shapes.Shape.FillProperty, resourceName);
        }
        #endregion ICodeInVisual

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
        #region Events
        private void EvtDragResize(object sender, MouseButtonEventArgs e)
        {
            this.GetRootView().DragNodes(TransformationMode.RESIZE, this);
            e.Handled = true; // To avoid bubbling http://www.codeproject.com/Articles/464926/To-bubble-or-tunnel-basic-WPF-events
        }

        private void EvtDragNode(object sender, MouseButtonEventArgs e) // abstract ?
        {
            this.GetRootView().DragNodes(TransformationMode.MOVE, this);
            e.Handled = true; // To avoid bubbling http://www.codeproject.com/Articles/464926/To-bubble-or-tunnel-basic-WPF-events
        }

        private void EvtRemoveNode(object sender, MouseButtonEventArgs e)
        {
            ((Panel)this.Parent).Children.Remove(this);
            //MainView.MainGrid.Children.Remove(lineInput);
            //MainView.MainGrid.Children.Remove(lineOutput);
            e.Handled = true; // To avoid bubbling http://www.codeproject.com/Articles/464926/To-bubble-or-tunnel-basic-WPF-events
        }
        #endregion Events
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

        public void SetNodeType(String type)
        {
            this.NodeType.Content = type;
        }

        public void SetName(String name)
        {
            this.NodeName.Text = name;
        }

        public String GetName()
        {
            return this.NodeName.Text;
        }
    } // Class BaseNode
} // Namespace
