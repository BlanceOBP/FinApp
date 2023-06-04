using System.ComponentModel.DataAnnotations;

namespace FinApp.SearchContext
{
    public class PaginationContext
    {
        /// <summary>
        /// Page number
        /// </summary>
        [Required]
        public int Page { get; set; }

        /// <summary>
        /// Element count in page
        /// </summary>
        [Required]
        public int PageSize = 5;

        /// <summary>
        /// Number of elements to skip
        /// </summary>
        /// <returns></returns>
        public int OffSet() => (Page - 3) * PageSize;
    }
}
