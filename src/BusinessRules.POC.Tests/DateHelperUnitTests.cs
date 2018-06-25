using BusinessRules.POC.Helpers;
using System;
using System.Collections.Generic;
using Xunit;
using System.Collections;

namespace BusinessRules.POC.Tests
{
    public class DateHelperUnitTests
    {
        private readonly IDateHelper _dateHelper;

        public DateHelperUnitTests()
        {
            _dateHelper = new DateHelper();
        }

        [Trait("Category", "DateHelperRules")]
        [Theory]
        [InlineData(1, 2017, "2017-01-27")]
        [InlineData(2, 2017, "2017-02-24")]
        [InlineData(03, 2017, "2017-03-31")]
        [InlineData(4, 2017, "2017-04-28")]
        [InlineData(5, 2017, "2017-05-26")]
        [InlineData(12, 2016, "2016-12-30")]
        [InlineData(11, 2016, "2016-11-25")]
        [InlineData(6, 2016, "2016-06-24")]
        public void GetLastFridayInMonthTest(int month, int year, string expectedDate)
        {
            var date = new DateTime(year, month, 1);
            var expectedValue = DateTime.Parse(expectedDate);

            while (date.Month == month)
            {
                var result = _dateHelper.GetLastFridayInMonth(date);
                Assert.Equal(expectedValue, result);
                date = date.AddDays(1);
            }
        }

        [Trait("Category", "DateHelperRules")]
        [Theory]
        [ClassData(typeof(CalAgeTestDataGenerator))]
        public void CalculateAgeBasedOnReference(DateTime referenceDate, DateTime doB, int expectedAge)
        {
            var actualAge = _dateHelper.GetAge(referenceDate, doB);
            Assert.Equal(expectedAge, actualAge);

        }

        [Trait("Category", "DateHelperRules")]
        [Theory]
        [ClassData(typeof(FindYearTestDataGenerator))]
        public void FindYearInWhichUserTurnsToGivenAge(int ageTurningTo, DateTime doB, int expectedYear)
        {
            var actualYear = _dateHelper.GetYearInWhichPersonTurnsTo(ageTurningTo, doB);
            Assert.Equal(expectedYear, actualYear);
        }
    }

    public class CalAgeTestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { new DateTime(2017,12,01), new DateTime(1982,10,01), 35 },
            new object[] { new DateTime(2017,10,01), new DateTime(1982,11,01), 34 }
        };

        public IEnumerator<object[]> GetEnumerator() 
        {
            return _data.GetEnumerator();
        }
     
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class FindYearTestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { 35, new DateTime(1982,10,01), 2017 },
            new object[] { 34, new DateTime(1982,10,01),2016 }
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
