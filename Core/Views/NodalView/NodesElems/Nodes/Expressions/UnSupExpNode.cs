using System;
using code_in.Views.NodalView.NodesElems.Items;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    class UnSupExpNode : AExpressionNode
    {
        public TextBox NodeText;

        public UnSupExpNode() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }

        public UnSupExpNode(System.Windows.ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetType("Unsupported Expr");
            TextBox tb = new TextBox();
            this.NodeText = tb;
            this.ContentLayout.Children.Add(tb);
        }
    }
}
