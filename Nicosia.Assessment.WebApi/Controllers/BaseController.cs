using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using Nicosia.Assessment.Application.Models;

namespace Nicosia.Assessment.WebApi.Controllers
{
    [Route("[Controller]")]
    [EnableCors]
    [ApiController]
    public class BaseController : Controller
    {
        protected void SetTokenCookie(string token)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        protected string IpAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        protected string GetNextPageUrl(PaginationRequest paginationRequest, long totalCount)
        {
            if (paginationRequest.Offset + paginationRequest.Count >= totalCount)
            {
                return string.Empty;
            }

            List<KeyValuePair<string, StringValues>> list = base.Request.Query.Where<KeyValuePair<string, StringValues>>((KeyValuePair<string, StringValues> q) => q.Key != "Offset").ToList();
            KeyValuePair<string, StringValues> item = new KeyValuePair<string, StringValues>("Offset", (paginationRequest.Offset + paginationRequest.Count).ToString());
            list.Add(item);
            QueryString queryString = default(QueryString);
            list.ForEach(delegate (KeyValuePair<string, StringValues> q)
            {
                queryString = queryString.Add(q.Key, (string)q.Value);
            });
            return base.Request.Path + queryString.ToString();
        }

    }
}
