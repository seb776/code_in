using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAntlr.CST
{
    public class Type : CSTNode
    {
        public Type _subType;

        public Type()
        {
            Name = "UnknownType";
        }
        public Type(string type)
        {
            Name = type;
        }
    }
}
