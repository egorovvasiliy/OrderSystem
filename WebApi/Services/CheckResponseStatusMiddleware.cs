using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public class CheckResponseStatusMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckResponseStatusMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await _next.Invoke(context);
            var x = context.Response.StatusCode;
        }
    }
}
