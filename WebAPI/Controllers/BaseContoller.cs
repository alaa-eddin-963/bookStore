using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected Guid GetUserIdFromToken()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdStr, out var userId))
                throw new UnauthorizedAccessException("Invalid or missing token.");
            return userId;
        }
    }
}
