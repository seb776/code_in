using code_in.Views.NodalView.NodesElems.Items.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace code_in.Views.NodalView.NodesElems.Items
{
    public class FuncDeclItem : ATypedMemberItem
    {
        StackPanel _params;
        public FuncDeclItem(ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            _params = new StackPanel();
            _params.Orientation = Orientation.Horizontal;
            var paramsOpen = new Label();
            paramsOpen.Content = "(";
            var paramsClose = new Label();
            paramsClose.Content = ")";
            this.AfterName.Children.Add(paramsOpen);
            this.AfterName.Children.Add(_params);
            this.AfterName.Children.Add(paramsClose);
        }

        public void AddParam(String type)
        {
            var lbl = new Label();
            lbl.Content = type;
            this._params.Children.Add(lbl);
        }

        public override void SetDynamicResources(String keyPrefix)
        {

        }
    }
}
