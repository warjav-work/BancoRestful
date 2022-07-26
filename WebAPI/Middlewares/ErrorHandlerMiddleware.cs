﻿using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WebAPI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                switch (ex)
                {
                    case Application.Exceptions.ApiExceptions e:

                        break;
                    case Application.Exceptions.ValidationException e:

                        break;

                }
            }
        }
    }
}
