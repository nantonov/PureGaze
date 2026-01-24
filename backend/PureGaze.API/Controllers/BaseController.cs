using Microsoft.AspNetCore.Mvc;
using PureGaze.API.Extensions;

namespace PureGaze.API.Controllers;

public class BaseController : Controller
{
    protected string Email => HttpContext.Request.GetEmail() ?? string.Empty;
}