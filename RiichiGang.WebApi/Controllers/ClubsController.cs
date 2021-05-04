using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RiichiGang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ApiControllerBase
    {
        public ClubsController(ILogger<ApiControllerBase> logger) : base(logger)
        {
        }

        // TODO GET

        // TODO GET {id}

        // TODO POST

        // TODO PATCH

        // TODO DELETE

        // TODO POST {id}/members/invite

        // TODO POST {id}/members/quit

        // TODO DELETE {id}/members/{userId}
    }
}