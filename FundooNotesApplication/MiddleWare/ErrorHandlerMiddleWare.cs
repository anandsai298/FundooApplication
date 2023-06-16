using com.sun.tools.@internal.ws.processor.model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace FundooNotesApplication.MiddleWare
{
    public class ErrorHandlerMiddleWare
    {
        private readonly RequestDelegate next;
        public ErrorHandlerMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception error)
            {
                var response=context.Response;
                response.ContentType = "application/json";
                switch(error)
                {
                    case ApplicationException e:
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case UnauthorizedAccessException e:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(new {success=false,Message=error});
                await response.WriteAsync(result);
            }
        }
    }
}
