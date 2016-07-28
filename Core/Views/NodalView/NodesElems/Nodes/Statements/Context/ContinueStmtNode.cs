using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Base;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements.Context
{
    class ContinueStmtNode : AContextStmtNode
    {
        public override void InstantiateASTNode()
        {
            this.GetNodePresenter().SetASTNode(new ICSharpCode.NRefactory.CSharp.ContinueStatement());
        }
        public ContinueStmtNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
        }
    }
}
