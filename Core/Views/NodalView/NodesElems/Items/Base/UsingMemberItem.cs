﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using code_in.Views.NodalView.NodesElems.Items.Assets;


namespace code_in.Views.NodalView.NodesElems.Items.Base
{
    public class UsingMemberItem : ANodeItem
    {
        protected ItemModifiers _modifiers;
        protected UsingMemberItem(ResourceDictionary themeResDict, INodalView nodalView) :
            base(themeResDict, nodalView)
        {
            _modifiers = new ItemModifiers(this.GetThemeResourceDictionary());
            this.BeforeName.Children.Add(_modifiers);
        }

        public override void SetThemeResources(String keyPrefix)
        {

        }
        public override void UpdateDisplayedInfosFromPresenter()
        {
            this.SetName(_nodePresenter.GetASTNode().ToString());
        }
    }
}
