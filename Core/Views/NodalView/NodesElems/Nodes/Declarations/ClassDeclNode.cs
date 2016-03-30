using code_in.Views.NodalView.NodesElems.Items.Assets;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace code_in.Views.NodalView.NodesElems.Nodes
{
    /// <summary>
    /// This defines the visual nodes for "classes", class here as a large meaning, it stand for enum, struct, class, interface
    /// </summary>
    public class ClassDeclNode : AOrderedContentNode
    {
        public enum EType
        {
            STRUCT = 0,
            CLASS = 1,
            INTERFACE = 2,
            ENUM = 3
        }
        private EType _type;
        public ScopeItem NodeScope = null;
        public ClassDeclNode(System.Windows.ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetNodeType("ClassDecl");
            this.SetName("Class1");
            this.SetDynamicResources("ClassDeclNode");
            //NodeScope = new ScopeItem(this.GetThemeResourceDictionary());
            //NodeScope.SetValue(Grid.ColumnProperty, 0);
            //NodeScope.Scope = ScopeItem.EScope.PRIVATE;
            //this.NodeHeader.Children.Add(this.NodeScope);

            _type = EType.CLASS;
            this._orderedLayout.Margin = new System.Windows.Thickness(0, 0, 0, 10);
        }
        public ClassDeclNode() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }
    }
}
