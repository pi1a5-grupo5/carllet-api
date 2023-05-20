using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        public User User => (User)HttpContext.Items["User"];
    }
}
