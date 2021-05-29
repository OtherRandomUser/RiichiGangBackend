using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RiichiGang.Service;
using RiichiGang.WebApi.ViewModel;

namespace RiichiGang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ApiControllerBase
    {
        private TournamentService _tournamentService;

        public TournamentsController(
            ILogger<ApiControllerBase> logger,
            TournamentService tournamentService
        )
            : base(logger)
        {
            _tournamentService = tournamentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public Task<ActionResult<IEnumerable<TournamentShortViewModel>>> GetAsync()
            => ExecuteAsync<IEnumerable<TournamentShortViewModel>>(() =>
            {
                var tournaments = _tournamentService.GetAll();

                if (!tournaments.Any())
                    return NoContent();

                return Ok(tournaments.Select(t => (TournamentShortViewModel) t));
            });

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
