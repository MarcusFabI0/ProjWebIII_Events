using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjWebIII_Events.Filters
{
    public class GeneralExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var problem = new ProblemDetails
            {
                Status = 500,
                Title = "Erro inesperado",
                Detail = "Ocorreu um erro inesperado na solitação",
                Type = context.Exception.GetType().Name
            };

            Console.WriteLine($"Tipo da exceção {context.Exception.GetType().Name}, mensagem {context.Exception.Message}, stack trace {context.Exception.StackTrace}");

            switch (context.Exception)
            {
                case ArgumentNullException:

                    context.Result = new ObjectResult(problem)
                    {
                        StatusCode = StatusCodes.Status501NotImplemented
                    };
                    break;

                case DivideByZeroException:
                    context.Result = new ObjectResult(problem)
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                    break;

                default:
                    context.Result = new ObjectResult(problem)
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                    break;
            }
        }
    }
}
