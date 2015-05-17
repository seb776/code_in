using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TestAntlr.CST
{
    public class VBCST : CSTNode
    {
        public List<Imports> _imports;
        public List<BlockStatement> _statements;

        public VBCST()
        {
            _imports = new List<Imports>();
            _statements = new List<BlockStatement>();
        }

        public virtual void GenerateCode(StringBuilder builder)
        {
            foreach (Imports i in _imports)
            {
                i.GenerateCode(builder);
            }
            foreach (BlockStatement s in _statements)
            {
                s.GenerateCode(builder);
            }
        }
    }
}
