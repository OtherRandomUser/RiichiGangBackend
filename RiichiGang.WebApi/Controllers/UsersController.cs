using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RiichiGang.Service;
using RiichiGang.Service.InputModel;
using RiichiGang.WebApi.Extensios;
using RiichiGang.WebApi.InputModels;
using RiichiGang.WebApi.ViewModel;

namespace RiichiGang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        private UserService _userService;

        public UsersController(ILogger<UsersController> logger, UserService userService)
            : base(logger)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public Task<ActionResult<LoginViewModel>> SignUpAsync(
            [FromBody] UserInputModel inputModel,
            [FromServices] AuthenticationService authService)
            => ExecuteAsync<LoginViewModel>(async () =>
            {
                var user = await _userService.AddUserAsync(inputModel);
                var token = authService.Authenticate(user, inputModel.Password);
                return Ok(new LoginViewModel
                {
                    User = user,
                    Token = token
                });
            });

        [HttpPost("login")]
        [AllowAnonymous]
        public Task<ActionResult<LoginViewModel>> LoginAsync(
            [FromBody] LoginInputModel inputModel,
            [FromServices] AuthenticationService authService)
            => ExecuteAsync<LoginViewModel>(() =>
            {
                var user = _userService.GetByEmail(inputModel.Email);

                if (user is null)
                    return NotFound();

                var token = authService.Authenticate(user, inputModel.Password);

                return Ok(new LoginViewModel
                {
                    User = user,
                    Token = token
                });
            });

        [HttpPatch]
        [Authorize]
        public Task<ActionResult<UserViewModel>> UpdateAsync([FromBody] UserInputModel inputModel)
            => ExecuteAsync<UserViewModel>(async () =>
            {
                var username = User.Username();
                var user = _userService.GetByUsername(username);

                if (user is null)
                    return NotFound();

                user = await _userService.UpdateUserAsync(user, inputModel);
                return Ok((UserViewModel) user);
            });

        [HttpDelete]
        [Authorize]
        public Task<ActionResult> DeleteAsync()
            => ExecuteAsync(async () =>
            {
                var username = User.Username();
                var user = _userService.GetByUsername(username);

                if (user is null)
                    return NotFound();

                await _userService.DeleteUserAsync(user);
                return Ok();
            });

        [HttpGet("{id}")]
        [Authorize]
        public Task<ActionResult> GetAsync(int id)
            => ExecuteAsync(() =>
            {
                var user = _userService.GetById(id);

                if (user is null)
                    return NotFound("Usuário não encontrado");

                return Ok((UserViewModel) user);
            });

        [HttpPost("notifications/{notificationId}/confirm")]
        [Authorize]
        public Task<ActionResult> ConfirmNotificationAsync(int notificationId)
            => ExecuteAsync(async () =>
            {
                var username = User.Username();
                var user = _userService.GetByUsername(username);

                if (user is null)
                    return NotFound();

                var notification = user.Notifications
                    .FirstOrDefault(n => n.Id == notificationId);

                if (notification is null)
                    return NotFound();

                if (notification.MembershipId.HasValue)
                    await _userService.ConfirmMembershipAsync(user, notification.MembershipId.Value);

                await _userService.DeleteNotificationAsync(notification);
                return Ok();
            });

        [HttpPost("notifications/{notificationId}/deny")]
        [Authorize]
        public Task<ActionResult> DenyNotificationAsync(int notificationId)
            => ExecuteAsync(async () =>
            {
                var username = User.Username();
                var user = _userService.GetByUsername(username);

                if (user is null)
                    return NotFound();

                var notification = user.Notifications
                    .FirstOrDefault(n => n.Id == notificationId);

                if (notification is null)
                    return NotFound();

                if (notification.MembershipId.HasValue)
                    await _userService.DenyMembershipAsync(user, notification.MembershipId.Value);

                await _userService.DeleteNotificationAsync(notification);
                return Ok();
            });
    }
}