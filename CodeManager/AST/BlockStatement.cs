using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAntlr.CST
{
    public abstract class BlockStatement : CSTNode
    {
        public abstract void GenerateCode(StringBuilder builder);
    }
}
