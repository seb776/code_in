using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems.Nodes
{
    public class NamespaceNode : OrderedContentNode
    {
        public NamespaceNode()
            : this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }

        public NamespaceNode(System.Windows.ResourceDictionary themeResDict) : base(themeResDict)
        {
            this.SetColorResource("NamespaceNodeColor");
            this.SetNodeType("Namespace");
            this.SetName("System.Collections.Generic.TestDeLaMuerte");
        }
        public override void SetDynamicResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }
    }
}
