using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using code_in.Views.NodalView.NodesElems.Items.Assets;
using code_in.Views.NodalView.NodesElems.Items.Base;
using code_in.Views.NodalView.NodesElems.Nodes;
using ICSharpCode.NRefactory.CSharp;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Media.Animation;
using code_in.Presenters.Nodal.Nodes;


namespace code_in.Views.NodalView.NodesElems.Items
{
    public class UsingDeclItem : UsingMemberItem
    {
        public UsingDeclaration UsingNode = null; // TODO move to ANodePresenter

        public UsingDeclItem(ResourceDictionary themeResDict, INodalView nodalView, INodePresenter presenter) :
            base(themeResDict, nodalView, presenter)
        {
            { // TODO This is temporary

            }
        }

 /*       void editButton_PreviewMouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var view = Code_inApplication.EnvironmentWrapper.CreateAndAddView<MainView.MainView>();
            view.EditFunction(this);
        }*/

        public override void OnMouseLeave()
        {
 
        }

        void _animEditDisapearCompleted(object sender, EventArgs e)
        {
        }

        public override void OnMouseEnter()
        {
        }

        public override void SetThemeResources(String keyPrefix)
        {

        }

    }
}
