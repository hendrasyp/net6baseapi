using System.Net;

namespace NET6Base.API.ApiResponse
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApiResponse
    {
        public ApiResponse ShowResponse(HttpStatusCode httpStatusCode, IQueryable<object> QueryableResult, string Description = "");
        public ApiResponse ShowResponse(HttpStatusCode httpStatusCode, object objectRow, string Description = "");
        public ApiResponse ShowResponse(HttpStatusCode httpStatusCode, string Description);
    }
}
