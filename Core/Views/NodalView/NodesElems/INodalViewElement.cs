using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView.NodesElems
{
    /// <summary>
    /// This interface must be implemented for an UI element to be inserted to the nodal view.
    /// </summary>
    public interface INodalViewElement
    {
        INodalView NodalView
        {
            get;
            set;
        }
    }
}
