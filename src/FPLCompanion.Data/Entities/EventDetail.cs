using MongoDB.Bson;

namespace FPLCompanion.Data.Entities
{
    public class EventDetail
    {
        public int id { get; set; }
        public List<EventElement> elements { get; set; }
    }

    public class EventElement
    {
        public ObjectId _id { get; set; }
        public int elementId { get; set; }
        public int eventId { get; set; }
        public ElementStat stats { get; set; }
        public List<Explain> explain { get; set; }
    }

    public class Explain
    {
        public int fixture { get; set; }
        public List<ExplainStat> stats { get; set; }
    }

    public class ElementStat
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

    public class ExplainStat
    {
        public string identifier { get; set; }
        public int points { get; set; }
        public int value { get; set; }
    }
}
