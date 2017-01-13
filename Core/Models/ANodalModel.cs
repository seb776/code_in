using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Models
{
    public interface INodalModel
    {
        bool IsSaved
        {
            get;
            set;
        }

        void Save();
    }
}
