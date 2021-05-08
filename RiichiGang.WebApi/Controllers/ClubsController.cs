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
        private UserService _userService;
        private ClubService _clubService;

        public ClubsController(
            ILogger<ApiControllerBase> logger,
            UserService userService,
            ClubService clubService
        )
            : base(logger)
        {
            _userService = userService;
            _clubService = clubService;
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

        // TODO POST {id}/members/invite

        // TODO POST {id}/members/quit

        // TODO DELETE {id}/members/{userId}
    }
}