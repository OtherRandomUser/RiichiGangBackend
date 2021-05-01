using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RiichiGang.Service;
using RiichiGang.Service.InputModel;
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
        public Task<ActionResult<UserViewModel>> SignUpAsync([FromBody] UserInputModel inputModel)
            => ExecuteAsync<UserViewModel>(async () =>
            {
                var user = await _userService.AddUserAsync(inputModel);
                return Ok((UserViewModel) user);
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
                var user = _userService.GetByEmail(inputModel.Email);

                if (user is null)
                    return NotFound();

                user = await _userService.UpdateUserAsync(user, inputModel);
                return Ok((UserViewModel) user);
            });

        [HttpDelete("{id}")]
        [Authorize]
        public Task<ActionResult> DeleteAsync(int id)
            => ExecuteAsync(async () =>
            {
                var user = _userService.GetById(id);

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
                    return NotFound();

                return Ok((UserViewModel) user);
            });
    }
}