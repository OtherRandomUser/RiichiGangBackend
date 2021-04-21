using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RiichiGang.WebApi.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected readonly ILogger _logger;

        public ApiControllerBase(ILogger<ApiControllerBase> logger)
        {
            _logger = logger;
        }

        protected async Task<ActionResult<T>> ExecuteAsync<T>(Func<Task<ActionResult<T>>> callback)
        {
            try
            {
                return await callback();
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(e, "Unhandled exception");
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "Unhandled exception");
                return BadRequest(e.Message);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, "Unhandled exception");
                return BadRequest(e.Message);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Unhandled exception");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
        }

        protected async Task<ActionResult> ExecuteAsync(Func<Task<ActionResult>> callback)
        {
            try
            {
                return await callback();
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(e, "Unhandled exception");
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "Unhandled exception");
                return BadRequest(e.Message);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, "Unhandled exception");
                return BadRequest(e.Message);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Unhandled exception");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
        }

        protected async Task<ActionResult<T>> ExecuteAsync<T>(Func<ActionResult<T>> callback)
        {
            try
            {
                return await Task.Run(callback);
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(e, "Unhandled exception");
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "Unhandled exception");
                return BadRequest(e.Message);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, "Unhandled exception");
                return BadRequest(e.Message);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Unhandled exception");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
        }

        protected async Task<ActionResult> ExecuteAsync(Func<ActionResult> callback)
        {
            try
            {
                return await Task.Run(callback);
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(e, "Unhandled exception");
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "Unhandled exception");
                return BadRequest(e.Message);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, "Unhandled exception");
                return BadRequest(e.Message);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Unhandled exception");
                return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
            }
        }
    }
}