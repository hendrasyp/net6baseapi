using Microsoft.Extensions.Options;
using NET6Base.API.Settings;
using System.Net;

namespace NET6Base.API.ApiResponse
{
    /// <summary>
    /// ....
    /// </summary>
    public class ApiResponseRepository : IApiResponse
    {
        IOptions<AppSettings> _options;

        /// <summary>
        /// ...
        /// </summary>
        /// <param name="options"></param>
        public ApiResponseRepository(IOptions<AppSettings> options)
        {
            _options = options; 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="QueryableResult"></param>
        /// <param name="Description">Kalo diisi, message jadi pake description</param>
        /// <returns></returns>
        public ApiResponse ShowResponse(HttpStatusCode httpStatusCode, IQueryable<object> QueryableResult, string Description = "")
        {
            ResponseMetadata responseMetadata = new ResponseMetadata();
            ApiResponse apiResponse = new ApiResponse();

            responseMetadata.code = httpStatusCode;
            switch (httpStatusCode)
            {
                case HttpStatusCode.OK:
                    responseMetadata.message = GeneralHttpStatus.OK.ToString();
                    break;
                case HttpStatusCode.Accepted:
                    responseMetadata.message = GeneralHttpStatus.Accepted.ToString();
                    break;
                case HttpStatusCode.BadRequest:
                    responseMetadata.message = GeneralHttpStatus.BadRequest.ToString();
                    break;
                case HttpStatusCode.Forbidden:
                    responseMetadata.message = GeneralHttpStatus.BadRequest.ToString();
                    break;

            }

            if (!string.IsNullOrEmpty(Description))
            {
                responseMetadata.message = Description;
            }
            apiResponse.metadata = responseMetadata;
            if (QueryableResult != null)
            {
                apiResponse.results = QueryableResult;
            }
            return apiResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="objectRow"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        public ApiResponse ShowResponse(HttpStatusCode httpStatusCode, object objectRow, string Description = "")
        {
            ResponseMetadata responseMetadata = new ResponseMetadata();
            ApiResponse apiResponse = new ApiResponse();

            responseMetadata.code = httpStatusCode;
            switch (httpStatusCode)
            {
                case HttpStatusCode.OK:
                    responseMetadata.message = GeneralHttpStatus.OK.ToString();
                    break;
                case HttpStatusCode.Accepted:
                    responseMetadata.message = GeneralHttpStatus.Accepted.ToString();
                    break;
                case HttpStatusCode.BadRequest:
                    responseMetadata.message = GeneralHttpStatus.BadRequest.ToString();
                    break;
                case HttpStatusCode.Forbidden:
                    responseMetadata.message = GeneralHttpStatus.BadRequest.ToString();
                    break;

            }

            if (!string.IsNullOrEmpty(Description))
            {
                responseMetadata.message = Description;
            }
            apiResponse.metadata = responseMetadata;
            if (objectRow != null)
            {
                apiResponse.response = objectRow;
            }
            return apiResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ApiResponse ShowResponse(HttpStatusCode httpStatusCode, string Description)
        {
            throw new NotImplementedException();
        }
    }
}
