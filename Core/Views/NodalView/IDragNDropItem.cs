﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_in.Views.NodalView
{
    public interface IDragNDropItem : ICodeInVisual
    {
        void SelectHighLight(bool highlighetd);
    }
}
