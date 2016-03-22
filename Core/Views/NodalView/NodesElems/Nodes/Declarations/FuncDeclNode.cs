using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Shapes;

namespace code_in.Views.NodalView.NodesElems.Nodes
{
    public class FuncDeclNode : AOrderedContentNode
    {
        public MethodDeclaration MethodNode = null;
        private Line _editIcon;

        public FuncDeclNode(System.Windows.ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetNodeType("Declaration");
            this.SetName("Function");
            _editIcon = new Line();
            _editIcon.SetValue(Grid.ColumnProperty, 4);
            _editIcon.X1 = _editIcon.Y1 = 0;
            _editIcon.X2 = _editIcon.Y2 = 10;
            _editIcon.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            _editIcon.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            _editIcon.StrokeThickness = 3;
            _editIcon.MouseDown += editIcon_MouseDown;
            this.NodeHeader.Children.Add(_editIcon);
            this.SetDynamicResources("FuncDeclNode");
        }

        void editIcon_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var view = Code_inApplication.EnvironmentWrapper.CreateAndAddView<MainView.MainView>();
            view.EditFunction(this);
        }
        public FuncDeclNode() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }

        #region ICodeInVisual
        public override void SetDynamicResources(string keyPrefix)
        {
            base.SetDynamicResources(keyPrefix);
            if(_editIcon != null)
            {
                _editIcon.SetResourceReference(Line.StrokeProperty, keyPrefix + "SecondaryColor");
            }
        }
        #endregion ICodeInVisual

        public override void RemoveNode(INodeElem node)
        {
            this.ContentGrid.Children.Remove(node as UIElement);
        }
    }
}
