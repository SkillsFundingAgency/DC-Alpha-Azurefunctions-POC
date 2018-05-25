using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules;
using Moq;
using Xunit;

namespace BusinessRules.POC.Tests
{
    public class OlassLearnerRuleTests
    {
        public OlassLearnerRuleTests()
        {
                
        }

        [Fact]
        [Trait("Category", "OlassLearnerRule-LearnDelFam66")]
        public void FAMType_Valid_And_FAM_Code_Valid_Returns_True()
        {
            //arrange
            var rule = new OlassLearnerDelFamRule(new ReferenceDataFromSettingsFile());


            //act
            var actual = rule.Evaluate(new LearningDeliveryFAM()
            {
                LearnDelFAMCode = "034",
                LearnDelFAMType = "LDM"

            });
            //assert

            Assert.True(actual);

        }

        [Fact]
        [Trait("Category", "OlassLearnerRule-LearnDelFam66")]
        public void FAMType_InValid_And_FAM_Code_Valid_Returns_False()
        {
            //arrange
            var rule = new OlassLearnerDelFamRule(new ReferenceDataFromSettingsFile());


            //act
            var actual = rule.Evaluate(new LearningDeliveryFAM()
            {
                LearnDelFAMCode = "034",
                LearnDelFAMType = "Dum"

            });
            //assert

            Assert.False(actual);

        }

        [Fact]
        [Trait("Category", "OlassLearnerRule-LearnDelFam66")]
        public void FAMType_Valid_And_FAM_Code_InValid_Returns_False()
        {
            //arrange
            var rule = new OlassLearnerDelFamRule(new ReferenceDataFromSettingsFile());


            //act
            var actual = rule.Evaluate(new LearningDeliveryFAM()
            {
                LearnDelFAMCode = "039",
                LearnDelFAMType = "LDM"

            });
            //assert

            Assert.False(actual);

        }

        [Fact]
        [Trait("Category", "OlassLearnerRule-LearnDelFam66")]
        public void NullValues_Returns_False()
        {
            //arrange
            var refDataMock = new Mock<IReferenceData<string, string>>();
            refDataMock.Setup(x => x.Get(It.Is<string>(p => p == AppConstants.LearnDelFam66OlassFamCode))).Returns("");
            refDataMock.Setup(x => x.Get(It.Is<string>(p => p == AppConstants.LearnDelFam66OlassFamType))).Returns("");


            var rule = new OlassLearnerDelFamRule(refDataMock.Object);


            //act
            var actual = rule.Evaluate(null);
            //assert

            Assert.False(actual);

        }


    }
}
