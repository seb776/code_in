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
        }

        public NamespaceNode(System.Windows.ResourceDictionary resDict) : base(resDict)
        {
            this.SetColorResource("NamespaceNodeColor");
            this.SetNodeType("Namespace");
            this.SetName("System.Collections.Generic.TestDeLaMuerte");
        }
    }
}
