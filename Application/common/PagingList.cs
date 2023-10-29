namespace Application.Common;

public class PagingList<T>
{
    public PagingList(IReadOnlyCollection<T> items, int pageIndex, int pageSize,string search, Enum orderBy)
    {
        Items = items;
        Count = items.Count;
        PageIndex = pageIndex;
        PageSize = pageSize;
        OrderBy = orderBy;
        Search = search;
    }

    public IEnumerable<T> Items { get; }
    public int Count { get; }
    public int PageIndex { get; }
    public int PageSize { get; }
    public string Search { get; }
    public Enum OrderBy { get; }
    
}