using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Items
{
    public class ClassItem : Base.NodeItem
    {
        public override void SetDynamicResources(String keyPrefix) { }
        static Random r = new Random();
        public Base.ScopeItem ItemScope;
        public ClassItem()// :
            //base()
        {
            Base.ScopeItem si = new Base.ScopeItem();
            si.Scope = (Base.ScopeItem.EScope)r.Next(0, 4);
            ItemScope = si;
            this.BeforeName.Children.Add(si);
            //this.SetItemType("Int");
        }

        public ClassItem(BaseNode parent) :
            this()
        {
            this._parentNode = parent;
        }
    }
}
