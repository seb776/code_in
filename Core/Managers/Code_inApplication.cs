using code_in.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in
{
    // This stores the managers of code_in
    public static class Code_inApplication
    {
        public static void StartApplication(IEnvironmentWrapper wrapper)
        {
            System.Diagnostics.Debug.Assert(wrapper != null, "You must give a valide wrapper in order to start the application !");
            _environmentWrapper = wrapper;
        }
        public static IEnvironmentWrapper _environmentWrapper = null;
        private static ThemeMgr _themeMgr = null;

        public static ThemeMgr ThemeMgr
        {
            get
            {
                if (_themeMgr == null)
                    _themeMgr = new ThemeMgr();
                return _themeMgr;
            }
        }
        public static IEnvironmentWrapper EnvironmentWrapper
        {
            get
            {
                return _environmentWrapper;
            }
        }
    }
}
