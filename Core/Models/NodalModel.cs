using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Models.NodalModel
{
    public class NodalModel
    {
        private SyntaxTree _ast;
        public SyntaxTree AST
        {
            private set
            {
                _ast = value;
            }
            get
            {
                return _ast;
            }
        }

        public NodalModel(SyntaxTree ast)
        {
            AST = ast;
        }
    }
}
