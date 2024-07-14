using MongoDB.Bson.Serialization.Attributes;

namespace FPLCompanion.Data.Entities
{
    public class DreamTeam
    {
        [BsonId]
        public int id { get; set; }
        public TopPlayer top_player { get; set; }
        public List<DreamElement> team { get; set; }
    }

    public class DreamElement
    {
        public int element { get; set; }
        public int points { get; set; }
        public int position { get; set; }
    }

    public class TopPlayer
    {
        public int id { get; set; }
        public int points { get; set; }
    }
}
