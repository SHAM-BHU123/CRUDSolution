using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ActionFilters
{

    public class ResponseHeaderFilterFactoryAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable => false;
        private  string? Key;
        private  string? Value;
        private  int Order;

        

        public ResponseHeaderFilterFactoryAttribute(string key,string value,int order)
        {
            Key= key;
            Value= value;
            Order= order;
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var filter = serviceProvider.GetRequiredService<ResponseHeaderActionFilter>();
            filter.Key=Key;
            filter.Value = Value;
            filter.Order=Order;
            return filter;
        }
    }

    public class ResponseHeaderActionFilter : IAsyncActionFilter, IOrderedFilter
    {
        public  string Key;
        public  string Value;
        public int Order { get; set; }
        private readonly ILogger<ResponseHeaderActionFilter> _logger;

        public ResponseHeaderActionFilter(ILogger<ResponseHeaderActionFilter> logger)
        {
            _logger= logger;
        }
        
        public  async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("Before logic-ResponseHeaderActionsFilter");
            
             await next();
            _logger.LogInformation("After logic-ResponseHeaderActionsFilter");
            context.HttpContext.Response.Headers[Key] = Value;

        }
    }
}
