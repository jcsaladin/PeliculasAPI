using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Filters
{
    public class ParseBadRequest : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var result = context.Result as IStatusCodeActionResult;
            if(result == null)
            {
                return;
            }

            var codeStatus = result.StatusCode;
            if (codeStatus == 400)
            {
                var response = new List<string>();
                var actualResponse = context.Result as BadRequestObjectResult;
                if (actualResponse.Value is string)
                {
                    response.Add(actualResponse.Value.ToString());
                }
                else if (actualResponse.Value is IEnumerable<IdentityError> errores)
                {
                    foreach (var error in errores)
                    {
                        response.Add(error.Description);
                    }
                }
                else
                {
                    foreach(var key in context.ModelState.Keys)
                    {
                        foreach(var error in context.ModelState[key].Errors)
                        {
                            response.Add($"{key.ToLower()}: {error.ErrorMessage}");
                        }
                    }
                }

                context.Result = new BadRequestObjectResult(response);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           
        }
    }
}