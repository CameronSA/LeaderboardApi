using Newtonsoft.Json;

namespace MABApi.Objects
{
    public class Player
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("firstname", Required = Required.Always)]
        public string FirstName { get; set; }
        
        [JsonProperty("lastname", Required = Required.Always)]
        public string LastName { get; set; }

        [JsonProperty("email", Required = Required.Always)]
        public string Email { get; set; }
    }
}