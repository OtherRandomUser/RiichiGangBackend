using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RiichiGang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ApiControllerBase
    {
        public TournamentsController(ILogger<ApiControllerBase> logger) : base(logger)
        {
        }

        // TODO GET

        // TODO GET {id}

        // TODO POST

        // TODO PATCH

        // TODO DELETE

        // TODO POST {id}/players/invite

        // TODO POST {id}/players/quit

        // TODO DELETE {id}/players/{userId}

        // TODO POST {id}/init

        // TODO POST {id}/game
    }
}