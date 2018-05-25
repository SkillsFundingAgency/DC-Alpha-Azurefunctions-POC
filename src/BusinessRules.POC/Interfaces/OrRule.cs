using System.Linq;

namespace BusinessRules.POC.Interfaces
{
    public class  OrRule<T> : IShortRule<T>
    {
        private readonly IShortRule<T>[] _rules;

        public OrRule(params IShortRule<T>[] rules)
        {
            _rules = rules;
        }       

        public bool Evaluate(T ObjectToValidate)
        {
            return _rules.Any(r => r.Evaluate(ObjectToValidate));
        }
    }
}