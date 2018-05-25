namespace BusinessRules.POC.Interfaces
{
    public interface ISharedRule<T, TResult> where T: class
    {
        TResult Evaluate(T objectToValidate);
    }
}