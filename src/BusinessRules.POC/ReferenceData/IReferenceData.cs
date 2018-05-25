using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.ReferenceData
{
    public interface IReferenceData<T, TResult>
    {
        TResult Get(T lookupKey);
    }
}
