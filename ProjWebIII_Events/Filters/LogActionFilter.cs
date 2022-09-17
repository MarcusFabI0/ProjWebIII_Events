using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjWebIII_Events.Filters
{
    public class LogActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Filtro de Action LogActionFilter (APÓS) OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           
            Console.WriteLine("Filtro de Action LogActionFilter (ANTES) OnActionExecuting"); 
        }
    }
}
