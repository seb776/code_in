using code_in.Views.NodalView.NodesElems.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Expressions
{
    public abstract class AExpressionNode : AValueNode
    {
        public AExpressionNode(ResourceDictionary themeResDict)
            : base(themeResDict)
        {
        }
        public override void SetThemeResources(string keyPrefix)
        {
        }
    }
}
