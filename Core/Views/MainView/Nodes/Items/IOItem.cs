using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.MainView.Nodes.Items
{
    public class IOItem : NodeItem
    {
        public IOItem() :
            base()
        {
            NodeAnchor na = new NodeAnchor(this);

            na.Name = "Anchor"; // Not necessary but to be clean
            this.Container.RegisterName("Anchor", na); // To be able to find it through container.FindName(String)

            na.Width = 10;
            na.Height = 20;
            this.Container.Children.Insert(0, na);
        }

        public IOItem(BaseNode parent) :
            this()
        {
            this._parentNode = parent;
        }
    }
}
