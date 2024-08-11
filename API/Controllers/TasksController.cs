using Application.Task.Command.Update;
using Domain.Core.Primitives;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : APIController
    {
        [HttpPut("Update")]
        [ProducesResponseType(typeof(TaskDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> UpdateTask([FromBody] UpdateUserTaskCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }
    }
}
