using code_in.Models.NodalModel;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Models
{
    public class ExecutionNodalModel
    {
        private DeclarationsNodalModel _associatedFile = null;
        private AstNode _root;

        public ExecutionNodalModel(DeclarationsNodalModel assocFile, AstNode root)
        {
            Debug.Assert(assocFile != null);
            Debug.Assert(root != null);
            _associatedFile = assocFile;
            _root = root;
        }
    }
}
