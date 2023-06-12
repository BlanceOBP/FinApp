namespace FinApp.Core.Models
{
    public class CollectionDto<T>
    {
        public List<T> Items { get; set; } = new List<T>();

        public int Total { get; set; }

    }
}
