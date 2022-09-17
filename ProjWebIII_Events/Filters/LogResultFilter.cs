using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjWebIII_Events.Filters
{
    public class LogResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("Filtro de Result LogResultFilter (APÓS) OnResultExecuted");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("Filtro de Result LogResultFilter (ANTES) OnResultExecuting");
        }
    }
}
