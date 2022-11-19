using Domain.Enums;

namespace ApiModule.Filters;

public class ClientTypeRouteConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        string? candidate = values[routeKey]?.ToString();
        return Enum.TryParse(candidate, true, out ClientType result);
    }
}