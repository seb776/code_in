using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    class ValueExprNode : AValueNode
    {
        public ValueExprNode(ResourceDictionary themeResDict)
            : base(themeResDict)
        { 
        
        }
        public override void InstantiateASTNode()
        {
            throw new NotImplementedException();
        }
    }
}
