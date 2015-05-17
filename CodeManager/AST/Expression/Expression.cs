using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAntlr.CST
{
    public class Expression : Operand
    {
        public CST.Type _returnType;
        public List<Operand> _arguments;

        public override void GenerateCode(StringBuilder builder)
        {

        }
    }
}
