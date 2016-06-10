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
        public TextBox NodeText;

        public UnSupStmtDeclNode() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }

        public UnSupStmtDeclNode(System.Windows.ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetType("Unsupported Stmt");
            this.CreateAndAddInput<FlowNodeItem>();
            this.CreateAndAddOutput<FlowNodeItem>();
            TextBox tb = new TextBox();
            this.NodeText = tb;
            this.ContentLayout.Children.Add(tb);
        }
    }
}
