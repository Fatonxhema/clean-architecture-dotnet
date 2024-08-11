using Domain.Core.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private IMediator? _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return result.Error.Code switch
            {
                HttpStatusCode.InternalServerError => StatusCode((int)HttpStatusCode.InternalServerError, result.Error),
                HttpStatusCode.NotFound => NotFound(result.Error),
                HttpStatusCode.BadRequest => BadRequest(result.Error),
                _ => StatusCode((int)result.Error.Code, result.Error)
            };
        }
    }
}
