namespace BusinessRules
{
    public class RuleResponse
    {
        public string RuleName { get; set; }
        public bool IsValid { get; set; }
        public string ErrorString { get; set; }
        public int Id { get; set; }
    }
}
