using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Views.MainView.Nodes.BaseNode visualNode = new Views.MainView.Nodes.BaseNode();
            visualNode.Width = 300;
            visualNode.Height = 250;
            visualNode.Margin = new System.Windows.Thickness(offsetX, offsetY, 0, 0);
            offsetX += 300;
            if (offsetX > 4000)
            {
                offsetX = 0;
                offsetY += 200;
            }

            visualNode.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            visualNode.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            mainGrid.Children.Add(visualNode);
            // BIG SWITCH-CASE
            if (node.Children != null)
            {
                foreach(var n in node.Children)
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
