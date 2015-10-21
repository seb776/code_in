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
        void _generateVisualASTRecur(ICSharpCode.NRefactory.CSharp.AstNode node, System.Windows.Controls.Grid mainGrid)
        {
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.NamespaceDeclaration))
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
                var foo = new IOItem();
                foo.Label.Content = node.EndLocation;
                visualNode.SetNodeName(namespaceDecl.Name);
                visualNode.AddInput(foo);
                visualNode.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                visualNode.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                mainGrid.Children.Add(visualNode);
            }
            else if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.MethodDeclaration))
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
                var foo = new IOItem();
                foo.Label.Content = node;
                ;
                visualNode.AddInput(foo);
                visualNode.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                visualNode.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                mainGrid.Children.Add(visualNode);
            }
            // BIG SWITCH-CASE
            if (node.Children != null)
            {
                foreach (var n in node.Children)
                {
                    _generateVisualASTRecur(n, mainGrid);
                }
            }
        }

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
