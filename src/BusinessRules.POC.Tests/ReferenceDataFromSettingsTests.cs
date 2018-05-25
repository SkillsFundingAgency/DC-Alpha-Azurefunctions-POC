using BusinessRules.POC.ReferenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BusinessRules.POC.Tests
{
    public class ReferenceDataFromSettingsTests
    {

        public static IEnumerable<object[]> GetLookupKeysWithExpectedValues()
        {
            yield return new object[] { "ApprenticeProgTypes", "2, 3, 20, 21, 2, 23, 25" };
            yield return new object[] { "ApprencticeProgAllowedStartDate", "2016-08-01" };
        }

      

        [Theory]
        [Trait("Category", "ReferenceData-Rule")]
        [MemberData(nameof(GetLookupKeysWithExpectedValues))]
        public void GetValuesBasedOnLookupkeys(string lookupKey, string expectedResult)
        {
            //arrange
            var refdataObject = new ReferenceDataFromSettingsFile();

            //act
            var actualResult = refdataObject.Get(lookupKey);

            //assert
            Assert.NotEmpty(actualResult);
            Assert.Equal(expectedResult, actualResult);
        }


     
    }
}
