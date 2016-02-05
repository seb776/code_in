using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes
{
    public class FuncDeclNode : IONode
    {
        public override void SetDynamicResources(string keyPrefix)
        {
            throw new NotImplementedException();
        }
        public FuncDeclNode(System.Windows.ResourceDictionary resDict) :
            base(resDict)
        {
            this.SetColorResource("FuncDeclNodeColor");
            this.SetNodeType("FunctionDecl");
            this.SetName("Func1");
        }
        public FuncDeclNode() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
        }

        //public FuncDeclNode(MainView view, String name) : base(view) {
        //    this.SetColorResource("FuncDeclNodeColor");
        //    this.SetNodeType("FunctionDecl");
        //    this.SetNodeName(name);
        //    this.EnableFeatures(EFeatures.EXPENDABLES, EFeatures.ISFLOWNODE, EFeatures.CONTAINSMODIFIERS);
        //}


        public override void AddNode<T>(T node)
        {
            this.ContentGrid.Children.Add(node as UIElement);
        }
    }
}
