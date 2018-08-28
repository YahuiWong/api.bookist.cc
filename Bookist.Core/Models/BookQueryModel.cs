using Anet;

namespace Bookist.Core.Models
{
    public class BookQueryModel : PageQuery
    {
        public string Tag { get; set; }
        public BookStatus? Status { get; set; }
    }
}
