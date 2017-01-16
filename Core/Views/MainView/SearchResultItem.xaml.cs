using code_in.Exceptions;
using code_in.Presenters.Nodal;
using code_in.Views.NodalView;
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
        private INodeElem _associatedNode;
        public INodeElem AssociatedNode
        {
            get
            {
                return _associatedNode;
            }
            set
            {
                _associatedNode = value;
                if (value.NodalView is ExecutionNodalView)
                {
                    this.ExecOrDecl.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xA2, 0xFF));
                    this.ExecOrDecl.Content = "E";
                }
                else
                {
                    this.ExecOrDecl.Foreground = new SolidColorBrush(Colors.GreenYellow);
                    this.ExecOrDecl.Content = "D";
                }
            }
        }

        public SearchResultItem(ResourceDictionary themeResDict)
        {
            InitializeComponent();
        }
        public SearchResultItem() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new DefaultCtorVisualException();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (AssociatedNode != null)
                {
                    AssociatedNode.FocusToNode();
                    Code_inApplication.RootDragNDrop.UnselectAllNodes();
                    Code_inApplication.RootDragNDrop.AddSelectItem(AssociatedNode);
                }
            }
        }
    }
}
