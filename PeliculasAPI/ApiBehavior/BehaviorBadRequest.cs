using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.ApiBehavior
{
    public static class BehaviorBadRequest
    {
        public static void Parse(ApiBehaviorOptions options)
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var response = new List<string>();
                foreach (var key in actionContext.ModelState.Keys)
                {
                    foreach (var error in actionContext.ModelState[key].Errors)
                    {
                        response.Add($"{key.ToLower()}: {error.ErrorMessage}");
                    }
                }

                return new BadRequestObjectResult(response);
            };
        }
    }
}