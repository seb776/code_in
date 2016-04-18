using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private Point _newNodePos = new Point();
        private ResourceDictionary _themeResourceDictionary = null;
        private INodeElem _parentView = null;
        private IVisualNodeContainerDragNDrop _rootView = null;

        public BaseNode(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
            this.SetDynamicResources("BaseNode");
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;
        }
        public BaseNode() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }

        #region INodeElem
        public void SetParentView(INodeElem parent) { _parentView = parent; }
        public INodeElem GetParentView() { return _parentView; }
        public virtual void SetRootView(IVisualNodeContainerDragNDrop root) { _rootView = root; }
        public IVisualNodeContainerDragNDrop GetRootView() { return _rootView; }
        public abstract void RemoveNode(INodeElem node);
        #endregion INodeElem

        #region ICodeInVisual
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public virtual void SetDynamicResources(String keyPrefix)
        {
            this.NodeName.SetResourceReference(ForegroundProperty, keyPrefix + "NameForeGroundColor");
            this.NodeType.SetResourceReference(ForegroundProperty, keyPrefix + "TypeForeGroundColor");
            this.NodeHeader.SetResourceReference(BackgroundProperty, keyPrefix + "MainColor");
            this.NodeBorder.SetResourceReference(BorderBrushProperty, keyPrefix + "MainColor");
            this.BackGrid.SetResourceReference(BackgroundProperty, keyPrefix + "SecondaryColor");
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
            this.GetRootView().DragNodes(TransformationMode.RESIZE, this, LineMode.NONE);
            e.Handled = true; // To avoid bubbling http://www.codeproject.com/Articles/464926/To-bubble-or-tunnel-basic-WPF-events
        }

        private void EvtDragNode(object sender, MouseButtonEventArgs e) // abstract ?
        {
            this.GetRootView().DragNodes(TransformationMode.MOVE, this, LineMode.NONE);
            e.Handled = true; // To avoid bubbling http://www.codeproject.com/Articles/464926/To-bubble-or-tunnel-basic-WPF-events
        }

        public virtual void EvtRemoveNode(object sender, MouseButtonEventArgs e)
        {
            ((Panel)this.Parent).Children.Remove(this);
            //MainView.MainGrid.Children.Remove(lineInput);
            //MainView.MainGrid.Children.Remove(lineOutput);
            e.Handled = true; // To avoid bubbling http://www.codeproject.com/Articles/464926/To-bubble-or-tunnel-basic-WPF-events
        }
        #endregion Events

        #region NodesCapabilities
        public void AddAttribute(String type)
        {
            Label lblType = new Label();
            lblType.Content = type;
            this.NodeAttributes.Children.Add(lblType);
        }

        public void SetNodeType(String type)
        {
            this.NodeType.Content = type;
        }

        public void SetName(String name)
        {
            this.NodeName.Content = name;
        }

        public String GetName()
        {
            return this.NodeName.Content as String;
        }

        public void MoveNode(Point pos)
        {
            this.Margin = new Thickness(pos.X, pos.Y, 0, 0);
            this.MoveNodeSpecial();
        }

        public abstract void MoveNodeSpecial();
        #endregion NodesCapabilities

        /// <summary>
        /// This function is not reversible. It removes the remove button from the node.
        /// </summary>
        public void MakeNotRemovable()
        {
        }

        private void ContentGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // This automatically updates the list of accessible nodes
            // Need to be optimized (compute only the first time, as it uses reflection)
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
                m1.DataContext = t;
                m1.Click += m1_Click;
                cm.Items.Add(m1);
            }
            cm.Margin = new Thickness(e.GetPosition((this.Parent as FrameworkElement).Parent as FrameworkElement).X, e.GetPosition((this.Parent as FrameworkElement).Parent as FrameworkElement).Y, 0, 0);
            cm.IsOpen = true;
            // Setting the position of the node if we create one to the place the menu has been opened
            _newNodePos.X = e.GetPosition(this).X;
            _newNodePos.Y = e.GetPosition(this).Y;
            e.Handled = true;
        }
        void m1_Click(object sender, RoutedEventArgs e)
        {
            MethodInfo mi = this.GetType().GetMethod("CreateAndAddNode");
            MethodInfo gmi = mi.MakeGenericMethod(((sender as MenuItem).DataContext as Type));
            BaseNode node = gmi.Invoke(this, null) as BaseNode;

            node.Margin = new Thickness(_newNodePos.X, _newNodePos.Y, 0, 0);
            //var node = this._rootNode.CreateAndAddNode<((sender as MenuItem).DataContext as Type)>();
        }


    } // Class BaseNode
} // Namespace
