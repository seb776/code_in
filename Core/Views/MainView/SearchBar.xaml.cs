using code_in.Views.NodalView.NodesElems;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
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

namespace code_in.Views.MainView
{
    /// <summary>
    /// Logique d'interaction pour SearchBar.xaml
    /// </summary>
    public partial class SearchBar : UserControl, IVisualNodeContainer, ICodeInVisual
    {
        private ResourceDictionary _resourceDictionary;
        public ResourceDictionary GetResourceDictionary() { return _resourceDictionary; }
        public T CreateAndAddNode<T>() where T : UIElement, INode
        {
            T node = (T)Activator.CreateInstance(typeof(T), _resourceDictionary);

            this.AddNode(node);
            return node;
        }
        public void AddNode<T>(T node) where T : UIElement, INode
        {
            this.SearchResult.Children.Add((UIElement)node);
        }
        public SearchBar(ResourceDictionary resDict)
        {
            this._resourceDictionary = resDict;
            this.Resources.MergedDictionaries.Add(this._resourceDictionary);
            InitializeComponent();
        }
        public SearchBar() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
        }

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
