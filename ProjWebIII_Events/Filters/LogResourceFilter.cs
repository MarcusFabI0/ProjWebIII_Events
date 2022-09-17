using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjWebIII_Events.Filters
{
    public class LogResourceFilter : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("Filtro de Resource LogResourceFilter (APÓS) OnResourceExecuted");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //context.HttpContext.Request.Headers
            Console.WriteLine("Filtro de Resource LogResourceFilter (ANTES) OnResourceExecuting");
        }
    }
}
