using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjWebIII_Events.Core.Interfaces;

namespace ProjWebIII_Events.Filters
{
    public class EventExistsById : ActionFilterAttribute
    {
        public ICityEventService _cityeventService;
        public EventExistsById(ICityEventService cityeventService)
        {
            _cityeventService = cityeventService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            long IdEvent = (long)context.ActionArguments["id"];

            if (_cityeventService.GetEventById(IdEvent) == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
    }
}

