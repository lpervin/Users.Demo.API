namespace Users.Application.DTOs;

public class PagedResults<TEntity>
{
    public IReadOnlyList<TEntity>? Results { get; set; }
    public int  CurrentPageNumber { get; set; }
    public int PageSize { get; set; }
    public long TotalRecordsCount { get; set; }
    public long PagesCount { get; set; }
    public int CurrentPageRecordsCount { get; set; }
  
}