﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container
{
    public abstract class IFood
    {
        public abstract string type { get; set; }
        public IFood() { }
    }
}
