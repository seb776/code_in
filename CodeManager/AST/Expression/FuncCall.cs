using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAntlr.CST
{
    public class FuncCall : Expression
    {
        public FuncCall(string name, List<Operand> o)
        {
            Name = name;
            _arguments = (o == null ? new List<Operand>() : o);
        }
        public override void GenerateCode(StringBuilder builder)
        {
            builder.Append(Name).Append("(");
            if (_arguments != null)
            {
                foreach (Operand o in _arguments)
                {
                    o.GenerateCode(builder);
                    if (o != _arguments.Last())
                        builder.Append(", ");
                }
            }
            builder.Append(")");
        }
    }
}
