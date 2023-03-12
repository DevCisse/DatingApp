using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Helpers
{
    //public class LogUserActivity : IAsyncActionFilter
    //{
    //    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    //    {
    //        //var key = context.ActionArguments.First().Key;
    //        //var jsonRequest = System.Text.Json.JsonSerializer.Serialize(context.ActionArguments.First().Value);
    //        var resultContext = await next();



    //        var result = resultContext.Result;
    //        if(result is ObjectResult objResult)
    //        {
    //            var code = objResult.StatusCode;
    //            var obj = System.Text.Json.JsonSerializer.Serialize(objResult.Value);
    //        }

    //        if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;


    //        var userId = resultContext.HttpContext.User.GetUserId();

    //        var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

    //        var user = await repo.GetUserByIdAsync(userId);
    //        user.LastActive = DateTime.UtcNow;
    //        await repo.SaveAllAsync();

    //    }
    //}

    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.GetUserId();

            var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
            var user = await repo.GetUserByIdAsync(userId);
            user.LastActive = DateTime.UtcNow;
            await repo.SaveAllAsync();
        }
    }
}
