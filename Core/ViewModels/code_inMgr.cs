using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.ViewModels
{
    public class code_inMgr
    {
        public CodeMgr.ACodeMgr _codeMgr;

        public code_inMgr()
        {
            _codeMgr = (new CSharpCodeMgr.CSharpCodeMgr()) as CodeMgr.ACodeMgr;
        }
    }
}
