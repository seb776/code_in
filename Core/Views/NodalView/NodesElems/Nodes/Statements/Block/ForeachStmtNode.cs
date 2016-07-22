using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements.Block
{
    class ForeachStmtNode : ABlockStmtNodes
    {
        public override void InstantiateASTNode()
        {
            this.GetNodePresenter().SetASTNode(new ICSharpCode.NRefactory.CSharp.ContinueStatement());
        }
        public DataFlowAnchor Condition = null;
        public FlowNodeAnchor trueAnchor = null;
        public ForeachStmtNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
             this.SetName("ForEach");
             Condition = this.CreateAndAddInput<DataFlowAnchor>();
             Condition.SetName("Condition");
             trueAnchor = this.CreateAndAddOutput<FlowNodeAnchor>();
             trueAnchor.SetName("True");
        }
    }
}
