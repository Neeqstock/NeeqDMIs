using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeeqDMIs.Utils.ValueFilters
{
    public interface ILongFilter
    {
        void Push(long value);
        long Pull();
    }
}
