using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.Data
{
    public interface ISharedData
    {
        List<int> ApprenticeProgTypes { get; set; }
        DateTime ApprencticeProgAllowedStartDateDoB48 { get; set; }
    }
}
