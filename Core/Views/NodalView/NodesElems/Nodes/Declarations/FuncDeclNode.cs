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
        public override void InstantiateASTNode()
        {
            MethodNode = new MethodDeclaration();
            this.GetNodePresenter().SetASTNode(MethodNode);
        }
        public MethodDeclaration MethodNode = null;

        public FuncDeclNode(System.Windows.ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetType("Declaration");
            this.SetName("Function");
            this.SetThemeResources("FuncDeclNode");
        }

        void editIcon_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //var view = Code_inApplication.EnvironmentWrapper.CreateAndAddView<MainView.MainView>();
            //view.EditFunction(this);
        }
        public FuncDeclNode() :
            this(Code_inApplication.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }

        #region ICodeInVisual
        public override void SetThemeResources(string keyPrefix)
        {
            base.SetThemeResources(keyPrefix);
        }
        #endregion ICodeInVisual
    }
}
