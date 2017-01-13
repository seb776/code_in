using code_in.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace code_in
{
    public interface IEnvironmentWrapper
    {
        T CreateAndAddView<T>(params object[] args) where T : UserControl, code_in.Views.ICode_inWindow;
        void CloseView<T>(T view) where T : UserControl, ICode_inWindow;
        //void RenameView<T>(T view, String name) where T : UserControl;
    }
}
