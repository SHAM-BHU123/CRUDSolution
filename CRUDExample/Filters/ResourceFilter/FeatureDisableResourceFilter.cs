using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ResourceFilter
{
    public class FeatureDisableResourceFilter : IAsyncResourceFilter
    {
        private ILogger<FeatureDisableResourceFilter> _logger;

        private readonly bool _isDisabled;

        public FeatureDisableResourceFilter(ILogger<FeatureDisableResourceFilter> logger, bool isDisabled=true)
        {
            _logger = logger;
            _isDisabled = isDisabled;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            _logger.LogInformation("{FilterName}.{MethodName}-before", nameof(FeatureDisableResourceFilter), nameof(OnResourceExecutionAsync));
            if (_isDisabled)
            {
                //context.Result = new NotFoundResult(); parmanent desable 
                context.Result = new StatusCodeResult(501);//not implemented 
            }
            else
            {
                await next();
            }
           
            _logger.LogInformation("{FilterName}.{MethodName}-after", nameof(FeatureDisableResourceFilter), nameof(OnResourceExecutionAsync));

        }
    }
}
