using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views
{
    public interface ICode_inWindow
    {
        IEnvironmentWindowWrapper EnvironmentWindowWrapper
        {
            get;
            set;
        }
    }
}
