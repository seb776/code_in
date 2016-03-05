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
        public SyntaxTree AST;

        public NodalModel(SyntaxTree ast)
        {
            AST = ast;
        }

    }
}
