using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.MainView.Nodes
{
    public class FuncDeclNode : BaseNode
    {
        public FuncDeclNode()
            : base()
        {
            this.NodeGrid.Children.Add(new BaseNode()); // Example of nodeception
            this.SetColorResource("FuncDeclNodeColor");
            this.SetNodeType("FunctionDecl");
            this.SetNodeName("Func1");
            this.EnableFeatures(EFeatures.EXPENDABLES, EFeatures.ISFLOWNODE, EFeatures.CONTAINSMODIFIERS);
        }

        public FuncDeclNode(MainView view, String name) : base(view) {
            this.SetColorResource("FuncDeclNodeColor");
            this.SetNodeType("FunctionDecl");
            this.SetNodeName(name);
            this.EnableFeatures(EFeatures.EXPENDABLES, EFeatures.ISFLOWNODE, EFeatures.CONTAINSMODIFIERS);
        }
    }
}
