namespace FPLCompanion.Dto
{
    public class ChipPlayDto
    {
        public string chip_name { get; set; }
        public int num_played { get; set; }
    }

    public class TopElementInfoDto
    {
        public int id { get; set; }
        public int points { get; set; }
    }

    public class EventDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime deadline_time { get; set; }
        public int average_entry_score { get; set; }
        public bool finished { get; set; }
        public bool data_checked { get; set; }
        public int? highest_scoring_entry { get; set; }
        public int deadline_time_epoch { get; set; }
        public int deadline_time_game_offset { get; set; }
        public int? highest_score { get; set; }
        public bool is_previous { get; set; }
        public bool is_current { get; set; }
        public bool is_next { get; set; }
        public bool cup_leagues_created { get; set; }
        public bool h2h_ko_matches_created { get; set; }
        public List<ChipPlayDto> chip_plays { get; set; }
        public int? most_selected { get; set; }
        public int? most_transferred_in { get; set; }
        public int? top_element { get; set; }
        public TopElementInfoDto top_element_info { get; set; }
        public int transfers_made { get; set; }
        public int? most_captained { get; set; }
        public int? most_vice_captained { get; set; }
    }

    public class GameSettingsDto
    {
        public int league_join_private_max { get; set; }
        public int league_join_public_max { get; set; }
        public int league_max_size_public_classic { get; set; }
        public int league_max_size_public_h2h { get; set; }
        public int league_max_size_private_h2h { get; set; }
        public int league_max_ko_rounds_private_h2h { get; set; }
        public string league_prefix_public { get; set; }
        public int league_points_h2h_win { get; set; }
        public int league_points_h2h_lose { get; set; }
        public int league_points_h2h_draw { get; set; }
        public bool league_ko_first_instead_of_random { get; set; }
        public object cup_start_event_id { get; set; }
        public object cup_stop_event_id { get; set; }
        public object cup_qualifying_method { get; set; }
        public object cup_type { get; set; }
        public int squad_squadplay { get; set; }
        public int squad_squadsize { get; set; }
        public int squad_team_limit { get; set; }
        public int squad_total_spend { get; set; }
        public int ui_currency_multiplier { get; set; }
        public bool ui_use_special_shirts { get; set; }
        public List<object> ui_special_shirt_exclusions { get; set; }
        public int stats_form_days { get; set; }
        public bool sys_vice_captain_enabled { get; set; }
        public int transfers_cap { get; set; }
        public double transfers_sell_on_fee { get; set; }
        public List<string> league_h2h_tiebreak_stats { get; set; }
        public string timezone { get; set; }
    }

    public class PhaseDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public int start_event { get; set; }
        public int stop_event { get; set; }
    }

    public class ElementStatDto
    {
        public string label { get; set; }
        public string name { get; set; }
    }

    public class GeneralInfoDto
    {
        public List<EventDto> events { get; set; }
        public GameSettingsDto game_settings { get; set; }
        public List<PhaseDto> phases { get; set; }
        public List<TeamDto> teams { get; set; }
        public int total_players { get; set; }
        public List<ElementDto> elements { get; set; }
        public List<ElementStatDto> element_stats { get; set; }
        public List<ElementTypeDto> element_types { get; set; }
    }
}
