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
                WPF.testNode n = new WPF.testNode(_window.grid_win, pos, pos);

                n.Title.Text = "Imports";
                n.Title.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.AliceBlue);
                n.spLeft.Children.Clear();
                n.spRight.Children.Clear();
                WPF.ItemNode item = new WPF.ItemNode();
                item.Text.Text = i.Name;
                n.spLeft.Children.Add(item);
                _window.grid_win.Children.Add(n);
            }
        }
    }
}
