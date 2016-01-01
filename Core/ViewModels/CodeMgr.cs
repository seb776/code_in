using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using code_in.Views.MainView.Nodes;
using ICSharpCode.NRefactory.TypeSystem;

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

        // (z0rg)TODO: Big refacto
        // - Create Node through mainview
        // - Improve code interface of nodes management
        // - Make this function more generic
        //      - avoid setting the name of each node depending of it's type, almost all nodes have a name
        //      - improve interface to make the parent totally transparent
        //      - ...
        // - Improve design to make the node alignement after the parsing
        void _generateVisualASTRecur(ICSharpCode.NRefactory.CSharp.AstNode node, System.Windows.Controls.Grid mainGrid,  Views.MainView.Nodes.BaseNode parent = null)
        {
            Views.MainView.Nodes.BaseNode newParent = null;
            if (node.Children == null)
                return;
            #region Namespace
            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.NamespaceDeclaration))
            {
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
                var tmpNode = (ICSharpCode.NRefactory.CSharp.NamespaceDeclaration)node;
                visualNode.SetNodeName(tmpNode.Name);
                visualNode.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                visualNode.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                if (newParent == null)
                    mainGrid.Children.Add(visualNode);
                else
                    newParent.NodeGrid.Children.Add(visualNode);
                newParent = visualNode;
            }
            #endregion
            #region Class

            if (node.GetType() == typeof(ICSharpCode.NRefactory.CSharp.TypeDeclaration))
            {
                Views.MainView.Nodes.ClassDeclNode visualNode = new Views.MainView.Nodes.ClassDeclNode();
                visualNode.Width = 400;
                visualNode.Height = 250;
                visualNode.Margin = new System.Windows.Thickness(offsetX, offsetY, 0, 0);
                offsetX += 400;
                if (offsetX > 4000)
                {
                    offsetX = 0;
                    offsetY += 250;
                }
                var tmpNode = (ICSharpCode.NRefactory.CSharp.TypeDeclaration)node;
                switch (tmpNode.Modifiers.ToString()) // Puts the right scope
                {
                    case "Public":
                        visualNode.NodeScope.Scope = Views.MainView.Nodes.Items.ScopeItem.EScope.PUBLIC;
                        break;
                    case "Private":
                        visualNode.NodeScope.Scope = Views.MainView.Nodes.Items.ScopeItem.EScope.PUBLIC;
                        break;
                    case "Protected":
                        visualNode.NodeScope.Scope = Views.MainView.Nodes.Items.ScopeItem.EScope.PROTECTED;
                        break;
                    default:
                        break;
                }
                visualNode.SetNodeName(tmpNode.Name);
                visualNode.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                visualNode.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                if (newParent == null)
                    mainGrid.Children.Add(visualNode);
                else
                    newParent.NodeGrid.Children.Add(visualNode);
                newParent = visualNode;
            }
            #endregion
            #region Method
            #endregion
            #region Attribute
            #endregion
            foreach (var n in node.Children)
                _generateVisualASTRecur(n, mainGrid, newParent);
        }

        public void LoadFile(String filePath, System.Windows.Controls.Grid mainGrid)
        {
            _codeData.AST = _parseFile(filePath);
            _generateVisualAST(mainGrid);
            // Do node placement:
            // - auto
            // - from file
        }

        public void SaveFile(String filePath)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath);
            String code = "// Generated by Visual Studio's Code_in.";
            sw.WriteLine(code);
            sw.Write(_codeData.AST.ToString());
        }
    }
}
