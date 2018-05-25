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
    public class RestartLearnerDelFamRuleUnitTests
    {
        private readonly RestartLearnerDelFamRule _restatLearnerDelFamRule;

        public RestartLearnerDelFamRuleUnitTests()
        {
            var refDataMock = new Mock<IReferenceData<string, string>>();
            refDataMock.Setup(x => x.Get(It.Is<string>(y => y == AppConstants.LearnDelFam66RestartFamType)))
                .Returns("RES");
            refDataMock.Setup(x => x.Get(It.Is<string>(y => y == AppConstants.LearnDelFam66RestartFamCode)))
                .Returns("1");
            _restatLearnerDelFamRule = new RestartLearnerDelFamRule(refDataMock.Object);
        }

        [Theory]
        [Trait("Category", "LearnDelFAMType66-Rule")]
        [MemberData(nameof(ParamValuesForTest))]
        public void RestartLearner_FamType_NotRES_Returns_False(LearningDeliveryFAM ldfam, bool expectedResult)
        {
            //arrange


            //act 
           var actual = _restatLearnerDelFamRule.Evaluate(ldfam);

            //assert
            Assert.Equal(expectedResult, actual);
        }

        public static IEnumerable<object[]> ParamValuesForTest()
        {
            yield return new object[] {new LearningDeliveryFAM() {LearnDelFAMCode = "Dummy"}, false};
            yield return new object[] {null, false};
            yield return new object[] {new LearningDeliveryFAM() {LearnDelFAMCode = "RES"}, false};
            yield return new object[] {new LearningDeliveryFAM() {LearnDelFAMCode = "1", LearnDelFAMType = "RES"}, true};
        }
    }
}
