using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAntlr.CST
{
    public class FuncBlockStmt : BlockStatement
    {
        public FuncBlockStmt(string name, CST.Type returnType, List<Declaration> parameters)
        {
            Name = name;
            _returnType = returnType;
            _parameters = (parameters == null ? new List<Declaration>() : parameters);
            _expressions = new List<Expression>();
        }
        public CST.Type _returnType;
        public List<Declaration> _parameters;
        public List<Expression> _expressions;

        public void AddExpression(Expression e)
        {
            _expressions.Add(e);
        }

        public override void GenerateCode(StringBuilder builder)
        {
            string kind = (_returnType == null ? "Sub " : "Function ");
            builder.Append(kind).Append(Name).Append("(");
            if (_parameters != null)
            {
                foreach (Declaration d in _parameters)
                {
                    d.GenerateCode(builder);
                    if (d != _parameters.Last())
                        builder.Append(", ");
                }
            }
            builder.Append(")");
            if (_returnType != null)
                builder.Append(" As ").Append(_returnType.Name);
            builder.Append("\n");
            foreach (Expression e in _expressions)
            {
                e.GenerateCode(builder);
                builder.Append("\n");
            }

            builder.Append("End ").Append(kind).Append("\n");
        }
    }
}
