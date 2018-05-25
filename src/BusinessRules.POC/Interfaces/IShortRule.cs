namespace BusinessRules.POC.Interfaces
{
    public interface IShortRule<T, TResult>
    {
        TResult Evaluate(T objectToValidate);
    }

    public interface IShortRule<T>
    {
        bool Evaluate(T objectToValidate);
    }
}