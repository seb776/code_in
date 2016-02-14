using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace code_in.Views.NodalView.NodesElems.Nodes
{
    public class FuncDeclNode : IONode
    {
        private int _offsetX = 0;

        public FuncDeclNode(System.Windows.ResourceDictionary themeResDict) :
            base(themeResDict)
        {
            this.SetColorResource("FuncDeclNodeColor");
            this.SetNodeType("FunctionDecl");
            this.SetName("Func1");
            this.SetDynamicResources("FuncDecl");
        }
        public FuncDeclNode() :
            this(code_in.Resources.SharedDictionaryManager.MainResourceDictionary)
        {
            throw new Exception("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)");
        }
        #region ICodeInVisual
        public override void SetDynamicResources(string keyPrefix)
        {
            //throw new NotImplementedException();
        }
        #endregion ICodeInVisual

        #region IVisualNodeContainer
        public override void AddNode<T>(T node, int index = -1)
        {
            this.ContentGrid.Children.Add(node as UIElement);
            BaseNode n = node as BaseNode;
            n.Margin = new Thickness(_offsetX, 0, 0, 0);
            _offsetX += 300;
        }
        public override void HighLightDropPlace(Point pos)
        { /*Show the whole Grid*/ }
        public override int GetDropIndex(Point pos)
        { return 0; }
        #endregion IVisualNodeContainer

    }
}
