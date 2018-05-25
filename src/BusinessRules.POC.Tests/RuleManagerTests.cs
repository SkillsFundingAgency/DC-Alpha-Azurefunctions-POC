using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
