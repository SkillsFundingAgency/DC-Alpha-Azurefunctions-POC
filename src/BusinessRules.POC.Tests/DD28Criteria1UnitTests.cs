using System.Collections.Generic;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.SharedRules.DD28;
using Moq;
using Xunit;

namespace BusinessRules.POC.Tests
{
    public class DD28Criteria1UnitTests
    {
        private readonly Mock<IReferenceData<string, string>> _mock;

        public DD28Criteria1UnitTests()
        {
            _mock = new Mock<IReferenceData<string, string>>();
            _mock.Setup(x => x.Get(It.Is<string>(k => k == AppConstants.DD28Criteria1EMPStats)))
                .Returns("11,12");
            _mock.Setup(x => x.Get(It.Is<string>(k => k == AppConstants.DD28Criteria1EMPType)))
                .Returns("BSI");
            _mock.Setup(x => x.Get(It.Is<string>(k => k == AppConstants.DD28Criteria1ESMCodes)))
                .Returns("3,4");
        }

        [Trait("Category", "DD28-SubRule-Rule")]
        [Fact]
        public void EMpStat_NotInAllowedlist_ReturnsFalse()
        {
            //arrange
            var dd28EmpStatTypeCode11Rule = new DD28Criteria1(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new LearningDelivery(),
                LearnerEmploymentStatusObj = new List<LearnerEmploymentStatus>()
                {
                    new LearnerEmploymentStatus()
                    {
                        EmpStat = 15,
                        EmploymentStatusMonitorings = new List<EmploymentStatusMonitoring>()
                        {
                            new EmploymentStatusMonitoring()
                            {
                                ESMCode = "5",
                                ESMType = "BIS"
                            }
                        }
                    }
                }
            };

            //act
            var actual = dd28EmpStatTypeCode11Rule.Evaluate(param);

            //assert
            Assert.False(actual);
        }

        [Trait("Category", "DD28-SubRule-Rule")]
        [Fact]
        public void EmpStatESMTypeAndCode_InAllowedValues_ReturnsTrue()
        {
            //arrange
            var dd28EmpStatTypeCode11Rule = new DD28Criteria1(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new LearningDelivery(),
                LearnerEmploymentStatusObj = new List<LearnerEmploymentStatus>() { 
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
            var actual = dd28EmpStatTypeCode11Rule.Evaluate(param);

            //assert
            Assert.True(actual);
        }

        [Trait("Category", "DD28-SubRule-Rule")]
        [Fact]
        public void ESMTypeAndCode_NotInAllowedValues_ReturnsFalse()
        {
            //arrange
            var dd28EmpStatTypeCode11Rule = new DD28Criteria1(_mock.Object);
            var param = new DD28SubModel()
            {
                LearningDeliveryObject = new LearningDelivery(),
                LearnerEmploymentStatusObj = new List<LearnerEmploymentStatus>()
                {
                    new LearnerEmploymentStatus()
                    {
                        EmpStat = 12,
                        EmploymentStatusMonitorings = new List<EmploymentStatusMonitoring>()
                        {
                            new EmploymentStatusMonitoring()
                            {
                                ESMCode = "1",
                                ESMType = "DUMMy"
                            }
                        }
                    }
                }
            };

            //act
            var actual = dd28EmpStatTypeCode11Rule.Evaluate(param);

            //assert
            Assert.False(actual);
        }
    }
}
