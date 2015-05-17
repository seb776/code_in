using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAntlr.CST
{
    public class Constant : Operand
    {
        public Constant(string value)
        {
            Name = value;
        }
    }
}
