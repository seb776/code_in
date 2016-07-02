using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;
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
        private ResourceDictionary _languageResourceDictionary = null;
        public SearchBar(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(this._languageResourceDictionary);
            InitializeComponent();
        }
        public SearchBar() :
            this(Code_inApplication.MainResourceDictionary)
        { throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)"); }
        #region IVisualNodeContainer
        public T CreateAndAddNode<T>(INodePresenter nodePresenter) where T : UIElement, INodeElem
        {
            T node = (T)Activator.CreateInstance(typeof(T), this._themeResourceDictionary);
            node.SetParentView(null);
            node.SetRootView(this);
            node.SetNodePresenter(nodePresenter);
            nodePresenter.SetView(node);
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
        public void DragNodes(TransformationMode transform, INodeElem node, LineMode lm)
        {
            //if (transform == TransformationMode.MOVE)
            //    _draggingNode = node;
        }
        public void DropNodes(INodeElem container)
        {

        }
        #endregion IVisualNodeContainerDragNDrop
        #region ICodeInVisual

        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public ResourceDictionary GetLanguageResourceDictionary() { return _languageResourceDictionary; }
        public void SetThemeResources(String keyPrefix)
        {
            throw new NotImplementedException();
        }
        public void SetLanguageResources(String keyPrefix)
        {
            throw new NotImplementedException();
        }
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
