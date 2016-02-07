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

namespace code_in.Views.MainView.Nodes.Items
{
    /// <summary>
    /// Interaction logic for ScopeItem.xaml
    /// </summary>
    public partial class ScopeItem : UserControl
    {
        public enum EScope
        {
            PUBLIC = 0,
            PRIVATE = 1,
            PROTECTED = 2,
            INTERNAL = 3
        }
        private EScope _scope;
        public EScope Scope
        {
            get
            { return _scope; }
            set
            {
                _scope = value;
                String[] refs = { "ScopePublic",
                                    "ScopePrivate",
                                    "ScopeProtected",
                                    "ScopeInternal"
                                  };
                this.Shape.SetResourceReference(Rectangle.FillProperty, refs[(int)value] + "Color");
                this.Symbol.SetResourceReference(Label.ContentProperty, refs[(int)value] + "String");
            }
        }
        public ScopeItem(ResourceDictionary resDict)
        {
            InitializeComponent();
        }
        public ScopeItem() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
        }

        void m1_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Temporary dirty as fu**
            String[] refs = { "ScopePublic",
                                    "ScopePrivate",
                                    "ScopeProtected",
                                    "ScopeInternal"
                                  };

            int i = 0;
            foreach (String s in refs)
            {
                if ((sender as MenuItem).Header.Equals(refs[i]))
                {
                    Scope = (EScope)i;
                    break;
                }
                ++i;
            }
        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            String[] refs = { "ScopePublic",
                                    "ScopePrivate",
                                    "ScopeProtected",
                                    "ScopeInternal"
                                  };
            ContextMenu cm = new ContextMenu();
            int i = 0;
            foreach (var r in refs)
            {
                MenuItem m = new MenuItem();
                m.Header = refs[i];
                m.Click += m1_Click;
                cm.Items.Add(m);
                ++i;
            }
            cm.IsOpen = true;
            e.Handled = true;
        }

    }
}
