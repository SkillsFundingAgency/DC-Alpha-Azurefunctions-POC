using System;
using System.Collections.Generic;
using BusinessRules.POC.ExternalData;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.SharedRules.DD29;
using Moq;
using Xunit;

namespace BusinessRules.POC.Tests
{
    public class DD29RuleTests
    {
        public DD29RuleTests()
        {
        }

        [Fact]
        [Trait("Category", "DD29-Rule")]
        public void ProgTypeNot24_Returns_N()
        {
            //arrange
            var larsExtDataMock = new Mock<IExternalData<string, List<string>>>();
            larsExtDataMock.Setup(x => x.Get(It.IsAny<string>())).Returns(It.IsAny<List<string>>());

            var refDataMock = new Mock<IReferenceData<string, string>>();
            refDataMock.Setup(x => x.Get(It.IsAny<string>())).Returns("24");

            var learner = new Learner()
            {
                LearnerEmploymentStatuses = new List<LearnerEmploymentStatus>()
                {
                    new LearnerEmploymentStatus()
                    {
                        DateEmpStatApp = new DateTime(2016, 08, 15)
                    }

                },
                LearningDeliveries = new List<LearningDelivery>()
                {
                    new LearningDelivery()
                    {
                        AimType = 1,
                        AimSeqNumber = 100,
                        FworkCode = "549",
                        ProgType = "14",
                        PwayCode = "1",
                        LearnAimRef = "ZPROG001",
                        DateOfBirth = new DateTime(1982,01,01),
                        LearnStartDate = new DateTime(2011, 05, 15)
                    },
                    new LearningDelivery()
                    {
                        AimType = 2,
                        AimSeqNumber = 100,
                        FworkCode = "549",
                        ProgType = "2",
                        PwayCode = "1",
                        LearnAimRef = "60005623",
                        DateOfBirth = new DateTime(1982,01,01),
                        LearnStartDate = new DateTime(2011, 05, 15)
                    },
                }
            };

            var dd29Rule = new DD29Rule(larsExtDataMock.Object, refDataMock.Object);

            //act
            var actual = dd29Rule.Evaluate(It.IsAny<Learner>());

            //assert
            Assert.Equal("N", actual);
        }

        [Fact]
        [Trait("Category", "DD29-Rule")]
        public void ProgType24_LARS_VALIDvalue_Returns_Y()
        {
            //arrange
            var larsExtDataMock = new Mock<IExternalData<string, List<string>>>();
            larsExtDataMock.Setup(x => x.Get(It.IsAny<string>())).Returns(new List<string>()
            {
                "2", "4"
            });

            var refDataMock = new Mock<IReferenceData<string, string>>();
            refDataMock.Setup(x => x.Get(It.Is<string>(p=> p== AppConstants.DD29LearningDeliveryProgType))).Returns( "24");
            refDataMock.Setup(x => x.Get(It.Is<string>(p => p == AppConstants.DD29LARSCategoryRef))).Returns("2,4");

            var learner = new Learner()
            {
                LearnerEmploymentStatuses = new List<LearnerEmploymentStatus>()
                {
                    new LearnerEmploymentStatus()
                    {
                        DateEmpStatApp = new DateTime(2016, 08, 15)
                    }

                },
                LearningDeliveries = new List<LearningDelivery>()
                {
                    new LearningDelivery()
                    {
                        AimType = 1,
                        AimSeqNumber = 100,
                        FworkCode = "549",
                        ProgType = "24",
                        PwayCode = "1",
                        LearnAimRef = "ZPROG001",
                        DateOfBirth = new DateTime(1982,01,01),
                        LearnStartDate = new DateTime(2011, 05, 15)
                    },
                    new LearningDelivery()
                    {
                        AimType = 2,
                        AimSeqNumber = 100,
                        FworkCode = "549",
                        ProgType = "2",
                        PwayCode = "1",
                        LearnAimRef = "60005623",
                        DateOfBirth = new DateTime(1982,01,01),
                        LearnStartDate = new DateTime(2011, 05, 15)
                    },
                }
            };

            var dd29Rule = new DD29Rule(larsExtDataMock.Object, refDataMock.Object);

            //act
            var actual = dd29Rule.Evaluate(learner);

            //assert
            Assert.Equal("Y", actual);
        }
    }
}
