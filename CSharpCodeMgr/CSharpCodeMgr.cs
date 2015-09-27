using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace code_in.CSharpCodeMgr
{
    public class CSharpCodeMgr : CodeMgr.ACodeMgr
    {
        private CodeDomProvider _provider;

        public CSharpCodeMgr()
        {
            _provider = new CSharpCodeProvider(/*providerOptions*/);
        }

        override public CodeMgr.Tree.GenericTree GenerateTreeFromFile(String filePath)
        {
            //String[] assemblies = { "System.Core.dll" };
            //CompilerParameters compilerParams = new CompilerParameters(assemblies);
            //CompilerResults compilerRes;
            //System.CodeDom.CodeCompileUnit compileUnit;
            //CodeMgr.Tree.GenericTree genericTree = new CodeMgr.Tree.GenericTree();

            //compileUnit = _provider.Parse(new System.IO.StreamReader(filePath));
            //if (compilerRes.Errors.Count != 0)
            //{
            //    foreach (var err in compilerRes.Errors)
            //    {
            //        throw new Exception(err.ToString());
            //    }
            //    throw new Exception("There are few errors during parsing of " + filePath);
            //}
            return genericTree;
        }
    }
}
