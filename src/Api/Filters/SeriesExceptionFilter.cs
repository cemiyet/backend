using System.Net;
using Cemiyet.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cemiyet.Api.Filters
{
    public class SeriesExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is SerieNotFoundException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            context.Result = new JsonResult(new { context.Exception.Message });
            base.OnException(context);
        }
    }
}
