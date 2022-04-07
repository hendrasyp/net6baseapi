using Newtonsoft.Json;
using System.Net;

namespace NET6Base.API.ApiResponse
{
    /// <summary>
    /// Response metadata
    /// </summary>
    public class ResponseMetadata
    {
        /// <summary>
        /// Returning available HttpStatusCode, e.g 200, 400, 404, 500, 505
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public HttpStatusCode code { get; set; }

        /// <summary>
        /// Pesan
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? message { get; set; }

        /// <summary>
        /// Deskripsi
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? description { get; set; }
    }
}
