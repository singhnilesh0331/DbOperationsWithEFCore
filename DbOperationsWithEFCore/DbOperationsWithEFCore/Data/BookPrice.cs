using System.Globalization;

namespace DbOperationsWithEFCore.Data
{
    public class BookPrice
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Amount { get; set; }
        public int CurrencyId { get; set; }
    }
}
