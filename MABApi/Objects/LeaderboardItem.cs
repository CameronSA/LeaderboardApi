using Newtonsoft.Json;

namespace MABApi.Objects
{
    public class LeaderboardItem
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("id")]
        public int PlayerId { get; set; }
        
        [JsonProperty("gamesplayed", Required = Required.Always)]
        public int GamesPlayed { get; set; }

        [JsonProperty("gamesplayed")]
        public int TotalScore { get; set; }
    }
}