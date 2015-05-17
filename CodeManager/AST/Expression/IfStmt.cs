using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAntlr.CST
{
    public class IfStmt : CSTNode
    {
        public enum CompType
        {
            IF = 0,
            ELSEIF = 1,
            ELSE = 2
        };

        public IfStmt(CompType t, ComparisonStmt stmt = null)
        {
            _compType = t;
            _comp = stmt;
            _expressions = new List<Expression>();
        }

        public ComparisonStmt _comp;
        public CompType _compType;
        public List<Expression> _expressions;

        public override void GenerateCode(StringBuilder builder)
        {
            builder.Append("If ");
            _comp.GenerateCode(builder);
            builder.Append("Then\n");
            foreach (Expression e in _expressions)
            {
                e.GenerateCode(builder);
                builder.Append("\n");
            }
        }
    }
}
