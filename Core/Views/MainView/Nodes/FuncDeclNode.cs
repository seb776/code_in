using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.MainView.Nodes
{
    public class FuncDeclNode : BaseNode
    {
        public FuncDeclNode() : base()
        {
            this.NodeGrid.Children.Add(new BaseNode());
        }
    }
}
