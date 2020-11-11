using NeeqDMIs.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeeqDMIs.Template
{
    public static class RackTemplate
    {
        private static DMIBoxTemplate dmibox;
        public static DMIBoxTemplate DMIBox { get => dmibox; set => dmibox = value; }
    }
}
