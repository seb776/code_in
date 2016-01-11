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

        /// <summary>
        /// A function declaration holds flying representation of the code so we do not 
        /// </summary>
        /// <param name="n"></param>
        public override void AddNode(BaseNode n)
        {
            System.Diagnostics.Debug.Assert(MainView != null && n != null && n != this);
            n.SetMainView(MainView);
            n.SetParent(this);
            this.FlyingContent.Children.Add(n);
        }
    }
}
