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

namespace code_in.Views.NodalView.NodesElems.Items.Assets
{
    /// <summary>
    /// Interaction logic for ScopeItem.xaml
    /// </summary>
    public partial class ScopeItem : UserControl, ICodeInVisual
    {
        public void SetDynamicResources(String keyPrefix)
        {

        }
        private ResourceDictionary _themeResourceDictionary = null;
        public ResourceDictionary GetThemeResourceDictionary() { return _themeResourceDictionary; }
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
        public ScopeItem(ResourceDictionary themeResDict)
        {
            this._themeResourceDictionary = themeResDict;
            this.Resources.MergedDictionaries.Add(this._themeResourceDictionary);
            InitializeComponent();
            this.Scope = EScope.PRIVATE;
        }
        public ScopeItem() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }
    }
}
