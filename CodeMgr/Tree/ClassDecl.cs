using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.CodeMgr.Tree
{
    public class ClassDecl : ADecl
    {
        protected enum Kind
        {
            STRUCT = 0,
            CLASS = 1
        }
        protected Kind _kind;
        override public void GenerateCode(StringBuilder sb) { }

    }
}
