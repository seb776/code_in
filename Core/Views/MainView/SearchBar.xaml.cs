using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Nodes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace code_in.Views.MainView
{
    /// <summary>
    /// Logique d'interaction pour SearchBar.xaml
    /// </summary>
    public partial class SearchBar : UserControl, IVisualNodeContainer, IVisualNodeContainerDragNDrop, ICodeInVisual
    {
        INodeElem _draggingNode = null;
        private ResourceDictionary _themeResourceDictionary = null;
        public SearchBar(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
            this.CreateAndAddNode<FuncDeclNode>();
            this.CreateAndAddNode<ClassDeclNode>();
            this.CreateAndAddNode<NamespaceNode>();
        }
        public SearchBar() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }
        #region IVisualNodeContainer
        public T CreateAndAddNode<T>() where T : UIElement, INodeElem
        {
            T node = (T)Activator.CreateInstance(typeof(T), this._themeResourceDictionary);
            node.SetParentView(this);
            node.SetRootView(this);
            this.AddNode(node);
            return node;
        }
        public void AddNode<T>(T node, int idx = -1) where T : UIElement, INodeElem
        {
            if (idx < 0)
                this.SearchResult.Children.Add(node as UIElement);
            else
                this.SearchResult.Children.Insert(idx, node as UIElement);
        }
        public void RemoveNode(INodeElem node)
        {
            throw new NotImplementedException();
        }
        public void HighLightDropPlace(Point pos) { }
        public int GetDropIndex(Point pos) { return 0; }
        #endregion IVisualNodeContainer
        #region IVisualNodeContainerDragNDrop
        public void SelectNode(INodeElem node) { } // Do nothing
        public void UnSelectNode(INodeElem node) { } // Do nothing
        public void UnSelectAll() { } // Do nothing
        public void DragNodes(TransformationMode transform, INodeElem node)
        {
            //if (transform == TransformationMode.MOVE)
            //    _draggingNode = node;
        }
        public void DropNodes(IVisualNodeContainer container)
        {

        }
        #endregion IVisualNodeContainerDragNDrop
        #region ICodeInVisual
        public void SetDynamicResources(String keyPrefix)
        {

        }
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        #endregion ICodeInVisual

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
        }

        private void Sb_Collapsed(object sender, RoutedEventArgs e)
        {
        }
    }
}
