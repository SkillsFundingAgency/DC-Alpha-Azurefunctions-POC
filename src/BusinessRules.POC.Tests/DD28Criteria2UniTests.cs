using System.Collections.Generic;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.SharedRules.DD28;
using Moq;
using Xunit;

namespace BusinessRules.POC.Tests
{
    public class DD28Criteria2UniTests
    {
        private readonly Mock<IReferenceData<string, string>> _mock;

        public DD28Criteria2UniTests()
        {
            _mock = new Mock<IReferenceData<string, string>>();
            _mock.Setup(x => x.Get(It.Is<string>(k => k == AppConstants.DD28Criteria2LearnerEmplStatusEmpStats)))
                .Returns("10,11,12,98");
            _mock.Setup(x => x.Get(It.Is<string>(k => k == AppConstants.DD28Criteria2EmpStatusMonitoringESMType)))
                .Returns("BSI");
            _mock.Setup(x => x.Get(It.Is<string>(k => k == AppConstants.DD28Criteria2EmpStatusMonitoringESMCodes)))
                .Returns("1,2");
        }

        [Fact]
        [Trait("Category", "DD28-SubRule-Rule")]
        public void FundModel_Invalid_ReturnsFalse()
        {
            //arrange
            var dd28RuleFundModelAndEmpStatEmpCodeCheck = new DD28RuleCriteria2(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new LearningDelivery()
                {
                    FundModel = 34
                },
                LearnerEmploymentStatusObj = new List<LearnerEmploymentStatus>()
                {
                    new LearnerEmploymentStatus()
                    {
                        EmpStat = 11,
                        EmploymentStatusMonitorings = new List<EmploymentStatusMonitoring>()
                        {
                            new EmploymentStatusMonitoring()
                            {
                                ESMCode = "3",
                                ESMType = "BSI"
                            },
                            new EmploymentStatusMonitoring()
                            {
                                ESMCode = "1",
                                ESMType = "DUmmy"
                            }
                        }
                    }
                }
            };

            //act
            var actual = dd28RuleFundModelAndEmpStatEmpCodeCheck.Evaluate(param);

            //assert
            Assert.False(actual);
        }

        [Fact]
        [Trait("Category", "DD28-SubRule-Rule")]
        public void Invalid_EMPStat_ReturnsFalse()
        {
            //arrange
            var dd28RuleFundModelAndEmpStatEmpCodeCheck = new DD28RuleCriteria2(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new LearningDelivery()
                {
                    FundModel = 35
                },
                LearnerEmploymentStatusObj = new List<LearnerEmploymentStatus>()
                {
                    new LearnerEmploymentStatus()
                    {
                        EmpStat = 110,
                        EmploymentStatusMonitorings = new List<EmploymentStatusMonitoring>()
                        {
                            new EmploymentStatusMonitoring()
                            {
                                ESMCode = "3",
                                ESMType = "BSI"
                            },
                            new EmploymentStatusMonitoring()
                            {
                                ESMCode = "1",
                                ESMType = "DUmmy"
                            }
                        }
                    }
                }
            };

            //act
            var actual = dd28RuleFundModelAndEmpStatEmpCodeCheck.Evaluate(param);

            //assert
            Assert.False(actual);
        }

        [Fact]
        [Trait("Category", "DD28-SubRule-Rule")]
        public void Invalid_ESMType_ReturnsFalse()
        {
            //arrange
            var dd28RuleFundModelAndEmpStatEmpCodeCheck = new DD28RuleCriteria2(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new LearningDelivery()
                {
                    FundModel = 35
                },
                LearnerEmploymentStatusObj = new List<LearnerEmploymentStatus>()
                {
                    new LearnerEmploymentStatus()
                    {
                        EmpStat = 11,
                        EmploymentStatusMonitorings = new List<EmploymentStatusMonitoring>()
                        {
                            new EmploymentStatusMonitoring()
                            {
                                ESMCode = "1",
                                ESMType = "Dummy2"
                            },
                            new EmploymentStatusMonitoring()
                            {
                                ESMCode = "1",
                                ESMType = "DUmmy"
                            }
                        }
                    }
                }
            };

            //act
            var actual = dd28RuleFundModelAndEmpStatEmpCodeCheck.Evaluate(param);

            //assert
            Assert.False(actual);
        }

        [Fact]
        [Trait("Category", "DD28-SubRule-Rule")]
        public void valid_Allvalues_ReturnsTrue()
        {
            //arrange
            var dd28RuleFundModelAndEmpStatEmpCodeCheck = new DD28RuleCriteria2(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new LearningDelivery()
                {
                    FundModel = 35
                },
                LearnerEmploymentStatusObj =
                    new List<LearnerEmploymentStatus>()
                    {
                        new LearnerEmploymentStatus()
                        {
                            EmpStat = 11,
                            EmploymentStatusMonitorings = new List<EmploymentStatusMonitoring>()
                            {
                                new EmploymentStatusMonitoring()
                                {
                                    ESMCode = "1",
                                    ESMType = "BSI"
                                },
                                new EmploymentStatusMonitoring()
                                {
                                    ESMCode = "1",
                                    ESMType = "DUmmy"
                                }
                            }
                        }
                    }
            };

            //act
            var actual = dd28RuleFundModelAndEmpStatEmpCodeCheck.Evaluate(param);

            //assert
            Assert.True(actual);
        }
    }
}
