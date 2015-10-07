using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Models
{
    public class CodeData
    {
        public ICSharpCode.NRefactory.CSharp.CSharpParser Parser
        {
            get;
            private set;
        }
        public ICSharpCode.NRefactory.CSharp.SyntaxTree AST;


        public CodeData()
        {
            Parser = new ICSharpCode.NRefactory.CSharp.CSharpParser();
            AST = null;
        }

    }
}
