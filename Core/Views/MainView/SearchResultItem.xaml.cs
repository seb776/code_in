using code_in.Exceptions;
using code_in.Presenters.Nodal;
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
    /// Logique d'interaction pour SearchResultItem.xaml
    /// </summary>
    public partial class SearchResultItem : UserControl
    {
        public SearchResultItem(ResourceDictionary themeResDict)
        {
            InitializeComponent();
            // TODO
        }
        public SearchResultItem() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new DefaultCtorVisualException();
        }
        public INodeElem AssociatedNode;

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (AssociatedNode != null)
                    AssociatedNode.FocusToNode();
            }
        }
    }
}
