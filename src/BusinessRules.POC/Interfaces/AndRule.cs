using System.Linq;

namespace BusinessRules.POC.Interfaces
{
    public class AndRule<T> : IShortRule<T>
    {
        private readonly IShortRule<T>[] _rules;

        public AndRule(params IShortRule<T>[] rules)
        {
            _rules = rules;
        }

        public bool Evaluate(T ObjectToValidate)
        {
            return _rules.All(r => r.Evaluate(ObjectToValidate));
        }
    }
}