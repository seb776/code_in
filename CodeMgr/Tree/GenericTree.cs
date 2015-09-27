using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.CodeMgr.Tree
{
    public class GenericTree
    {
        List<PreDecl> _preDecl;
        List<AExtendedDecl> decls;

        public GenericTree()
        {

        }

        public String GenerateTree()
        {
            StringBuilder   sb = new StringBuilder();

            foreach (PreDecl decl in _preDecl)
            {
                decl.GenerateCode(sb);
            }
            return sb.ToString();
        }
    }
}
