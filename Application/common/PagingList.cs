namespace Application.Common;

public class PagingList<T>
{
    public PagingList(IReadOnlyCollection<T> items, int pageIndex, int pageSize)
    {
        Items = items;
        Count = items.Count;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }

    public IEnumerable<T> Items { get; }
    public int Count { get; }
    public int PageIndex { get; }
    public int PageSize { get; }
    
}