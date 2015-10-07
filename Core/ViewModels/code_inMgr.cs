using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.ViewModels
{
    public class code_inMgr
    {
        Views.MainView.MainView  _mainView;
        CodeMgr _codeMgr;
        public code_inMgr(Views.MainView.MainView mainView)
        {
            _codeMgr = new CodeMgr();
            _mainView = mainView;
        }

        public void LoadFile(String filePath)
        {
            _codeMgr.LoadFile(filePath, _mainView.MainGrid);
        }
    }
}
