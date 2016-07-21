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
        public ScopeItem Scope
        {
            get;
            private set;
        }
        protected TypeInfo _typeInfo;
        public ItemModifiers Modifiers
        {
            get;
            private set;
        }
        protected ATypedMemberItem(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            Scope = new ScopeItem(this.GetThemeResourceDictionary());
            _typeInfo = new TypeInfo(this.GetThemeResourceDictionary());
            Modifiers = new ItemModifiers(this.GetThemeResourceDictionary());
            this.BeforeName.Children.Add(Scope);
            this.BeforeName.Children.Add(Modifiers);
            this.BeforeName.Children.Add(_typeInfo);
        }
        public void setTypeFromString(string type){
            _typeInfo.SetTypeFromString(type);
        }
    }
}
