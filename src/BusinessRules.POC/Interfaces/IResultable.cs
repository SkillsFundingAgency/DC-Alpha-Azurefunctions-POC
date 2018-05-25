namespace BusinessRules.POC.Interfaces
{
    public interface IResultable<TResult>
    {
        TResult Result { get; set; }
    }
}