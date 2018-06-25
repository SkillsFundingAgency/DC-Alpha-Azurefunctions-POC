using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.RuleLearnDelFAMType66;
using Moq;
using System;
using System.Collections.Generic;
using Autofac;
using BusinessRules.POC.Configuration;
using BusinessRules.POC.Enums;
using BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules;
using Xunit;

namespace BusinessRules.POC.Tests
{
    public class LearnDelFAMType66RuleUnitTests
    {
        private IContainer _container;
        private ILifetimeScope _scope;

        public LearnDelFAMType66RuleUnitTests()
        {
        }

        [Trait("Category", "LearnDelFAMType66-Rule")]
        [Fact]
        public void LearnDelFAMType66AgeLessThan24ReturnsTrue()
        {
            //arrange
            var learnerDobNotNullMock = new Mock<ISharedRule<Learner, bool>>();
            learnerDobNotNullMock.Setup(x => x.Evaluate(It.IsAny<Learner>()))
                .Returns(true);

            var fetchSpecificFundModelMock = new Mock<IFetchSpecificFundModelsLDsWithLearnStartDate>();
            fetchSpecificFundModelMock.Setup(x => x.Evaluate(It.IsAny<Learner>()))
                .Returns(new List<LearningDelivery>()
            {
                new LearningDelivery()
            });

            var checkAgeLimitMock = new Mock<IPickValidLdsWithAgeLimitFamTypeAndCode>();
            checkAgeLimitMock.Setup(x => x.Evaluate(It.IsAny<List<LearningDelivery>>())).Returns(It.IsAny<List<LearningDelivery>>());

            var learnerDelFam66ExclusionRuleMock = new Mock<ILearnerDelFam66ExclusionRule>();
            learnerDelFam66ExclusionRuleMock.Setup(x => x.Evaluate(It.IsAny<Learner>())).Returns(false);
            
            var validator = new LearnDelFAMType66Validator(learnerDobNotNullMock.Object, 
                fetchSpecificFundModelMock.Object, checkAgeLimitMock.Object, learnerDelFam66ExclusionRuleMock.Object);

            //act
            var actual = validator.Validate(new Learner());

            //assert
            Assert.NotNull(actual);
            Assert.True(actual.IsValid);
        }

        [Fact]
        [Trait("Category", "LearnDelFAMType66-Rule")]
        public void LearnDelFAMType66_LDsFundModelIsNot35_ReturnsTrue()
        {
            //arrange
            var learnerDobNotNullMock = new Mock<ISharedRule<Learner, bool>>();
            learnerDobNotNullMock.Setup(x => x.Evaluate(It.IsAny<Learner>()))
                .Returns(false);

            var fetchSpecificFundModelMock = new Mock<IFetchSpecificFundModelsLDsWithLearnStartDate>();
            fetchSpecificFundModelMock.Setup(x => x.Evaluate(It.IsAny<Learner>()))
                .Returns(new List<LearningDelivery>(){});

            var checkAgeLimitMock = new Mock<IPickValidLdsWithAgeLimitFamTypeAndCode>();
            checkAgeLimitMock.Setup(x => x.Evaluate(It.IsAny<List<LearningDelivery>>())).Returns(It.IsAny<List<LearningDelivery>>());

            var learnerDelFam66ExclusionRuleMock = new Mock<ILearnerDelFam66ExclusionRule>();
            learnerDelFam66ExclusionRuleMock.Setup(x => x.Evaluate(It.IsAny<Learner>())).Returns(false);

            var validator = new LearnDelFAMType66Validator(learnerDobNotNullMock.Object,
                fetchSpecificFundModelMock.Object, checkAgeLimitMock.Object, learnerDelFam66ExclusionRuleMock.Object);

            //act
            var actual = validator.Validate(new Learner());

            //assert
            Assert.NotNull(actual);
            Assert.True(actual.IsValid);
        }

        [Trait("Category", "LearnDelFAMType66-Rule")]
        [Fact]
        public void LearnersAgeLessthan25_ReturnsTrue()
        {
            //arrange
            var learnerDobNotNullMock = new Mock<ISharedRule<Learner, bool>>();
            learnerDobNotNullMock.Setup(x => x.Evaluate(It.IsAny<Learner>()))
                .Returns(false);

            var fetchSpecificFundModelMock = new Mock<IFetchSpecificFundModelsLDsWithLearnStartDate>();
            fetchSpecificFundModelMock.Setup(x => x.Evaluate(It.IsAny<Learner>()))
                .Returns(new List<LearningDelivery>() { new LearningDelivery() });

            var checkAgeLimitMock = new Mock<IPickValidLdsWithAgeLimitFamTypeAndCode>();
            checkAgeLimitMock.Setup(x => x.Evaluate(It.IsAny<List<LearningDelivery>>())).Returns(new List<LearningDelivery>());

            var learnerDelFam66ExclusionRuleMock = new Mock<ILearnerDelFam66ExclusionRule>();
            learnerDelFam66ExclusionRuleMock.Setup(x => x.Evaluate(It.IsAny<Learner>())).Returns(false);

            var validator = new LearnDelFAMType66Validator(learnerDobNotNullMock.Object,
                fetchSpecificFundModelMock.Object, checkAgeLimitMock.Object, learnerDelFam66ExclusionRuleMock.Object);

            //act
            var actual = validator.Validate(new Learner());

            //assert
            Assert.NotNull(actual);
            Assert.True(actual.IsValid);
        }

        [Trait("Category", "LearnDelFAMType66-Rule")]
        [Fact]
        public void LearnersAgeGreaterThan25_And_LARsNVQ_Valid_ReturnsFalse()
        {
            //arrange
            var learnerDobNotNullMock = new Mock<ISharedRule<Learner, bool>>();
            learnerDobNotNullMock.Setup(x => x.Evaluate(It.IsAny<Learner>()))
                .Returns(false);

            var fetchSpecificFundModelMock = new Mock<IFetchSpecificFundModelsLDsWithLearnStartDate>();
            fetchSpecificFundModelMock.Setup(x => x.Evaluate(It.IsAny<Learner>()))
                .Returns(new List<LearningDelivery>() { new LearningDelivery() });

            var checkAgeLimitMock = new Mock<IPickValidLdsWithAgeLimitFamTypeAndCode>();
            checkAgeLimitMock.Setup(x => x.Evaluate(It.IsAny<List<LearningDelivery>>())).Returns(new List<LearningDelivery>()
            {
                new LearningDelivery()
                {
                    FundModel = 35
                }
            });

            var learnerDelFam66ExclusionRuleMock = new Mock<ILearnerDelFam66ExclusionRule>();
            learnerDelFam66ExclusionRuleMock.Setup(x => x.Evaluate(It.IsAny<Learner>())).Returns(false);

            var validator = new LearnDelFAMType66Validator(learnerDobNotNullMock.Object,
                fetchSpecificFundModelMock.Object, checkAgeLimitMock.Object, learnerDelFam66ExclusionRuleMock.Object);

            //act
            var actual = validator.Validate(new Learner());

            //assert
            Assert.NotNull(actual);
            Assert.False(actual.IsValid);
        }

        [Trait("Category", "LearnDelFAMType66-Rule")]
        [Fact]
        public void LearnersAgeGreaterThan25_And_LARsNVQ_Valid_WithIoC_ReturnsFalse()
        {
            //arrange
            if(_container == null) ConfigureIoCContainer();
            using (var scope = _container.BeginLifetimeScope())
            {
                //var validator1 = new LearnDelFAMType66Validator(scope.ResolveKeyed<ISharedRule<Learner, bool>>(SharedRuleNames.LearnerDobShouldNotBeNull),
                //    );
                var learnerDobNotNull =
                    scope.ResolveKeyed<ISharedRule<Learner, bool>>(SharedRuleNames.LearnerDobShouldNotBeNull);
                var fetchSpecificFundModel = scope.Resolve<IFetchSpecificFundModelsLDsWithLearnStartDate>();

                var checkAgeLimit = scope.Resolve<IPickValidLdsWithAgeLimitFamTypeAndCode>();
                var learnerDelFam66ExclusionRule = scope.Resolve<ILearnerDelFam66ExclusionRule>();

                var validator = new LearnDelFAMType66Validator(learnerDobNotNull,
                    fetchSpecificFundModel, checkAgeLimit, learnerDelFam66ExclusionRule);

                //act
                var actual = validator.Validate(new Learner()
                {
                    DateOfBirth = new DateTime(1982,01,01),
                    LearningDeliveries = new List<LearningDelivery>()
                    {
                        new LearningDelivery()
                        {
                            LearnAimRef = "22",
                            FundModel = 36,
                            LearnStartDate = new DateTime(2016,05,5)
                        },
                        new LearningDelivery()
                        {
                            FundModel = 35,
                            LearnAimRef = "Z0007835",
                            LearnStartDate = new DateTime(2017, 06,11),
                            DateOfBirth = new DateTime(1982, 01,01),
                            LearningDeliveryFAMs = new List<LearningDeliveryFAM>()
                            {
                                new LearningDeliveryFAM()
                                {
                                    LearnDelFAMCode = "1",
                                    LearnDelFAMType = "FFI"
                                }
                            }
                        }
                    }
                });

                //assert
                Assert.NotNull(actual);
                Assert.False(actual.IsValid);
            }
        }

        private void ConfigureIoCContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<BusinessLogicAutofacModule>();
            _container = builder.Build();
            _scope = _container.BeginLifetimeScope();
        }
    }
}
