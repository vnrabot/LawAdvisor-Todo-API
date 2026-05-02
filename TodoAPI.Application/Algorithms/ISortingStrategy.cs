namespace TodoAPI.Application.Algorithms
{
    public interface ISortingStrategy<T>
    {
        List<T> Sort(List<T> input);
    }
}