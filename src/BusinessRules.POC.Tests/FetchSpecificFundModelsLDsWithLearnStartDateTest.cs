using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.RuleLearnDelFAMType66;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessRules.POC.Tests
{
    public class FetchSpecificFundModelsLDsWithLearnStartDateTest
    {
        private FetchSpecificFundModelsLDsWithLearnStartDate _rule;

        public FetchSpecificFundModelsLDsWithLearnStartDateTest()
        {
            var mockRefData = new Mock<IReferenceData<string, string>>();
            mockRefData.Setup(x => x.Get(It.IsAny<string>())).Returns("2016-08-01");

            _rule = new FetchSpecificFundModelsLDsWithLearnStartDate(mockRefData.Object);
        }

        [Fact]
        [Trait("Category", "LearnDelFAMType66-Rule")]
        public void FundModelNot35_Returns_ZeroList()
        {

            var parameter = new Learner()
            {
                LearningDeliveries = new List<LearningDelivery>()
                {
                    new LearningDelivery()
                    {
                        FundModel = 36,
                        LearnStartDate = new DateTime(2016,05,5)
                    },
                    new LearningDelivery()
                    {
                        FundModel = 39,
                        LearnStartDate = new DateTime(2017,05,5)
                    }
                }
            };

            var actual = _rule.Evaluate(parameter);

            Assert.Empty(actual);
        }
        
        [Fact]
        [Trait("Category", "LearnDelFAMType66-Rule")]
        public void FundModel35_Returns_OneItemList()
        {
            var parameter = new Learner()
            {
                LearningDeliveries = new List<LearningDelivery>()
                {
                    new LearningDelivery()
                    {
                        FundModel = 36,
                        LearnStartDate = new DateTime(2016,05,5)
                    },
                    new LearningDelivery()
                    {
                        FundModel = 35,
                        LearnStartDate = new DateTime(2017,09,5)
                    }
                }
            };

            var actual = _rule.Evaluate(parameter);

            Assert.Single(actual);
        }
    }
}
