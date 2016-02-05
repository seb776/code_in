using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.ViewModels
{
    public class code_inMgr
    {
        public Views.MainView.MainView  _mainView;
        public ThemeMgr                 _themeMgr;
        public CodeMgr                  _codeMgr;
        public code_inMgr(Views.MainView.MainView mainView)
        {
            _codeMgr = new CodeMgr(this);
            _mainView = mainView;
            _themeMgr = new ThemeMgr(this);
        }

        public void LoadFile(String filePath)
        {
            _codeMgr.LoadFile(filePath, null);
        }
    }
}
