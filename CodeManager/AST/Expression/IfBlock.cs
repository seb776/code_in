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

        public override void GenerateCode(StringBuilder builder)
        {
            foreach (IfStmt s in _statements)
            {
                s.GenerateCode(builder);
            }
        }
    }
}
