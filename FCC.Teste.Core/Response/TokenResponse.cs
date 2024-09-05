using System.Text.Json.Serialization;

namespace FCC.Teste.Core.Response
{
    public class TokenResponse
    {
        [JsonPropertyName("token")]
        public TokenInfo Token { get; set; }

        public class TokenInfo
        {
            [JsonPropertyName("data")]
            public TokenData? Data { get; set; }
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        public class TokenData
        {
            [JsonPropertyName("token")]
            public string Token { get; set; }
            [JsonPropertyName("userId")]
            public string UserId { get; set; }
        }
    }
}
