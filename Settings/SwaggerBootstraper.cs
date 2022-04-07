using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace NET6Base.API.Settings.Swagger
{
    /// <summary>
    /// Swagger configuration bootstrap
    /// </summary>
    public static class SwaggerBootstraper
    {
        public static void SwaggerInstall(this IServiceCollection service)
        {
            service.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;

            });

            service.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.DefaultApiVersion = ApiVersion.Default;
                setup.SubstituteApiVersionInUrl = true;
            });

            service.ConfigureOptions<SwaggerCostumValues>();

            service.AddSwaggerGen(opts =>
            {
                // Custom header for authorization on swagger
                opts.OperationFilter<SwaggerOperationFilter>();

                opts.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.RelativePath}");

                //opts.TagActionsBy(api =>
                //{
                //    if (api.GroupName != null)
                //    {
                //        return new[] { api.GroupName };
                //    }

                //    var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                //    if (controllerActionDescriptor != null)
                //    {
                //        return new[] { controllerActionDescriptor.ControllerName };
                //    }

                //    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                //});

                opts.ExampleFilters();

                //jangan dipake, malah bikin versioning ga jalan
                //opts.DocInclusionPredicate((name, api) => true);

                opts.CustomOperationIds(apiDesc =>
                {
                    return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
                });

                opts.IgnoreObsoleteActions();
                opts.IgnoreObsoleteProperties();

                opts.DocumentFilter<OperationsOrderingFilter>();

                // Include Comment
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(System.AppContext.BaseDirectory, xmlFile);
                opts.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                opts.EnableAnnotations();

            });
            service.AddSwaggerGenNewtonsoftSupport();
            // service.AddSwaggerExamplesFromAssemblyOf<Startup>();
            service.AddMvcCore().AddApiExplorer();
        }
    }
    /// <summary>
    /// Swagger Custom Values
    /// </summary>
    public class SwaggerCostumValues
        : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public SwaggerCostumValues(
            IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            // add swagger document for every API version discovered
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    CreateVersionInfo(description));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private OpenApiInfo CreateVersionInfo(
                ApiVersionDescription description)
        {
            string apiDesc = $"<strong>HOM (AFYA) API</strong> V {description.ApiVersion} adalah aplikasi yang berfungsi untuk menjembatani " +
                $"Front-End dan Back-End pada aplikasi HOM yang dikembangkan oleh PT Daya Indosa Pratama.";

            var info = new OpenApiInfo()
            {
                Title = "HOM API",
                Version = description.ApiVersion.ToString(),
                Contact = new OpenApiContact()
                {
                    Name = "Daya Indosa Pratama",
                    Email = "api@dayaindosa.com",
                    Url = new System.Uri("http://dayaindosa.com"),
                },
                License = new OpenApiLicense()
                {
                    Name = "Proprietary Software",
                    Url = new System.Uri("http://dayaindosa.com")
                },
                Description = apiDesc,
                TermsOfService = new System.Uri("http://dayaindosa.com")
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }

    public class SwaggerOperationFilter : IOperationFilter
    {

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            #region KONFIGURASI API DESCRIPTION
            var apiDescription = context.ApiDescription;

            if (operation.Parameters == null)
            {
                return;
            }

            #endregion

            #region KONFIGURASI HEADER PARAMETER
            var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;


            var allowAnonymous = (operation.OperationId == "Authentication_Post") || (operation.OperationId == "Authentication_Get");
            if (!allowAnonymous)
            {
                var _accessor = new HttpContextAccessor();
                var env = _accessor.HttpContext.RequestServices.GetRequiredService<Microsoft.AspNetCore.Hosting.IWebHostEnvironment>();
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                if (operation.OperationId.ToString().Contains("BPJS_"))
                {
                    if (operation.OperationId.ToString().Equals("BPJS_Authentication_Get"))
                    {
                        if (env.IsDevelopment())
                        {
                            operation.Parameters.Add(new OpenApiParameter()
                            {
                                Name = "x-username",
                                In = ParameterLocation.Header,
                                Required = false,
                                Description = "Nama user",
                                Schema = new OpenApiSchema
                                {
                                    Type = "String",
                                    Example = new OpenApiString("hospital")
                                }
                            });
                            operation.Parameters.Add(new OpenApiParameter()
                            {
                                Name = "x-password",
                                In = ParameterLocation.Header,
                                Required = false,
                                Description = "Password User",
                                Schema = new OpenApiSchema
                                {
                                    Type = "String",
                                    Example = new OpenApiString("serverh5n")
                                }
                            });
                        }
                        else
                        {
                            operation.Parameters.Add(new OpenApiParameter()
                            {
                                Name = "x-username",
                                In = ParameterLocation.Header,
                                Required = false,
                                Description = "Nama user",
                                Schema = new OpenApiSchema
                                {
                                    Type = "String",
                                    Example = new OpenApiString("")
                                }
                            });
                            operation.Parameters.Add(new OpenApiParameter()
                            {
                                Name = "x-password",
                                In = ParameterLocation.Header,
                                Required = false,
                                Description = "Password User",
                                Schema = new OpenApiSchema
                                {
                                    Type = "String",
                                    Example = new OpenApiString("")
                                }
                            });
                        }

                    }
                    else
                    {

                        if (env.IsDevelopment())
                        {
                            operation.Parameters.Add(new OpenApiParameter()
                            {
                                Name = "x-username",
                                In = ParameterLocation.Header,
                                Required = false,
                                Description = "Nama user",
                                Schema = new OpenApiSchema
                                {
                                    Type = "String",
                                    Example = new OpenApiString("hospital")
                                }
                            });
                            operation.Parameters.Add(new OpenApiParameter()
                            {
                                Name = "x-token",
                                In = ParameterLocation.Header,
                                Required = false,
                                Description = "(BPJS Bridging) Didapat pada saat login (string base64)",
                                Schema = new OpenApiSchema
                                {
                                    Type = "String",
                                    Example = new OpenApiString("DIP_HOM_WEBADMIN")
                                }
                            });
                        }
                        else
                        {
                            operation.Parameters.Add(new OpenApiParameter()
                            {
                                Name = "x-username",
                                In = ParameterLocation.Header,
                                Required = false,
                                Description = "(BPJS Bridging) Didapat pada saat login (string base64)",
                                Schema = new OpenApiSchema
                                {
                                    Type = "String",
                                    Example = new OpenApiString("")
                                }
                            });
                            operation.Parameters.Add(new OpenApiParameter()
                            {
                                Name = "x-token",
                                In = ParameterLocation.Header,
                                Required = false,
                                Description = "(BPJS Bridging) Didapat pada saat login (string base64)",
                                Schema = new OpenApiSchema
                                {
                                    Type = "String",
                                    Example = new OpenApiString("")
                                }
                            });
                        }


                    }
                }
                else
                {
                    if (env.IsDevelopment())
                    {
                        if (operation.OperationId.ToString().Contains("BPJS_"))
                        {
                            operation.Parameters.Add(new OpenApiParameter()
                            {
                                Name = "x-username",
                                In = ParameterLocation.Header,
                                Required = false,
                                Description = "Nama user",
                                Schema = new OpenApiSchema
                                {
                                    Type = "String",
                                    Example = new OpenApiString("hospital")
                                }
                            });
                            operation.Parameters.Add(new OpenApiParameter()
                            {
                                Name = "x-token",
                                In = ParameterLocation.Header,
                                Required = false,
                                Description = "(BPJS Bridging) Didapat pada saat login (string base64)",
                                Schema = new OpenApiSchema
                                {
                                    Type = "String",
                                    Example = new OpenApiString("DIP_HOM_WEBADMIN")
                                }
                            });
                        }
                        else
                        {
                            operation.Parameters.Add(new OpenApiParameter()
                            {
                                Name = "token",
                                In = ParameterLocation.Header,
                                Description = "Didapat pada saat login",
                                Required = false,
                                Schema = new OpenApiSchema
                                {
                                    Type = "String",
                                    Default = new OpenApiString("DIP_HOM_WEBADMIN")
                                }
                            });
                        }
                    }
                    else
                    {
                        operation.Parameters.Add(new OpenApiParameter()
                        {
                            Name = "token",
                            In = ParameterLocation.Header,
                            Required = false,
                            Description = "Didapat pada saat login (string base64)",
                            Schema = new OpenApiSchema
                            {
                                Type = "String",
                                Example = new OpenApiString("")
                            }
                        });
                    }

                    var opID = operation.OperationId.ToLower();
                }
            }

            #endregion

            #region REMOVE API Version Header Parameter
            var parametersToRemove = operation.Parameters.Where(x => x.Name == "api-version").ToList();
            foreach (var parameter in parametersToRemove)
                operation.Parameters.Remove(parameter);
            #endregion
        }
    }

    public class OperationsOrderingFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument openApiDoc, DocumentFilterContext context)
        {
            Dictionary<KeyValuePair<string, OpenApiPathItem>, int> paths = new Dictionary<KeyValuePair<string, OpenApiPathItem>, int>();
            int tempOrder = 9999;
            foreach (var path in openApiDoc.Paths)
            {
                // path.Value.Description
                SwaggerControllerOrderAttribute orderAttribute = context.ApiDescriptions.FirstOrDefault(x => x.RelativePath.Replace("/", string.Empty)
                    .Equals(path.Key.Replace("/", string.Empty), StringComparison.InvariantCultureIgnoreCase))?
                    .ActionDescriptor?.EndpointMetadata?.FirstOrDefault(x => x is SwaggerControllerOrderAttribute) as SwaggerControllerOrderAttribute;

                SwaggerControllerDescriptionAttribute descAttribute = context.ApiDescriptions.FirstOrDefault(x => x.RelativePath.Replace("/", string.Empty)
                    .Equals(path.Key.Replace("/", string.Empty), StringComparison.InvariantCultureIgnoreCase))?
                    .ActionDescriptor?.EndpointMetadata?.FirstOrDefault(x => x is SwaggerControllerDescriptionAttribute) as SwaggerControllerDescriptionAttribute;

                if (descAttribute == null)
                {
                    path.Value.Description = "__ UNKNOWN DESCRIPTION __";
                    path.Value.Summary = "ROOT MENU";
                }
                else
                {
                    path.Value.Description = descAttribute?.Description;
                    path.Value.Summary = descAttribute?.GroupMenu;
                }


                if (orderAttribute != null)
                {
                    if (orderAttribute == null)
                        throw new ArgumentNullException("there is no order for operation " + path.Key);

                    int order = orderAttribute.Order;
                    paths.Add(path, order);
                }
                else
                {
                    int order = tempOrder;
                    paths.Add(path, order);
                    tempOrder--;
                }
            }

            var orderedPaths = paths.OrderBy(x => x.Value).ToList();
            openApiDoc.Paths.Clear();
            orderedPaths.ForEach(x => openApiDoc.Paths.Add(x.Key.Key, x.Key.Value));

            var newPaths = new OpenApiPaths();
            foreach (var path in orderedPaths)
            {
                newPaths.Add(path.Key.Key, path.Key.Value);
            }

            openApiDoc.Paths = newPaths;
        }

    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SwaggerControllerOrderAttribute : Attribute
    {
        /// <summary>
        /// Gets the sorting order of the controller.
        /// </summary>
        public int Order { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerControllerOrderAttribute"/> class.
        /// </summary>
        /// <param name="order">Sets the sorting order of the controller.</param>
        public SwaggerControllerOrderAttribute(int order)
        {
            Order = order;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SwaggerControllerDescriptionAttribute : Attribute
    {
        /// <summary>
        /// Gets the sorting order of the controller.
        /// </summary>
        public string Description { get; private set; }
        public string GroupMenu { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerControllerDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="_description">Sets the sorting order of the controller.</param>
        public SwaggerControllerDescriptionAttribute(string _description)
        {
            Description = _description;
        }
        public SwaggerControllerDescriptionAttribute(string _description, string groupMenu)
        {
            Description = _description;
            GroupMenu = groupMenu;
        }
    }
}
