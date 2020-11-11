using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeeqDMIs.Utils.ValueFilters
{
    public interface IDoubleFilter
    {
        void Push(double value);
        double Pull();
    }
}
