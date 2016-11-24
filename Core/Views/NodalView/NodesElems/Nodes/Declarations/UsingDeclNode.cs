using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows;
using code_in.Presenters.Nodal;
using code_in.Presenters.Nodal.Nodes;


namespace code_in.Views.NodalView.NodesElems.Nodes
{
    public class UsingDeclNode : AOrderedContentNode
    {

        public UsingDeclNode(System.Windows.ResourceDictionary themeResDict, INodalView nodalView, INodePresenter nodePresenter) :
            base(themeResDict, nodalView)
        {
            this.Presenter = nodePresenter;
            this.SetType("using");
            this.SetThemeResources("UsingDeclNode");
            this.DockPanelNode.MouseRightButtonDown += DockPanelNode_MouseRightButtonDown;
        }
        static void _addUsing(object[] objects)
        {
            NodePresenter nodePres = objects[0] as NodePresenter;

        }
        static void _removeNode(object[] objects)
        {
            NodePresenter nodePres = objects[0] as NodePresenter;
            nodePres.GetView().Remove();
        }
        void DockPanelNode_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //var options = Presenter.GetMenuOptions(); // TODO astNode is null
            Tuple<EContextMenuOptions,Action<object[]>>[] options = {
                new Tuple<EContextMenuOptions,Action<object[]>>(EContextMenuOptions.ADD, _addUsing),
                new Tuple<EContextMenuOptions,Action<object[]>>(EContextMenuOptions.REMOVE, _removeNode)
                                                            };
            code_in.Views.NodalView.NodalView.CreateContextMenuFromOptions(options, this.GetThemeResourceDictionary(), this.Presenter);
            e.Handled = true;
        }

        #region ICodeInVisual
        public override void SetThemeResources(string keyPrefix)
        { /*
            base.SetThemeResources(keyPrefix);
            if (_editIcon != null)
            {
                _editIcon.SetResourceReference(Line.StrokeProperty, keyPrefix + "SecondaryColor");
            } */
        }
        #endregion ICodeInVisual
        public override void UpdateDisplayedInfosFromPresenter()
        {
            // TODO update childs
        }
        public override void RemoveNode(INodeElem node)
        {
            this._orderedLayout.Children.Remove(node as UIElement);
        }
    }
}
