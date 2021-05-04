using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RiichiGang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesetsController : ApiControllerBase
    {
        public RulesetsController(ILogger<ApiControllerBase> logger) : base(logger)
        {
        }

        // TODO GET

        // TODO GET {id}

        // TODO POST

        // TODO PATCH

        // TODO DELETE
    }
}