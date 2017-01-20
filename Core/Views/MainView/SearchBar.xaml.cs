using code_in.Exceptions;
using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;
using code_in.Views.NodalView;
using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Nodes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace code_in.Views.MainView
{
    /// <summary>
    /// Logique d'interaction pour SearchBar.xaml
    /// </summary>
    public partial class SearchBar : UserControl
    {
        private ResourceDictionary _themeResourceDictionary = null;
        private ResourceDictionary _languageResourceDictionary = null;
        public INodalView _nodalView;
        code_in.Views.NodalView.ExecutionNodalView.SearchOptions _searchOptions;
        public SearchBar(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this._languageResourceDictionary = Code_inApplication.LanguageResourcesDictionary;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            this.Resources.MergedDictionaries.Add(this._languageResourceDictionary);
            InitializeComponent();
            SetLanguageResources("");
            _searchOptions = new ExecutionNodalView.SearchOptions(false);
            CheckBoxCaseSensitive.IsChecked = _searchOptions.CaseSensitive;
        }
        public SearchBar(INodalView MainView) :
            this(Code_inApplication.MainResourceDictionary)
        { throw new DefaultCtorVisualException(); }

        #region ICodeInVisual

        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
        public ResourceDictionary GetLanguageResourceDictionary() { return _languageResourceDictionary; }
        public void SetThemeResources(String keyPrefix)
        {
            throw new NotImplementedException();
        }
        public void SetLanguageResources(String keyPrefix)
        {
            this.SearchButton.SetResourceReference(Button.ContentProperty, "Search");
            this.SearchBox.SetResourceReference(TextBox.TextProperty, "Search");
            this.CheckBoxCaseSensitive.SetResourceReference(CheckBox.ContentProperty, "CaseSensitiveKey");
        }
        #endregion ICodeInVisual

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.SearchResult.Items.Clear();
            var searchResults = this._nodalView.SearchMatchinNodes(this.SearchBox.Text, this._searchOptions);

            foreach (var category in searchResults.Keys)
            {
                var categoryItem = new TreeViewItem();
                categoryItem.Foreground = new SolidColorBrush(Colors.White);
                categoryItem.Header = category;
                this.SearchResult.Items.Add(categoryItem);
                categoryItem.IsExpanded = true;
                foreach (var result in searchResults[category])
                {
                    var resultItem = new SearchResultItem(this._themeResourceDictionary);

                    resultItem.ResultName.Content = result.GetName();
                    resultItem.AssociatedNode = result;
                    categoryItem.Items.Add(resultItem);
                }
            }
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

        private void SearchBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.SearchBox.SelectAll();
            e.Handled = true;
        }

        private void CheckBox_CaseSensitive_Clicked(object sender, RoutedEventArgs e)
        {
            _searchOptions.CaseSensitive = (sender as CheckBox).IsChecked.GetValueOrDefault();
        }
    }
}
