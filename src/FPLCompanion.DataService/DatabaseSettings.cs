namespace FPLCompanion.DataService
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;
    }

    public static class DatabaseTables
    {
        public const string Element = "Element";
        public const string Team = "Team";
        public const string ElementType = "ElementType";
    }
}
