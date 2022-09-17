using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjWebIII_Events.Filters
{
    public class LogAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //context.HttpContext.Request.Headers
            Console.WriteLine("Filtro de Autorização LogAuthorizationFilter OnAuthorization");
        }
    }
}
