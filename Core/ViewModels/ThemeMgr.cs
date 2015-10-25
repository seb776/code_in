using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.ViewModels
{
    interface IThemeData { }

    public class ThemeMgr
    {
        private code_inMgr _mainMgr;

        public ThemeMgr(code_inMgr mainMgr) {
            this._mainMgr = mainMgr;
        }

        public void setTheme() {

        } 
    }
}
