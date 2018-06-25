namespace BusinessRules
{
    public interface IRule
    {
        RuleResponse Execute(CustomersData data);
    }
}
