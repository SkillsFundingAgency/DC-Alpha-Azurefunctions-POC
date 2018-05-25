namespace BusinessRules.POC.Interfaces
{
    public static class Myextensions
    {
        // Helpers for AndRule / OrRule

        public static IShortRule<T> And<T>(params IShortRule<T>[] rules)
        {
            return new AndRule<T>(rules);
        }

        public static IShortRule<T> Or<T>(params IShortRule<T>[] rules)
        {
            return new OrRule<T>(rules);
        }
    }
}