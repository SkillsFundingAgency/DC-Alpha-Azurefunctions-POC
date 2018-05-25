using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules
{
    public class SayHello
    {
        public string Display(string name)
        {           
            return $"Hello {name}!"; 
        }
    }
}
