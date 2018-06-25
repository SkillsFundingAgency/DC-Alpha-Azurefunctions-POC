using System;
using System.Collections.Generic;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.SharedRules;
using BusinessRules.POC.SharedRules.DD28;
using Moq;
using Xunit;

namespace BusinessRules.POC.Tests
{
    public class DD28RuleUnitTests
    {
        public DD28RuleUnitTests()
        {
        }

        [Fact]
        [Trait("Category", "DD28-Rule")]
        public void Dd28Rule_FundModelIsNot35_Returns_N()
        {
            //arrange
            var dd28PickMatchingEmpRecordMock = new Mock<IShortRule<DD28SubModel, LearnerEmploymentStatus>>();
            dd28PickMatchingEmpRecordMock.Setup(x => x.Evaluate(It.IsAny<DD28SubModel>()))
                .Returns(new LearnerEmploymentStatus()
                {
                    EmpStat = 15,
                    EmploymentStatusMonitorings = new List<EmploymentStatusMonitoring>()
                    {
                        new EmploymentStatusMonitoring()
                        {
                            ESMCode = "3",
                            ESMType = "Dummy"
                        }
                    }

                });

            var dd28Criteria1Mock = new Mock<IDD28RuleCriteria>();
            dd28Criteria1Mock.Setup(x => x.Evaluate(It.IsAny<DD28SubModel>())).Returns(false);

            var dd28Criteria2Mock = new Mock<IDD28RuleCriteria>();
            dd28Criteria2Mock.Setup(x => x.Evaluate(It.IsAny<DD28SubModel>())).Returns(false);

            var dd28Criteria3Mock = new Mock<IDD28RuleCriteria>();
            dd28Criteria3Mock.Setup(x => x.Evaluate(It.IsAny<DD28SubModel>())).Returns(false);

            var dd28ruleObj = new DD28Rule(dd28PickMatchingEmpRecordMock.Object,
                new List<IDD28RuleCriteria>() {dd28Criteria1Mock.Object, dd28Criteria2Mock.Object,
            dd28Criteria3Mock.Object });

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
                        ProgType = "2",
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

            //act
            var actual = dd28ruleObj.Evaluate(learner);

            //assert 
            Assert.Equal("N",actual);
        }

        [Fact]
        [Trait("Category", "DD28-Rule")]
        public void Dd28Rule_FundModelIs35_EMPStatESMTypeCodeAreValid_Returns_Y()
        {
            //arrange
            var dd28PickMatchingEmpRecordMock = new Mock<IShortRule<DD28SubModel, LearnerEmploymentStatus>>();
            dd28PickMatchingEmpRecordMock.Setup(x => x.Evaluate(It.IsAny<DD28SubModel>()))
                .Returns(new LearnerEmploymentStatus()
                {
                    EmpStat = 11,
                    EmploymentStatusMonitorings = new List<EmploymentStatusMonitoring>()
                    {
                        new EmploymentStatusMonitoring()
                        {
                            ESMCode = "2",
                            ESMType = "BSI"
                        }
                    }

                });

            var dd28Criteria1Mock = new Mock<IDD28RuleCriteria>();
            dd28Criteria1Mock.Setup(x => x.Evaluate(It.IsAny<DD28SubModel>())).Returns(false);

            var dd28Criteria2Mock = new Mock<IDD28RuleCriteria>();
            dd28Criteria2Mock.Setup(x => x.Evaluate(It.IsAny<DD28SubModel>())).Returns(true);

            var dd28Criteria3Mock = new Mock<IDD28RuleCriteria>();
            dd28Criteria3Mock.Setup(x => x.Evaluate(It.IsAny<DD28SubModel>())).Returns(false);


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
                        ProgType = "2",
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
            var dd28ruleObj = new DD28Rule(dd28PickMatchingEmpRecordMock.Object,
                new List<IDD28RuleCriteria>() {dd28Criteria1Mock.Object, dd28Criteria2Mock.Object,
                    dd28Criteria3Mock.Object });
            
            //act
            var actual = dd28ruleObj.Evaluate(learner);

            //assert 
            Assert.Equal("Y", actual);
        }
    }
}
