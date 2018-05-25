using BusinessRules.POC.ExternalData;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.RuleLearnDelFAMType66;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BusinessRules.POC.Tests
{
    public class ValidateLARNotionalNVQLevelRuleUnitTests
    {
        private IValidateLARNotionalNVQLevelRule _ValidateLARNotionalNVQLevelRule;
        public ValidateLARNotionalNVQLevelRuleUnitTests()
        {
            
        }

        [Trait("Category", "ValidateLARNotionalNVQ-Rule")]
        [Fact]
        public void InvalidLARSNVQValuesReturnsFalse()
        {
             
            var mockExternalData = new Mock<IExternalData<string, List<string>>>();
            mockExternalData.Setup(x => x.Get(It.IsAny<string>())).Returns(new List<string>() { "a", "b" });

            var mockRefData = new Mock<IReferenceData<string, string>>();
            mockRefData.Setup(x => x.Get(It.IsAny<string>())).Returns("A,1,2");

            _ValidateLARNotionalNVQLevelRule = new ValidateLARNotionalNVQLevelRule(mockExternalData.Object, mockRefData.Object);

            var parameter = new LearningDelivery()
            {
                LearnAimRef = ""
            };

            var actual = _ValidateLARNotionalNVQLevelRule.Evaluate(parameter);
            Assert.False(actual);
        }

        [Fact]
        [Trait("Category", "ValidateLARNotionalNVQ-Rule")]
        public void ValidLARSNVQValuesReturnsTrue()
        {

            var mock = new Mock<IExternalData<string, List<string>>>();
            mock.Setup(x => x.Get(It.IsAny<string>())).Returns(new List<string>() { "E", "b" });

            var mockRefData = new Mock<IReferenceData<string, string>>();
            mockRefData.Setup(x => x.Get(It.IsAny<string>())).Returns("E,1");

            _ValidateLARNotionalNVQLevelRule = new ValidateLARNotionalNVQLevelRule(mock.Object, mockRefData.Object);

            var parameter = new LearningDelivery()
            {
                LearnAimRef = ""
            };

            var actual = _ValidateLARNotionalNVQLevelRule.Evaluate(parameter);
            Assert.True(actual);
        }
    }
}
