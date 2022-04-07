﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace NET6Base.API.Settings
{
    /// <summary>
    /// Controller documentation convention
    /// </summary>
    public class ControllerDocumentationConvention : IControllerModelConvention
    {
        void IControllerModelConvention.Apply(ControllerModel controller)
        {
            if (controller == null)
                return;

            foreach (var attribute in controller.Attributes)
            {
                if (attribute.GetType() == typeof(RouteAttribute))
                {
                    var routeAttribute = (RouteAttribute)attribute;
                    if (!string.IsNullOrWhiteSpace(routeAttribute.Name))
                        controller.ControllerName = routeAttribute.Name;
                }
            }

        }
    }
}
