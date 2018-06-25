using System.Collections.Generic;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.SharedRules.DD28;
using Moq;
using Xunit;

namespace BusinessRules.POC.Tests
{
    public class DD28Criteria3UnitTests
    {
        private readonly Mock<IReferenceData<string, string>> _mock;

        public DD28Criteria3UnitTests()
        {
            _mock = new Mock<IReferenceData<string, string>>();
            _mock.Setup(x => x.Get(It.Is<string>(k => k == AppConstants.DD28Criteria3EMPStats)))
                .Returns("10");
            _mock.Setup(x => x.Get(It.Is<string>(k => k == AppConstants.DD28Criteria3ESMTypePart1)))
                .Returns("EII");
            _mock.Setup(x => x.Get(It.Is<string>(k => k == AppConstants.DD28Criteria3ESMCodesPart1)))
                .Returns("2");
            _mock.Setup(x => x.Get(It.Is<string>(k => k == AppConstants.DD28Criteria3ESMTypePart2)))
                .Returns("BSI");
            _mock.Setup(x => x.Get(It.Is<string>(k => k == AppConstants.DD28Criteria3ESMCodesPart2)))
                .Returns("3,4");
        }

        [Fact]
        [Trait("Category", "DD28-SubRule-Rule")]
        public void EMPStat_Invalid_ReturnsFalse()
        {
            //arrange
            var dd28Criteria3 = new DD28Criteria3(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new LearningDelivery()
                {
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
            var actual = dd28Criteria3.Evaluate(param);

            //assert
            Assert.False(actual);
        }

        [Fact]
        [Trait("Category", "DD28-SubRule-Rule")]
        public void ESMType_Invalid_ReturnsFalse()
        {
            //arrange
            var dd28Criteria3 = new DD28Criteria3(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new LearningDelivery()
                {
                },
                LearnerEmploymentStatusObj =
                    new List<LearnerEmploymentStatus>()
                    {
                        new LearnerEmploymentStatus()
                        {
                            EmpStat = 10,
                            EmploymentStatusMonitorings = new List<EmploymentStatusMonitoring>()
                            {
                                new EmploymentStatusMonitoring()
                                {
                                    ESMCode = "2",
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
            var actual = dd28Criteria3.Evaluate(param);

            //assert
            Assert.False(actual);
        }

        [Fact]
        [Trait("Category", "DD28-SubRule-Rule")]
        public void ESMTypeCodePart1_valid_ESMTypeCodePart2_Invalid_ReturnsFalse()
        {
            //arrange
            var dd28Criteria3 = new DD28Criteria3(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new LearningDelivery()
                {
                },
                LearnerEmploymentStatusObj = new List<LearnerEmploymentStatus>()
                {
                    new LearnerEmploymentStatus()
                    {
                        EmpStat = 10,
                        EmploymentStatusMonitorings = new List<EmploymentStatusMonitoring>()
                        {
                            new EmploymentStatusMonitoring()
                            {
                                ESMCode = "2",
                                ESMType = "EII"
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
            var actual = dd28Criteria3.Evaluate(param);

            //assert
            Assert.False(actual);
        }

        [Fact]
        [Trait("Category", "DD28-SubRule-Rule")]
        public void ESMTypeCodePart1_valid_ESMTypeCodePart2_valid_ReturnsTrue()
        {
            //arrange
            var dd28Criteria3 = new DD28Criteria3(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new LearningDelivery()
                {
                },
                LearnerEmploymentStatusObj = new List<LearnerEmploymentStatus>()
                {
                    new LearnerEmploymentStatus()
                    {
                        EmpStat = 10,
                        EmploymentStatusMonitorings = new List<EmploymentStatusMonitoring>()
                        {
                            new EmploymentStatusMonitoring()
                            {
                                ESMCode = "2",
                                ESMType = "EII"
                            },
                            new EmploymentStatusMonitoring()
                            {
                                ESMCode = "3",
                                ESMType = "BSI"
                            }
                        }
                    }
                }
            };

            //act
            var actual = dd28Criteria3.Evaluate(param);

            //assert
            Assert.True(actual);
        }

        [Fact]
        [Trait("Category", "DD28-SubRule-Rule")]
        public void ESMTypeCodePart1_valid_ESMTypeCodePart2_valid_ReturnsTrue2()
        {
            //arrange
            
            var dd28Criteria3 = new DD28Criteria3(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new LearningDelivery()
                {
                },
                LearnerEmploymentStatusObj = new List<LearnerEmploymentStatus>()
                {
                    new LearnerEmploymentStatus()
                    {
                        EmpStat = 10,
                        EmploymentStatusMonitorings = new List<EmploymentStatusMonitoring>()
                        {
                            new EmploymentStatusMonitoring()
                            {
                                ESMCode = "2",
                                ESMType = "EII"
                            },
                            new EmploymentStatusMonitoring()
                            {
                                ESMCode = "4",
                                ESMType = "BSI"
                            }
                        }
                    }
                }
            };

            //act
            var actual = dd28Criteria3.Evaluate(param);

            //assert
            Assert.True(actual);
        }
    }
}
