using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public abstract class HomeController : ControllerBase
    {
        public Guid UserId { get; set; }
    }
}
