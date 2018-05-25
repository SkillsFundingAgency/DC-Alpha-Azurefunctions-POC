using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.SharedRules
{
    public class LearnerDoBShouldNotBeNullRule : ISharedRule<Learner, bool>
    {

        public LearnerDoBShouldNotBeNullRule()
        {

        }

        public bool Evaluate(Learner objectToValidate)
        {
            return objectToValidate.DateOfBirth == null ? true : false;
        }


    }
}
