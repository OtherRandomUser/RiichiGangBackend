using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RiichiGang.Service;
using RiichiGang.Service.InputModel;
using RiichiGang.WebApi.Extensios;
using RiichiGang.WebApi.ViewModel;

namespace RiichiGang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ApiControllerBase
    {
        private ClubService _clubService;
        private RulesetService _rulesetService;
        private TournamentService _tournamentService;
        private UserService _userService;

        public TournamentsController(
            ILogger<ApiControllerBase> logger,
            ClubService clubService,
            RulesetService rulesetService,
            TournamentService tournamentService,
            UserService userService
        )
            : base(logger)
        {
            _clubService = clubService;
            _rulesetService = rulesetService;
            _userService = userService;
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

        [HttpGet("{id}")]
        [AllowAnonymous]
        public Task<ActionResult<TournamentViewModel>> GetAsync(int id)
            => ExecuteAsync<TournamentViewModel>(() =>
            {
                var username = User.Username();
                var user = _userService.GetByUsername(username);

                var tournament = _tournamentService.GetById(id);

                if (tournament is null)
                    return NotFound();

                return Ok(new TournamentViewModel(tournament, user));
            });

        [HttpGet("{id}/brackets/{bracketId}")]
        [AllowAnonymous]
        public Task<ActionResult<BracketViewModel>> GetAsync(int id, int bracketId)
            => ExecuteAsync<BracketViewModel>(() =>
            {
                var tournament = _tournamentService.GetById(id);

                if (tournament is null)
                    return NotFound();

                var bracket = tournament.Brackets?.FirstOrDefault(b => b.Id == bracketId);

                if (bracket is null)
                    return NotFound();

                return Ok(new BracketViewModel(bracket, tournament));
            });

        [HttpPost]
        [Authorize]
        public Task<ActionResult<TournamentViewModel>> PostAsync([FromBody] TournamentInputModel inputModel)
            => ExecuteAsync<TournamentViewModel>(async () =>
            {
                var username = User.Username();
                var user = _userService.GetByUsername(username);

                var ruleset = _rulesetService.GetById(inputModel.RulesetId);

                if (ruleset is null)
                    return NotFound();

                var club = ruleset.Club;

                if (club?.OwnerId != user.Id)
                    return Forbid();

                var tournament = await _tournamentService.AddTournamentAsync(inputModel, ruleset, club);
                return Ok(new TournamentViewModel(tournament, user));
            });

        [HttpPut("{id}")]
        [Authorize]
        public Task<ActionResult<TournamentViewModel>> PatchAsync([FromBody] TournamentInputModel inputModel, int id)
            => ExecuteAsync<TournamentViewModel>(async () =>
            {
                var username = User.Username();
                var user = _userService.GetByUsername(username);

                var tournament = _tournamentService.GetById(id);

                if (tournament is null)
                    return NotFound();

                if (tournament.Club?.OwnerId != user.Id)
                    Forbid();

                var ruleset = _rulesetService.GetById(inputModel.RulesetId);

                if (ruleset is null)
                    return NotFound();

                var club = ruleset.Club;

                if (club?.OwnerId != user.Id)
                    return Forbid();

                tournament = await _tournamentService.UpdateTournamentAsync(tournament, inputModel, ruleset);
                return Ok(new TournamentViewModel(tournament, user));
            });

        [HttpDelete("{id}")]
        [Authorize]
        public Task<ActionResult> DeleteAsync(int id)
            => ExecuteAsync(async () =>
            {
                var username = User.Username();
                var user = _userService.GetByUsername(username);

                var tournament = _tournamentService.GetById(id);

                if (tournament is null)
                    return NotFound();

                if (tournament.Club?.OwnerId != user.Id)
                    Forbid();

                await _tournamentService.DeleteTournamentAsync(tournament);
                return Ok();
            });

        [HttpPost("{id}/players/invite")]
        [Authorize]
        public Task<ActionResult<TournamentViewModel>> AskInviteAsync(int id)
            => ExecuteAsync<TournamentViewModel>(async () =>
            {
                var username = User.Username();
                var user = _userService.GetByUsername(username);

                var tournament = _tournamentService.GetById(id);

                if (tournament is null)
                    return NotFound();

                await _tournamentService.AskInviteAsync(tournament, user);
                return Ok(new TournamentViewModel(tournament, user));
            });

        [HttpDelete("{id}/players")]
        [Authorize]
        public Task<ActionResult<TournamentViewModel>> QuitAsync(int id)
            => ExecuteAsync<TournamentViewModel>(async () =>
            {
                var username = User.Username();
                var user = _userService.GetByUsername(username);

                var tournament = _tournamentService.GetById(id);

                if (tournament is null)
                    return NotFound();

                await _tournamentService.QuitAsync(tournament, user);
                return Ok(new TournamentViewModel(tournament, user));
            });

        [HttpDelete("{id}/players/{userId}")]
        [Authorize]
        public Task<ActionResult<TournamentViewModel>> RemovePlayerAsync(int id, int userId)
            => ExecuteAsync<TournamentViewModel>(async () =>
            {
                var username = User.Username();
                var owner = _userService.GetByUsername(username);

                var tournament = _tournamentService.GetById(id);

                if (tournament is null)
                    return NotFound();

                if (tournament.Club.OwnerId != owner.Id)
                    return Forbid();

                var user = _userService.GetById(userId);

                if (user is null)
                    return NotFound();

                await _tournamentService.QuitAsync(tournament, user);
                return Ok(new TournamentViewModel(tournament, owner));
            });

        [HttpPost("{id}/init")]
        [Authorize]
        public Task<ActionResult<TournamentViewModel>> InitTournamentAsync(int id)
            => ExecuteAsync<TournamentViewModel>(async () =>
            {
                var username = User.Username();
                var owner = _userService.GetByUsername(username);

                var tournament = _tournamentService.GetById(id);

                if (tournament is null)
                    return NotFound();

                if (tournament.Club.OwnerId != owner.Id)
                    return Forbid();

                await _tournamentService.InitTournamentAsync(tournament);
                return Ok(new TournamentViewModel(tournament, owner));
            });

        [HttpPost("{id}/brackets/{bracketId}/series/{seriesId}")]
        [Authorize]
        public Task<ActionResult<BracketViewModel>> AddGameAsync(
            int id, int bracketId, int seriesId,
            [FromBody] GameInputModel inputModel)
            => ExecuteAsync<BracketViewModel>(async () =>
            {
                var username = User.Username();
                var owner = _userService.GetByUsername(username);

                var tournament = _tournamentService.GetById(id);

                if (tournament is null)
                    return NotFound();

                if (tournament.Club.OwnerId != owner.Id)
                    return Forbid();

                var bracket = tournament.Brackets.SingleOrDefault(b => b.Id == bracketId);

                if (bracket is null)
                    return NotFound();

                var series = bracket.Series.FirstOrDefault(s => s.Id == seriesId);

                if (series is null)
                    return NotFound();

                await _tournamentService.AddGameAsync(bracket, series, inputModel);

                tournament = _tournamentService.GetById(id);
                bracket = tournament.Brackets.SingleOrDefault(b => b.Id == bracketId);

                return Ok(new BracketViewModel(bracket, tournament));
            });
    }
}
