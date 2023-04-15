namespace FinApp.MiddleEntity
{
    public class PaginationFilter<T>
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public List<T> TypeList { get; set; } = new List<T>();

    }
}
