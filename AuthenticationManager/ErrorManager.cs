﻿using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace AuthenticationManager
{
    public class ErrorManager
    {
        private readonly RequestDelegate _next;

        public ErrorManager(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(new
            {
                error = "Ocurrió un error",
                details = ex.Message
            });

            return context.Response.WriteAsync(result);
        }
    }

}