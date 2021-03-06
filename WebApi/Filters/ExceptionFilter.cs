﻿using BLL.LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        ILogerService logerService;
        public ExceptionFilter(ILogerService _logerService) {
            logerService = _logerService;
        }
        public override void OnException(ExceptionContext context)
        {
            logerService.SendMessageToLog(context.Exception.Message);
            context.HttpContext.Response.StatusCode = 500;
            context.ExceptionHandled = true;
            context.Result = new JsonResult(
                new
                {
                    ExceptionMessage = context.Exception.Message
                });
        }
    }
}
