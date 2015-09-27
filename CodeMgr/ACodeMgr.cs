using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.CodeMgr
{
    public abstract class ACodeMgr
    {
        abstract public Tree.GenericTree GenerateTreeFromFile(String filePath);
    }
}
