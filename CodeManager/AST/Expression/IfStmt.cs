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
            _comp = (stmt == null ? new ComparisonStmt() : stmt);
            _expressions = new List<Expression>();
        }

        public ComparisonStmt _comp;
        public CompType _compType;
        public List<Expression> _expressions;

        public override void GenerateCode(StringBuilder builder)
        {
            if (_compType >= CompType.ELSEIF)
                builder.Append("Else");
            if (_compType <= CompType.ELSEIF)
                builder.Append("If ");
            if (_compType == CompType.ELSE)
                builder.Append(" ");
            _comp.GenerateCode(builder);
            if (_compType != CompType.ELSE)
                builder.Append(" Then\n");
            if (_expressions.Count == 0)
                builder.Append("\n");
            foreach (Expression e in _expressions)
            {
                e.GenerateCode(builder);
                builder.Append("\n");
            }
        }
    }
}
