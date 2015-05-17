using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;


namespace TestAntlr
{
    public class ParserVb
    {
        public CST.VBCST vbcst;
        private string _fileName;
        IParseTree _tree;
        StreamWriter _out;

        private ParserVb()
        {
        }

        public ParserVb(string fileName)
        {
            vbcst = new CST.VBCST();
            _fileName = fileName;
            AntlrFileStream stream;
            try
            {
                stream = new AntlrFileStream(fileName);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("File not found !");
                return;
            }
            ITokenSource lexer = new VisualBasic6Lexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            VisualBasic6Parser parser = new VisualBasic6Parser(tokens);
            parser.BuildParseTree = true;
            _tree = parser.startRule();
            _out = new StreamWriter("testTree.txt");
        }

        public void ConvertCSTToAST()
        {
            convertCSTToAST(_tree, vbcst);
        }

        private void convertCSTToAST(IParseTree tree, CST.CSTNode cst)
        {
            if (!tree.GetType().Equals(typeof(VisualBasic6Parser.ModuleBodyElementContext)))
            {
                for (int i = 0; i != tree.ChildCount; i++)
                {
                    convertCSTToAST(tree.GetChild(i), cst);
                }
            }
            else
            {
                convertModuleBodyElementContext(tree);
            }
        }

        private void convertModuleBodyElementContext(IParseTree tree)
        {
            if (tree.GetChild(0).GetType().Equals(typeof(VisualBasic6Parser.SubStmtContext)) ||
                tree.GetChild(0).GetType().Equals(typeof(VisualBasic6Parser.FunctionStmtContext)))
            {
                CST.FuncBlockStmt stmt = new CST.FuncBlockStmt("", null, null);
                for (int i = 0; i != tree.GetChild(0).ChildCount; ++i)
                {
                    IParseTree locTree = tree.GetChild(0).GetChild(i);

                    if (locTree.GetType().Equals(typeof(VisualBasic6Parser.AmbiguousIdentifierContext)))
                    {
                        stmt.Name = locTree.GetText();
                    }

                    else if (locTree.GetType().Equals(typeof(VisualBasic6Parser.ArgListContext)))
                    {
                        for (int j = 0; j != locTree.ChildCount; ++j)
                        {
                            if (locTree.GetChild(j).GetType().Equals(typeof(VisualBasic6Parser.ArgContext)))
                            {
                                for (int k = 0; k != locTree.GetChild(j).ChildCount; ++k)
                                {
                                    CST.Declaration dec = new CST.Declaration();
                                    stmt._parameters.Add(dec);
                                    if (locTree.GetChild(j).GetChild(k).GetType().Equals(typeof(VisualBasic6Parser.AmbiguousIdentifierContext)))
                                    {
                                        // Stock le nom 
                                        dec.Name = locTree.GetChild(j).GetChild(k).GetText();
                                    }
                                    if (locTree.GetChild(j).GetChild(k).GetType().Equals(typeof(VisualBasic6Parser.AsTypeClauseContext)))
                                    {
                                        for (int l = 0; l != locTree.GetChild(j).GetChild(k).ChildCount; ++l)
                                        {
                                            if (locTree.GetChild(j).GetChild(k).GetChild(l).GetType().Equals(typeof(VisualBasic6Parser.TypeContext)))
                                            {
                                                // Stock le type
                                                dec._type.Name = locTree.GetChild(j).GetChild(k).GetChild(l).GetText();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    else if (locTree.GetType().Equals(typeof(VisualBasic6Parser.AsTypeClauseContext)))
                    {
                        for (int j = 0; j != locTree.ChildCount; ++j )
                        {
                            if (locTree.GetChild(j).GetType().Equals(typeof(VisualBasic6Parser.TypeContext)))
                            {
                                stmt._returnType = new CST.Type();
                                stmt._returnType.Name = locTree.GetChild(j).GetText();
                                break;
                            }
                        }
                    }
                }
                vbcst._statements.Add(stmt);
            }
            else if (tree.GetChild(0).GetType().Equals(typeof(VisualBasic6Parser.ModuleBlockContext)))
            {
                for (int i = 0; i != tree.GetChild(0).GetChild(0).ChildCount; ++i)
                {
                    if (tree.GetChild(0).GetChild(0).GetChild(i).GetType().Equals(typeof(VisualBasic6Parser.BlockStmtContext)))
                    {
                        CST.Imports import = new CST.Imports();
                        System.Windows.Forms.MessageBox.Show(tree.GetChild(0).GetChild(0).GetChild(i).GetType().ToString());
                        import.Name = getImport(tree.GetChild(0).GetChild(0).GetChild(i));
                        vbcst._imports.Add(import);
                    }
                }
            }
        }

        private string getImport(IParseTree tree)
        {
            string txt = "";
            if (tree.GetType().Equals(typeof(VisualBasic6Parser.ArgsCallContext)))
                return tree.GetText();
            for (int i = 0; i != tree.ChildCount; ++i)
            {
                txt = getImport(tree.GetChild(i));
                if (txt.Length != 0)
                    return txt;
            }
            return txt;
        }

        public void affTree()
        {
            affTree(_tree, 0);
        }

        private void affTree(IParseTree tree, int offset)
        {
            string space = "";
            string toWrite = "";

            for (int i = 0; i < offset; i++)
                space += " ";
            toWrite += space;
            toWrite += "|" + offset.ToString() + "[" + tree.GetType() + "]\n";
            toWrite += "(>" + tree.GetText() + "<)\n";
            _out.Write(toWrite);
            for (int i = 0; i != tree.ChildCount; i++)
            {
                affTree(tree.GetChild(i), offset + 1);
            }
        }
    }
}
