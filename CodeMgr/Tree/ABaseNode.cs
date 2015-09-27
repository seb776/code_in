using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.CodeMgr.Tree
{
    public abstract class ABaseNode
    {
        protected String _name;
        public abstract void GenerateCode(StringBuilder sb);
    }
}
