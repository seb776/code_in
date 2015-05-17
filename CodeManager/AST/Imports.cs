using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAntlr.CST
{
    public class Imports : CSTNode
    {
        public virtual void GenerateCode(StringBuilder builder)
        {
            builder.Append("Imports ").Append(Name).Append("\n");
        }
    }
}
