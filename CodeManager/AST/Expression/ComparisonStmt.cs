using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAntlr.CST
{
    public class ComparisonStmt : Expression
    {
        public override void GenerateCode(StringBuilder builder)
        {
            builder.Append("0 = 0");
        }
    }
}
