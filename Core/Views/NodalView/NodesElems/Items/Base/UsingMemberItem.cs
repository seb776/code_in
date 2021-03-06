﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using code_in.Views.NodalView.NodesElems.Items.Assets;
using code_in.Presenters.Nodal.Nodes;


namespace code_in.Views.NodalView.NodesElems.Items.Base
{
    public class UsingMemberItem : ANodeItem
    {
        protected ItemModifiers _modifiers;
        protected UsingMemberItem(ResourceDictionary themeResDict, INodalView nodalView, INodePresenter presenter) :
            base(themeResDict, nodalView, presenter)
        {
            _modifiers = new ItemModifiers(this.GetThemeResourceDictionary());
            this.BeforeName.Children.Add(_modifiers);
        }

        public override void SetThemeResources(String keyPrefix)
        {

        }
        public override void UpdateDisplayedInfosFromPresenter()
        {
            this.SetName(Presenter.GetASTNode().ToString());
        }
    }
}
