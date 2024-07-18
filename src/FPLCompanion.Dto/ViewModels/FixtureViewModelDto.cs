namespace FPLCompanion.Dto.ViewModels
{
    public class FixtureViewModelDto
    {
        public int id { get; set; }
        public int code { get; set; }
        public int @event { get; set; }
        public bool started { get; set; }
        public bool finished { get; set; }
        public DateTime kickoff_time { get; set; }
        public TeamViewModelDto home { get; set; }
        public TeamViewModelDto away { get; set; }
        public int? team_h_score { get; set; }
        public int? team_a_score { get; set; }
        public int team_h_difficulty { get; set; }
        public int team_a_difficulty { get; set; }
    }
}
