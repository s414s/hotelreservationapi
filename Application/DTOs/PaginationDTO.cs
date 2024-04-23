namespace Application.DTOs;

public record PaginationDTO<T> where T : class
{
    public int CurrentPage { get; init; }
    public int TotalPages { get; init; }
    public int TotalItems { get; init; }
    public IEnumerable<T> Data { get; init; }

    public PaginationDTO(IEnumerable<T> items, int currentPage, int totalPages, int totalItems)
    {
        Data = items;
        CurrentPage = currentPage;
        TotalPages = totalPages;
        TotalItems = totalItems;
    }
}
