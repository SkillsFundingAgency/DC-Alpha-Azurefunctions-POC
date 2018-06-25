using Xunit;

namespace BusinessRules.POC.Tests
{
    public class RuleManagerTests
    {
        [Fact]
        public void TestRuleManager()
        {
            var obj = new RuleManager.RuleManager();
            obj.ExecuteRules();
        }
    }
}
