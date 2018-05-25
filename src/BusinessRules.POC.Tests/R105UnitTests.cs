using BusinessRules.POC.RuleR105;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentValidation;
using BusinessRules.POC.Models;

namespace BusinessRules.POC.Tests
{
    public class R105UnitTests
    {
        private R105Validator _r105RuleValidator;

        public R105UnitTests()
        {
            _r105RuleValidator = new R105Validator(
                new R105PickLdFamActTypes(),
                new LearningDeliveryNoOverlappingDatesRule()
            );
        }

        [Trait("Category", "R105-Rule")]
        [Fact]      
        public void R105RuleWithNoOverlappingDates()
        {
            //arrange          
            var learner = new Learner()
            {
                LearningDeliveries = new List<LearningDelivery>()
                {
                    new LearningDelivery()
                    {
                        LearnAimRef = "",
                        AimSeqNumber = 1,
                        LearningDeliveryFAMs = new List<LearningDeliveryFAM>()
                        {
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 01),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 09)
                            },
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "2",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 21)
                            },
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 01),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 09)
                            },
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "3",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 12, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 12, 21)
                            },
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ABC"

                            }
                        }
                    }
                }
            };

            

            //act
            var results = _r105RuleValidator.Validate(learner);
            
            //assert
            Assert.True(results.IsValid);
        }

        [Trait("Category", "R105-Rule")]
        [Fact]
        public void WithOverlappingDates_Returns_False()
        {
            //arrange          
            var ldObj = new Learner()
            {
                LearningDeliveries = new List<LearningDelivery>()
                {
                    new LearningDelivery()
                    {
                        LearnAimRef = "",
                        AimSeqNumber = 1,
                        LearningDeliveryFAMs = new List<LearningDeliveryFAM>()
                        {
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 01),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 10)
                            },
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "2",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 21)
                            },
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 01),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 09)
                            },
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "3",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 12, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 12, 21)
                            }
                        }
                    }
                }
            };



            //act
            var results = _r105RuleValidator.Validate(ldObj);

            //assert
            Assert.False(results.IsValid);
        }

        [Trait("Category","R105-Rule")]
        [Fact]
        public void RuleWithOverlappingDates2_Return_False()
        {
            //arrange          
            var ldObj = new Learner()
            {
                LearningDeliveries = new List<LearningDelivery>()
                {
                    new LearningDelivery()
                    {
                        LearnAimRef = "",
                        AimSeqNumber = 1,
                        LearningDeliveryFAMs = new List<LearningDeliveryFAM>()
                        {
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 01),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 10)
                            },
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "2",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 5),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 5)
                            }

                        }

                    },
                    new LearningDelivery()
                    {
                        LearnAimRef = "",
                        AimSeqNumber = 1,
                        LearningDeliveryFAMs = new List<LearningDeliveryFAM>()
                        {
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 09, 01),
                                LearnDelFAMDateTo = new DateTime(2017, 10, 10)
                            },
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "2",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 10, 5),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 5)
                            }

                        }
                    }
                }
            };

            //act
            var results = _r105RuleValidator.Validate(ldObj);

            //assert
            Assert.False(results.IsValid);
        }

        [Trait("Category", "R105-Rule")]
        [Fact]
        public void WithOverlappingDatesThreeCodes()
        {
            //arrange          
            var ldObj = new Learner()
            {
                LearningDeliveries = new List<LearningDelivery>()
                {
                    new LearningDelivery()
                    {
                        LearnAimRef = "",
                        AimSeqNumber = 1,
                        LearningDeliveryFAMs = new List<LearningDeliveryFAM>()
                        {
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 01),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 09)
                            },
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "2",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 21)
                            },
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 01),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 09)
                            },
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "3",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 21),
                                LearnDelFAMDateTo = new DateTime(2017, 12, 21)
                            }
                        }
                    }
                }
            };



            //act
            var results = _r105RuleValidator.Validate(ldObj);

            //assert
            Assert.False(results.IsValid);
        }

        [Trait("Category", "R105-Rule")]
        [Fact]
        public void WithOverlappingDatesButSameCodes()
        {
            //arrange          
            var ldObj = new Learner()
            {
                LearningDeliveries = new List<LearningDelivery>()
                {
                    new LearningDelivery()
                    {
                        LearnAimRef = "",
                        AimSeqNumber = 1,
                        LearningDeliveryFAMs = new List<LearningDeliveryFAM>()
                        {
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 12, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 12, 21)
                            },
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 12, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 12, 21)
                            }
                        }
                    }
                }
            };



            //act
            var results = _r105RuleValidator.Validate(ldObj);

            //assert
            Assert.True(results.IsValid);
        }

        [Trait("Category", "R105-Rule")]
        [Fact]
        public void WithNoDifferentFAMTypeReturnsTrue()
        {
            //arrange          
            var ldObj = new Learner()
            {
                LearningDeliveries = new List<LearningDelivery>()
                {
                    new LearningDelivery()
                    {
                        LearnAimRef = "",
                        AimSeqNumber = 1,
                        LearningDeliveryFAMs = new List<LearningDeliveryFAM>()
                        {
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "XYZ",
                                LearnDelFAMDateFrom = new DateTime(2017, 12, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 12, 21)
                            },
                            new LearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "2",
                                LearnDelFAMType = "BCD",
                                LearnDelFAMDateFrom = new DateTime(2017, 12, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 12, 21)
                            }
                        }
                    }
                }
            };



            //act
            var results = _r105RuleValidator.Validate(ldObj);

            //assert
            Assert.True(results.IsValid);
        }


    }
}
