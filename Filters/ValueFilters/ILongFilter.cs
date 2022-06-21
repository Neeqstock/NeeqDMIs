namespace NeeqDMIs.Filters.ValueFilters
{
    public interface ILongFilter
    {
        void Push(long value);
        long Pull();
    }
}
