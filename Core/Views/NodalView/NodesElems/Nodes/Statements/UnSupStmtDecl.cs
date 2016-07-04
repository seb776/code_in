using code_in.Views.NodalView.NodesElems.Anchors;
using code_in.Views.NodalView.NodesElems.Items;
using code_in.Views.NodalView.NodesElems.Nodes.Statements.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace code_in.Views.NodalView.NodesElems.Nodes.Statements
{
    class UnSupStmtDeclNode : AStatementNode
    {
        public TextBox NodeText = null;
        public FlowNodeAnchor FlowOutAnchor = null;

        public UnSupStmtDeclNode() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }

        public UnSupStmtDeclNode(System.Windows.ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetType("Unsupported Stmt");
            FlowOutAnchor = this.CreateAndAddOutput<FlowNodeAnchor>();
            this.NodeText = new TextBox();
            this.ContentLayout.Children.Add(this.NodeText);
        }
    }
}
