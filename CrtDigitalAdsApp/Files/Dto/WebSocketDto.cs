namespace CrtDigitalAdsApp.Dto
{
    using Newtonsoft.Json;

    internal class WebSocketDto
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; } = false;

        [JsonProperty("command")]
        public string Command { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }
    }
}