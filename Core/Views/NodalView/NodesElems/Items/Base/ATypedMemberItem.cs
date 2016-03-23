using code_in.Views.NodalView.NodesElems.Items.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace code_in.Views.NodalView.NodesElems.Items.Base
{
    public abstract class ATypedMemberItem : ANodeItem
    {
        protected ScopeItem _scope;
        protected TypeInfo _typeInfo;
        protected ATypedMemberItem(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            _scope = new ScopeItem(this.GetThemeResourceDictionary());
            _typeInfo = new TypeInfo(this.GetThemeResourceDictionary());
            this.BeforeName.Children.Add(_scope);
            this.BeforeName.Children.Add(_typeInfo);
        }
    }
}
