namespace FinApp.MiddleEntity
{
    public class ResponseType<T>
    {
        public List<T> ListOfType { get; set; } = new List<T>();

        public int CurrentPage { get; set; }

        public int CountPage { get; set; }

    }
}
