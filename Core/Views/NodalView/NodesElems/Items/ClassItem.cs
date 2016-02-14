using code_in.Views.NodalView.NodesElems.Items.Assets;
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
        public Assets.ScopeItem ItemScope;
        public ClassItem(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            ScopeItem si = new ScopeItem(this.GetThemeResourceDictionary());
            si.Scope = (ScopeItem.EScope)r.Next(0, 4);
            ItemScope = si;
            this.BeforeName.Children.Add(si);
            //this.SetItemType("Int");
        }
        public ClassItem()
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }
    }
}
