using System.ComponentModel;

namespace NET6Base.API.Settings
{
    public enum GeneralHttpStatus
    {
        /// <summary>
        /// Successful
        /// </summary>
        [Description("OK")]
        OK = 200,

        /// <summary>
        /// Created
        /// </summary>
        [Description("Created")]
        Created = 201,

        /// <summary>
        /// Accepted
        /// </summary>
        [Description("Accepted")]
        Accepted = 201,

        /// <summary>
        /// Created
        /// </summary>
        [Description("NoContent")]
        NoContent = 204,

        /// <summary>
        /// Bad Request/Bad input parameter.
        /// </summary>
        [Description("Bad Request")]
        BadRequest = 400,

        /// <summary>
        /// The client passed in the invalid Auth token. Client should refresh the token and then try again.
        /// </summary>
        [Description("Unauthorized")]
        Unauthorized = 401,

        /// <summary>
        /// Application try to access to properties not belong to an App.
        /// </summary>
        [Description("Forbidden")]
        Forbidden = 403,

        /// <summary>
        /// Resource not found.
        /// </summary>
        [Description("NotFound")]
        NotFound = 404,

        /// <summary>
        /// The resource doesn't support the specified HTTP verb.
        /// </summary>
        [Description("Method Not Allowed")]
        MethodNotAllowed = 405,

        /// <summary>
        /// Servers are not working as expected. The request is probably valid but needs to be requested again later.
        /// </summary>
        [Description("Internal Server Error")]
        InternalServerError = 500,

        /// <summary>
        /// Service Unavailable.
        /// </summary>
        [Description("Service Unavailable")]
        ServiceUnavailable = 503
    }
}
