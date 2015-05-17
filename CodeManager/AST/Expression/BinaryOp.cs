using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAntlr.CST
{
    public class BinaryOp : Expression
    {
        public BinaryOp(string opera, Operand a, Operand b)
        {
            _arguments = new List<Operand>();

            _arguments.Add(a);
            _arguments.Add(b);
            Name = opera;
        }

        public override void GenerateCode(StringBuilder builder)
        {
            _arguments[0].GenerateCode(builder);
            builder.Append(" ").Append(Name).Append(" ");
            if (_arguments[1] != null)
                _arguments[1].GenerateCode(builder);
        }
    }
}
