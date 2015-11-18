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
            Type[] scopes = {
                                typeof(ScopeIco.InternalItem),
                                typeof(ScopeIco.PublicItem),
                                typeof(ScopeIco.ProtectedItem),
                                typeof(ScopeIco.PrivateItem)
            };
            
            this.Container.Children.Remove(this.Anchor);
            this.Container.Children.Insert(0, (FrameworkElement)Activator.CreateInstance(scopes[r.Next(0,4)]));
        }
    }
}
