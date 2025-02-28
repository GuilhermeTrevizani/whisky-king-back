using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WhiskyKing.API.Controllers;

[ApiController, Authorize]
public class BaseController : ControllerBase
{
}