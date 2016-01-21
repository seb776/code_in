using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.MainView.Nodes
{
    public class NamespaceNode : BaseNode
    {
        public NamespaceNode()
            : this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
        }

        public NamespaceNode(System.Windows.ResourceDictionary resDict) : base(resDict)
        {
            this.DisableFeatures(EFeatures.CONTAINSMODIFIERS, EFeatures.EXPENDABLES, EFeatures.ISFLOWNODE);
            this.SetColorResource("NamespaceNodeColor");
            this.SetNodeType("Namespace");
            this.SetNodeName("System.Collections.Generic.TestDeLaMuerte");
        }

        public NamespaceNode(MainView view) : this(view.ResourceDict) { 
        }
    }
}
