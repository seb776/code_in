using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeManager
{
    public class Test
    {
        public void TestAST()
        {
            TestAntlr.CST.VBCST ast = new TestAntlr.CST.VBCST();

            ast._imports.Add(new TestAntlr.CST.Imports("Test"));
            ast._imports.Add(new TestAntlr.CST.Imports("System"));

            TestAntlr.CST.FuncBlockStmt funcMain = new TestAntlr.CST.FuncBlockStmt("Main", null, null);
            ast._statements.Add(funcMain);
            funcMain._parameters.Add(new TestAntlr.CST.Declaration("ByVal", new TestAntlr.CST.Type("Boolean"), "b"));
            funcMain._expressions.Add(new TestAntlr.CST.Declaration("Dim", new TestAntlr.CST.Type("Integer"), "testVar"));
            TestAntlr.CST.FuncCall call = new TestAntlr.CST.FuncCall("toto", null);
            List<TestAntlr.CST.Operand> args = new List<TestAntlr.CST.Operand>();
            args.Add(new TestAntlr.CST.BinaryOp("+", new TestAntlr.CST.Constant("42"), new TestAntlr.CST.FuncCall("zbam", null)));
            call._arguments.Add(new TestAntlr.CST.FuncCall("titi", args));
            funcMain._expressions.Add(call);
            

            StringBuilder builder = new StringBuilder();

            ast.GenerateCode(builder);
            System.Windows.Forms.MessageBox.Show(builder.ToString());
        }
    }
}
