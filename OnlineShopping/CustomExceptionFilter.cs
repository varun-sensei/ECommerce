using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopping
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        
            public override void OnException(ExceptionContext context)
            {
                context.Result = new ViewResult { ViewName = "Error" };
                context.ExceptionHandled = true;
            }
        
    }
}
