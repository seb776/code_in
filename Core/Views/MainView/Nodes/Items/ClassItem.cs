using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.MainView.Nodes.Items
{
    public class ClassItem : NodeItem
    {
        static Random r = new Random();
        public ClassItem() :
            base()
        {
            ScopeItem si = new ScopeItem();
            si.Scope = (ScopeItem.EScope)r.Next(0, 4);
            this.Container.Children.Insert(0, si);
            this.SetItemType("Int");
        }
    }
}
