namespace FPLCompanion.Dto
{
    public class DreamTeamDto
    {
        public TopPlayerDto top_player { get; set; }
        public List<DreamElementDto> team { get; set; }
    }

    public class DreamElementDto
    {
        public int element { get; set; }
        public int points { get; set; }
        public int position { get; set; }
    }

    public class TopPlayerDto
    {
        public int id { get; set; }
        public int points { get; set; }
    }
}
