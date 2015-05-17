using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAntlr.CST
{
    public class IfBlock : Expression
    {
        public List<IfStmt> _statements;

        public IfBlock()
        {
            _statements = new List<IfStmt>();
        }
        public override void GenerateCode(StringBuilder builder)
        {
            foreach (IfStmt s in _statements)
            {
                s.GenerateCode(builder);
            }
            builder.Append("End If\n");
        }
    }
}
