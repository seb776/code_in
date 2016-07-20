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

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements
{
    public class VarDeclStmtNode : ADefaultStatementNode
    {

        public VarDeclStmtNode(ResourceDictionary themeResDict) : base(themeResDict)
        {
            this.SetType("VarDecl");
            this.SetThemeResources("VarDeclStmtNode");
        }
    }
}
