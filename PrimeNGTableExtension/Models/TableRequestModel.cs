namespace PrimeNGTableExtension.Models
{
    public class TableRequestModel
    {
        public int? First { get; set; }
        public int? Rows { get; set; }
        public int? SortOrder { get; set; }
        public string? SortField { get; set; }
        public Dictionary<string, List<TableFilterModel>>? Filters { get; set; }
        public List<TableSortModel>? MultiSortMeta { get; set; }
    }
}
