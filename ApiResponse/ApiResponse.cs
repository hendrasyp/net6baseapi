using Newtonsoft.Json;

namespace NET6Base.API.ApiResponse
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public ResponseMetadata metadata { get; set; }

        /// <summary>
        /// ....
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IQueryable<Object> results { get; set; }

        /// <summary>
        /// ....
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object? response { get; set; }
    }
}
