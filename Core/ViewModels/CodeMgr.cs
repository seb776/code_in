using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using code_in.Views.MainView.Nodes;

namespace code_in.ViewModels
{
    public class CodeMgr
    {
        private Models.CodeData _codeData;

        public CodeMgr()
        {
            _codeData = new Models.CodeData();
        }
        private ICSharpCode.NRefactory.CSharp.SyntaxTree _parseFile(String filePath)
        {
            System.IO.StreamReader fileStream = new System.IO.StreamReader(filePath);
            return _codeData.Parser.Parse(fileStream);
        }

        private void _generateVisualAST(System.Windows.Controls.Grid mainGrid)
        {
            _generateVisualASTRecur(_codeData.AST, mainGrid);
        }
        int offsetX = 0;
        int offsetY = 0;

        void _generateVisualASTRecur(ICSharpCode.NRefactory.CSharp.AstNode node, System.Windows.Controls.Grid mainGrid, ICSharpCode.NRefactory.CSharp.AstNode parent = null)
        {
            ICSharpCode.NRefactory.CSharp.AstNode newParent = null;
            if (node.Children == null)
                return;
            #region Namespace
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.NamespaceDeclaration))
            {
                StringBuilder b = new StringBuilder();
                b.Append(node.Role.ToString());
                b.Append(":");
                b.Append(node.GetType().Name);
                Views.MainView.Nodes.NamespaceNode visualNode = new Views.MainView.Nodes.NamespaceNode();
                visualNode.Width = 400;
                visualNode.Height = 250;
                visualNode.Margin = new System.Windows.Thickness(offsetX, offsetY, 0, 0);
                offsetX += 400;
                if (offsetX > 4000)
                {
                    offsetX = 0;
                    offsetY += 250;
                }
                visualNode.SetNodeName(b.ToString());
                visualNode.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                visualNode.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                if (newParent == null)
                    mainGrid.Children.Add(visualNode);
               // else
                 //   newParent.Children.Add(visualNode);
                newParent = node;
            }
            #endregion
            #region Class
            #endregion
            #region Method
            #endregion
            #region Attribute
            #endregion
            foreach (var n in node.Children)
            _generateVisualASTRecur(n, mainGrid, newParent);
        }
        /*void _generateVisualASTRecur(ICSharpCode.NRefactory.CSharp.AstNode node, System.Windows.Controls.Grid mainGrid)
        {
#region Namespace Node
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.NamespaceDeclaration)) //Namespace Node
                /*Possibilité d'utiliser des BlockStatement pour délimiter les Namespaces* /
            {
                var namespaceDecl = (ICSharpCode.NRefactory.CSharp.NamespaceDeclaration)node;
                Views.MainView.Nodes.NamespaceNode visualNode = new Views.MainView.Nodes.NamespaceNode();
                visualNode.Width = 300;
                visualNode.Height = 250;
                visualNode.Margin = new System.Windows.Thickness(offsetX, offsetY, 0, 0);
                offsetX += 300;
                if (offsetX > 4000)
                {
                    offsetX = 0;
                    offsetY += 200;
                }
                var foo = new code_in.Views.MainView.Nodes.Items.NodeItem();
                foo.ItemName.Text = node.EndLocation.ToString();
                visualNode.SetNodeName(namespaceDecl.Name);
                visualNode.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                visualNode.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                mainGrid.Children.Add(visualNode);
            }
#endregion
#region Methode declaration Node
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.MethodDeclaration)) //Method declaration Node
            {

                Views.MainView.Nodes.FuncDeclNode visualNode = new Views.MainView.Nodes.FuncDeclNode();
                visualNode.Width = 300;
                visualNode.Height = 250;
                visualNode.Margin = new System.Windows.Thickness(offsetX, offsetY, 0, 0);
                offsetX += 300;
                if (offsetX > 4000)
                {
                    offsetX = 0;
                    offsetY += 200;
                }
                var foo = new code_in.Views.MainView.Nodes.Items.NodeItem();
                foo.ItemName.Text = node.ToString();
                ;
                visualNode.AddInput(foo);
                visualNode.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                visualNode.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                mainGrid.Children.Add(visualNode);
            }
#endregion
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.))

            // BIG SWITCH-CASE
            if (node.Children != null)
            {
                foreach (var n in node.Children)
                {
                    _generateVisualASTRecur(n, mainGrid);
                }
            }
        }
*/
        public void LoadFile(String filePath, System.Windows.Controls.Grid mainGrid)
        {
            _codeData.AST = _parseFile(filePath);
            _generateVisualAST(mainGrid);
        }

        public void SaveFile(String filePath)
        {

        }
    }
}
