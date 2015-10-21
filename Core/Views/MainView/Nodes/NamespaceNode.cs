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
            : base()
        {
            this.DisableFeatures(EFeatures.CONTAINSMODIFIERS, EFeatures.EXPENDABLES, EFeatures.ISFLOWNODE);
            this.SetColorResource("NamespaceNodeColor");
            this.SetNodeType("Namespace");
            this.SetNodeName("System.Collections.Generic.TestDeLaMuerte");
        }

        public NamespaceNode(MainView view) : base(view) { }
    }
}
