using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ResultFilter
{
    public class PersonsListResultFilter : IAsyncActionFilter
    {
        private readonly ILogger<PersonsListResultFilter> _logger;
        public PersonsListResultFilter(ILogger<PersonsListResultFilter> logger)
        {
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("{FilterName}.{MethodName} -before",
                nameof(PersonsListResultFilter),nameof(OnActionExecutionAsync));
            await next();
            _logger.LogInformation("{FilterName}.{MethodName} -after",
                nameof(PersonsListResultFilter), nameof(OnActionExecutionAsync));

            context.HttpContext.Response.Headers["Last-Modified"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }
    }
}
