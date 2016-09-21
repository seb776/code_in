using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Exceptions
{
    /// <summary>
    /// This class is an exception thrown if we have the default constructor used to
    /// instante a visual element. (see ICodeInVisual)
    /// </summary>
    class DefaultCtorVisualException : Exception
    {
        public DefaultCtorVisualException() :
            this("z0rg: You shall not pass ! (Never use the Default constructor, if this shows up it's probably because you let something in the xaml and it should not be there)")
        {
        }
        public DefaultCtorVisualException(string msg) :
            base(msg)
        {
        }
    }
}
