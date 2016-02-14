using code_in.Views.NodalView.NodesElem.Nodes.Base;
using code_in.Views.NodalView.NodesElems.Nodes.Base;
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
            var editIcon = new Line();
            editIcon.SetValue(Grid.ColumnProperty, 4);
            editIcon.X1 = editIcon.Y1 = 0;
            editIcon.X2 = editIcon.Y2 = 10;
            editIcon.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            editIcon.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            editIcon.StrokeThickness = 3;
            editIcon.Stroke = new SolidColorBrush(Colors.Black);
            this.NodeHeader.Children.Add(editIcon);
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
    }
}
