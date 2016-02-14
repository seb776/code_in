using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Nodes.Base
{
    public abstract class AFlyingContentNode : AContentNode
    {
        protected AFlyingContentNode(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
        }

        public override void AddNode<T>(T node, int index = -1)
        {
            this.ContentGrid.Children.Add(node as UIElement);
        }
    }
}
