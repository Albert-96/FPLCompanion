namespace FPLCompanion.Dto
{
    public class EventDetailDto
    {
        public int id { get; set; }
        public List<EventElementDto> elements { get; set; }
    }

    public class EventElementDto
    {
        public int id { get; set; }
        public int eventId { get; set; }
        public ElementStatDto stats { get; set; }
        public List<ExplainDto> explain { get; set; }
    }

    public class ExplainDto
    {
        public int fixture { get; set; }
        public List<ExplainStatDto> stats { get; set; }
    }

    public class ElementStatDto
    {
        public int minutes { get; set; }
        public int goals_scored { get; set; }
        public int assists { get; set; }
        public int clean_sheets { get; set; }
        public int goals_conceded { get; set; }
        public int own_goals { get; set; }
        public int penalties_saved { get; set; }
        public int penalties_missed { get; set; }
        public int yellow_cards { get; set; }
        public int red_cards { get; set; }
        public int saves { get; set; }
        public int bonus { get; set; }
        public int bps { get; set; }
        public string influence { get; set; }
        public string creativity { get; set; }
        public string threat { get; set; }
        public string ict_index { get; set; }
        public int starts { get; set; }
        public string expected_goals { get; set; }
        public string expected_assists { get; set; }
        public string expected_goal_involvements { get; set; }
        public string expected_goals_conceded { get; set; }
        public int total_points { get; set; }
        public bool in_dreamteam { get; set; }
    }

    public class ExplainStatDto
    {
        public string identifier { get; set; }
        public int points { get; set; }
        public int value { get; set; }
    }
}
