namespace FPLCompanion.Dto.ViewModels
{
    public class PlayerDetailViewDto
    {
        public string? _Id { get; set; }
        public int? code { get; set; }
        public TeamViewModelDto? team { get; set; }
        public string positionInfo { get; set; }
        public string first_name { get; set; }
        public string second_name { get; set; }
        public float? current_cost { get; set; }
        public float? form { get; set; }
        public float? points_per_game { get; set; }
        public int? total_points { get; set; }
        public int? bonus { get; set; }
        public int? bps { get; set; }
        public int? minutes { get; set; }
        public int? goals_scored { get; set; }
        public int? assists { get; set; }
        public int? clean_sheets { get; set; }
        public int? goals_conceded { get; set; }
        public int? own_goals { get; set; }
        public int? penalties_saved { get; set; }
        public int? penalties_missed { get; set; }
        public int? yellow_cards { get; set; }
        public int? red_cards { get; set; }
        public int? saves { get; set; }
        public float? value_form { get; set; }
        public float? value_season { get; set; }
        public int? influence_rank { get; set; }
        public int? influence_rank_type { get; set; }
        public int? creativity_rank { get; set; }
        public int? creativity_rank_type { get; set; }
        public int? threat_rank { get; set; }
        public int? threat_rank_type { get; set; }
        public int? ict_index_rank { get; set; }
        public int? ict_index_rank_type { get; set; }
        public string selected_by_percent { get; set; }
        public List<ElementFixtureViewModelDto> elementFixtures { get; set; }
        public List<HistoryViewDto> previousFixtures { get; set; }
        public List<HistoryPastViewDto> previousSeasons { get; set; }
    }

    public class ElementFixtureViewModelDto
    {
        public int id { get; set; }
        public int code { get; set; }
        public int @event { get; set; }
        public int minutes { get; set; }
        public bool finished { get; set; }
        public DateTime kickoff_time { get; set; }
        public TeamViewModelDto home { get; set; }
        public TeamViewModelDto away { get; set; }
        public int? team_h_score { get; set; }
        public int? team_a_score { get; set; }
        public int difficulty { get; set; }
        public bool is_home { get; set; }
    }

    public class HistoryViewDto
    {
        public int fixture { get; set; }
        public TeamViewModelDto? opponent_team { get; set; }
        public int total_points { get; set; }
        public bool was_home { get; set; }
        public int team_h_score { get; set; }
        public int team_a_score { get; set; }
        public int round { get; set; }
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
    }

    public class HistoryPastViewDto
    {
        public string season_name { get; set; }
        public int start_cost { get; set; }
        public int end_cost { get; set; }
        public int total_points { get; set; }
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
    }
}
