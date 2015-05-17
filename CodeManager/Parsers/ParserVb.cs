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

                    // nom de la fonction

                    if (locTree.GetType().Equals(typeof(VisualBasic6Parser.AmbiguousIdentifierContext)))
                    {
                        stmt.Name = locTree.GetText();
                    }
                    
                    // liste des paramètres

                    else if (locTree.GetType().Equals(typeof(VisualBasic6Parser.ArgListContext)))
                    {
                        for (int j = 0; j != locTree.ChildCount; ++j)
                        {
                            if (locTree.GetChild(j).GetType().Equals(typeof(VisualBasic6Parser.ArgContext)))
                            {
                                for (int k = 0; k != locTree.GetChild(j).ChildCount; ++k)
                                {
                                    CST.Declaration dec = new CST.Declaration();
                                    if (locTree.GetChild(j).GetChild(k).GetType().Equals(typeof(VisualBasic6Parser.AmbiguousIdentifierContext)))
                                    {
                                        stmt._parameters.Add(dec);
                                        dec.Name = locTree.GetChild(j).GetChild(k).GetText();
                                    }
                                    if (locTree.GetChild(j).GetChild(k).GetType().Equals(typeof(VisualBasic6Parser.AsTypeClauseContext)))
                                    {
                                        for (int l = 0; l != locTree.GetChild(j).GetChild(k).ChildCount; ++l)
                                        {
                                            if (locTree.GetChild(j).GetChild(k).GetChild(l).GetType().Equals(typeof(VisualBasic6Parser.TypeContext)))
                                                dec._type.Name = locTree.GetChild(j).GetChild(k).GetChild(l).GetText();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // type de retour

                    else if (locTree.GetType().Equals(typeof(VisualBasic6Parser.AsTypeClauseContext)))
                    {
                        for (int j = 0; j != locTree.ChildCount; ++j )
                        {
                            if (locTree.GetChild(j).GetType().Equals(typeof(VisualBasic6Parser.TypeContext)))
                            {
                                stmt._returnType = new CST.Type();
                                stmt._returnType.Name = locTree.GetChild(j).GetText();
                            }
                        }
                    }

                    // instructions dans le bloc

                    else if (locTree.GetType().Equals(typeof(VisualBasic6Parser.BlockContext)))
                    {
                        for (int j = 0; j != locTree.ChildCount; ++j)
                        {
                            // Déclaration variable

                            if (locTree.GetChild(j).GetType().Equals(typeof(VisualBasic6Parser.BlockStmtContext)))
                            {
                               if (locTree.GetChild(j).GetChild(0).GetType().Equals(typeof(VisualBasic6Parser.VariableStmtContext)))
                               {
                                   for (int k = 0; k != locTree.GetChild(j).GetChild(0).ChildCount; ++k)
                                   {
                                       if (locTree.GetChild(j).GetChild(0).GetChild(k).GetType().Equals(typeof(VisualBasic6Parser.VariableListStmtContext)))
                                       {
                                           for (int l = 0; l != locTree.GetChild(j).GetChild(0).GetChild(k).GetChild(0).ChildCount; ++l)
                                           {
                                               CST.Declaration dec = new CST.Declaration();
                                               if (locTree.GetChild(j).GetChild(0).GetChild(k).GetChild(0).GetChild(l).GetType().Equals(typeof(VisualBasic6Parser.AmbiguousIdentifierContext)))
                                               {
                                                   // Stock nom de la variable déclaré
                                                   dec.Name = locTree.GetChild(j).GetChild(0).GetChild(k).GetChild(0).GetChild(l).GetText();
                                                   stmt._expressions.Add(dec);
                                               }

                                               if (locTree.GetChild(j).GetChild(0).GetChild(k).GetChild(0).GetChild(l).GetType().Equals(typeof(VisualBasic6Parser.AsTypeClauseContext)))
                                               {
                                                   for (int m = 0; m != locTree.GetChild(j).GetChild(0).GetChild(k).GetChild(0).GetChild(l).ChildCount; ++m)
                                                   {
                                                       if (locTree.GetChild(j).GetChild(0).GetChild(k).GetChild(0).GetChild(l).GetChild(m).GetType().Equals(typeof(VisualBasic6Parser.TypeContext)))
                                                       {
                                                           // Stock le type de la variable déclaré
                                                           dec._type.Name = locTree.GetChild(j).GetChild(0).GetChild(k).GetChild(0).GetChild(l).GetChild(m).GetText();
                                                       }
                                                   }
                                               }
                                           }
                                       }
                                   }
                               }

                               // Assignation variable
                               
                               else if (locTree.GetChild(j).GetChild(0).GetType().Equals(typeof(VisualBasic6Parser.LetStmtContext)))
                               {
                                   for (int k = 0; k != locTree.GetChild(j).GetChild(0).ChildCount; ++k)
                                   {
                                       CST.BinaryOp bin = new CST.BinaryOp("=", null, null);
                                       if (locTree.GetChild(j).GetChild(0).GetChild(k).GetType().Equals(typeof(VisualBasic6Parser.ImplicitCallStmt_InStmtContext)))
                                       {
                                           // nom de la variable
                                           bin._arguments[0] = new CST.Constant(locTree.GetChild(j).GetChild(0).GetChild(k).GetText());
                                           stmt._expressions.Add(bin);
                                       }

                                       if (locTree.GetChild(j).GetChild(0).GetChild(k).GetType().Equals(typeof(VisualBasic6Parser.VsLiteralContext)))
                                       {
                                           // valeur assigné
                                           bin._arguments[1] = new CST.Constant(locTree.GetChild(j).GetChild(0).GetChild(k).GetText());
                                       }
                                   }
                               }
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
                        //System.Windows.Forms.MessageBox.Show(tree.GetChild(0).GetChild(0).GetChild(i).GetType().ToString());
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
