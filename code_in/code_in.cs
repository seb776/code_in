using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in
{
    public class CodeIn
    {
        public TestAntlr.ParserVb _parser;
        public UserControl1 _window;

        public CodeIn(UserControl1 win, string filePath)
        {
            _window = win;
            _parser = new TestAntlr.ParserVb(filePath);
            _parser.ConvertCSTToAST();
            _parser.affTree();
            update();
        }
        static double pos = 5.0;
        public void update()
        {
            foreach (TestAntlr.CST.Imports i in _parser.vbcst._imports)
            {
                pos += 100.0;
                WPF.testNode n = new WPF.testNode(_window.grid_win, 100.0, pos);

                n.Title.Text = "Imports";
                n.Title.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.AliceBlue);
                n.spLeft.Children.Clear();
                n.spRight.Children.Clear();
                WPF.ItemNode item = new WPF.ItemNode();
                item.Text.Text = i.Name;
                n.spLeft.Children.Add(item);
                _window.grid_win.Children.Add(n);
            }
            pos = -200.0;
            foreach (TestAntlr.CST.BlockStatement stmt in _parser.vbcst._statements)
            {
                if (stmt.GetType().Equals(typeof(TestAntlr.CST.FuncBlockStmt)))
                {
                    TestAntlr.CST.FuncBlockStmt f = (TestAntlr.CST.FuncBlockStmt)stmt;
                    pos += 350.0;
                    WPF.testNode n = new WPF.testNode(_window.grid_win, 450.0, pos);

                    n.Grid.Width = 300.0;
                    n.Grid.Height = 300.0;
                    n.Title.Text = "Function " + f.Name;

                    n.spLeft.Children.Clear();
                    WPF.ItemNode itemFlowIn = new WPF.ItemNode();
                    itemFlowIn.Circle.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.GreenYellow);
                    itemFlowIn.Text.Text = "ExecutionIn";
                    n.spLeft.Children.Add(itemFlowIn);
                    foreach (var param in f._parameters)
                    {
                        WPF.ItemNode item = new WPF.ItemNode();
                        item.Text.Text = param._type.Name + " " + param.Name;
                        n.spLeft.Children.Add(item);
                    }
                    n.spRight.Children.Clear();
                    WPF.ItemNode itemFlowExec = new WPF.ItemNode();

                    itemFlowExec.Text.Text = "ExecutionOut";
                    itemFlowExec.Circle.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.GreenYellow);
                    itemFlowExec.Orientation = 1;
                    n.spRight.Children.Add(itemFlowExec);
                    if (f._returnType != null)
                    {
                        WPF.ItemNode itemRet = new WPF.ItemNode();
                        itemRet.Text.Text = f._returnType.Name;
                        itemRet.Orientation = 1;
                        n.spRight.Children.Add(itemRet);
                    }
                    _window.grid_win.Children.Add(n);
                    foreach (var e in f._expressions)
                    {
                        pos += 150.0;
                        WPF.testNode node = new WPF.testNode(_window.grid_win, 450.0, pos + 10.0);

                        node.spLeft.Children.Clear();
                        node.spRight.Children.Clear();

                        if (e.GetType().Equals(typeof(TestAntlr.CST.Declaration)))
                        {
                            node.Title.Text = "Dim " + ((TestAntlr.CST.Declaration)e)._type.Name;

                            WPF.ItemNode outVal = new WPF.ItemNode();
                            outVal.Orientation = 1;
                            outVal.Text.Text = e.Name;

                            node.spRight.Children.Add(outVal);
                        }
                        if (e.GetType().Equals(typeof(TestAntlr.CST.BinaryOp)))
                        {
                            node.Title.Text = "Binary Operator: " + e.Name;

                            WPF.ItemNode    execIn = new WPF.ItemNode();
                            WPF.ItemNode    execOut = new WPF.ItemNode();

                            execIn.Circle.Fill = execOut.Circle.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.GreenYellow);

                            execOut.Orientation = 1;

                            execIn.Text.Text = "ExecutionIn";
                            execOut.Text.Text = "ExecutionOut";

                            WPF.ItemNode p1 = new WPF.ItemNode();
                            WPF.ItemNode p2 = new WPF.ItemNode();

                            WPF.ItemNode ret = new WPF.ItemNode();

                            p1.Text.Text = e._arguments[0].Name;
                            p2.Text.Text = "42";

                            ret.Text.Text = "ReturnValue";
                            ret.Orientation = 1;

                            node.spLeft.Children.Add(execIn);
                            node.spLeft.Children.Add(p1);
                            node.spLeft.Children.Add(p2);

                            node.spRight.Children.Add(execOut);
                            node.spRight.Children.Add(ret);

                        }
                        _window.grid_win.Children.Add(node);
                    }
                }
            }
        }
    }
}
