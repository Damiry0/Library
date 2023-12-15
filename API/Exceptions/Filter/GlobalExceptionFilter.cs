using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BooksAPI.Exceptions.Filter;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;
    private readonly IHostEnvironment _hostEnvironment;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, IHostEnvironment hostEnvironment)
    {
        _logger = logger;
        _hostEnvironment = hostEnvironment;
    }

    public void OnException(ExceptionContext context)
    {
        if (!_hostEnvironment.IsDevelopment())
        {
            return;
        }

        switch (context.Exception)
        {
            case NotFoundException:
                _logger.LogWarning(context.Exception.Message);
                context.Result = new NotFoundObjectResult(context.Exception.Message);
                break;
        }
    }
}