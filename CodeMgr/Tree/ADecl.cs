using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.CodeMgr.Tree
{
    public abstract class ADecl : AExtendedDecl
    {
        protected Type _type;
        protected enum Scope
        {
            PUBLIC = 0,
            PRIVATE = 1,
            PROTECTED = 2
        }
        protected enum Modifier
        {
            CONST = 0,
            STATIC = 1,
        }
        protected Scope _scope;
    }
}
