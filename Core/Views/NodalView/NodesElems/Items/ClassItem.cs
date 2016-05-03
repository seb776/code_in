using code_in.Views.NodalView.NodesElems.Items.Assets;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Items
{
    public class ClassItem : ATypedMemberItem
    {
        public override void SetThemeResources(String keyPrefix) { }
        static Random r = new Random();
        public ClassItem(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            _scope.Scope = (ScopeItem.EScope)r.Next(0, 4);
        }
        public ClassItem() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }
    }
}
