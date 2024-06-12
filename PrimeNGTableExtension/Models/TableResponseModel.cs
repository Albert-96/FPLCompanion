namespace PrimeNGTableExtension.Models
{
    public class TableResponseModel<T>
    {
        public IEnumerable<T> Records { get; set; }

        public int TotalRecords { get; set; }
    }
}
