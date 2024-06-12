using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FPLCompanion.Data.Entities
{
    public class Element
    {
        [BsonId]
        public int id { get; set; }
        public int? chance_of_playing_next_round { get; set; }
        public int? chance_of_playing_this_round { get; set; }
        public int? code { get; set; }
        public int? cost_change_event { get; set; }
        public int? cost_change_event_fall { get; set; }
        public int? cost_change_start { get; set; }
        public int? cost_change_start_fall { get; set; }
        public int? dreamteam_count { get; set; }
        public int? element_type { get; set; }
        public float? ep_next { get; set; }
        public float? ep_this { get; set; }
        public int? event_points { get; set; }
        public string first_name { get; set; }
        public float? form { get; set; }
        public bool in_dreamteam { get; set; }
        public string news { get; set; }
        public DateTime? news_added { get; set; }
        public int? now_cost { get; set; }
        public float? current_cost { get; set; }
        public string photo { get; set; }
        public float? points_per_game { get; set; }
        public string second_name { get; set; }
        public string selected_by_percent { get; set; }
        public bool special { get; set; }
        public int? squad_number { get; set; }
        public string status { get; set; }
        public int? team { get; set; }
        public int? team_code { get; set; }
        public int? total_points { get; set; }
        public int? transfers_in { get; set; }
        public int? transfers_in_event { get; set; }
        public int? transfers_out { get; set; }
        public int? transfers_out_event { get; set; }
        public float? value_form { get; set; }
        public float? value_season { get; set; }
        public string web_name { get; set; }
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
        public int? bonus { get; set; }
        public int? bps { get; set; }
        public float? influence { get; set; }
        public float? creativity { get; set; }
        public float? threat { get; set; }
        public float? ict_index { get; set; }
        public int? influence_rank { get; set; }
        public int? influence_rank_type { get; set; }
        public int? creativity_rank { get; set; }
        public int? creativity_rank_type { get; set; }
        public int? threat_rank { get; set; }
        public int? threat_rank_type { get; set; }
        public int? ict_index_rank { get; set; }
        public int? ict_index_rank_type { get; set; }
        public int? corners_and_indirect_freekicks_order { get; set; }
        public string corners_and_indirect_freekicks_text { get; set; }
        public int? direct_freekicks_order { get; set; }
        public string direct_freekicks_text { get; set; }
        public int? penalties_order { get; set; }
        public string penalties_text { get; set; }
        public Team? teamInfo { get; set; }
        public ElementType? elementTypeInfo { get; set; }
    }
}
