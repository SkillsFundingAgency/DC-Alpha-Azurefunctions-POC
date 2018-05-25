using System.Collections.Generic;
using System;
namespace BusinessRules.POC.Data
{
    public class SharedData : ISharedData
    {

        private List<int> _apprenticeProgtypes = new List<int>() { 2, 3, 20, 21, 2, 23, 25 };
        private DateTime _ApprencticeProgAllowedStartDateDoB48 = new DateTime(2016, 08, 01);

        public SharedData()
        {
        }

        public List<int> ApprenticeProgTypes
        {
            get { return _apprenticeProgtypes; }       
            set { _apprenticeProgtypes = value; }
        }

        public DateTime ApprencticeProgAllowedStartDateDoB48
        {
            get { return _ApprencticeProgAllowedStartDateDoB48; }
            set { _ApprencticeProgAllowedStartDateDoB48 = value; }
        }

    }
}
