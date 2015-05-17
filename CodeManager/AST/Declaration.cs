using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAntlr.CST
{
    public class Declaration : Expression
    {
        public string kind;
        public Type _type;

        public Declaration(string kindDecl, CST.Type type, string name)
        {
            _type = type;
            Name = name;
            kind = kindDecl;
        }

        public override void GenerateCode(StringBuilder builder)
        {
            builder.Append(kind).Append(" ").Append(Name).Append(" As ").Append(_type.Name);
        }
    }
}
