using code_in.Exceptions;
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
    public partial class SearchBar : UserControl, IVisualNodeContainer, IContainerDragNDrop
    {
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
        { throw new DefaultCtorVisualException(); }

        #region IVisualNodeContainer
        public T CreateAndAddNode<T>(INodePresenter nodePresenter) where T : UIElement, code_in.Views.NodalView.INode
        {
            T node = (T)Activator.CreateInstance(typeof(T), this._themeResourceDictionary); // TODO From Seb: This may crash if the constructor is not implemented
            node.SetParentView(null);
            node.SetNodePresenter(nodePresenter);
            nodePresenter.SetView(node);
            this.AddNode(node);
            return node;
        }
        public void AddNode<T>(T node, int idx = -1) where T : UIElement, code_in.Views.NodalView.INode
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

        #endregion IVisualNodeContainer
        #region IContainerDragNDrop
        public void SelectNode(INodeElem node) { } // Do nothing
        public void UnSelectNode(INodeElem node) { } // Do nothing

        #endregion IContainerDragNDrop
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





        public void UnSelectAllNodes()
        {
            throw new NotImplementedException();
        }

        public void DragNodes()
        {
            throw new NotImplementedException();
        }

        public void DropNodes(IContainerDragNDrop container)
        {
            throw new NotImplementedException();
        }

        public int GetDropNodeIndex(Point pos)
        {
            throw new NotImplementedException();
        }

        public void HighLightDropNodePlace(Point pos)
        {
            throw new NotImplementedException();
        }

        public void DragLink(NodalView.NodesElems.Anchors.AIOAnchor from, bool isGenerated)
        {
            throw new NotImplementedException();
        }

        public void DropLink(NodalView.NodesElems.Anchors.AIOAnchor to, bool isGenerated)
        {
            throw new NotImplementedException();
        }

        public void UpdateDragState(Point mousePosition)
        {
            throw new NotImplementedException();
        }


        public bool IsDropNodeValid()
        {
            throw new NotImplementedException();
        }


        public void RevertChange()
        {
            throw new NotImplementedException();
        }

        public void AddSelectNode(NodalView.IDragNDropItem item)
        {
            throw new NotImplementedException();
        }

        public void AddSelectNodes(System.Collections.Generic.List<NodalView.IDragNDropItem> items)
        {
            throw new NotImplementedException();
        }

        public void Drag(EDragMode dragMode)
        {
            throw new NotImplementedException();
        }

        public void UpdateDragInfos(Point mousePosToMainGrid)
        {
            throw new NotImplementedException();
        }

        public new void Drop(System.Collections.Generic.IEnumerable<NodalView.IDragNDropItem> items)
        {
            throw new NotImplementedException();
        }

        public bool IsDropValid(System.Collections.Generic.IEnumerable<NodalView.IDragNDropItem> items)
        {
            throw new NotImplementedException();
        }
    }
}
