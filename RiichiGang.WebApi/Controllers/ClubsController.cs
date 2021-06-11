using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RiichiGang.Domain;
using RiichiGang.Service;
using RiichiGang.Service.InputModel;
using RiichiGang.WebApi.Extensios;
using RiichiGang.WebApi.ViewModel;

namespace RiichiGang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ApiControllerBase
    {
        private ClubService _clubService;
        private RulesetService _rulesetService;
        private UserService _userService;

        public ClubsController(
            ILogger<ApiControllerBase> logger,
            RulesetService rulesetService,
            ClubService clubService,
            UserService userService
        )
            : base(logger)
        {
            _clubService = clubService;
            _rulesetService = rulesetService;
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public Task<ActionResult<IEnumerable<ClubShortViewModel>>> GetAsync()
            => ExecuteAsync<IEnumerable<ClubShortViewModel>>(() =>
            {
                var clubs = _clubService.GetAll();

                if (!clubs.Any())
                    return NoContent();

                return Ok(clubs.Select(c => (ClubShortViewModel) c));
            });

        [HttpGet("{id}")]
        [AllowAnonymous]
        public Task<ActionResult<ClubViewModel>> GetAsync(int id)
            => ExecuteAsync<ClubViewModel>(() =>
            {
                var club = _clubService.GetById(id);

                if (club is null)
                    return NotFound();

                return Ok((ClubViewModel) club);
            });

        [HttpPost]
        [Authorize]
        public Task<ActionResult<ClubViewModel>> PostAsync([FromBody] ClubInputModel inputModel)
            => ExecuteAsync<ClubViewModel>(async () =>
            {
                var username = User.Username();
                var user = _userService.GetByUsername(username);

                var club = await _clubService.AddClubAsync(inputModel, user);
                return Ok((ClubViewModel) club);
            });

        [HttpPut("{id}")]
        [Authorize]
        public Task<ActionResult<ClubViewModel>> PutAsync(int id, [FromBody] ClubInputModel inputModel)
            => ExecuteAsync<ClubViewModel>(async () =>
            {
                var username = User.Username();
                var user = _userService.GetByUsername(username);

                var club = _clubService.GetById(id);

                if (club is null)
                    return NotFound();

                if (club.OwnerId != user.Id)
                    return Forbid();

                club = await _clubService.UpdateClubAsync(club, inputModel);
                return Ok((ClubViewModel) club);
            });

        [HttpDelete("{id}")]
        [Authorize]
        public Task<ActionResult> DeleteAsync(int id)
            => ExecuteAsync(async () =>
            {
                var username = User.Username();
                var user = _userService.GetByUsername(username);

                var club = _clubService.GetById(id);

                if (club is null)
                    return NotFound();

                if (club.OwnerId != user.Id)
                    return Forbid();

                await _clubService.DeleteClubAsync(club);
                return Ok();
            });

        [HttpPost("{id}/members/invite")]
        [Authorize]
        public Task<ActionResult<ClubViewModel>> AskInviteAsync(int id)
            => ExecuteAsync<ClubViewModel>(async () =>
            {
                var username = User.Username();
                var user = _userService.GetByUsername(username);

                var club = _clubService.GetById(id);

                if (club is null)
                    return NotFound();

                await _clubService.AskInviteAsync(user, club);
                return Ok((ClubViewModel) _clubService.GetById(id));
            });

        [HttpDelete("{id}/members")]
        [Authorize]
        public Task<ActionResult<ClubViewModel>> QuitAsync(int id)
            => ExecuteAsync<ClubViewModel>(async () =>
            {
                var username = User.Username();
                var user = _userService.GetByUsername(username);

                var club = _clubService.GetById(id);

                if (club is null)
                    return NotFound();

                await _clubService.QuitAsync(user, club);
                return Ok((ClubViewModel) _clubService.GetById(id));
            });

        [HttpDelete("{id}/members/{userId}")]
        [Authorize]
        public Task<ActionResult<ClubViewModel>> RemoveMemberAsync(int id, int userId)
            => ExecuteAsync<ClubViewModel>(async () =>
            {
                var username = User.Username();
                var owner = _userService.GetByUsername(username);

                var club = _clubService.GetById(id);

                if (club is null)
                    return NotFound();

                if (club.OwnerId != owner.Id)
                    return Forbid();

                var user = _userService.GetById(userId);

                if (user is null)
                    return NotFound();

                await _clubService.QuitAsync(user, club);
                return Ok((ClubViewModel) _clubService.GetById(id));
            });

        [HttpGet("{id}/rulesets")]
        [AllowAnonymous]
        public Task<ActionResult<IEnumerable<RulesetViewModel>>> GetRulesetsAsync(int id)
            => ExecuteAsync<IEnumerable<RulesetViewModel>>(() =>
            {
                var club = _clubService.GetById(id);

                if (club is null)
                    return NotFound();

                if (!club.Rulesets.Any())
                    return NoContent();

                return Ok(club.Rulesets.Select(r => (RulesetViewModel) r));
            });

        [HttpPost("{id}/rulesets")]
        [Authorize]
        public Task<ActionResult<RulesetViewModel>> PostRulesetAsync(int id, [FromBody] RulesetInputModel inputModel)
            => ExecuteAsync<RulesetViewModel>(async () =>
            {
                var username = User.Username();
                var owner = _userService.GetByUsername(username);

                var club = _clubService.GetById(id);

                if (club is null)
                    return NotFound();

                if (club.OwnerId != owner.Id)
                    return Forbid();

                var ruleset = await _rulesetService.AddRulesetAsync(inputModel, club);
                return Ok((RulesetViewModel) ruleset);
            });

        [HttpPatch("{id}/rulesets/{rulesetId}")]
        [Authorize]
        public Task<ActionResult<RulesetViewModel>> PatchRulesetAsync(int id, int rulesetId, [FromBody] RulesetInputModel inputModel)
            => ExecuteAsync<RulesetViewModel>(async () =>
            {
                var username = User.Username();
                var user = _userService.GetByUsername(username);

                var club = _clubService.GetById(id);

                if (club is null)
                    return NotFound();

                var ruleset = club.Rulesets.FirstOrDefault(r => r.Id == rulesetId);

                if (ruleset is null)
                    return NotFound();

                if (club.OwnerId != user.Id)
                    return Forbid();

                ruleset = await _rulesetService.UpdateRulesetAsync(inputModel, ruleset);
                return Ok((RulesetViewModel) ruleset);
            });

        [HttpDelete("{id}/rulesets/{rulesetId}")]
        [Authorize]
        public Task<ActionResult<RulesetViewModel>> DeleteRulesetAsync(int id, int rulesetId)
            => ExecuteAsync<RulesetViewModel>(async () =>
            {
                var username = User.Username();
                var user = _userService.GetByUsername(username);

                var club = _clubService.GetById(id);

                if (club is null)
                    return NotFound();

                var ruleset = club.Rulesets.FirstOrDefault(r => r.Id == rulesetId);

                if (ruleset is null)
                    return NotFound();

                if (club.OwnerId != user.Id)
                    return Forbid();

                await _rulesetService.DeleteRulesetAsync(ruleset);
                return Ok();
            });
    }
}