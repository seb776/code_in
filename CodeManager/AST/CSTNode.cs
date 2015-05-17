using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAntlr.CST
{
    public class CSTNode
    {
        public string Name;
        public virtual void GenerateCode(StringBuilder builder)
        {
            builder.Append(Name);
        }
    }
}
